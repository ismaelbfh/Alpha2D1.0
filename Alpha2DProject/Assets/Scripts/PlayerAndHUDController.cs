using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAndHUDController : MonoBehaviour {

    //TODO: EXPLICA MEJOR LOS COMENTARIOS DE LOS METODOS

    //Variables del Jugador:
    private GameObject _Jugador;
    private float _TamañoX;
    private Rigidbody2D _cuerpoJugador;
    private Animator _animatorJugador;
    private Image _BarraVida;

    //PLAYER:
    public float velocidadJugador = 2f;
    public float fuerzaSalto;
    public float VidaJugador;
    //HUD:
    public bool PulsoIz = false;
    public bool PulsoDe = false;

    //GETTERS Y SETTERS:
    public GameObject Jugador
    {
        get
        {
            return _Jugador;
        }

        set
        {
            _Jugador = value;
        }
    }
    public float TamañoX
    {
        get
        {
            return _TamañoX;
        }

        set
        {
            _TamañoX = value;
        }
    }
    public Rigidbody2D CuerpoJugador
    {
        get
        {
            return _cuerpoJugador;
        }

        set
        {
            _cuerpoJugador = value;
        }
    }
    public Animator AnimatorJugador
    {
        get
        {
            return _animatorJugador;
        }

        set
        {
            _animatorJugador = value;
        }
    }
    public Image BarraVida
    {
        get
        {
            return _BarraVida;
        }

        set
        {
            _BarraVida = value;
        }
    }

    private void Start()
    {
        //Jugador:
        Jugador = GameObject.FindGameObjectWithTag("Player"); //Objeto Jugador
        CuerpoJugador = Jugador.GetComponent<Rigidbody2D>(); //Cuerpo del jugador
        AnimatorJugador = Jugador.GetComponent<Animator>(); //Animador del jugador
        //HUD
        BarraVida = GameObject.Find("BarraVida").GetComponent<Image>();
		TamañoX = Jugador.transform.localScale.x;
    }

    private void FixedUpdate()
    {
        AnimatorJugador.SetFloat("speed", Mathf.Abs(CuerpoJugador.velocity.x));

        if (PulsoDe) //Si hemos pulsado el boton de la derecha le cambiamos escala y le aplicamos una fuerza
        {
            CuerpoJugador.velocity = new Vector2(velocidadJugador, CuerpoJugador.velocity.y);
			Jugador.transform.localScale = new Vector3(TamañoX, Jugador.transform.localScale.y, 0f);
        }
        else if (PulsoIz) //Si pulsamos el de la izquierda se lanzara esto
        {
            CuerpoJugador.velocity = new Vector2(-velocidadJugador, CuerpoJugador.velocity.y);
			Jugador.transform.localScale = new Vector3(-TamañoX, Jugador.transform.localScale.y, 0f);
        }
    
    }

    /// Funcion para activar/desactivar movimiento --> Se activan mediante un objeto "Event Trigger" en los Canvas de movimiento (BotonMovimientoIz y BotonMovimientoDe)
    /// 1 es Izquierda, 2 es detenido y 3 es derecha.
    /// </summary>
    /// <param name="direccion"></param>
    public void ActivarDesactivarMovimiento(int direccion)
    {
        PulsoDe = direccion == 1 ? false : (direccion == 2 ? false : true);
        PulsoIz = direccion == 1 ? true : false;

        //TODAS ESAS LINEAS ME LAS HE FUMADO POR ESTAS DOS DE ARRIBA ^^ [BORRAR lo de ABAJO]
        //if (direccion == 1){
        //    PulsoDe = false;
        //    PulsoIz = true;
        //}
        //else if (direccion == 2)
        //{
        //    PulsoDe = false;
        //    PulsoIz = false;
        //}
        //else if (direccion == 3)
        //{
        //    PulsoIz = false;
        //    PulsoDe = true;
        //}
      
    }
    /// <summary>
    /// Funcion de Salto que se activarán al pulsar encima del objeto Canvas llamado "AreaSaltar" con un objeto "Event Trigger"
    /// </summary>
    public void SaltarJugador()
    {
        CuerpoJugador.velocity = new Vector2(CuerpoJugador.velocity.x, fuerzaSalto);
        AnimatorJugador.SetFloat("vSpeed", Mathf.Abs(CuerpoJugador.velocity.y));
    }

    /// <summary>
    /// ¿QUE HACE?
    /// </summary>
    public void DejarSaltar()
    {
	    AnimatorJugador.SetFloat ("vSpeed", 0f);
    }
    
    /// <summary>
    /// ¿QUE HACE?
    /// </summary>
    /// <param name="daño"></param>
    public void BajarVida(float daño)
    {
        VidaJugador -= daño;
        BarraVida.fillAmount = VidaJugador;
    }
}
