using UnityEngine;

public class JugadorAtaque : MonoBehaviour
{
    [SerializeField] private Transform controladoAtaque;
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private int dañoAtaque;
    [SerializeField] private Animator animator;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoUltimoAtaque;
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
        if (Time.time < tiempoUltimoAtaque + tiempoEntreAtaques) {return;}
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
