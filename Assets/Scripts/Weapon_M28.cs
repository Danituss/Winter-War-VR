namespace VRTK
{
    using UnityEngine;

    public class Weapon_M28 : GunMechanics {
        protected override void Recoil() {
            base.Recoil();
        }
        protected override void FireBullet()
        {
            base.FireBullet();
            //cocked = false;
        }
    }
}
