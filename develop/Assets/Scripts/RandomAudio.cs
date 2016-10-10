using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomAudio : MonoBehaviour {

    [SerializeField] private List<AudioClip> m_clips;
    private AudioSource origenAudio;
    private int ultimo;
    private int largo;

	void Start () {
        origenAudio = this.GetComponent<AudioSource>();
        largo = m_clips.Count;
        ultimo = -1;
	}
	
	public void PlaySonido () {
        /*
        if (ultimo > 0)
        {

        }
        else 
        {*/
            ultimo = Random.Range(0, largo);
            origenAudio.PlayOneShot(m_clips[ultimo]);
            Destroy(this.gameObject, m_clips[ultimo].length);
        //}
	}
}
