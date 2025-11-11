using UnityEngine;

public class Rawdamage : MonoBehaviour
{
    [SerializeField] private int dañoPorToque;
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.TryGetComponent(out VidaPlayer vidaPlayer))
        {
            vidaPlayer.TomarDaño(dañoPorToque); // Daño fijo de 5
        }
      
    }
}
