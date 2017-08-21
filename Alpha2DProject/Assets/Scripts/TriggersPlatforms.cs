using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersPlatforms : MonoBehaviour {

    public GameObject EsqueletoAsociado;
    EnemySkeletonController controladorEsqueleto;
    public bool EsEnemigo;

    private void Start()
    {
       controladorEsqueleto = EsqueletoAsociado.GetComponent<EnemySkeletonController>();
    }

    void OnTriggerEnter2D(Collider2D Colision)
    {
        if (!EsEnemigo)
        {
            if (Colision.tag == "Player")
            {
                controladorEsqueleto.JugadorEnPlataforma = true;
                controladorEsqueleto.posicionJugador.x = Colision.transform.position.x;
                controladorEsqueleto.posicionJugador.y = Colision.transform.position.y;
            }
        }
    }

    void OnTriggerExit2D(Collider2D Colision)
    {
        if (!EsEnemigo)
        {
            if (Colision.tag == "Player")
            {
                controladorEsqueleto.JugadorEnPlataforma = false;
                controladorEsqueleto.posicionJugador.x = 0;
                controladorEsqueleto.posicionJugador.y = 0;
                controladorEsqueleto.VoltearCaminar = true;
            }

        }else if(EsEnemigo){

        if (Colision.name == EsqueletoAsociado.name)
        {
            controladorEsqueleto.voltear = true;
        }

        }
    }

}
