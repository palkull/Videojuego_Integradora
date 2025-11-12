using UnityEngine;

public class Curar : MonoBehaviour
{
    [SerializeField] private int cantidadCuracion;
    [SerializeField] private Animator animator; // Reference to the Animator component for handling attack animations.
    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out VidaPlayer vidaPlayer))
        {
            vidaPlayer.CurarVida(cantidadCuracion);
            animator.SetTrigger("isUsed");
            Destroy(gameObject, 0.5f); // Destroy the healing item after use
        }
    }
}
