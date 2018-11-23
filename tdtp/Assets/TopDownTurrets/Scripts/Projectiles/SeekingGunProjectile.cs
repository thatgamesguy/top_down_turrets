using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	[RequireComponent (typeof(Rigidbody2D))]
	public class SeekingGunProjectile : StandardGunProjectile
	{		
		public float Velocity = 40f;
		public float TurnSpeed = 5f;
		public Transform Target { get; set; }

		private Rigidbody2D rigibody2D;
		
		public override void Awake ()
		{
			base.Awake ();
			
			rigibody2D = GetComponent<Rigidbody2D> ();
		}


		private Transform GetNearestObject (string tag)
		{
			var objs = GameObject.FindGameObjectsWithTag (tag);

			Transform closest = null;

			float closestDistance = float.MaxValue;

			foreach (var obj in objs) {

				var heading = obj.transform.position - transform.position;
				var distance = heading.magnitude;

				if (distance < closestDistance && IsTargetInFront (obj.transform)) {
					closestDistance = distance;
					closest = obj.transform;
				}
			}

			return closest;
		}

		public override void Update ()
		{
			base.Update ();
			
			RotateTowardsTarget ();

			rigibody2D.AddForce (transform.up * Velocity);

		} 

		private void RotateTowardsTarget ()
		{
			if (Target != null) {
				var dir = Target.position - transform.position;
				var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.AngleAxis (angle, Vector3.forward), Time.deltaTime * TurnSpeed);
			}
		}

		public Vector2 GetForce ()
		{
			return Target.position - transform.position;
		}
		
		private bool IsTargetInFront (Transform target)
		{
			var heading = target.position - transform.position;
		
			var dot = Vector2.Dot (heading, transform.up);

			return dot > 1.2f; 
		}
		
		


	}
}
