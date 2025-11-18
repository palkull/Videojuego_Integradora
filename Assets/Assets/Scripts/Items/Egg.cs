using UnityEngine;
using System;

public class Egg : MonoBehaviour
{
    public static Action<int> onEggCollected;
    [SerializeField] private int eggValue;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            RecollectEgg();
        }
    }

    private void RecollectEgg() {
        // Here you can add code to give the player an egg or increase egg count
            onEggCollected?.Invoke(eggValue);
            Debug.Log("Egg collected!");
            Destroy(gameObject); // Remove the egg from the scene
    }
}
