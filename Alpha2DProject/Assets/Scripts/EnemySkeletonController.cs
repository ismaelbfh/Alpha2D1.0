using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySkeletonController : MonoBehaviour {

    //Variables del Enemigo:
    GameObject Enemigo;
	Transform transformEnemigo;
    public float velocidadCaminar; 
    public float fuerzaSalto;
    public float vida = 1.0f;
    public Rigidbody2D cuerpoEnemigo;
    public Animator animatorEnemigo;
	Image ImagenRendererEnemigo;
    public bool voltear;
    public bool Saltar;
    float contadorSalto;

    //IA:
    public bool JugadorEnPlataforma = false;
    public Vector2 posicionJugador;
    public bool VoltearCaminar;
    public GameObject Arma;
    GameObject ArmaGuardada;
    public GameObject PosicionadorArma;
    AnimationClip AnimacionAtaque;
    bool lanzarCuchillo = false;
	public bool Caminar = false;
	public bool MirarHaciaIzquierda = true;

    private void Start()
    {
        //Inicialización:
        Enemigo = this.gameObject;
		transformEnemigo = Enemigo.transform;
        cuerpoEnemigo = Enemigo.GetComponent<Rigidbody2D>();
        animatorEnemigo = Enemigo.GetComponent<Animator>();
        posicionJugador = new Vector2(0,0);
        ArmaGuardada = Arma;
		if (!MirarHaciaIzquierda) {
			transformEnemigo.localScale = new Vector3 (Enemigo.transform.localScale.x * -1,Enemigo.transform.localScale.y,0f);
		}
    }


    void FixedUpdate()
    {
        //Caminar y Atacar:
        if (!JugadorEnPlataforma) // Si el jugador no esta en la plataforma no atacara y caminara o se mantendrá quieto:
        {
			if (Caminar) {
				cuerpoEnemigo.velocity = new Vector2 (velocidadCaminar, cuerpoEnemigo.velocity.y);
				DejarAtacar ();
			} else { // Si el enemigo no debe caminar, se mantendra quieto, en idle.
				DejarAtacar ();
			}
        }
        else //En cambio si el jugador esta en la plataforma no caminara y atacara:
        {
            Atacar();
        }

        //Voltear al Enemigo
        if (voltear)
        {
			transformEnemigo.localScale = new Vector3 (Enemigo.transform.localScale.x * -1,Enemigo.transform.localScale.y,0f);
			velocidadCaminar *= -1;
			if (transformEnemigo.localScale.x == Mathf.Abs (transformEnemigo.localScale.x)) {
				MirarHaciaIzquierda = true;
			} else {
				MirarHaciaIzquierda = false;
			}
			voltear = false;
        }

		//Hacer que siempre valla hacia donde mira:
        if (Saltar)
        {
            animatorEnemigo.SetFloat("vspeed", 0.1f);
        }
        else
        {
            cuerpoEnemigo.velocity = new Vector3(cuerpoEnemigo.velocity.x, cuerpoEnemigo.velocity.y - Time.deltaTime);
        }
    }

    private void Atacar()
    {
        //Animación de Ataque:
        animatorEnemigo.SetBool("attack", true);
        //Voltear hacia la posición del jugador:
        if (posicionJugador != (new Vector2(0, 0)))
        {
            //Verificar si esta de un lado u otro:
			if (posicionJugador.x < this.transform.position.x)
            {
				transformEnemigo.localScale = new Vector3 (Mathf.Abs(Enemigo.transform.localScale.x),Enemigo.transform.localScale.y,0f);
                PosicionadorArma.transform.localPosition = new Vector3(PosicionadorArma.transform.localPosition.x * (-1), PosicionadorArma.transform.localPosition.y, PosicionadorArma.transform.localPosition.z); 
				MirarHaciaIzquierda = true;
			}
			else if(posicionJugador.x < this.transform.position.x)
            {
				transformEnemigo.localScale = new Vector3 (-Enemigo.transform.localScale.x,Enemigo.transform.localScale.y,0f);
                PosicionadorArma.transform.localPosition = new Vector3(Mathf.Abs(PosicionadorArma.transform.localPosition.x), PosicionadorArma.transform.localPosition.y, PosicionadorArma.transform.localPosition.z); 
				MirarHaciaIzquierda = false;
            }

        }
        //Crear el sprite del cuchillo en el segundo:
        if (lanzarCuchillo)
        {
            lanzarCuchillo = false;
            Arma = Instantiate(Arma, PosicionadorArma.transform.position, Arma.transform.rotation, Enemigo.transform) as GameObject;
			if (MirarHaciaIzquierda)
            {
                Arma.GetComponent<Rigidbody2D>().velocity = new Vector2(-400, 0);
            }
            else
            {
                Arma.GetComponent<Rigidbody2D>().velocity = new Vector2(400, 0);
            }
            Arma.name = "Cuchillo1";
            Arma = ArmaGuardada;
        }
    }

    public void LanzarCuchillo()
    {
        lanzarCuchillo = true;
    }

    private void DejarAtacar()
    {
        animatorEnemigo.SetBool("attack", false);
		if (Mathf.Abs (transformEnemigo.localScale.x) == transformEnemigo.localScale.x && Mathf.Abs (velocidadCaminar) == velocidadCaminar) {
			velocidadCaminar = -velocidadCaminar;
			MirarHaciaIzquierda = true;
		} else if (Mathf.Abs (transformEnemigo.localScale.x) != transformEnemigo.localScale.x && Mathf.Abs (velocidadCaminar) != velocidadCaminar) {
			velocidadCaminar = Mathf.Abs(velocidadCaminar);
			MirarHaciaIzquierda = false;
		}
    }

    private void SaltarEsqueleto()
    {
        if (Saltar)
        {
            cuerpoEnemigo.velocity = new Vector3(0 /*Para que no se mueva mientras salta */, fuerzaSalto);
        }
    }

    private void DejarSaltar()
    {
        Saltar = false;
		animatorEnemigo.SetFloat("vspeed", cuerpoEnemigo.velocity.y);
    }

}