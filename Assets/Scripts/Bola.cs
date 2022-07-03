using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bola : MonoBehaviour {

    public float velocidad = 30.0f;

    AudioSource fuenteDeAudio;
    public AudioClip audioGol, audioRaqueta, audioRebote, audioInicio, audioFinal;


    public int golesIzquierda = 0;
    public int golesDerecha = 0;

    public Text contadorIzquierda;
    public Text contadorDerecha;
    public Text informacion;

    public float timeRemaining = 180; //Tiempo de partida en seg
    public bool timerIsRunning = false; //Temporizador Activo/Desactivo
    public Text timeText; //Contador de tiempo
    public GameObject bola;

    // Use this for initialization
    void Start () {

        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;

        fuenteDeAudio = GetComponent<AudioSource>();

        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();

        timerIsRunning = true;

        fuenteDeAudio.clip = audioInicio;
        fuenteDeAudio.Play();

	}

    void OnCollisionEnter2D(Collision2D micolision)
    {
        if(micolision.gameObject.name == "RaquetaIzquierda")
        {
            int x = 1;

            int y = direccionY(transform.position, micolision.transform.position);

            Vector2 direccion = new Vector2(x, y);

            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();

        }

        else if(micolision.gameObject.name == "RaquetaDerecha")
        {
            int x = -1;

            int y = direccionY(transform.position, micolision.transform.position);

            Vector2 direccion = new Vector2(x, y);

            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

            fuenteDeAudio.clip = audioRaqueta;
            fuenteDeAudio.Play();
        }

        if(micolision.gameObject.name=="Arriba" || micolision.gameObject.name == "Abajo")
        {
            fuenteDeAudio.clip = audioRebote;
            fuenteDeAudio.Play();
        }

    }


    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        if (posicionBola.y > posicionRaqueta.y)
        {
            return 1;
        }
        else if (posicionBola.y < posicionRaqueta.y)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public void reiniciarBola(string direccion)
    {
        transform.position = Vector2.zero;

        velocidad = 30;

        if (direccion == "Derecha")
        {
            golesDerecha++;
            contadorDerecha.text = golesDerecha.ToString();
            GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
        }
        else if (direccion == "Izquierda")
        {
            golesIzquierda++;
            contadorIzquierda.text = golesIzquierda.ToString();
            GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
        }

        fuenteDeAudio.clip = audioGol;
        fuenteDeAudio.Play();
    }




    //Temporizador
    void DisplayTime(float timetoDisplay)
    {
        timetoDisplay += 1;
        float minutes = Mathf.FloorToInt(timetoDisplay / 60);
        float seconds = Mathf.FloorToInt(timetoDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void gameend()
    {
        fuenteDeAudio.clip = audioFinal;
        fuenteDeAudio.Play();
    }

    void Contador()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;


                bola.GetComponent<Bola>().gameObject.SetActive(false);

                if (golesDerecha > golesIzquierda)
                {
                    fuenteDeAudio.clip = audioFinal;
                    fuenteDeAudio.Play();
                    informacion.text = "Gana jugador de la derecha. \nPreciona P o clic para reiniciar";
                }
                else
                {
                    fuenteDeAudio.clip = audioFinal;
                    fuenteDeAudio.Play();
                    informacion.text = "Gana jugador de la izquierda. \nPreciona P o clic para reiniciar";
                }
            }
        }
    }

    void GolReview()
    {
        if (golesDerecha >= 5)
        {
            bola.GetComponent<Bola>().gameObject.SetActive(false);
            fuenteDeAudio.clip = audioFinal;
            fuenteDeAudio.Play();
            informacion.text = "Gana jugador de la derecha. \nPreciona P o clic para reiniciar";
            
        }
        else if (golesIzquierda >= 5)
        {
            gameend();
            bola.GetComponent<Bola>().gameObject.SetActive(false);
            fuenteDeAudio.clip = audioFinal;
            fuenteDeAudio.Play();
            informacion.text = "Gana jugador de la izquierda. \nPreciona P o clic para reiniciar";
        }
    }






    // Update is called once per frame
    void Update()
    {
        velocidad = velocidad + (0.1f + 0.1f * (golesIzquierda + golesDerecha));
        Contador();
        GolReview();
    }
}
