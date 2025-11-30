using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [Header("Configuración de Movimiento")]
    

    private const string VELOCIDAD_MOVIMIENTO_HORIZONTAL = "VelocidadHorizontal";
    private const string VELOCIDAD_MOVIMIENTO_VERTICAL = "VelocidadVertical";
    private const string EN_SUELO = "EnSuelo";
    

    [SerializeField] private float entradaHorizontal;
    [SerializeField] private float velocidadMovimiento = 5f;
    
    [Header("Configuración de Salto")]

    [SerializeField] private float fuerzaSalto = 6f;
    private bool entradaSalto;

    [SerializeField] private Transform controladorSuelo;

    [SerializeField] public bool enSuelo;

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
        ControlarAnimaciones();

    }

    private void ControlarMovimientoHorizontal()
    {

        if (enSuelo == false)
        {
            velocidadMovimiento = 4f;
        } else
        {
            velocidadMovimiento = 5f;
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

    private void ControlarAnimaciones()
    {
        animator.SetFloat(VELOCIDAD_MOVIMIENTO_HORIZONTAL, Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat(VELOCIDAD_MOVIMIENTO_VERTICAL, Mathf.Sign(rb.linearVelocity.y));
        animator.SetBool(EN_SUELO, enSuelo);
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
