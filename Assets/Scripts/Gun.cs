namespace VRTK.examples
{
    using UnityEngine;

    public class Gun : VRTK_InteractableObject
    {
        public float damage = 10f;
		public GameObject barrel;
		public GameObject muzzleLight;
		public ParticleSystem particle;

		public int clipSize;
		int clipCurrent;

		// True if weapon is ready to be fired 
		bool cocked;

		// Sounds
		public AudioClip fire;

		// Ei lataus ääniä?
		//public AudioClip reload;

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
			// We check if clip has bullets in it
			if (clipCurrent > 0 && cocked == true) {
				base.StartUsing (usingObject);
				FireBullet ();
			}
        }

        protected void Start()
        {
			clipCurrent = clipSize;
			cocked = true;
        }

            // when the fire button is pressed the gun shoots a raycast and looks if it hit a target that can be damaged
         void FireBullet()
        {
			particle.Play ();
			muzzleLight.SetActive (true);
			clipCurrent -= 1;
			cocked = false;
            RaycastHit hit;
            if (Physics.Raycast(barrel.transform.position, transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }
    }
}