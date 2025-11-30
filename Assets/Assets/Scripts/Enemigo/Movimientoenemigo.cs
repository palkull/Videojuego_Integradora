using System;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    [Header("Referencias")]
    private const string VELOCIDAD_MOVIMIENTO_HORIZONTAL = "VelocidadHorizontal";
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    [SerializeField] private EstadosEnemigo estadoActual;
    [SerializeField] private LayerMask capasSuelo;

    [Header("Movimiento Horizontal")]
    [SerializeField] private float velocidadDeMovimientoBase;
    [SerializeField] private float velocidadDeMovimientoActual;
    [SerializeField] private Transform controladorFrente;
    [SerializeField] private float distanciaRayoFrente;
    [SerializeField] private float distanciaSuelo;
    [SerializeField] private bool tocandoSuelo;
    [SerializeField] private Transform controladorSuelo;
    private bool tocandoSueloFrente;

    [Header("Esperar")]
    [SerializeField] private float tiempoAEsperar;
    private float tiempoAEsperarActual;
    [Header("Salto")]
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private Transform controladorEstaSuelo;
    [SerializeField] private bool enSuelo;

    [Header("Detección de suelo frente arriba")]
    [SerializeField] private Transform controladorFrenteArriba;
    [SerializeField] private bool enSueloFrenteArriba;

    [SerializeField] private float tiempoParaDesocupar;

    private void Update()
    {
        tocandoSueloFrente = Physics2D.Raycast(controladorFrente.position, transform.right * -1, distanciaRayoFrente, capasSuelo);
        enSueloFrenteArriba = Physics2D.Raycast(controladorFrenteArriba.position, transform.right * -1, distanciaRayoFrente, capasSuelo);
        tocandoSuelo = Physics2D.Raycast(controladorSuelo.position, transform.up * -1, distanciaSuelo, capasSuelo);
        enSuelo = Physics2D.OverlapBox(controladorEstaSuelo.position, dimensionesCaja, 0f, capasSuelo);
        ControlarAnimaciones();
    }

    private void FixedUpdate()
    {
        switch (estadoActual)
        {
            case EstadosEnemigo.Correr:
                ComportamientoCorrer();
                break;
            case EstadosEnemigo.Esperar:
                ComportamientoEsperar();
                break;
            case EstadosEnemigo.Saltar:
                ComportamientoSaltar();
                break;
            case EstadosEnemigo.Ocupado:
                ComportamientoOcupado();
                break;
        }
        ControlarAnimaciones();
    }

    private void ComportamientoEsperar()
    {
          if (tiempoAEsperarActual > 0)
        {
            tiempoAEsperarActual -= Time.fixedDeltaTime;
        }else
        {
            CambiarAEstadoCorrer();

        }

        
    }
    private void ComportamientoSaltar()
    {
        Correr();
        if (enSuelo)
        {
            CambiarAEstadoCorrer();
        }
    }

    private void ComportamientoCorrer()
    {
        Correr();

        
        if (tocandoSueloFrente)
        {
            if (enSueloFrenteArriba)
        {
            Girar();
            CambiarAEstadoEsperar();
            
        }else
        {
            Saltar();
            CambiarAEstadoSaltar();
        }
        }

        if (!tocandoSuelo && enSuelo)
        {

            Saltar();
            CambiarAEstadoSaltar();
        }

    }

    private void ComportamientoOcupado()
    {
        if (Time.time > tiempoParaDesocupar)
        {
            animator.SetBool("Ocupado", false);
            CambiarAEstadoEsperar();
        }
    }

    public void CambiarEstadoOcupado(float tiempoAOcupar)
    {
        tiempoParaDesocupar = Time.time + tiempoAOcupar;
        estadoActual = EstadosEnemigo.Ocupado;
        animator.SetBool("Ocupado", true);
    }
    private void CambiarAEstadoCorrer()
    {
        estadoActual = EstadosEnemigo.Correr;
        velocidadDeMovimientoActual = velocidadDeMovimientoBase;
    }

    private void CambiarAEstadoSaltar()
    {
        estadoActual = EstadosEnemigo.Saltar;
    }

    private void CambiarAEstadoEsperar()
    {
        velocidadDeMovimientoActual = 0;
        rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
        estadoActual = EstadosEnemigo.Esperar;
        tiempoAEsperarActual = tiempoAEsperar;
    }

    private void Saltar()
    {
        rb2D.AddForce(new Vector2(0f, fuerzaDeSalto), ForceMode2D.Impulse);
    }
   
    private void ControlarAnimaciones()
    {
        animator.SetFloat(VELOCIDAD_MOVIMIENTO_HORIZONTAL, Mathf.Abs(rb2D.linearVelocity.x));
        // animator.SetFloat(VELOCIDAD_MOVIMIENTO_VERTICAL, Mathf.Sign(rb.linearVelocity.y));
        // animator.SetBool(EN_SUELO, enSuelo);
    }
    private void Correr()
    {
        float direccion = transform.eulerAngles.y == 0 ? -1 : 1;
        rb2D.linearVelocity = new Vector2(direccion * velocidadDeMovimientoActual, rb2D.linearVelocity.y);
    }
   

    private void Girar()
    {
        Vector3 rotacion = transform.eulerAngles;
        rotacion.y = rotacion.y == 0 ? 180 : 0;
        transform.eulerAngles = rotacion;
    }

   
   //Dibujar rayos y cajas en el editor para facilitar la depuración---------------------------//

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorFrente.position, controladorFrente.position + distanciaRayoFrente * transform.right * -1);
        Gizmos.DrawLine(controladorFrenteArriba.position, controladorFrenteArriba.position + distanciaRayoFrente * transform.right * -1);
        Gizmos.DrawLine(controladorSuelo.position, controladorSuelo.position + distanciaSuelo * transform.up * -1);
        Gizmos.DrawWireCube(controladorEstaSuelo.position, dimensionesCaja);
    }
}