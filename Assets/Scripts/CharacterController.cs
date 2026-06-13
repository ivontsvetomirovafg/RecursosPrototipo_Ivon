using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterControler : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] 
    private float speed;

    [Header("Vida")]
    public float maxLife;
    public float currentLife;

    [Header("Ataque")]
    public float baseDamage;
    public float damage;

    [Header("Stats")]
    [SerializeField] 
    private float espadaNivel;
    private float armaduraNivel;
    private float picoNivel;

    [Header("Audio")]
    [SerializeField] 
    private AudioClip attackSound;
    [SerializeField] 
    private AudioClip deathSound;

    [Header("UI Stats")]
    [SerializeField]
    private Text lifeText;
    [SerializeField]
    private Text damageText;
    [SerializeField]
    private Image lifeBar;
    [SerializeField]
    private Animator gameOverAnim; 
    [SerializeField]
    private GameObject gameOverPanel; 

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private LevelManager levelManager;
    private SwordController sword;
    public GameObject pico;
    public bool knockback;
    private Coroutine damagePotionCoroutine;


    private void Start()
    {
        damage = baseDamage;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        sword = GetComponentInChildren<SwordController>();

        UpdateLife();
        UpdateDamage();
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
        if (currentLife <=0)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            if (pico.activeSelf == true)
            {
                pico.GetComponent<PicoController>().Attack();
            }
            else
            {
                AudioManager.Instance.PlaySFX(attackSound);
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
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        rb.linearVelocity = Vector2.zero;
        gameOverAnim.SetTrigger("GameOver");
        gameOverPanel.SetActive(true);
        AudioManager.Instance.PlaySFX(deathSound);
        enabled = false;
    }

    public void UpdateLife()
    {
        lifeBar.fillAmount = currentLife / maxLife;
        lifeText.text = "LIFE: " + currentLife + " / " + maxLife;
        
    }
    public void UpdateDamage()
    {
        damageText.text = "DMG: " + damage;
    }

    // POCION DE VIDA Y DE DAÑO 

    public void HealLife(float amount)
    {
        currentLife = Mathf.Min(currentLife + amount, maxLife); //para que no sobrepase el max daño
        UpdateLife();
    }

    public void PocionDaño(float bonus, float duration)
    {
        if (damagePotionCoroutine != null)
        {
            StopCoroutine(damagePotionCoroutine);
        }               
        damagePotionCoroutine = StartCoroutine(DañoTemp (bonus, duration));
 
    }

    private IEnumerator DañoTemp (float bonus, float duration)
    {
        damage += bonus;
        UpdateDamage();

        yield return new WaitForSeconds(duration);
        damage -= bonus;
        UpdateDamage();
        damagePotionCoroutine = null;
    }    
}
