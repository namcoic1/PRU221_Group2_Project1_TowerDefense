using UnityEngine;

public class ShootItem : MonoBehaviour
{
    //FIELDS
    //graphics (the sprite renderer)
    public Transform graphics;
    //damage
    public int damage;
    //speed
    public float flySpeed, rotateSpeed;

    Enemy detectedEnemy;

    //METHODS
    //Init
    public void Init(int dmg)
    {
        damage = dmg;
    }
    //Trigger with enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            // collision.GetComponent<Enemy>().LoseHealth(damage);
            detectedEnemy = collision.GetComponent<Enemy>();
            bool enemyDied = detectedEnemy.LoseHealth(damage);
            if (enemyDied)
            {
                detectedEnemy = null;
            }
            Destroy(gameObject);
        }
        if (collision.tag == "Out")
        {
            Destroy(gameObject);
        }
    }
    //Handle rotation and flying
    void Update()
    {
        Rotate();
        FlyForward();
    }
    void Rotate()
    {
        graphics.Rotate(new Vector3(0, 0, -rotateSpeed * Time.deltaTime));
    }
    void FlyForward()
    {
        transform.Translate(transform.right * flySpeed * Time.deltaTime);
    }

}
