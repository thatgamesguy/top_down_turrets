using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	public abstract class DPSTurret : Turret
	{
		public int DPS = 2;
		public float DPSTime = 1f;
		
		protected override void UpdateProjectile (GameObject bullet, bool setOwner)
		{
			var projectile = bullet.GetComponent<SpecialGunProjectile> ();
			
			if (setOwner)
				projectile.Owner = this;
			
			projectile.DPS = DPS;
			projectile.DamageTime = DPSTime;
			projectile.OwnerID = gameObject.GetInstanceID ();
		}
		
		
	}
}
