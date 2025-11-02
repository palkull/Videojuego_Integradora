using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float entradaHorizontal;
    [SerializeField] private float velocidadMovimiento = 5f;

    [SerializeField] private float fuerzaSalto = 10f;
    private bool entradaSalto;

    [SerializeField] private Transform controladorSuelo;

    [SerializeField] private bool enSuelo;

    [SerializeField] private Vector2 dimensionesCaja;

    [SerializeField] private LayerMask capaSalto;

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
        if (Input.GetButtonDown("Jump"))
        {
            entradaSalto = true;
        }
        
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, capaSalto);
    }
    private void FixedUpdate()
    {
        ControlarMovimientoHorizontal();

        if (entradaSalto  && enSuelo)
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            entradaSalto = false;
        }

    }

    private void ControlarMovimientoHorizontal()
    {

        if (enSuelo == false)
        {
            velocidadMovimiento = 3f;
        } else
        {
            velocidadMovimiento = 10f;
        }
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

    void OnDrawGizmos()
    {
        if (controladorSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
        }
    }
}
