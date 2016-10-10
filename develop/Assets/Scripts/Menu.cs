using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            Application.LoadLevel("Mapa Recortado");
        }
    }
}
