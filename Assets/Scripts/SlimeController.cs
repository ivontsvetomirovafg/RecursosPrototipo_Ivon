using UnityEngine;

public class SlimeController : EnemyController
{
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
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