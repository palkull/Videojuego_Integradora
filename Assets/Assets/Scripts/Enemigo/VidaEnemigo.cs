using System;
using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    
    [Header("Referencias")]

    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    [SerializeField] private MovimientoEnemigo movimientoEnemigo;

    [Header("Vida Enemigo")]
    [SerializeField] private int vidaMaxima;
    [SerializeField] private int vidaActual;

    [Header("Retroceso")]
    [SerializeField] private Vector2 fuerzaRetroceso;
    [SerializeField] private float duracionRetroceso;


    private void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void TomarDaño(int cantidadDeDaño, Transform sender)
    {
        int cantidadDeVidaTemporal = vidaActual - cantidadDeDaño;
        cantidadDeVidaTemporal = Mathf.Clamp(cantidadDeVidaTemporal, 0, vidaMaxima);



        if (cantidadDeVidaTemporal <= 0)
        {
            Morir();
        }
        else
        {
            vidaActual = cantidadDeVidaTemporal;
        }
        Retroceso(sender);
    }

    private void Retroceso(Transform sender)
    {
        movimientoEnemigo.CambiarEstadoOcupado(duracionRetroceso);

        Vector2 dirreccion =  (transform.position - sender.position).normalized;

        Vector2 fuerza = new(Math.Sign(- dirreccion.x) * fuerzaRetroceso.x, fuerzaRetroceso.y);

        rb2D.linearVelocity = Vector2.zero;

        rb2D.AddForce(new Vector2(fuerza.x * -transform.localScale.x, fuerzaRetroceso.y), ForceMode2D.Impulse);
        
        animator.SetTrigger("Damage");
    }

    private void Morir()
    {
        Debug.Log("El enemigo ha muerto.");
        Destroy(gameObject);
    }
    
}
