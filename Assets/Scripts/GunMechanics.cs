namespace VRTK
{
    using UnityEngine;

    public class GunMechanics : VRTK_InteractableObject
    {
        [Range(0,50)]
        public float damage = 10f;
		public float pushForce;
		public GameObject barrel;
		public GameObject muzzleLight;
		public ParticleSystem particle;
		private AudioSource audioSource;
        Recoil recoil_script;
        public AudioClip fallbackClip, stoneClip, iceClip, fleshClip, metalClip, woodClip, noAmmo;
        
        public int maxAmmo, currentAmmo;

        // True if weapon is ready to be fired
        bool cocked;

		// Ei lataus ääniä?
		//public AudioClip reload;

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            // We check if clip has bullets in it (and is cocked), else play noAmmo audioclip
            if (cocked == true && (currentAmmo >= 1))
            {
				base.StartUsing (usingObject);
				FireBullet ();
			}
            else
            {
                audioSource.PlayOneShot(noAmmo);
            }
        }

        public void Reload()
        {
            currentAmmo = maxAmmo;
            Debug.Log("Ammo Reloaded");
        }

        protected void Start()
        {
			cocked = true;
			audioSource = GetComponent<AudioSource>();
            recoil_script = GetComponent<Recoil>();
        }
        
        // when the fire button is pressed the gun shoots a raycast and looks if it hit a target that can be damaged
        protected void FireBullet()
        {

            //recoil_script.StartRecoil(0.2f, 5, 10);

			particle.Play ();
			audioSource.Play();
			currentAmmo -= 1;
			//cocked = false;
            RaycastHit hit;
            if (Physics.Raycast(barrel.transform.position, transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                /*
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }*/

                //If hit physical object
                if (hit.rigidbody)
                {
                    hit.rigidbody.AddForce(gameObject.transform.forward * pushForce, ForceMode.Impulse);
                }

                //If hit shooting range target
                if (hit.transform.name.Contains("Board"))
                {
                    Target_Script ts = hit.transform.GetComponent<Target_Script>();
                    //ts.StopAllCoroutines();
                    ts.MakeFall();
                    //ts.StartCoroutine(ts.BringDown());
                }

                PhysicalMaterial mat = hit.transform.GetComponent<PhysicalMaterial>();

                GameObject go = new GameObject("hit");
                go.transform.position = hit.transform.position;

                go.AddComponent<HitSoundPlayer>();
                go.GetComponent<HitSoundPlayer>().PlaySound(mat.material, fallbackClip, stoneClip, iceClip, fleshClip, metalClip, woodClip);
            }
        }

        protected virtual void Recoil() {
            
        }
    }
}