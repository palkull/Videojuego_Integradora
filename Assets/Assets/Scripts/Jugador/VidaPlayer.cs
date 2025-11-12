using UnityEngine;
using System; // needed for Action delegate

public class VidaPlayer : MonoBehaviour
{

    public Action<int> JugadorTomoDaño; // Event to notify when the player takes damage
    public Action<int> JugadorCuroVida;

    [SerializeField] private int vidaMaxima;
    [SerializeField] private int vidaActual;

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void TomarDaño(int cantidadDeDaño)
    {
        int cantidadDeVidaTemporal = vidaActual - cantidadDeDaño;
        cantidadDeVidaTemporal = Mathf.Clamp(cantidadDeVidaTemporal, 0, vidaMaxima);

        vidaActual = cantidadDeVidaTemporal;

        JugadorTomoDaño?.Invoke(vidaActual);

        if (cantidadDeVidaTemporal <= 0)
        {
            Morir();
        }
        else
        {
            vidaActual = cantidadDeVidaTemporal;
        }
    }

    public void CurarVida(int Curacion)
    {
        int cantidadDeVidaTemporal = vidaActual + Curacion;
        cantidadDeVidaTemporal = Mathf.Clamp(cantidadDeVidaTemporal, 0, vidaMaxima);

        vidaActual = cantidadDeVidaTemporal;

        JugadorCuroVida?.Invoke(vidaActual);

        
    }

    private void Morir()
    {
        Debug.Log("El jugador ha muerto.");
        Destroy(gameObject);
        // Aquí puedes agregar lógica adicional para cuando el jugador muere, como reiniciar el nivel.
    }

    public int GetVidaActual() => vidaActual;
    public int GetVidaMaxima() => vidaMaxima;
   
}
