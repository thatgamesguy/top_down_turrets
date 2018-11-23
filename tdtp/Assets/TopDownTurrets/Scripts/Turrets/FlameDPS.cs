using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	public class FlameDPS : DPSTurret
	{		
		private GameObject flameInstance;

		// Use this for initialization
		public override void Awake ()
		{
			base.Awake ();
			
			flameInstance = (GameObject)Instantiate (BulletPrefab);
			flameInstance.transform.SetParent (transform, false);
			flameInstance.transform.position = projSpawnLoc.position;
			flameInstance.transform.rotation = projSpawnLoc.rotation;
			
			var flameBurst = flameInstance.GetComponent<FlameBurst> ();
			
			flameBurst.DPS = DPS;
			flameBurst.DamageTime = DPSTime;
		}
	
		void Update ()
		{
			if (NoTargets ()) {
				flameInstance.SetActive (false);
				return;
			}
			
			if (TargetDestroyed ()) {
				flameInstance.SetActive (false);
				GetTarget ();
				return;
			}
			
			RotateTowardsTarget ();
			
			if (ClearPathToEnemy (transform.right)) {
				flameInstance.SetActive (true);
			} else {
				flameInstance.SetActive (false);
			}
		}
	}
}
