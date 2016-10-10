using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Objetos
{
    public class Manager : MonoBehaviour
    {
        public bool oleada1;
        public bool oleada2;
        public bool oleada3;

        public List<GameObject> OleadaBarco1;
        public List<GameObject> OleadaBarco2;
        public List<GameObject> OleadaBarco3;

        public List<GameObject> OleadaAvion1;
        public List<GameObject> OleadaAvion2;
        public List<GameObject> OleadaAvion3;

        public List<GameObject> OleadaAvionEnemigo1;
        public List<GameObject> OleadaAvionEnemigo2;
        public List<GameObject> OleadaAvionEnemigo3;


        private int cantidad1;
        private int cantidad2;
        private int cantidad3;

        Text texto;

        void Start()
        {
            try
            {
                texto = GameObject.Find("Stage").GetComponent<Text>() as Text;
            }
            catch (Exception e) { }
            Time.timeScale = 1;
            cantidad1 = OleadaBarco1.Count;
            cantidad2 = OleadaBarco2.Count;
            cantidad3 = OleadaBarco3.Count;
            oleada1 = true;
            oleada2 = false;
            oleada3 = false;

        }

        void Update()
        {
            if (Input.GetButtonDown("Restart"))
            {
                Application.LoadLevel(Application.loadedLevelName);
            }

            if (verificacionOleada() == true)
            {
                activacionOleadasBarco();
                activacionOleadasAviones();
            }
        }


        #region Oleadas Barcos

        public void Destruccion()
        {
            if (oleada1 == true)
            {
                cantidad1--;
            }
            if (oleada2 == true)
            {
                cantidad2--;
            }
            if (oleada3 == true)
            {
                cantidad3--;
            }
        }


        public bool verificacionOleada()
        {
            if (cantidad1 > 0 && oleada1 == true)
            {
                return true;
            }

            if (cantidad1 <= 0 && oleada1 == true)
            {
                oleada1 = false;
                oleada2 = true;
                oleada3 = false;
                return true;
            }

            if (cantidad2 <= 0 && oleada2 == true)
            {
                oleada1 = false;
                oleada2 = false;
                oleada3 = true;
                return true;
            }
            return false;
        }

        public void activacionOleadasBarco()
        {

            if (oleada1)
            {
                foreach (GameObject barco in OleadaBarco1)
                {
                    if (barco != null)
                    {
                        barco.SetActive(true);
                    }
                }
                try
                {
                    texto.text = "Stage 1";
                }
                catch (Exception e) { }
            }

            if (oleada2)
            {
                foreach (GameObject barco in OleadaBarco2)
                {
                    if (barco != null)
                    {
                        barco.SetActive(true);
                    }
                }
                try
                {
                    texto.text = "Stage 2";
                }
                catch (Exception e) { }
            }
            if (oleada3)
            {
                foreach (GameObject barco in OleadaBarco3)
                {
                    if (barco != null)
                    {
                        barco.SetActive(true);
                    }
                }
                try
                {
                    texto.text = "Stage 3";
                }
                catch (Exception e) { }
            }

        }

        #endregion

        #region Oleadas Aviones

        public void activacionOleadasAviones()
        {
            //Se podría hacer que cuando empiece la oleada 2 desaparezcan los de la oleada 1.
            //Además, cuando se pierda el target deberían destruirse.
            System.Random rnd = new System.Random();
            if (oleada1)
            {
                foreach (GameObject avionAliado in OleadaAvion1)
                {
                    GameObject barcoRandom = OleadaBarco1[rnd.Next(0,OleadaBarco1.Count)];
                    if (Application.loadedLevelName != "Menu" && avionAliado)
                        avionAliado.GetComponent<MovAvion>().enabled = true;
                    if (barcoRandom && avionAliado)
                        avionAliado.GetComponent<AeroplaneAiControl>().SetTarget(barcoRandom.transform.FindChild("Target"));
                }
                foreach (GameObject avionEnemigo in OleadaAvionEnemigo1)
                {
                    GameObject aliadoRandom = OleadaAvion1[rnd.Next(0, OleadaAvion1.Count)];
                    if (avionEnemigo != null && aliadoRandom)
                    {
                        avionEnemigo.SetActive(true);
                        avionEnemigo.GetComponent<AeroplaneAiControl>().SetTarget(aliadoRandom.transform.FindChild("Target"));
                    }

                }
            }

            if (oleada2)
            {
                foreach (GameObject avionAliado in OleadaAvion2)
                {
                    GameObject barcoRandom = OleadaBarco2[rnd.Next(0, OleadaBarco2.Count)];
                    if (Application.loadedLevelName != "Menu")
                        avionAliado.GetComponent<MovAvion>().enabled = true;
                    if (barcoRandom && avionAliado)
                        avionAliado.GetComponent<AeroplaneAiControl>().SetTarget(barcoRandom.transform.FindChild("Target"));
                }
                foreach (GameObject avionEnemigo in OleadaAvionEnemigo2)
                {
                    GameObject aliadoRandom = OleadaAvion2[rnd.Next(0,OleadaAvion2.Count)];
                    if (avionEnemigo != null)
                    {
                        avionEnemigo.SetActive(true);
                        avionEnemigo.GetComponent<AeroplaneAiControl>().SetTarget(aliadoRandom.transform.FindChild("Target"));
                    }

                }
            }

            if (oleada3)
            {
                foreach (GameObject avionAliado in OleadaAvion3)
                {
                    GameObject barcoRandom = OleadaBarco3[rnd.Next(0,OleadaBarco3.Count)];
                    if (Application.loadedLevelName != "Menu")
                        avionAliado.GetComponent<MovAvion>().enabled = true;
                    if (barcoRandom && avionAliado)
                        avionAliado.GetComponent<AeroplaneAiControl>().SetTarget(barcoRandom.transform.FindChild("Target"));
                }
                foreach (GameObject avionEnemigo in OleadaAvionEnemigo3)
                {
                    GameObject aliadoRandom = OleadaAvion3[rnd.Next(0, OleadaAvion3.Count)];
                    if (avionEnemigo != null)
                    {
                        avionEnemigo.SetActive(true);
                        avionEnemigo.GetComponent<AeroplaneAiControl>().SetTarget(aliadoRandom.transform.FindChild("Target"));
                    }

                }
            }
        }

        #endregion

        #region Asignar Random
        public Transform GetRandomTarget()
        {
            if (oleada1)
            {
                foreach (GameObject barco in OleadaBarco1)
                {
                    if (barco)
                        return (barco.transform.FindChild("Target"));
                }
            }
            else if (oleada2)
            {
                foreach (GameObject barco in OleadaBarco2)
                {
                    if (barco)
                        return (barco.transform.FindChild("Target"));
                }
            }
            else
            {
                foreach (GameObject barco in OleadaBarco3)
                {
                    if (barco)
                        return (barco.transform.FindChild("Target"));
                }
            }
            return (null);
        }
        #endregion
    }
}