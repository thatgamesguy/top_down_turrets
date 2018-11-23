using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TopDownTurrets
{
	public enum SPECIAL_DAMAGE_TYPE
	{
		ICE_CASE,
		CHAIN_LIGHTNING,
		FIRE
	}

/// <summary>
/// Controls animation and damage for special projectiles (currently ice and chain lightning).
/// </summary>
	[RequireComponent (typeof(EnemyHealth))]
	[RequireComponent (typeof(Enemy))]
	public class DamageAnimationController : MonoBehaviour
	{
		public GameObject IceCaseAnimation;
		public GameObject LightningAnimation;
		public GameObject FireAnimation;
	
		public float ChainLightningRange = 3f;
		public int MaxChainLightningJumps = 6;
		public float ChainLightningDelay = 0.4f;

		private Enemy movement;
		private EnemyHealth health;
	
		private delegate void ApplyAnimation (float dps,float time);
	
		private Dictionary<SPECIAL_DAMAGE_TYPE, ApplyAnimation> damageMethodLookUp;

		private bool encasedInIce = false;
		public bool EncasedInIce { get { return encasedInIce; } }

		void Awake ()
		{
			if (!IceCaseAnimation) {
				Debug.Log ("Ice case animation has not been set");
			}

			if (!LightningAnimation) {
				Debug.Log ("Lightning animation has not been set");
			}
	
			damageMethodLookUp = new Dictionary<SPECIAL_DAMAGE_TYPE, ApplyAnimation> ();
			damageMethodLookUp.Add (SPECIAL_DAMAGE_TYPE.ICE_CASE, ApplyIceCase);
			damageMethodLookUp.Add (SPECIAL_DAMAGE_TYPE.CHAIN_LIGHTNING, ApplyLightningChain);
			damageMethodLookUp.Add (SPECIAL_DAMAGE_TYPE.FIRE, ApplyFire);
		
			movement = GetComponent<Enemy> ();
			health = GetComponent<EnemyHealth> ();
		}
	
		public void ApplyDamage (SPECIAL_DAMAGE_TYPE damageType, float dps, float time)
		{
			damageMethodLookUp [damageType] (dps, time);
		}
	
		private void ApplyIceCase (float dps, float time)
		{
			if (!encasedInIce) {
				encasedInIce = true;
				var ice = (GameObject)Instantiate (IceCaseAnimation, transform.position, Quaternion.identity);
				ice.transform.SetParent (transform);
				health.ApplyDPS (dps, time);
				movement.CanMove = false;
				Invoke ("DisableIceCase", 2.5f);
			}
		}

		private void DisableIceCase ()
		{
			encasedInIce = false;
		}
		
		private void ApplyFire (float dps, float time)
		{
			Instantiate (FireAnimation, transform.position, Quaternion.identity);
			//fire.transform.SetParent (transform);
			health.ApplyDPS (dps, time);
		}

		private void ApplyLightningChain (float dps, float time)
		{

			health.ApplyDPS (dps, time);	
			
			var enemy = GetClosestEnemy ();
			
			if (enemy) {			
				
				enemy.gameObject.AddComponent<ElectricHit> ();
			
				SpawnLightningChain (enemy);
				
				var other = enemy.GetComponent<DamageAnimationController> ();
				
				if (other) {
					other.StartLightningRoutine (dps, time, ChainLightningDelay);
				}
			}
			
		}
		
		private IEnumerator ApplyLightningChain (float dps, float time, float delay)
		{
			yield return new WaitForSeconds (delay);
			
			ApplyLightningChain (dps, time);
			
		}
		
		public void StartLightningRoutine (float dps, float time, float delay)
		{
			StartCoroutine (ApplyLightningChain (dps, time, delay));
		}
		
		private void SpawnLightningChain (Transform otherEnemy)
		{
			var dir = otherEnemy.transform.position - transform.position;
			
			var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
			
			// 512 = pixels of bolt sprite, 100f = pixels per units.
			var objectWidthSize = 512f / 100f;
			
			var lightning = (GameObject)Instantiate (LightningAnimation, transform.position, Quaternion.AngleAxis (angle, Vector3.forward));
			lightning.transform.localScale = new Vector3 (0.3f, dir.magnitude / objectWidthSize, lightning.transform.localScale.z);
		}
		
		private Transform GetClosestEnemy ()
		{
			var enemies = Physics2D.OverlapCircleAll (transform.position, ChainLightningRange, 1 << LayerMask.NameToLayer ("Enemy"));
			
			Transform closest = null;
			float closestDistanceSqr = Mathf.Infinity;
			var currentPosition = transform.position;
			
			foreach (var t in enemies) {
				if (t.GetInstanceID () == GetInstanceID () || t.GetComponent<ElectricHit> () != null)
					continue;
				
				Vector3 directionToTarget = t.transform.position - currentPosition;
				float dSqrToTarget = directionToTarget.sqrMagnitude;
				if (dSqrToTarget < closestDistanceSqr) {
					closestDistanceSqr = dSqrToTarget;
					closest = t.transform;
				}
			}
			
			return closest;
		}
	
	}
}
