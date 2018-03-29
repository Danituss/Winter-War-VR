using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundPlayer : MonoBehaviour {

    private AudioClip hitSound;
    private AudioSource audioSource;

	void Awake () {
        gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!audioSource.isPlaying && audioSource.clip != null) {
            Destroy(this);
        }
	}

    public void PlaySound(PhysicalMaterial.physMaterial mat, AudioClip a, AudioClip b, AudioClip c, AudioClip d, AudioClip e, AudioClip f) 
    {
        switch (mat.material) 
        {
            case PhysicalMaterial.physMaterial.fallback:
                audioSource.clip = a;
                break;
        }

        audioSource.Play();
    }
}
