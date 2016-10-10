using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Objetos;
using System;

public class Boom : MonoBehaviour {

	public GameObject explosion;
    Text texto;

    void Start()
    {
        try
        {
            texto = GameObject.FindGameObjectWithTag("Gui").GetComponent<Text>() as Text;
        }
        catch (Exception e) { }
        Destroy(this.gameObject, 15f);
    }

	void OnCollisionEnter (Collision col)
	{
        if (col.gameObject.tag == "Terreno")
		{
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
		}
        else if (col.gameObject.tag == "Enemigo")
        {
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            if(Application.loadedLevelName != "Menu")
                texto.text = "An enemy ship has been destroyed";

            col.transform.parent.parent.parent.GetComponent<Vida>().QuitarVida(3);
            GameObject.Find("Player").GetComponent<Manager>().Destruccion();
            //Destroy(col.transform.parent.parent.gameObject);
            Destroy(this.gameObject);
        }
	}

}
