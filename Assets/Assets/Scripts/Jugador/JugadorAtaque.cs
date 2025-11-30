using System;
using UnityEngine;

public class JugadorAtaque : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    [SerializeField] private Transform controladoAtaque; //Get the position from where the attack is made. Using a Transform allows for easy adjustment in the Unity Editor.
    [SerializeField] private Transform controladoAtaqueUp; //Get the position from where the attack is made. Using a Transform allows for easy adjustment in the Unity Editor.
    [SerializeField] private Transform controladoAtaqueDown;
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private int dañoAtaque;
    [SerializeField] private Animator animator; // Reference to the Animator component for handling attack animations.
    
    [Header("Temporizadores de Ataque")]
    [SerializeField] private float tiempoEntreAtaques; // Cooldown time between attacks
    [SerializeField] private float tiempoUltimoAtaque;// Time when the last attack was made
    [SerializeField] private Player player; // Reference to the Player script to check if the player is on the ground

    private void Update()
    {
        // 1. ATAQUE HACIA ARRIBA
    if (Input.GetKey(KeyCode.W) && Input.GetButtonDown("Fire1"))
    {
        IntentarAtacarArriba();
    }
    // 2. ATAQUE HACIA ABAJO
    else if (Input.GetKey(KeyCode.S) && Input.GetButtonDown("Fire1") && !player.enSuelo)
    {
        IntentarAtacarAbajo();
    }
    // 3. ATAQUE NORMAL
    else if (!Input.GetKey(KeyCode.W) && Input.GetButtonDown("Fire1"))
    {
        IntentarAtacar();
    }
    }

    private void IntentarAtacarAbajo()
    {
        Debug.Log("Ataque Abajo");
        animator.SetTrigger("AtaqueDown");
        if (Time.time < tiempoUltimoAtaque + tiempoEntreAtaques) {return;} // Check if enough time has passed since the last attack
        AtacarDown();
    }

    private void AtacarDown()
    {
        tiempoUltimoAtaque = Time.time;
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(controladoAtaqueDown.position, rangoDeAtaque);
        foreach (Collider2D enemigo in enemigos)
        {
            if(enemigo.TryGetComponent(out VidaEnemigo vidaEnemigo))
            {
                vidaEnemigo.TomarDaño(dañoAtaque, transform); // Asumiendo que el daño es 10
            }
        }    }

    private void IntentarAtacarArriba()
    {
        Debug.Log("Ataque Arriba");
        animator.SetTrigger("AtaqueUp");
        if (Time.time < tiempoUltimoAtaque + tiempoEntreAtaques) {return;} // Check if enough time has passed since the last attack
        AtacarUp();
    }

    private void AtacarUp()
    {
        tiempoUltimoAtaque = Time.time;
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(controladoAtaqueUp.position, rangoDeAtaque);
        foreach (Collider2D enemigo in enemigos)
        {
            if(enemigo.TryGetComponent(out VidaEnemigo vidaEnemigo))
            {
                vidaEnemigo.TomarDaño(dañoAtaque, transform); // Asumiendo que el daño es 10
            }
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
                vidaEnemigo.TomarDaño(dañoAtaque, transform); // Asumiendo que el daño es 10
            }
        }
    }
    private void OnDrawGizmos() {
        if (controladoAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladoAtaque.position, rangoDeAtaque);
        Gizmos.DrawWireSphere(controladoAtaqueUp.position, rangoDeAtaque);
        Gizmos.DrawWireSphere(controladoAtaqueDown.position, rangoDeAtaque);
    }
}
