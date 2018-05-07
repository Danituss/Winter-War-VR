namespace VRTK
{
    using UnityEngine;

    public class RealGunMechanics : VRTK_InteractableObject
    {
        [Range(0, 50)]
        public float damage = 10f, pushForce;
        public int maxAmmo, currentAmmo;
        [Header("References")]
        public GameObject barrel;
        public ParticleSystem particle;
        private AudioSource audioSource;
        public ClipSnapDropZone clipSnapDropZone;
        Recoil recoil_script;
        private VRTK_ControllerEvents controllerEvents1;
        private VRTK_ControllerEvents controllerEvents2;
        public GameObject impactController;

        public AudioClip noAmmo;
        public GameObject emptyBullet;
        public Transform emptyBulletEjectPos;

        // True if weapon is ready to be fired
        [HideInInspector]
        public bool cocked;

        private GameObject trigger;
        private BoltActionSlide slide;

        private Rigidbody slideRigidbody;
        private Collider slideCollider;

        private VRTK_ControllerEvents controllerEvents;

        private float minTriggerRotation = -10f;
        private float maxTriggerRotation = 45f;


        protected void Start()
        {
            cocked = true;
            audioSource = GetComponent<AudioSource>();
            recoil_script = GetComponent<Recoil>();
        }

        protected override void Awake()
        {
            base.Awake();

            trigger = transform.Find("TriggerHolder").gameObject;

            slide = transform.Find("Slide").GetComponent<BoltActionSlide>();
            slideRigidbody = slide.GetComponent<Rigidbody>();
            slideCollider = slide.GetComponent<Collider>();
        }

        protected override void Update()
        {
            base.Update();
            if (controllerEvents)
            {
                var pressure = (maxTriggerRotation * controllerEvents.GetTriggerAxis()) - minTriggerRotation;
                trigger.transform.localEulerAngles = new Vector3(0f, pressure, 0f);
            }
            else
            {
                trigger.transform.localEulerAngles = new Vector3(0f, minTriggerRotation, 0f);
            }
        }



        private void ToggleCollision(Rigidbody objRB, Collider objCol, bool state)
        {
            objRB.isKinematic = state;
            objCol.isTrigger = state;
        }

        private void ToggleSlide(bool state)
        {
            if (!state)
            {
                slide.ForceStopInteracting();
            }
            slide.enabled = state;
            slide.isGrabbable = state;
            ToggleCollision(slideRigidbody, slideCollider, state);
        }

        public override void Grabbed(VRTK_InteractGrab currentGrabbingObject)
        {

            base.Grabbed(currentGrabbingObject);

            controllerEvents = currentGrabbingObject.GetComponent<VRTK_ControllerEvents>();

            ToggleSlide(true);

            //Limit hands grabbing when picked up
            if (VRTK_DeviceFinder.GetControllerHand(currentGrabbingObject.controllerEvents.gameObject) == SDK_BaseController.ControllerHand.Left)
            {
                allowedTouchControllers = AllowedController.LeftOnly;
                allowedUseControllers = AllowedController.LeftOnly;
                slide.allowedGrabControllers = AllowedController.RightOnly;
            }
            else if (VRTK_DeviceFinder.GetControllerHand(currentGrabbingObject.controllerEvents.gameObject) == SDK_BaseController.ControllerHand.Right)
            {
                allowedTouchControllers = AllowedController.RightOnly;
                allowedUseControllers = AllowedController.RightOnly;
                slide.allowedGrabControllers = AllowedController.LeftOnly;
            }
        }

        public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
        {

            base.Ungrabbed(previousGrabbingObject);

            ToggleSlide(false);

            //Unlimit hands
            allowedTouchControllers = AllowedController.Both;
            allowedUseControllers = AllowedController.Both;
            slide.allowedGrabControllers = AllowedController.Both;

            controllerEvents = null;

        }
        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            //If the weapon has been just reloaded, enable reloading again afte firing the first shot
            if (currentAmmo == maxAmmo)
            {
                clipSnapDropZone.enabled = true;
            }

            // We check if clip has bullets in it (and is cocked), else play noAmmo audioclip
            if (cocked == true && (currentAmmo >= 1))
            {
                base.StartUsing(usingObject);
                FireBullet();
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

        // when the fire button is pressed the gun shoots a raycast and looks if it hit a target that can be damaged
        protected virtual void FireBullet()
        {
            VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(controllerEvents1.gameObject), 1f, 0.05f, 0.01f);
            if (controllerEvents2 != null)
            {
                VRTK_ControllerHaptics.TriggerHapticPulse(VRTK_ControllerReference.GetControllerReference(controllerEvents2.gameObject), 1f, 0.05f, 0.01f);
            }
            //recoil_script.StartRecoil(0.2f, 5, 10);

            particle.Play();
            audioSource.Play();
            currentAmmo -= 1;
            cocked = false;
            EjectShell();

            RaycastHit hit;
            if (Physics.Raycast(barrel.transform.position, transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                // Spawns a new impact object that handles particles
                PhysicalMaterial mat = hit.transform.GetComponent<PhysicalMaterial>();
                GameObject impact = Instantiate(impactController, hit.transform.position, gameObject.transform.rotation);
                impact.GetComponent<ImpactController>().PlayImpactEffects(mat.material);
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
            }
        }

        protected virtual void Recoil()
        {

        }

        public virtual void EjectShell() {
            if (currentAmmo > 0) {
                GameObject emptyBullet_clone;
                emptyBullet_clone = Instantiate(emptyBullet, emptyBulletEjectPos.position, emptyBulletEjectPos.rotation);
                emptyBullet_clone.GetComponent<Rigidbody>().AddForce(new Vector3(0.5f, 0.5f, 0) * 0.2f, ForceMode.Impulse);
            }
        }
    }
}