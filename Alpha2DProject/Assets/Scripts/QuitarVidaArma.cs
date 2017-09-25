using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent(typeof(BoxCollider2D))]

public class QuitarVidaArma : MonoBehaviour
{
    //Variables
    private float contadorEliminar = 1.5f;

    public float DañoACausar;
    public PlayerAndHUDController ScriptPlayer;
     

     void Awake()
     {
         ScriptPlayer = GameObject.Find("HUD").GetComponent<PlayerAndHUDController>();
     }

     void Start()
     {
         //Inicialización:
     }

     void OnTriggerEnter2D(Collider2D colision)
     {
         if (colision.tag == "Player")
         {
             ScriptPlayer.BajarVida(DañoACausar);
             Destroy(this.gameObject);
         }
     }

     void Update()
     {
         if (contadorEliminar <= 0)
         {
             Destroy(this.gameObject);
         }
         contadorEliminar -= Time.deltaTime;
     }


}
