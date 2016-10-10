using UnityEngine;
using System.Collections;

public class MovAvion : MonoBehaviour
{

    public GameObject[] puntos;
    private int IPuntos = 0; //Index Puntos


    private int velocidad = 5;
    private int velocidadRotacion = 3;

    public enum EstadoAvion { LISTO, NOLISTO }
    public EstadoAvion estadoAvion = EstadoAvion.LISTO;

    private Vector3 direccionMovimiento;

    void Start()
    {
        //StartCoroutine("CorrutinaTiempo");
    }
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.name == "PuntoA")
        {
            IPuntos = 1;
            this.GetComponent<Objetos.AeroplaneAiControl>().enabled = true;
            this.GetComponent<Objetos.AeroplaneController>().enabled = true;
        }
        if (col.gameObject.name == "PuntoB")
        {
            this.GetComponent<MovAvion>().enabled = false;

            estadoAvion = EstadoAvion.NOLISTO;
            IPuntos = 0;
        }
    }



    void Update()
    {
        if (estadoAvion == EstadoAvion.LISTO)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(puntos[IPuntos].transform.position - transform.position), velocidadRotacion * Time.deltaTime);
            direccionMovimiento = (puntos[IPuntos].transform.position - transform.position).normalized;
            transform.position += direccionMovimiento * velocidad * Time.deltaTime;
        }


    }

}
