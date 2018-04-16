using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundPlayer : MonoBehaviour {

    private AudioClip hitSound;
    private AudioSource audioSource;

	void Awake () {
        gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        audioSource.spatialBlend = 1.0f; //Makes the audio 3D
        audioSource.volume = 0.7f;
		audioSource.maxDistance = 100f;
	}
	
	// Update is called once per frame
	void Update () {
        if (!audioSource.isPlaying && audioSource.clip != null) {
            Destroy(gameObject);
        }
	}

    public void PlaySound(PhysicalMaterial.physMaterial mat, AudioClip fallback, AudioClip stone, AudioClip ice, AudioClip flesh, AudioClip metal, AudioClip wood) 
    {
        switch (mat) 
        {
           case PhysicalMaterial.physMaterial.flesh:
                audioSource.clip = flesh;
                break;
            case PhysicalMaterial.physMaterial.ice:
                audioSource.clip = ice;
                break;
            case PhysicalMaterial.physMaterial.metal:
                audioSource.clip = metal;
                break;
            case PhysicalMaterial.physMaterial.stone:
                audioSource.clip = stone;
                break;
            case PhysicalMaterial.physMaterial.wood:
                audioSource.clip = wood;
                break;
            default:
                audioSource.clip = fallback;
                break;
        }

        audioSource.Play();
    }
}
