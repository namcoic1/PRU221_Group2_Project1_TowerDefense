using UnityEngine;

public class Tower_Mask : Tower
{
    public Animator animator;

    protected override void Start()
    {
        Debug.Log("MASK.");
        animator.Play("Appear");
    }
    public void DangerHealth()
    {
        Debug.Log("DESTROY MASK.");
        // animator.Play("Disappear");
    }
}
