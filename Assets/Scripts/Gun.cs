namespace VRTK.examples
{
    using UnityEngine;

    public class Gun : VRTK_InteractableObject
    {
        public float damage = 10f;

        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            FireBullet();
        }

        protected void Start()
        {
           
        }
            // when the fire button is pressed the gun shoot a raycast and looks if it hit a target that can be damaged
         void FireBullet()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.forward, out hit))
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