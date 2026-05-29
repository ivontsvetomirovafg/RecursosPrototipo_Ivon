using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] 
    private float damage;
    [SerializeField] 
    private float knockBackForce;
    private Animator animator;
    private LevelManager levelManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    // Update is called once per frame
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                enemy.KnockBack(knockBackForce, (enemy.transform.position - transform.position).normalized);
            }
        }
        else if (collision.CompareTag("Wood"))
        {
            Debug.Log("Entra en el trigger");
            Destroy(collision.gameObject);
            //levelManager.wood++;
        }
    }
}
