using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	public class FlameBurst : MonoBehaviour
	{
		public int DPS { get; set; }
		public float DamageTime { get; set; }

		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.CompareTag ("Enemy")) {
				var otherController = other.GetComponent<DamageAnimationController> ();
				
				otherController.ApplyDamage (SPECIAL_DAMAGE_TYPE.FIRE, DPS, DamageTime);
			}
		}
	}
}
