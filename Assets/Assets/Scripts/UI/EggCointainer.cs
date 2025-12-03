using UnityEngine;

public class EggCointainer : MonoBehaviour
{
    private EggUI[] eggsUI;

    [SerializeField] private NestLife nestLife;

    private void Awake()
    {
        eggsUI = GetComponentsInChildren<EggUI>(true);
    }

    private void Start() {
        nestLife = FindFirstObjectByType<NestLife>();

        nestLife.NestTomoDaño += ActivateEggs;
        nestLife.NestCuroVida += ActivateEggs;

        ActivateEggs(nestLife.GetVidaActual());
    }

    private void OnDisable() {
        nestLife.NestTomoDaño -= ActivateEggs;
        nestLife.NestCuroVida -= ActivateEggs;
    }
    public void ActivateEggs(int vida)
    {
        for (int i = 0; i < eggsUI.Length; i++)
        {
            if (i < vida)

            {
                if(eggsUI[i].IsActive()) { continue; }

                eggsUI[i].EggActive();
            }
            else
            {
                if(!eggsUI[i].IsActive()) { continue; }
                eggsUI[i].EggInactive();
            }
        }
    }
}
