using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	[RequireComponent (typeof(AudioSource))]
	public class EnemyHealth : MonoBehaviour
	{
		public float MaxHealth = 10f;

		public AudioClip[] OnHitSounds;
		public GameObject OnDeadAnimation;
		public GameObject[] OnDeadSprites;

		private float? dpsAmount = null;
	
	
		void Awake ()
		{
			if (OnDeadSprites == null || OnDeadSprites.Length == 0) {
				Debug.LogError ("Please set sprites to be shown when zombie dies");
			}
		
		
		}

		private void PlayHitSound ()
		{
			GetComponent<AudioSource> ().PlayOneShot (OnHitSounds [Random.Range (0, OnHitSounds.Length)]);
		}
	
		public void OnHit (float damageAmount)
		{
			PlayHitSound ();
			MaxHealth -= damageAmount;

			if (MaxHealth <= 0f) {
				OnDead ();
			}
		}

		public void ApplyDPS (float dps, float time)
		{
			PlayHitSound ();
			this.dpsAmount = dps;
			Invoke ("DisableDPS", time);
		}

		private void DisableDPS ()
		{
			dpsAmount = null;
		}

		void Update ()
		{
			if (dpsAmount.HasValue) {
	
				MaxHealth -= dpsAmount.Value * Time.deltaTime;

				if (MaxHealth <= 0f) {
					OnDead ();
				}
			}
		}

		private void OnDead ()
		{
			if (OnDeadAnimation) {
				Instantiate (OnDeadAnimation, transform.position, Quaternion.identity);
			}

			Instantiate (OnDeadSprites [Random.Range (0, OnDeadSprites.Length)], transform.position, Quaternion.identity);

			// For demo purposes. Used to limit number of enemies on screen at once.
			EnemySpawner.instance.EnemyRemoved ();
			
			KillCount.instance.EnemyKilled ();
		
			Destroy (gameObject);

		}
	}
}

