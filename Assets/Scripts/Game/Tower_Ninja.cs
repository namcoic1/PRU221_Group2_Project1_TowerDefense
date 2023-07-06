using System.Collections;
using UnityEngine;

public class Tower_Ninja : Tower
{
    //FIELDS
    //damage
    public int damage;
    //prefab (shooting item)
    public GameObject prefab_shootItem;

    public Animator animator;

    //shoot interval
    public float interval;

    private IEnumerator coroutine;


    //METHODS
    //init (start the shooting interval)
    protected override void Start()
    {
        Debug.Log("NINJA.");
        animator.Play("Appear");
        //start the shooting interval IEnum
        // coroutine = ShootDelay();
        // StartCoroutine(coroutine);
        StartCoroutine(ShootDelay());
    }
    public void DangerHealth()
    {
        Debug.Log("DESTROY NINJA.");
        // animator.Play("Disappear");
    }
    //Interval for shooting
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(interval);
        ShootItem();
        // StartCoroutine(coroutine);
        StartCoroutine(ShootDelay());
    }
    //Shoot an item
    void ShootItem()
    {
        //Instantiate shoot item
        GameObject shotItem = Instantiate(prefab_shootItem, transform);
        //Set its values  
        shotItem.GetComponent<ShootItem>().Init(damage);
    }
    public void Stop()
    {
        Debug.Log("STOP.");
        StopCoroutine(coroutine);
        // StopCoroutine("ShootDelay");
        // StopAllCoroutines();
    }
}
