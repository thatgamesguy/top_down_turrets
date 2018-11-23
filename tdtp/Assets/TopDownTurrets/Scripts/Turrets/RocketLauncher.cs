using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	public class RocketLauncher : DamageTurret
	{

		public GameObject TurretFlash;

		private float currentDelay = 0f;

		// Update is called once per frame
		void Update ()
		{
			currentDelay += Time.deltaTime;
		
			if (NoTargets ())
				return;
		
			if (TargetDestroyed ()) {
				GetTarget ();
				return;
			}
		
			RotateTowardsTarget ();
			
			
			if (ClearPathToEnemy (transform.right) && BulletReady ()) {
				Fire ();
				TurretFlash.SetActive (true);
			} else {
				TurretFlash.SetActive (false);
			}
		}
		
		public override void Fire ()
		{
			var bullet = GetBullet ();
			bullet.transform.position = projSpawnLoc.position;
			bullet.transform.rotation = projSpawnLoc.rotation;
            bullet.SetActive(true);
			bullet.GetComponent<SeekingGunProjectile> ().Target = currentTarget;
			bullet.GetComponent<Rigidbody2D> ().AddForce (transform.right * ProjectileLauchSpeed);
		}
		
		private bool TargetInFront ()
		{
			var heading = currentTarget.position - transform.position;
			
			var dot = Vector2.Dot (heading, transform.right);
			
			return dot > 2f; 
		}
		
		private bool BulletReady ()
		{
			if (currentDelay >= ShootSpeed) {
				currentDelay = 0f;
				return true;
			}
			
			return false;
		}
	}
}
