using UnityEngine;

public class HeartCointainer : MonoBehaviour
{
    private HeartUI[] heartsUI;

    [SerializeField] private VidaPlayer vidaJugador;

    private void Awake()
    {
        heartsUI = GetComponentsInChildren<HeartUI>(true);
    }

    private void Start() {
        vidaJugador = FindFirstObjectByType<VidaPlayer>();

        vidaJugador.JugadorTomoDaño += ActivateHearts;
        vidaJugador.JugadorCuroVida += ActivateHearts;

        ActivateHearts(vidaJugador.GetVidaActual());
    }

    private void OnDisable() {
        vidaJugador.JugadorTomoDaño += ActivateHearts;
        vidaJugador.JugadorCuroVida += ActivateHearts;
    }
    public void ActivateHearts(int vida)
    {
        for (int i = 0; i < heartsUI.Length; i++)
        {
            if (i < vida)

            {
                if(heartsUI[i].IsActive()) { continue; }

                heartsUI[i].HeartActive();
            }
            else
            {
                if(!heartsUI[i].IsActive()) { continue; }
                heartsUI[i].HeartInactive();
            }
        }
    }
}
