using UnityEngine;

public class Curar : MonoBehaviour
{
    [SerializeField] private int cantidadCuracion;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out VidaPlayer vidaPlayer))
        {
            vidaPlayer.CurarVida(cantidadCuracion);
            // Destroy(gameObject);
        }
    }
}
