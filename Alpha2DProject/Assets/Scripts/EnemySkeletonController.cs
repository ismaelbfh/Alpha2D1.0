using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySkeletonController : MonoBehaviour {

    //Variables del Enemigo:
    private GameObject _Enemigo;
    private Transform _transformEnemigo;
    private float _contadorSalto;
    private Image _ImagenRendererEnemigo;
    private bool _Voltear;
    private bool _Saltar;
    private Rigidbody2D _cuerpoEnemigo;
    private Animator _animatorEnemigo;
	private float _VidaEnemigo = 1;
	private bool EraEstatico;

    //publicas
    public float velocidadCaminar;
    public float fuerzaSalto;
	public bool Estatico;
    
    /// <summary>
    /// IA DEL ENEMIGO EN ESTE TRAMO
    /// </summary>
    
    //Estas han de ser privadas por su uso en el script y por temas de perdidas de datos al fluir por otras plataformas, tales como servidores futuros, etc.
    private AnimationClip _AnimacionAtaque;
    private GameObject _ArmaGuardada;
    private bool _isLanzarCuchillo = false;
    private bool _MirarHaciaIzquierda = true;
    private bool _JugadorEnPlataforma = false;
    private bool _VoltearCaminar;
	private Vector2 _PosicionJugador;

    //[SerializeField]  De esta forma podemos depurar la private sin ser public ;)

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
		EraEstatico = Estatico;
    }

    /// <summary>
    /// Método de Físicas. Se utiliza para mover al enemigo. Hacer que ataque y salte.
    /// </summary>
    private void FixedUpdate()
    {

		if (Enemigo.GetComponent<SpriteRenderer> ().flipX) {//Viendo hacia la derecha:
			velocidadCaminar = Mathf.Abs (velocidadCaminar);
			PosicionadorArma.transform.localPosition = new Vector3 (Mathf.Abs(PosicionadorArma.transform.localPosition.x),PosicionadorArma.transform.localPosition.y,PosicionadorArma.transform.localPosition.z);
		} else {//Viendo hacia la izquierda:
			velocidadCaminar = velocidadCaminar == Mathf.Abs (velocidadCaminar) ? velocidadCaminar * -1 : velocidadCaminar;
			if (PosicionadorArma.transform.localPosition.x == Mathf.Abs (PosicionadorArma.transform.localPosition.x)) {
				PosicionadorArma.transform.localPosition = new Vector3 (PosicionadorArma.transform.localPosition.x * -1,PosicionadorArma.transform.localPosition.y,PosicionadorArma.transform.localPosition.z);
			}
		}

        //Caminar y Atacar:
        if (!JugadorEnPlataforma) // Si el jugador no esta en la plataforma no atacara y caminara o se mantendrá quieto:
        {
			//Verificar si el esqueleto está configurado para caminar o para estar estático:
			if (Estatico) {
				_animatorEnemigo.SetFloat ("walk",-0.1f);
				DejarAtacar();
			} else {
					CuerpoEnemigo.velocity = new Vector2 (velocidadCaminar, CuerpoEnemigo.velocity.y);
					_animatorEnemigo.SetFloat ("walk",0.1f);
					DejarAtacar();
			}
        }
        else //En cambio si el jugador esta en la plataforma no caminara y atacara:
        {
				Atacar ();
        }

        //Voltear al Ene1migo
        if (Voltear)
        {
			Enemigo.GetComponent<SpriteRenderer> ().flipX = Enemigo.GetComponent<SpriteRenderer> ().flipX ? false : true;
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
	/// Activa la Función de Ataque del enemigo, activandole su animación y verificando si se debe lanzar el cuchillo (Que se activa solo cuando el animation event lo indica en
	/// el método de LanzarCuchillo)
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

	/// <summary>
	/// Activa el Lanzar Cuchillo para que se lance el cuchillo en su respectivo tiempo y no siempre.
	/// </summary>
    public void LanzarCuchillo()
    {
        isLanzarCuchillo = true;
    }

    /// <summary>
	/// Hace que el enemigo deje de atacar quitandole la animación. Se activa cuando el player sale de la plataforma (Se lo indica el triggersPlatforms.cs).
    /// </summary>
    private void DejarAtacar()
    {
        AnimatorEnemigo.SetBool("attack", false);
    }

    /// <summary>
    /// Hace que el esqueleto Salte. No esta completo.
    /// </summary>
    private void SaltarEsqueleto()
    {
        if (Saltar )
        {
            CuerpoEnemigo.velocity = new Vector3(0 /*Para que no se mueva mientras salta */, fuerzaSalto);
        }
    }

    /// <summary>
	/// Hace que el esqueleto deje de hacer la animación de salto y le va quitando velocidad.
    /// </summary>
    private void DejarSaltar()
    {
        Saltar = false;
		AnimatorEnemigo.SetFloat("vspeed", CuerpoEnemigo.velocity.y);
    }


	/// <summary>
	/// Funcion que le baja la vida al Esqueleto, es llamado por cada objeto con un script QuitarVidaArma con un bool llamado "DañarAPlayer" desactivado.
	/// </summary>
	public void BajarVida(float Daño){
		_VidaEnemigo -= Daño;
		//Activar Animacion de Electrocucion
		_animatorEnemigo.SetBool("playerattack",true);
		Estatico = true;
	}

	public void Morir(){
		Destroy (this.gameObject);
	}

	public void TerminarBajarVida(){
		if (_VidaEnemigo > 0) {
			_animatorEnemigo.SetBool ("playerattack", false);

			if (!EraEstatico) {
				Estatico = false;
			}

		} else {

			if (this.transform.localScale.x > 0.3f && this.transform.localScale.y > 0.3f) {
				float x = this.transform.localScale.x;
				float y = this.transform.localScale.y;
				x -= 20 * Time.deltaTime;
				y -= 20 * Time.deltaTime;
				this.transform.localScale = new Vector3 (x, y, 1f);
			} else {
				Destroy (this.gameObject);
			}
		}

	}

}