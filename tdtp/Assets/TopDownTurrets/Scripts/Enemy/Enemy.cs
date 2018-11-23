using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	[RequireComponent (typeof(Animator))]
	[RequireComponent (typeof(Rigidbody2D))]
	public class Enemy : MonoBehaviour
	{
		public float MoveSpeed;
		public int DamageAmount = 2;

		private GameObject turret;
	
		private bool canMove;
		public bool CanMove {
			set {
				canMove = value;
			}
		}
		
		private Animator animator;
		private int attackHash = Animator.StringToHash ("attacking");
		
		private TurretHealth turretHealth;

		void Awake ()
		{
			animator = GetComponent<Animator> ();
			canMove = false;
			GetNearestTurret ();
		}
		
		private void GetNearestTurret ()
		{
			var turrets = GameObject.FindGameObjectsWithTag ("Turret");
			
			GameObject closest = null;
			float distance = float.MaxValue;
			
			foreach (var t in turrets) {
				var dis = (t.transform.position - transform.position).magnitude;
				
				if (dis < distance) {
					distance = dis;
					closest = t;
				}
			}
			
			if (closest != null) {
				turretHealth = closest.GetComponent<TurretHealth> ();
			}
			
			turret = closest;
		}

		public void SpawnComplete ()
		{
			canMove = true;
		}

		void Update ()
		{
			if (!canMove) {
				animator.SetBool (attackHash, false);
				return;
			}
			
			if (turret == null) {
				animator.SetBool (attackHash, false);
				GetNearestTurret ();
				return;
			}
		
			if (InAttackRange ()) {
				animator.SetBool (attackHash, true);
			} else {
				animator.SetBool (attackHash, false);
				MoveToTarget ();
			}
			
		}
		
		private bool InAttackRange ()
		{
			return (turret.transform.position - transform.position).magnitude < 0.5f;
		}
		
		private void MoveToTarget ()
		{
			var heading = turret.transform.position - transform.position;
			var distance = heading.magnitude;
			var dir = heading / distance;
			
			var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.Translate (dir * MoveSpeed * Time.deltaTime, Space.World);
		}
		
		public void ApplyDamageToTurret ()
		{
			if (turretHealth != null)
				turretHealth.ApplyDamage (DamageAmount);
		}
	}
}
