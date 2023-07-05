using Assets.Scripts.Game;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Health,AttackPower,MoveSpeed
    private int health, attackPower;
    private float moveSpeed;

    public string enemyName;
    public Animator animator;
    public float attackInterval;
    Coroutine attackOrder;
    Tower detectedTower;

    void Update()
    {
        if (!detectedTower)
        {
            Move();
        }
    }

    IEnumerator Attack()
    {
        animator.Play("Attack", 0, 0);
        //Wait attackInterval 
        yield return new WaitForSeconds(attackInterval);
        //Attack Again
        attackOrder = StartCoroutine(Attack());
    }

    //Moving forward
    void Move()
    {
        animator.Play("Move");

        if (enemyName.Equals("bee"))
        {
            moveSpeed = ConfigurationUtils.MoveSpeedBee;
        }
        else
        {
            moveSpeed = ConfigurationUtils.MoveSpeedSlug;
        }

        transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
    }

    public void InflictDamage()
    {
        if (enemyName.Equals("bee"))
        {
            attackPower = ConfigurationUtils.AttackPowerBee;
        }
        else
        {
            attackPower = ConfigurationUtils.AttackPowerSlug;
        }

        bool towerDied = detectedTower.LoseHealth(attackPower);

        if (towerDied)
        {
            detectedTower = null;
            StopCoroutine(attackOrder);
        }
    }

    //Lose health
    public void LoseHealth()
    {
        if (enemyName.Equals("bee"))
        {
            health = ConfigurationUtils.HealthBee;
        }
        else
        {
            health = ConfigurationUtils.HealthSlug;
        }

        //Decrease health value
        health--;
        //Blink Red animation
        StartCoroutine(BlinkRed());
        //Check if health is zero => destroy enemy
        if (health <= 0)
            Destroy(gameObject);
    }

    IEnumerator BlinkRed()
    {
        //Change the spriterendere color to red
        GetComponent<SpriteRenderer>().color = Color.red;
        //Wait for really small amount of time 
        yield return new WaitForSeconds(0.2f);
        //Revert to default color
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (detectedTower)
            return;

        if (collision.tag == "Tower")
        {
            detectedTower = collision.GetComponent<Tower>();
            attackOrder = StartCoroutine(Attack());
        }

        if (collision.tag == "House")
        {
            Debug.Log("Lost health");
            FindObjectOfType<HealthSystem>().LoseHealth();
            Destroy(gameObject);
        }
    }
}
