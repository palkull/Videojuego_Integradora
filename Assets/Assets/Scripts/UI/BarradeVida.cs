// using UnityEngine;
// using UnityEngine.UI;

// public class BarradeVida : MonoBehaviour
// {
//     [SerializeField] private Slider sliderbarraDeVida;
//     [SerializeField] private VidaPlayer vidaJugador;

//     private void Start()
//     {
//         vidaJugador = FindFirstObjectByType<VidaPlayer>();

//         vidaJugador.JugadorTomoDaño += CambiarBarradeVidaTomarDaño;
//         vidaJugador.JugadorCuroVida += CambiarBarradeVidaCuroVida;

//         IniciarBarradeVida(vidaJugador.GetVidaActual(), vidaJugador.GetVidaMaxima());
//     }
    
//     private void OnDisable() {
//         vidaJugador.JugadorTomoDaño -= CambiarBarradeVidaTomarDaño;
//         vidaJugador.JugadorCuroVida -= CambiarBarradeVidaCuroVida;
//     }

//     public void IniciarBarradeVida(int vidaActual, int vidaMaxima)
//     {
//         sliderbarraDeVida.maxValue = vidaMaxima;
//         sliderbarraDeVida.value = vidaActual;
//     }

//     private void CambiarBarradeVidaTomarDaño(int vidaActual)
//     {
//         sliderbarraDeVida.value = vidaActual;
//     }
//     private void CambiarBarradeVidaCuroVida(int vidaActual)
//     {
//         sliderbarraDeVida.value = vidaActual;
//     }
// }
