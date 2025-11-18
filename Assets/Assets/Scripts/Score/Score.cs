using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI eggstext;
    [SerializeField] private int eggScore;

private void Start() {
    Textupdate();
}

    private void OnEnable() {
        Egg.onEggCollected += PlusEggs;
    }

    private void OnDisable() {
        Egg.onEggCollected -= PlusEggs;
        
    }
    private void  PlusEggs(int eggs)
    {
        eggScore += eggs;
        Textupdate();

    }

    private void Textupdate()
    {
        eggstext.text=eggScore.ToString("D5");
    }
}
