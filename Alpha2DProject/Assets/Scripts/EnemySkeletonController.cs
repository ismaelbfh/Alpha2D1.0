using System.Collections;
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

    private void Start()
    {
        //Inicialización:
        Enemigo = this.gameObject;
        cuerpoEnemigo = Enemigo.GetComponent<Rigidbody2D>();
        animatorEnemigo = Enemigo.GetComponent<Animator>();
        spriteRendererEnemigo = Enemigo.GetComponent<SpriteRenderer>();
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
        animatorEnemigo.SetBool("attack",true);
        //Crear el sprite del cuchillo en el segundo
    }

    private void DejarAtacar()
    {
        animatorEnemigo.SetBool("attack", false);
    }

}
