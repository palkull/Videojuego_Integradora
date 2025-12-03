using UnityEngine;

public class EggUI : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private bool isActive;

    public void EggActive()
    {
        animator.SetTrigger("Restore");

        isActive = true;
    }

    public void EggInactive()
    {        
        animator.SetTrigger("Hit");

        isActive = false;
    }

    public bool IsActive() => isActive;
}
