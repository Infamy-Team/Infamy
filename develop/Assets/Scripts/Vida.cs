using UnityEngine;
using System.Collections;

namespace Objetos
{
    public class Vida : MonoBehaviour
    {

        [SerializeField] public int m_vida = 3;

        public void QuitarVida(int cant)
        {            
            m_vida -= cant;
            //Debug.Log(this.name + ": " + m_vida);
            if (m_vida <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}