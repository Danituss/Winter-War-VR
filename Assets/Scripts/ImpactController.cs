using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactController : MonoBehaviour {

    private AudioClip hitSound;
    private AudioSource audioSource;
	[Header("Impact sounds")]
	public AudioClip stoneImpactSound;
	public AudioClip iceImpactSound, fleshImpactSound, metalImpactSound, woodImpactSound, fallback;
	[Header("Impact particles")]
	public ParticleSystem stoneP;
	public ParticleSystem iceP, fleshP, metalP, woodP;

	void Awake () {
        gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        audioSource.spatialBlend = 1.0f; //Makes the audio 3D
        audioSource.volume = 0.7f;
		audioSource.maxDistance = 100f;
		transform.forward = -transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
        if (!audioSource.isPlaying && audioSource.clip != null) {
            Destroy(gameObject);
        }
	}

    public void PlayImpactEffects(PhysicalMaterial.physMaterial mat) 
    {
        switch (mat) 
        {
           case PhysicalMaterial.physMaterial.flesh:
			audioSource.clip = fleshImpactSound;
			Instantiate(fleshP, transform.position, gameObject.transform.rotation);
                break;
            case PhysicalMaterial.physMaterial.ice:
			audioSource.clip = iceImpactSound;
			Instantiate(iceP, transform.position, gameObject.transform.rotation);
                break;
            case PhysicalMaterial.physMaterial.metal:
			audioSource.clip = metalImpactSound;
			Instantiate(metalP, transform.position, gameObject.transform.rotation);
                break;
            case PhysicalMaterial.physMaterial.stone:
			audioSource.clip = stoneImpactSound;
			Instantiate(stoneP, transform.position, gameObject.transform.rotation);
                break;
            case PhysicalMaterial.physMaterial.wood:
			audioSource.clip = woodImpactSound;
			Instantiate(woodP, transform.position, gameObject.transform.rotation);
                break;
            default:
                audioSource.clip = fallback;
                break;
        }

        audioSource.Play();
    }

}