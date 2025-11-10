using UnityEngine;

public class JugadorAtaque : MonoBehaviour
{
    [SerializeField] private Transform controladoAtaque;
    [SerializeField] private float rangoDeAtaque;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Lógica de ataque aquí
            Atacar();
        }
    }

    private void Atacar()
    {
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(controladoAtaque.position, rangoDeAtaque);
        foreach (Collider2D enemigo in enemigos)
        {
            Debug.Log("Enemigo atacado: " + enemigo.name);
        }
    }
    private void OnDrawGizmos() {
        if (controladoAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladoAtaque.position, rangoDeAtaque);
    }
}
