using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace Objetos
{
    [RequireComponent(typeof(AeroplaneController))]
    public class AeroplaneUserControl2Axis : MonoBehaviour
    {

        #region Por defecto
        // these max angles are only used on mobile, due to the way pitch and roll input are handled
        public float maxRollAngle = 80;
        public float maxPitchAngle = 80;

        // reference to the aeroplane that we're controlling
        private AeroplaneController m_Aeroplane;
        Text texto;
        AudioSource metralleta, helice;

        private void Start()
        {
            if (Application.loadedLevelName != "Menu")
            {
                texto = GameObject.FindGameObjectWithTag("Gui").GetComponent<Text>() as Text;
                metralleta = GameObject.Find("OVRCameraRig").GetComponent<AudioSource>() as AudioSource;
                metralleta.Play();
                helice = GameObject.Find("Helice").GetComponent<AudioSource>() as AudioSource;
                helice.Play();
                for (int i = 0; i < 100; i++)
                {
                    helice.volume += 0.01f;
                }
            }
        }

        private void Awake()
        {
            // Set up the reference to the aeroplane controller.
            m_Aeroplane = GetComponent<AeroplaneController>();
        }

        private void FixedUpdate()
        {
            // Read input for the pitch, yaw, roll and throttle of the aeroplane.
            float roll = CrossPlatformInputManager.GetAxis("Horizontal");
            float pitch = CrossPlatformInputManager.GetAxis("Vertical");
            bool airBrakes = CrossPlatformInputManager.GetButton("Fire3");

            // auto throttle up, or down if braking.
            float throttle = airBrakes ? -1 : 1;
#if MOBILE_INPUT
            AdjustInputForMobileControls(ref roll, ref pitch, ref throttle);
#endif

            if (Input.GetButtonDown("Fire1"))
            {
                // Audio
                metralleta.volume = 0;
                for (int i = 0; i < 70; i++)
                {
                    metralleta.volume += 0.01f;
                }
            }
            if (Input.GetButton("Fire1"))
            {
                this.GetComponent<Objetos.AeroplaneController>().DispararAmetralladora();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                this.GetComponent<Objetos.AeroplaneController>().m_disparando = false;
                //metralleta.volume = 0;
                for (int i = 0; i < 100; i++)
                {
                    metralleta.volume -= 0.01f;
                }
            }

            if (Input.GetButtonDown("Fire2") && !this.GetComponent<Objetos.AeroplaneController>().despegando && this.GetComponent<Objetos.AeroplaneController>().Preparado()) // Bomba
            {
                this.GetComponent<Objetos.AeroplaneController>().DispararBomba();
                texto.text = "Bomb dropped";
            }

            if (Input.GetButtonDown("Speed"))
            {
                this.GetComponent<Objetos.AeroplaneController>().AumentarVelocidad();
            }
            if (Input.GetButtonDown("Escape"))
            {
                Application.LoadLevel("Menu");
            }

            // Pass the input to the aeroplane
            m_Aeroplane.Move(roll, pitch, 0, throttle, airBrakes);
        }


        private void AdjustInputForMobileControls(ref float roll, ref float pitch, ref float throttle)
        {
            // because mobile tilt is used for roll and pitch, we help out by
            // assuming that a centered level device means the user
            // wants to fly straight and level!

            // this means on mobile, the input represents the *desired* roll angle of the aeroplane,
            // and the roll input is calculated to achieve that.
            // whereas on non-mobile, the input directly controls the roll of the aeroplane.

            float intendedRollAngle = roll * maxRollAngle * Mathf.Deg2Rad;
            float intendedPitchAngle = pitch * maxPitchAngle * Mathf.Deg2Rad;
            roll = Mathf.Clamp((intendedRollAngle - m_Aeroplane.RollAngle), -1, 1);
            pitch = Mathf.Clamp((intendedPitchAngle - m_Aeroplane.PitchAngle), -1, 1);

            // similarly, the throttle axis input is considered to be the desired absolute value, not a relative change to current throttle.
            float intendedThrottle = throttle * 0.5f + 0.5f;
            throttle = Mathf.Clamp(intendedThrottle - m_Aeroplane.Throttle, -1, 1);
        }
        #endregion

    }
}
