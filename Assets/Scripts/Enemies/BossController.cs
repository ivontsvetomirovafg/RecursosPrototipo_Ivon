using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public enum BossStates { Waiting, Jumping, Roar};

    private LevelManager levelManager;

    [Header("Variables Generales")]
    [SerializeField]
    private BossStates currentState;
    private Transform player;
    private Animator animator;
    [SerializeField]
    private float bossLife = 750;
    [SerializeField]
    private float damage = 25;
    [SerializeField]
    private float knockBackForce;
    [SerializeField]
    private GameObject puerta; 

    [Header ("Waiting")]
    [SerializeField]
    private float waitingTime;

    [Header("Jumping")]
    [SerializeField]
    private float maxJump = 6;
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
    private AudioClip jump;
    [SerializeField]
    private AudioClip key;

    [Header("Victory")]
    [SerializeField]
    private GameObject panelEnd; 
    [SerializeField]
    private Animator end1;
    [SerializeField]
    private Animator end2;
    private bool activado = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        currentState = BossStates.Waiting;
    }

    void Update()
    {
        if (activado == false) 
        {
            return;
        }
    
        if (transform.position.x < player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
        
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && activado == false)
        {
            activado = true;
            if (levelManager.espadaActual != null)
            {
                bossLife = 2000f;
                damage = 100;
            }
            puerta.SetActive(false);
            ChangeState();
        }
    }
    void ChangeState()
    {
        CharacterControler character = player.GetComponent<CharacterControler>();
        if (character.currentLife <= 0)
        {
            animator.SetBool("Attacking", false);
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
        }
    }
    IEnumerator WaitingCoroutine()
    {
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
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(timeToJump);
        Vector2 start = transform.position;
        Vector2 target = player.position;

        float t = 0;
        while (t < 1)
        {        
            AudioManager.Instance.PlaySFX(jump);
            t += Time.deltaTime * jumpSpeed;
            float posX = Mathf.Lerp(start.x, target.x, t);
            float posY = Mathf.Lerp(start.y, target.y, t);
            posY += 2 * maxJump * t * (1 - t); // arco
            transform.position = new Vector2(posX, posY);
            yield return null;
        }

        animator.SetBool("Attacking", false);

        currentState = BossStates.Waiting;
        ChangeState();
    }

    IEnumerator RoarCoroutine()
    {
        yield return new WaitForSeconds(timeToSpawn);
        AudioManager.Instance.PlaySFX(roar);
        Instantiate(slimeprefab, slimeSpawnPoint.position, slimeSpawnPoint.rotation);
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
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;

            panelEnd.SetActive(true);
            end1.SetTrigger("End");
            end2.SetTrigger("End");
            StartCoroutine(Final());
        }
        else
        {
            animator.SetTrigger("Hit");           
        }
    }

    private IEnumerator Final()
    {
        //para que desaparezca el boss
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float duracion = 1.5f;
        float t = 0;
        while (t < duracion) 
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / duracion);
            sprite.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        AudioManager.Instance.PlaySFX(key);
        AudioManager.Instance.FadeOutMusic(1.5f);
        sprite.enabled = false;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(5f);

        AudioManager.Instance.StopMusic();
        yield return new WaitForSecondsRealtime(5f);

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
