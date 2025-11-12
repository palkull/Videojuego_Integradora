using UnityEngine;

public class JugadorAtaque : MonoBehaviour
{
    [SerializeField] private Transform controladoAtaque; //Get the position from where the attack is made. Using a Transform allows for easy adjustment in the Unity Editor.
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private int dañoAtaque;
    [SerializeField] private Animator animator; // Reference to the Animator component for handling attack animations.
    [SerializeField] private float tiempoEntreAtaques; // Cooldown time between attacks
    [SerializeField] private float tiempoUltimoAtaque;// Time when the last attack was made
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Lógica de ataque aquí
            IntentarAtacar();
        }
    }
    private void IntentarAtacar()
    {
        if (Time.time < tiempoUltimoAtaque + tiempoEntreAtaques) {return;} // Check if enough time has passed since the last attack
        Atacar();
    }
    private void Atacar()
    {

        animator.SetTrigger("Ataque");

        tiempoUltimoAtaque = Time.time;
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(controladoAtaque.position, rangoDeAtaque);
        foreach (Collider2D enemigo in enemigos)
        {
            if(enemigo.TryGetComponent(out VidaEnemigo vidaEnemigo))
            {
                vidaEnemigo.TomarDaño(dañoAtaque); // Asumiendo que el daño es 10
            }
        }
    }
    private void OnDrawGizmos() {
        if (controladoAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladoAtaque.position, rangoDeAtaque);
    }
}
