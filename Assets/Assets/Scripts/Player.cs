using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float entradaHorizontal;
    [SerializeField] private float velocidadMovimiento = 5f;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        entradaHorizontal = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        ControlarMovimientoHorizontal();

    }

    private void ControlarMovimientoHorizontal()
    {
        rb.linearVelocity = new Vector2(entradaHorizontal * velocidadMovimiento, rb.linearVelocity.y);

        if ((entradaHorizontal > 0 && !MirandoDerecha()) || (entradaHorizontal < 0 && MirandoDerecha()))
        {
            Girar();
        }
    }


    private void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    private bool MirandoDerecha()
    {
        return transform.localScale.x == 1;
    }


}
