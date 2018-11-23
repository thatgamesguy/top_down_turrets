using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	public class IceDPS : DPSTurret
	{
	
		private Animator _animator;
		private int firingHash = Animator.StringToHash ("firing");
		private float currentDelay = 0f;
		private DamageAnimationController controller;
		
		public override void Awake ()
		{
			_animator = GetComponent<Animator> ();
			_animator.speed = ShootSpeed;
			base.Awake ();
		}

		void Update ()
		{
			currentDelay += Time.deltaTime;
			if (NoTargets ()) {
				_animator.SetBool (firingHash, false);
				return;
			}
			
			if (TargetDestroyed () || TargetEncasedInIce ()) {
				_animator.SetBool (firingHash, false);
				GetTarget ();
				return;
			}
			
			RotateTowardsTarget ();
			
			if (ClearPathToEnemy (transform.right) && BulletReady ()) {
				_animator.SetBool (firingHash, true);
			} else {
				_animator.SetBool (firingHash, false);
			}
		}
		
		private bool TargetEncasedInIce ()
		{
			if (controller == null || currentTarget.gameObject != controller.gameObject) {
				controller = currentTarget.GetComponent<DamageAnimationController> ();
			}

			return controller.EncasedInIce;
		}
		
		protected override void GetTarget ()
		{
			for (int i =0; i <targets.Count; i++) {
				var t = targets [i];
				
				if (t == null) {
					DeregisterTarget (t);
					continue;
				}
				
				var iceController = t.GetComponent<DamageAnimationController> ();
				
				if (iceController == null || !iceController.EncasedInIce) {
					currentTarget = t;
					break;
				}
			}
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
