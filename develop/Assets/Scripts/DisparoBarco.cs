using UnityEngine;
using System.Collections;

public class DisparoBarco : MonoBehaviour
{
    [SerializeField] private GameObject[] m_canones;
    [SerializeField] private float m_tiempoDisparo = 3.5f;
    [SerializeField] private GameObject m_balaPrefab;
    [SerializeField] private GameObject m_explosionPrefab;

    //private GameObject[] m_canones;
    private bool disparando = false;
    private GameObject balaInstanciada;
    private Transform target;

    void Start()
    {
        //canones = GetComponentsInChildren<GameObject>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Aliado" || col.tag == "Player")
        {
            target = col.gameObject.transform;
            disparando = true;
            StartCoroutine("CorrutinaDisparar");
        }
    }

    public IEnumerator CorrutinaDisparar()
    {
        while (disparando)
        {
            Disparar();
            yield return new WaitForSeconds(m_tiempoDisparo);
        }
    }

    public void Disparar()
    {
        foreach (GameObject canon in m_canones)
        {
            if (target)
            {
                // Rotar canon
                canon.transform.rotation = Quaternion.Slerp(canon.transform.rotation,
                Quaternion.LookRotation(target.position - canon.transform.position), 100 * Time.deltaTime);

                // Instanciar
                balaInstanciada = (GameObject)Instantiate(m_balaPrefab, canon.transform.position, canon.transform.rotation);
                Rigidbody rigidBala = balaInstanciada.GetComponent<Rigidbody>();
                rigidBala.AddForce(balaInstanciada.transform.forward * 25000);
                StartCoroutine("ExplosionBala", balaInstanciada.transform);
                Destroy(balaInstanciada, 0.23f);
            }
        }
    }

    private IEnumerator ExplosionBala(Transform bala) {
        yield return new WaitForSeconds(0.13f);
        if(bala)
            Instantiate(m_explosionPrefab, bala.position, bala.rotation);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Aliado" || col.tag == "Player")
        {
            disparando = false;
        }
    }
}
