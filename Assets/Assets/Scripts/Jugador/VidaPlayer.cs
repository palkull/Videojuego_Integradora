using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
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

        if (cantidadDeVidaTemporal <= 0)
        {
            Morir();
        }
        else
        {
            vidaActual = cantidadDeVidaTemporal;
        }
    }

    private void Morir()
    {
        Debug.Log("El jugador ha muerto.");
        // Aquí puedes agregar lógica adicional para cuando el jugador muere, como reiniciar el nivel.
    }
}
