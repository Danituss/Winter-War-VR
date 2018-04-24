namespace VRTK
{
    using UnityEngine;

    public class GunMechanics : VRTK_InteractableObject
    {
        [Range(0,50)]
        public float damage = 10f, pushForce;
        public int maxAmmo, currentAmmo;
        [Header("References")]
		public GameObject barrel;
		public ParticleSystem particle;
        private AudioSource audioSource;
        public ClipSnapDropZone clipSnapDropZone;
        Recoil recoil_script;
        [Header("Impact sounds")]
        public AudioClip fallbackClip;
        public AudioClip stoneClip, iceClip, fleshClip, metalClip, woodClip, noAmmo;
        private VRTK_ControllerEvents controllerEvents1;
        private VRTK_ControllerEvents controllerEvents2;

        // True if weapon is ready to be fired
        public bool cocked;

        // Ei lataus ääniä?
        //public AudioClip reload;
        public override void Grabbed(VRTK_InteractGrab currentGrabbingObject)
        {
            
            base.Grabbed(currentGrabbingObject);


            if (controllerEvents1 != null)
            {
                controllerEvents2 = currentGrabbingObject.GetComponent<VRTK_ControllerEvents>();
            }
            else
            {


                controllerEvents1 = currentGrabbingObject.GetComponent<VRTK_ControllerEvents>();
            }
            Debug.Log(controllerEvents1);
            Debug.Log(controllerEvents2);
        }

        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
        {
            
            base.Ungrabbed(previousGrabbingObject);

            if (controllerEvents1 == null)
            {
                controllerEvents2 = null;
            } 
            controllerEvents1 = null;
            Debug.Log(controllerEvents1);
            Debug.Log(controllerEvents2);
        }
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

            if(currentAmmo == maxAmmo) 
            {
                clipSnapDropZone.enabled = true;
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
        protected virtual void FireBullet()
        {
            VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(controllerEvents1.gameObject), 1f, 0.05f, 0.01f);
            if (controllerEvents2 != null)
            {
                VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(controllerEvents2.gameObject), 1f, 0.05f, 0.01f);
            }
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