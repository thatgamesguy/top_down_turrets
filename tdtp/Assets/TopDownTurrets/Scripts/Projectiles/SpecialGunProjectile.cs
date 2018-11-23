using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	public class SpecialGunProjectile : Projectile
	{
		public SPECIAL_DAMAGE_TYPE DamageType;
		
		private float dps = 1;
		public float DPS { set { dps = value; } }
		
		private float damageTime;
		public float DamageTime { set { damageTime = value; } }
	
		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.CompareTag ("Turret") && !other.isTrigger) {
				var turretHealth = other.GetComponent<TurretHealth> ();
				turretHealth.ApplyDamage (1);
				
				ReturnProjectile ();
			} else if (other.CompareTag ("Wall")) {

				ReturnProjectile ();
			} else if (other.CompareTag ("Enemy")) {
				var damageController = other.GetComponent<DamageAnimationController> ();
			
				if (!damageController) {
					Debug.LogError ("Enemy should have DamageAnimationController script to apply special damage");
				} else {
					damageController.ApplyDamage (DamageType, dps, damageTime);
				}
			
				ReturnProjectile ();
			}
		}
	}
}