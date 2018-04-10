namespace VRTK.examples
{
    using UnityEngine;

    public class Gun : VRTK_InteractableObject
    {
        [Range(0,50)]
        public float damage = 10f;
		public float pushForce;
		public GameObject barrel;
		public GameObject muzzleLight;
		public ParticleSystem particle;
		private AudioSource audioSource;

        public AudioClip fallbackClip, stoneClip, iceClip, fleshClip, metalClip, woodClip;

		//public int clipSize;
		//int clipCurrent;

		// True if weapon is ready to be fired 
		bool cocked;

		// Ei lataus ääniä?
		//public AudioClip reload;

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
			// We check if clip has bullets in it
			if (cocked == true) {
				base.StartUsing (usingObject);
				FireBullet ();
			}
        }

        protected void Start()
        {
			//clipCurrent = clipSize;
			cocked = true;
			audioSource = GetComponent<AudioSource>();
        }
        
            // when the fire button is pressed the gun shoots a raycast and looks if it hit a target that can be damaged
         void FireBullet()
        {
			SteamVR_Controller.Input((int)SteamVR_TrackedObject.EIndex.Device1).TriggerHapticPulse(500);
			SteamVR_Controller.Input((int)SteamVR_TrackedObject.EIndex.Device2).TriggerHapticPulse(500);


			particle.Play ();
			audioSource.Play();
			//clipCurrent -= 1;
			//cocked = false;
            RaycastHit hit;
            if (Physics.Raycast(barrel.transform.position, transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
				if (hit.rigidbody){
					hit.rigidbody.AddForce (gameObject.transform.forward * pushForce, ForceMode.Impulse);
						}
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                
                PhysicalMaterial mat = hit.transform.GetComponent<PhysicalMaterial>();

                GameObject go = new GameObject("hit");
                go.transform.position = hit.transform.position;

                go.AddComponent<HitSoundPlayer>();
                go.GetComponent<HitSoundPlayer>().PlaySound(mat.material, fallbackClip, stoneClip, iceClip, fleshClip, metalClip, woodClip);
               }
        }
    }
}