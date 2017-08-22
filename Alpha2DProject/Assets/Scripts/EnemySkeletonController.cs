﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonController : MonoBehaviour {

    //Variables del Enemigo:
    GameObject Enemigo;
    public float velocidadCaminar; 
    public float fuerzaSalto;
    public float vida = 1.0f;
    public Rigidbody2D cuerpoEnemigo;
    public Animator animatorEnemigo;
    SpriteRenderer spriteRendererEnemigo;
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
    float contadorAtaque = 1.533f;
    float añadidorAtaque;

    private void Start()
    {
        //Inicialización:
        Enemigo = this.gameObject;
        cuerpoEnemigo = Enemigo.GetComponent<Rigidbody2D>();
        animatorEnemigo = Enemigo.GetComponent<Animator>();
        spriteRendererEnemigo = Enemigo.GetComponent<SpriteRenderer>();
        posicionJugador = new Vector2(0,0);
        ArmaGuardada = Arma;
    }


    void FixedUpdate()
    {
        //Caminar y Atacar:
        if (!JugadorEnPlataforma) // Si el jugador no esta en la plataforma no atacara y caminara:
        {
            cuerpoEnemigo.velocity = new Vector2(velocidadCaminar, cuerpoEnemigo.velocity.y);
            DejarAtacar();
        }
        else //En cambio si el jugador esta en la plataforma no caminara y atacara:
        {
            Atacar();
        }

        //Voltearse al llegar al final de la plataforma:
        if (voltear)
        {
            if (spriteRendererEnemigo.flipX == true)
            {
                spriteRendererEnemigo.flipX = false;
                velocidadCaminar = -velocidadCaminar;
                voltear = false;
            }
            else
            {
                spriteRendererEnemigo.flipX = true;
                velocidadCaminar = Mathf.Abs(velocidadCaminar);
                voltear = false;
            }
        }

        if (Saltar && contadorSalto <= 0)
        {
            cuerpoEnemigo.velocity = new Vector3(0 /*Para que no se mueva mientras salta */, fuerzaSalto);
            animatorEnemigo.SetFloat("vspeed", Mathf.Abs(cuerpoEnemigo.velocity.y));
            Saltar = false;
            contadorSalto = 3.0f;
        }

        if(contadorSalto <= 3.0f && contadorSalto > 0){
            contadorSalto -= Time.deltaTime;
            cuerpoEnemigo.velocity = new Vector3(cuerpoEnemigo.velocity.x, cuerpoEnemigo.velocity.y - Time.deltaTime);
            animatorEnemigo.SetFloat("vspeed", Mathf.Abs(cuerpoEnemigo.velocity.y));
            if(cuerpoEnemigo.velocity.y < 0){
                animatorEnemigo.SetFloat("vspeed", 0);
            }
        }
        else if (contadorSalto <= 0)
        {
            contadorSalto = 0;
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
                spriteRendererEnemigo.flipX = false;
            }
            else
            {
                spriteRendererEnemigo.flipX = true;
            }

        }
        //Crear el sprite del cuchillo en el segundo:
        if (contadorAtaque <= 0)
        {
            contadorAtaque = 2.535f;
            Arma = Instantiate(Arma,PosicionadorArma.transform.position,Arma.transform.rotation,Enemigo.transform) as GameObject;

            if (!spriteRendererEnemigo.flipX) //depende la posicion lanzalo en una posicion negativa, o positiva (derecha o izquierda)
            {
                Arma.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            }
            else
            {
                Arma.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
            }
            Arma.name = "Cuchillo1";
            Arma = ArmaGuardada;
        }
        else
        {
            contadorAtaque -= Time.deltaTime;
        }

    }

    private void DejarAtacar()
    {
        animatorEnemigo.SetBool("attack", false);
        if (VoltearCaminar)
        {
            voltear = true;
            VoltearCaminar = false;
            contadorAtaque = 1.533f;
        }
    }

}