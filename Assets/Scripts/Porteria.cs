using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porteria : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D bola)
    {
       if(bola.name == "Bola")
       {
            if(this.name == "Izquierda")
            {
                bola.GetComponent<Bola>().reiniciarBola("Derecha");
            }
            else if(this.name == "Derecha")
            {
                bola.GetComponent<Bola>().reiniciarBola("Izquierda");
            }

       } 
    }




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
