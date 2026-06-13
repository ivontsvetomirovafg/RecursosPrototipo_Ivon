using UnityEngine;

public class SkelletonController : EnemyController
{
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
            attacking = false;
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("PlayerDetected", false);
            return;
        }
        base.Update();
        
        if (attacking == true)
        {
            Vector3 distance = player.position - transform.position;
            float distanceSq = distance.sqrMagnitude;
            if (distanceSq > Mathf.Pow(stopDistance, 2))
            {
                attacking = false;
            }
        }
    }
}