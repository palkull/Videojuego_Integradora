using UnityEngine;

public class VidaEnemigo : MonoBehaviour
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
        Debug.Log("El enemigo ha muerto.");
        Destroy(gameObject);
    }
}
