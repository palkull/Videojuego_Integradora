using UnityEngine;

public class HeartUI : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private bool isActive;

    public void HeartActive()
    {
        animator.SetTrigger("Restore");

        isActive = true;
    }

    public void HeartInactive()
    {        
        animator.SetTrigger("Hit");

        isActive = false;
    }

    public bool IsActive() => isActive;
}