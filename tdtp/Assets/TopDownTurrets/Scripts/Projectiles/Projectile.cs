using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	[RequireComponent (typeof(Rigidbody2D))]
	public abstract class Projectile : MonoBehaviour
	{
		public float MaxTimeAlive = 2f;
		public bool DestroyOnEnemyImpact = true;


		private int damageAmount;
		public int DamageAmount { set { damageAmount = value; } }

		protected Turret owner;
		public Turret Owner {
			set {
				owner = value;
			}
		}
		
		public int OwnerID;
		


		private float currentTimeAlive;

		public virtual void Awake ()
		{
			gameObject.SetActive (false);
		}

		void OnEnable ()
		{
			currentTimeAlive = 0f;
		}

		public virtual void Update ()
		{
			currentTimeAlive += Time.deltaTime;
			if (currentTimeAlive >= MaxTimeAlive) {
				ReturnProjectile ();
			}

		}


		protected void ApplyDamage (Collider2D other)
		{
			other.GetComponent<EnemyHealth> ().OnHit (damageAmount);
		}

	
		
	

		/// <summary>
		/// Gets the status of the gun in case it has been removed from scene/disabled.
		/// </summary>
		/// <returns><c>true</c>, if owner not null and gun object active.</returns>
		private bool TurretActive ()
		{
			return owner != null && owner.gameObject.activeInHierarchy;
		}

		protected void ReturnProjectile ()
		{
			if (TurretActive ()) {
				owner.PoolObject (gameObject);
			} else {
				Destroy (gameObject);
			}
		}


	}
}

