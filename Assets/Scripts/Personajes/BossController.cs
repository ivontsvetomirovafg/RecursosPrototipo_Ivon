using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public enum BossStates { Waiting, Jumping, Roar, Death};

    private LevelManager levelManager;

    [Header("Variables Generales")]
    [SerializeField]
    private BossStates currentState;
    private Transform player;
    private Animator animator;
    [SerializeField]
    private float bossLife;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float knockBackForce;
    [SerializeField]
    private Sprite muertoSprite;

    [Header ("Waiting")]
    [SerializeField]
    private float waitingTime;

    [Header("Jumping")]
    [SerializeField]
    private float maxJump = 12;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float timeToJump;

    [Header("Roar")]
    [SerializeField]
    private GameObject slimeprefab;
    [SerializeField]
    private Transform slimeSpawnPoint;
    [SerializeField]
    private float timeToSpawn;

    [Header("Sound")]
    [SerializeField]
    private AudioClip roar;
    [SerializeField]
    private AudioClip dead;
    [SerializeField]
    private AudioClip jump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        currentState = BossStates.Waiting;
        ChangeState();

        if (levelManager.espadaActual != null)
        {
            bossLife = 1000f;
            damage = 100;
        }
    }
    void ChangeState()
    {
        CharacterControler character = player.GetComponent<CharacterControler>();
        if (character.currentLife <= 0)
        {
            animator.SetBool("Attacking", false);
            animator.SetBool("PlayerDetected", false);
            return;
        }
        switch (currentState)
        {

            case BossStates.Waiting:
                StartCoroutine(WaitingCoroutine());
                break;
            case BossStates.Jumping:
                StartCoroutine(JumpCoroutine());
                break;
            case BossStates.Roar:
                StartCoroutine(RoarCoroutine());
                break;
            case BossStates.Death:
                break;
        }
    }
    IEnumerator WaitingCoroutine()
    {
        if(transform.position.x < player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
        GetComponent<Rigidbody2D>().linearVelocity= Vector2.zero;

        yield return new WaitForSeconds(1);

        if (transform.position.x < player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        yield return new WaitForSeconds(1);
        currentState = (BossStates)Random.Range(1, 3);
        ChangeState();
    }
    IEnumerator JumpCoroutine()
    {
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(timeToJump);
        Vector2 start = transform.position;
        Vector2 target = player.position;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * jumpSpeed;
            float posX = Mathf.Lerp(start.x, target.x, t);
            float posY = Mathf.Lerp(start.y, target.y, t);
            posY += 2 * maxJump * t * (1 - t); // arco
            transform.position = new Vector2(posX, posY);
            yield return null;
        }

        animator.SetBool("Attack", false);

        currentState = BossStates.Waiting;
        ChangeState();
    }

    IEnumerator RoarCoroutine()
    {
        //AudioManager.Instance.PlaySFX(roar);
        animator.SetBool("Roar", true);
        yield return new WaitForSeconds(timeToSpawn);
        Instantiate(slimeprefab, slimeSpawnPoint.position, slimeSpawnPoint.rotation);
        animator.SetBool("Roar", false);
        yield return new WaitForSeconds(timeToSpawn);
        
        currentState = BossStates.Waiting;
        ChangeState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag== "Player")
        {
            collision.gameObject.GetComponent<CharacterControler>().TakeDamage(damage);
            ContactPoint2D point = collision.GetContact(0);
            if(transform.position.x <player.position.x) //derecha
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * knockBackForce);
            }
            else //izquierda
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * knockBackForce);
            }
        } 
    }

    public void TakeDamage(float _damage)
    {
        bossLife -= _damage;
        if (bossLife <= 0)
        {
            //muerto
            //AudioManager.Instance.PlaySFX(dead);
            currentState = BossStates.Death;
            StopAllCoroutines();
            animator.SetTrigger("Death");
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            this.enabled = false;
        }
        else
        {
            //hit
            animator.SetTrigger("Hit");           
        }
    }
}
