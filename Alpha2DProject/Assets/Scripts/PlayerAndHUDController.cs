using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndHUDController : MonoBehaviour {

    //Inicialización de Variables:_____________________________________________
    //Variables del Jugador:
    GameObject Jugador;
    public float velocidadJugador = 2f;
    public float fuerzaSalto;
    [SerializeField]
    public Rigidbody2D cuerpoJugador;
    [SerializeField]
    public Animator animatorJugador;
    //Variables de HUD:
    public bool PulsoIz = false;
    public bool PulsoDe = false;
    public float signoPharallax = 0f;

    private void Start()
    {
        //Jugador:
        Jugador = GameObject.FindGameObjectWithTag("Player"); //Objeto Jugador
        cuerpoJugador = Jugador.GetComponent<Rigidbody2D>(); //Cuerpo del jugador
        animatorJugador = Jugador.GetComponent<Animator>(); //Animador del jugador
    }

    private void FixedUpdate()
    {
        //Movimiento Player mediante los botones correspondientes___________________________________________

        if (PulsoDe) //Si hemos pulsado el boton de la derecha le cambiamos escala y le aplicamos una fuerza
        {
            cuerpoJugador.velocity = new Vector2(velocidadJugador, cuerpoJugador.velocity.y);
            Jugador.transform.localScale = new Vector3(-1f, 1f, 1f);
            signoPharallax = -1f;
        }
        else if (PulsoIz) //Si pulsamos el de la izquierda se lanzara esto
        {
            cuerpoJugador.velocity = new Vector2(-velocidadJugador, cuerpoJugador.velocity.y);
            Jugador.transform.localScale = new Vector3(1f, 1f, 1f);
            signoPharallax = 1f;
        }
        else //Si no es ninguna de las dos haz esto
        {
            signoPharallax = 0f;
        }

        animatorJugador.SetFloat("speed", Mathf.Abs(cuerpoJugador.velocity.x));
    }

    //Funcion para activar/desactivar movimiento --> Se activan mediante un objeto "Event Trigger" en los Canvas de movimiento (BotonMovimientoIz y BotonMovimientoDe)
    //1 es Izquierda, 2 es detenido y 3 es derecha.
    public void ActivarDesactivarMovimiento(int direccion)
    {
        if(direccion == 1){
            PulsoDe = false;
            PulsoIz = true;
        }
        else if (direccion == 2)
        {
            PulsoDe = false;
            PulsoIz = false;
        }
        else if (direccion == 3)
        {
            PulsoIz = false;
            PulsoDe = true;
        }
      
    }

    //Salto del Jugador:
    //Funcion de Salto que se activarán al pulsar encima del objeto Canvas llamado "AreaSaltar" con un objeto "Event Trigger"
    public void SaltarJugador()
    {
        cuerpoJugador.velocity = new Vector2(cuerpoJugador.velocity.x, fuerzaSalto);

        animatorJugador.SetFloat("vSpeed", Mathf.Abs(cuerpoJugador.velocity.y));
    }

    public void DejarSaltar()
    {
        animatorJugador.SetFloat("vSpeed", 0);
    }

}
