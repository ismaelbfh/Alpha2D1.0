using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharallaxScript : MonoBehaviour {

    PlayerAndHUDController ControladorHud; //Referencia al script que controla los movimientos del usuario y los objetos de interfaz en pantalla.

    private float offset; //Para cambiar el Offset

    public float speed; //Velocidad que se le aplica

    public float contador;

    private void Start()
    {
        ControladorHud = GameObject.Find("HUD").GetComponent<PlayerAndHUDController>(); //coje el objeto entero de HUD con todos sus hijos
    }

    private void Update ()
    {
       if(ControladorHud.signoPharallax != 0){
           ActivarPharallax(ControladorHud.signoPharallax);
       }
    }

    private void ActivarPharallax(float signo)
    {
        offset = speed * contador * signo;
        contador += Time.deltaTime;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
