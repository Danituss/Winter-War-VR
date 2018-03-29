namespace VRTK.examples
{
    using UnityEngine;

    public class Gun : VRTK_InteractableObject
    {
        public float damage = 10f;
		public float pushForce;
		public GameObject barrel;
		public GameObject muzzleLight;
		public ParticleSystem particle;

		//public int clipSize;
		//int clipCurrent;

		// True if weapon is ready to be fired 
		bool cocked;

		// Sounds
		public AudioClip fire;

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
        }

        AudioClip iceImpactSound;
            // when the fire button is pressed the gun shoots a raycast and looks if it hit a target that can be damaged
         void FireBullet()
        {
			particle.Play ();
			muzzleLight.SetActive (true);
			//clipCurrent -= 1;
			//cocked = false;
            RaycastHit hit;
            if (Physics.Raycast(barrel.transform.position, transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
				if (hit.rigidbody){
					hit.rigidbody.AddForce (Vector3.forward * pushForce, ForceMode.Impulse);
						}
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                AudioClip hitAudio;
                PhysicalMaterial mat = hit.transform.GetComponent<PhysicalMaterial>();
                /*switch (mat.material) {
                    case PhysicalMaterial.physMaterial.ice:
                        hitAudio = iceImpactSound;
                        break;

                }*/

                GameObject go = new GameObject("hit");
                go.transform.position = hit.transform.position;
                /*
                go.AddComponent<HitSoundPlayer>();
                go.GetComponent<HitSoundPlayer>().PlaySound(hitAudio);
                */
               }
        }
    }
}