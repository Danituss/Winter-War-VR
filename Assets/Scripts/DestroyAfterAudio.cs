using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAudio : MonoBehaviour {

    private void Awake()
    {
        GetComponent<AudioSource>().Play();
    }

    void LateUpdate () {
		if(!GetComponent<AudioSource>().isPlaying)
        {
            Destroy(this.gameObject);
        }
	}
}
