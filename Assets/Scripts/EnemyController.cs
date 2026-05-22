using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    public float life;
    [SerializeField]
    public float speed;
    public bool playerDetected;
    public Rigidbody2D rb;
    public Animator animator;
    public Transform player;
    public float stopDistance;
    public bool attacking;
    public float damage;
    public bool knockback;
    
    public LevelManager levelManager;
    
    [SerializeField]
    private AudioClip dead;
    [SerializeField]
    private AudioClip hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (knockback == true)
        {
            return;
        }

        if (playerDetected == true && attacking == false)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            rb.linearVelocity = direction * speed;

            // Girar sprite
            if (direction.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (direction.x < 0)
            {
                transform.eulerAngles = Vector3.zero;
            }

            float distanceSqr = (player.position - transform.position).sqrMagnitude;

            if (distanceSqr <= stopDistance * stopDistance)
            {
                //AudioManager.Instance.PlaySFX(hit);
                attacking = true;
                rb.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {            
            Invoke("StartMoving", animator.GetCurrentAnimatorStateInfo(0).length);
            player = collision.transform;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterControler>().TakeDamage(damage);
            //player.KnockBack(knockback, (player.transform.position - transform.position).normalized);
        }
    }
    private void StartMoving()
    {
        playerDetected = true;
        animator.SetBool("PlayerDetected", true);
    }
    public void TakeDamage(float _damage)
    {
        life-=_damage;
        if (life <=0)
        {
            //AudioManager.Instance.PlaySFX(dead);
            animator.SetTrigger("Hit");
            int amount = Random.Range(1, 4);
            levelManager.AddItem("Slime", amount);
            Destroy(gameObject);
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }
    public void KnockBack(float _knockbackForce, Vector2 _direct)
    {
        knockback = true; 
        rb.AddForce(_direct * _knockbackForce);
        Invoke("KnockbackEnd", 1f);
    }
    private void KnockbackEnd()
    {
        knockback = false;
    }
}
