using UnityEngine;

public class SkelletonController : EnemyController
{
    private Vector2 moveDirection;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (player == null) 
        {
            return;
        }

        CharacterControler character = player.GetComponent<CharacterControler>();
        if (character.currentLife <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("Attacking", false);
            return;
        }
        base.Update();

        if (playerDetected == true) 
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            if (direction.x > 0) 
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else 
            {
                transform.eulerAngles = Vector3.zero;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterControler>().TakeDamage(damage);
            Vector2 bounceDir = (transform.position - player.position).normalized;
            rb.linearVelocity = bounceDir * speed;
        }
    }
}
