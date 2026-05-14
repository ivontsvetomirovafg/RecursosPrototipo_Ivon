using UnityEngine;

public class CharacterControler : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] 
    private float speed;

    [Header("Vida")]
    [SerializeField] 
    private float maxLife;
    private float currentLife;

    [Header("Ataque")]
    [SerializeField] 
    private float damage;

    [Header("Audio")]
    [SerializeField] 
    private AudioClip hitSound;
    [SerializeField] 
    private AudioClip attackSound;
    [SerializeField] 
    private AudioClip deathSound;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        currentLife = maxLife;
    }

    private void Update()
    {
        if (currentLife <=0)
        {
            return;
        }

        //Movimiento 

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetBool("Run", movement != Vector2.zero);        
        
        if (movement.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (movement.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }

        Attack();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * speed;
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Attack1"))
        {
            animator.SetTrigger("Attack");
            //AudioManager.Instance.PlaySFX(attackSound);       
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();

            if (enemy != null)
            {
                //enemy.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(float damageTaken)
    {
        currentLife -= damageTaken;

        if (currentLife <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hit");
            //AudioManager.Instance.PlaySFX(hitSound);      
        }
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        rb.linearVelocity = Vector2.zero;
        //AudioManager.Instance.PlaySFX(deathSound);
        enabled = false;
    }
}
