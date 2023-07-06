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
        if (!detectedTower && FindObjectOfType<HealthSystem>().healthCount > 0)
        {
            Move();
        }
        else if (FindObjectOfType<HealthSystem>().healthCount == 0)
        {
            // animator.Play("Move");
            // FindObjectOfType<Tower_Pink>().Stop();
            // FindObjectOfType<Tower_Ninja>().Stop();
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
        else if (enemyName.Equals("slug"))
        {
            moveSpeed = ConfigurationUtils.MoveSpeedSlug;
        }
        else
        {
            moveSpeed = ConfigurationUtils.MoveSpeedPiranha;
        }

        transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
    }

    public void InflictDamage()
    {
        if (enemyName.Equals("bee"))
        {
            attackPower = ConfigurationUtils.AttackPowerBee;
        }
        else if (enemyName.Equals("slug"))
        {
            attackPower = ConfigurationUtils.AttackPowerSlug;
        }
        else
        {
            attackPower = ConfigurationUtils.AttackPowerPiranha;
        }

        bool towerDied = detectedTower.LoseHealth(attackPower);

        if (towerDied)
        {
            detectedTower = null;
            StopCoroutine(attackOrder);
        }
    }

    //Lose health
    public bool LoseHealth(int amount)
    {
        if (enemyName.Equals("bee"))
        {
            health = ConfigurationUtils.HealthBee;
        }
        else if (enemyName.Equals("slug"))
        {
            health = ConfigurationUtils.HealthSlug;
        }
        else
        {
            health = ConfigurationUtils.HealthPiranha;
        }

        //Decrease health value
        // health--;
        // health -= 2;
        health -= amount;
        Debug.Log(health + " - " + amount);
        //Blink Red animation
        StartCoroutine(BlinkRed());
        //Check if health is zero => destroy enemy
        if (health <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
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
            SSTools.ShowMessage("Warning! Losing health.", SSTools.Position.bottom, SSTools.Time.halfSecond);
            FindObjectOfType<HealthSystem>().LoseHealth();
            Destroy(gameObject);
        }
    }
}
