using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	public class StandardGunProjectile : Projectile
	{
		public GameObject[] AnimationOnImpactPrefabs;
		
		public bool PlayAnimationOnWallHit = false;

		void OnTriggerEnter2D (Collider2D other)
		{
		
			if (other.CompareTag ("Turret") && !other.isTrigger) {
				if (PlayAnimationOnWallHit) {
					InitDamageAnimation (other);
				}
				var turretHealth = other.GetComponent<TurretHealth> ();
				turretHealth.ApplyDamage (1);
				
				ReturnProjectile ();
			} else if (other.CompareTag ("Wall")) {
                if (PlayAnimationOnWallHit) {
					InitDamageAnimation (other);
				}
				ReturnProjectile ();
			} else if (other.CompareTag ("Enemy")) {
                InitDamageAnimation (other);
				ApplyDamage (other);
				if (DestroyOnEnemyImpact)
					ReturnProjectile ();
			}
		}
		
		protected void InitDamageAnimation (Collider2D other)
		{
			if (ImpactAnimationPresent ()) {
				var dir = transform.up.normalized;
				var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
				
				Instantiate (AnimationOnImpactPrefabs [Random.Range (0, AnimationOnImpactPrefabs.Length)], 
				             transform.position, Quaternion.AngleAxis (angle, Vector3.forward));
			}
		}
		
		private bool ImpactAnimationPresent ()
		{
			return AnimationOnImpactPrefabs != null && AnimationOnImpactPrefabs.Length > 0;
		}

	}
}
