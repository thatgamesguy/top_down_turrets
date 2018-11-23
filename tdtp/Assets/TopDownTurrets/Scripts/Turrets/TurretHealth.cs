using UnityEngine;
using System.Collections;

public class TurretHealth : MonoBehaviour
{

	public int MaxHealth = 20;
	public GameObject ExplosionPrefab;

	public void ApplyDamage (int damage)
	{
		MaxHealth -= damage;
		
		if (MaxHealth <= 0) {
			OnDead ();
		}
	}
	
	private void OnDead ()
	{
		Instantiate (ExplosionPrefab, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
