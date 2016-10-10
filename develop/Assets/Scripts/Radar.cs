using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour {

    public GameObject[] aviones;

    IEnumerator OnTriggerStay(Collider col)
    {

        if (col.tag == "Aliado" || col.tag == "Player")
        {
            foreach (GameObject avion in aviones)
            {
                if (avion && col)
                {
                    avion.SendMessage("SetTarget", col.gameObject.transform);
                    //avion.GetComponent<SalidaHangar>().enabled = true;
                    avion.GetComponent<Objetos.AeroplaneAiControl>().enabled = true;
                    avion.GetComponent<Objetos.AeroplaneController>().enabled = true;
                    yield return new WaitForSeconds(6f);
                }
            }
        }
    }

}
