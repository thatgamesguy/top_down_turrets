using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	public class LightningDPS : DPSTurret
	{
		private float currentDelay = 0f;
		
		void Update ()
		{
			currentDelay += Time.deltaTime;
			
			if (NoTargets ()) {
				return;
			}
			
			if (TargetDestroyed () || TargetElectrified ()) {
				GetTarget ();
				return;
			}
			
			RotateTowardsTarget ();
			
			if (ClearPathToEnemy (transform.right) && BulletReady ()) {
				Fire ();
			} 
		}
		
		private bool TargetElectrified ()
		{
			return false;
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
