using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySkeletonController : MonoBehaviour {


    //TODO: NO DEBES HACER TANTAS VARIABLES PUBLICAS, ESO NOS RALENTIZA EL FUTURO MANTENIMIENTO y LIA AL QUE SE METE CON EL CODIGO PORQUE NO SABE SI INTERACTUARA DESDE EL EXTERIOR O NO

    //Variables tipo "public" -> para cambiar en inspector desde el modo grafico en Unity (se usan para velocidad, vida, etc.)
    //Variables tipo "private" -> Es lo mismo que no poner nada delante, y serán las que no cambies desde el inspector (boleanos controlables desde script...)
    // Si quieres simular una public para depurar a la vez usa:
    //                 Variables tipo "[SerializeField] private" -> Se usaran las que se cambien desde script igual que las privadas pero si en algun momento necesitamos depurarlo y verlo en el inspector se usa el "[SerializeField]" encima del mismo


    //Variables del Enemigo:
    private GameObject _Enemigo;
    private Transform _transformEnemigo;
    private float _contadorSalto;
    private Image _ImagenRendererEnemigo;
    private bool _Voltear;
    private bool _Saltar;
    private Rigidbody2D _cuerpoEnemigo;
    private Animator _animatorEnemigo;
	private bool _EnSuelo;
	private float _RadioDetectarSuelo = 0.07f;

    //publicas
    public float velocidadCaminar;
    public float fuerzaSalto;
    public float vida = 1.0f;
	public Transform VerificadorSuelo; 
	public LayerMask LayerSuelo;
    
    /// <summary>
    /// IA DEL ENEMIGO EN ESTE TRAMO
    /// </summary>
    
    //Estas han de ser privadas por su uso en el script y por tenmas de perdidas de datos al fluir por otras plataformas, tales como servidores futuros, etc.
    private AnimationClip _AnimacionAtaque;
    private GameObject _ArmaGuardada;
    private bool _isLanzarCuchillo = false;
    private bool _MirarHaciaIzquierda = true;
    private bool _JugadorEnPlataforma = false;
    private bool _VoltearCaminar;
	private Vector2 _PosicionJugador;

    [SerializeField]  //De esta forma podemos depurar la private sin ser public ;)
    private bool _Caminar = false; 

    public GameObject Arma;
    public GameObject PosicionadorArma;


    /*GETTERS Y SETTERS DE LA CLASE, por normativa vamos a hacer los atributos privates y vamos a hacer unos metodos publicos:
      GETTERS(que devuelven el valor del atributo) o SETTERS(que establecen el valor a dicho atributo)*/

    //ESTAS PROPIEDADES PERMITEN DESDE CUALQUIER LADO REFERENCIAR A LAS VARIABLES "private" pero no se veran en el inspector (esto es lo correcto)
    public bool JugadorEnPlataforma
    {
        get
        {
            return _JugadorEnPlataforma;
        }

        set
        {
            _JugadorEnPlataforma = value;
        }
    }
    public bool MirarHaciaIzquierda
    {
        get
        {
            return _MirarHaciaIzquierda;
        }

        set
        {
            _MirarHaciaIzquierda = value;
        }
    }
    public bool Caminar
    {
        get
        {
            return _Caminar;
        }

        set
        {
            _Caminar = value;
        }
    }
    public bool isLanzarCuchillo
    {
        get
        {
            return _isLanzarCuchillo;
        }

        set
        {
            _isLanzarCuchillo = value;
        }
    }
    public GameObject ArmaGuardada
    {
        get
        {
            return _ArmaGuardada;
        }

        set
        {
            _ArmaGuardada = value;
        }
    }
    public AnimationClip AnimacionAtaque
    {
        get
        {
            return _AnimacionAtaque;
        }

        set
        {
            _AnimacionAtaque = value;
        }
    }
    public bool Voltear
    {
        get
        {
            return _Voltear;
        }

        set
        {
            _Voltear = value;
        }
    }
    public bool Saltar
    {
        get
        {
            return _Saltar;
        }

        set
        {
            _Saltar = value;
        }
    }
    public bool VoltearCaminar
    {
        get
        {
            return _VoltearCaminar;
        }

        set
        {
            _VoltearCaminar = value;
        }
    }
    public Rigidbody2D CuerpoEnemigo
    {
        get
        {
            return _cuerpoEnemigo;
        }

        set
        {
            _cuerpoEnemigo = value;
        }
    }
    public Animator AnimatorEnemigo
    {
        get
        {
            return _animatorEnemigo;
        }

        set
        {
            _animatorEnemigo = value;
        }
    }
    public GameObject Enemigo
    {
        get
        {
            return _Enemigo;
        }

        set
        {
            _Enemigo = value;
        }
    }
    public Transform TransformEnemigo
    {
        get
        {
            return _transformEnemigo;
        }

        set
        {
            _transformEnemigo = value;
        }
    }
    public float ContadorSalto
    {
        get
        {
            return _contadorSalto;
        }

        set
        {
            _contadorSalto = value;
        }
    }
    public Image ImagenRendererEnemigo
    {
        get
        {
            return _ImagenRendererEnemigo;
        }

        set
        {
            _ImagenRendererEnemigo = value;
        }
	}
    public Vector2 PosicionJugador
    {
        get
        {
            return _PosicionJugador;
        }

        set
        {
            _PosicionJugador = value;
        }
    }

    private void Start()
    {
        //Inicialización:
        Enemigo = this.gameObject;
		TransformEnemigo = Enemigo.transform;
        CuerpoEnemigo = Enemigo.GetComponent<Rigidbody2D>();
        AnimatorEnemigo = Enemigo.GetComponent<Animator>();
        PosicionJugador = new Vector2(0, 0);
        ArmaGuardada = Arma;
		if (!MirarHaciaIzquierda) {
			TransformEnemigo.localScale = new Vector3 (Enemigo.transform.localScale.x * -1,Enemigo.transform.localScale.y,0f);
		}

    }

    /// <summary>
    /// Método de Físicas. Se utiliza para mover al enemigo. Hacer que ataque y salte.
    /// </summary>
    private void FixedUpdate()
    {

        //Caminar y Atacar:
        if (!JugadorEnPlataforma) // Si el jugador no esta en la plataforma no atacara y caminara o se mantendrá quieto:
        {
			if (Caminar) {
				CuerpoEnemigo.velocity = new Vector2 (velocidadCaminar, CuerpoEnemigo.velocity.y);
				DejarAtacar();
			} else { // Si el enemigo no debe caminar, se mantendra quieto, en idle.
				DejarAtacar();
			}
        }
        else //En cambio si el jugador esta en la plataforma no caminara y atacara:
        {
				Atacar ();
        }

        //Voltear al Enemigo
        if (Voltear)
        {
			TransformEnemigo.localScale = new Vector3 (Enemigo.transform.localScale.x * -1,Enemigo.transform.localScale.y,0f);
			velocidadCaminar *= -1;
			if (TransformEnemigo.localScale.x == Mathf.Abs (TransformEnemigo.localScale.x)) {
				MirarHaciaIzquierda = true;
			} else {
				MirarHaciaIzquierda = false;
			}
			Voltear = false;
        }

		//Hacer que siempre valla hacia donde mira:
        if (Saltar)
        {
            AnimatorEnemigo.SetFloat("vspeed", 0.1f);
        }
        else
        {
            CuerpoEnemigo.velocity = new Vector3(CuerpoEnemigo.velocity.x, CuerpoEnemigo.velocity.y - Time.deltaTime);
        }
    }

    /// <summary>
    /// Debes comentar todos los metodos y su funcionamiento:
    /// Por ejemplo: se activa desde el event trigger de x objeto y realiza x accion
    /// </summary>
    private void Atacar()
    {
		//Animación de Ataque:
		AnimatorEnemigo.SetBool ("attack", true);
			//Crear el sprite del cuchillo en el segundo:
			if (isLanzarCuchillo) {
				isLanzarCuchillo = false;
				Arma = (GameObject) Instantiate (Arma, PosicionadorArma.transform.position, PosicionadorArma.transform.rotation, TransformEnemigo);
				Arma.transform.localPosition = PosicionadorArma.transform.localPosition;
				if (MirarHaciaIzquierda) {
					Arma.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-10, 0);
				} else {
					Arma.GetComponent<Rigidbody2D> ().velocity = new Vector2 (10, 0);
				}
				Arma.name = "Cuchillo1";
				Arma = ArmaGuardada;
			}
    }

		
    public void LanzarCuchillo()
    {
        isLanzarCuchillo = true;
    }

    /// <summary>
    /// Debes comentar todos los metodos y su funcionamiento:
    /// Por ejemplo: se activa desde el event trigger de x objeto y realiza x accion
    /// </summary>
    private void DejarAtacar()
    {
        AnimatorEnemigo.SetBool("attack", false);
		if (Mathf.Abs (TransformEnemigo.localScale.x) == TransformEnemigo.localScale.x && Mathf.Abs (velocidadCaminar) == velocidadCaminar) {
			velocidadCaminar = -velocidadCaminar;
			MirarHaciaIzquierda = true;
		} else if (Mathf.Abs (TransformEnemigo.localScale.x) != TransformEnemigo.localScale.x && Mathf.Abs (velocidadCaminar) != velocidadCaminar) {
			velocidadCaminar = Mathf.Abs(velocidadCaminar);
			MirarHaciaIzquierda = false;
		}
    }

    /// <summary>
    /// Debes comentar todos los metodos y su funcionamiento:
    /// Por ejemplo: se activa desde el event trigger de x objeto y realiza x accion
    /// </summary>
    private void SaltarEsqueleto()
    {
        if (Saltar )
        {
            CuerpoEnemigo.velocity = new Vector3(0 /*Para que no se mueva mientras salta */, fuerzaSalto);
        }
    }

    /// <summary>
    /// Debes comentar todos los metodos y su funcionamiento:
    /// Por ejemplo: se activa desde el event trigger de x objeto y realiza x accion
    /// </summary>
    private void DejarSaltar()
    {
        Saltar = false;
		AnimatorEnemigo.SetFloat("vspeed", CuerpoEnemigo.velocity.y);
    }


}