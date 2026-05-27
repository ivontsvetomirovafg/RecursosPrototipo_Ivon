using UnityEngine;
using UnityEngine.UI;

public class CharacterControler : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] 
    private float speed;

    [Header("Vida")]
    [SerializeField] 
    private float maxLife;
    public float currentLife;

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
    private LevelManager levelManager;
    private SwordController sword;
    public bool knockback;

    [SerializeField]
    private Image lifeBar;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        sword = GetComponentInChildren<SwordController>();

        UpdateLife();
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
        {
            if (Input.GetMouseButtonDown(1))
            {
                sword.Attack();
            }
        }
    }
    public void KnockBack(float _knockbackForce, Vector2 _direct)
    {
        knockback = true;
        rb.AddForce(_direct * _knockbackForce);
        Invoke("KnockbackEnd", 0.5f);
    }

    private void KnockbackEnd()
    {
        knockback = false;
    }

    public void TakeDamage(float damageTaken)
    {
        currentLife -= damageTaken;

        if (currentLife <= 0)
        {
            Die();
        }

        UpdateLife();
        animator.SetTrigger("Hit");

        //AudioManager.Instance.PlaySFX(hitSound);      
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        rb.linearVelocity = Vector2.zero;
        //AudioManager.Instance.PlaySFX(deathSound);
        enabled = false;
    }

    public void UpdateLife()
    {
        lifeBar.fillAmount = currentLife / maxLife;
    }
}
