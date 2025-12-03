using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
     [Header("Detección del Jugador")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distanciaDeteccionPlayer = 5f;
    [SerializeField] private Transform controladoAtaque;
    [SerializeField] private float tiempoEntreAtaques; // Cooldown time between attacks
    [SerializeField] private float tiempoUltimoAtaque;
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private int dañoAtaque;
    


    void Update()
    {
        float distanciaAlPlayer = Vector2.Distance(transform.position, playerTransform.position); // Calcula la distancia al jugador
        if (distanciaAlPlayer <= distanciaDeteccionPlayer)
        {
            // Vector2 direccion = (playerTransform.position - transform.position).normalized;
            Debug.Log("Nest Detectded ");
            // animator.SetBool("PlayerDetectado", true);
            IntentarAtacar();
        }else
        {
            Debug.Log("Nest No Detectded");
            // animator.SetBool("PlayerDetectado", false);
        }
    }

    private void Atacar()
    {

        // animator.SetTrigger("Ataque");

        tiempoUltimoAtaque = Time.time;
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(controladoAtaque.position, rangoDeAtaque);
        foreach (Collider2D enemigo in enemigos)
        {
            if(enemigo.TryGetComponent(out NestLife nestLife))
            {
                nestLife.TomarDaño(dañoAtaque); // Asumiendo que el daño es 10
            }
        }
    }
    private void IntentarAtacar()
    {
        if (Time.time < tiempoUltimoAtaque + tiempoEntreAtaques) {return;} // Check if enough time has passed since the last attack
        Atacar();
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladoAtaque.position, rangoDeAtaque);
    }
}
