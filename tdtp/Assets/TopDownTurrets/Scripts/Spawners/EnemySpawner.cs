using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	public class EnemySpawner : MonoBehaviour
	{
		public GameObject EnemyPrefab;
		public float SpawnTime = 0.8f;
		public int MaxEnemiesOnScreen = 10;
	
		private int currentEnemyCount = 0;

		private static EnemySpawner _instance;
		public static EnemySpawner instance { get { return _instance; } }

		void Awake ()
		{
			_instance = this;
		}

		// Use this for initialization
		void Start ()
		{

			InvokeRepeating ("SpawnEnemy", 0f, SpawnTime);
		}
	
		public void EnemyRemoved ()
		{
			currentEnemyCount--;
		}

		private void SpawnEnemy ()
		{
			if (currentEnemyCount >= MaxEnemiesOnScreen)
				return;
			
			currentEnemyCount++;
			var position = Environment.instance.RandomFloorTile.transform.position;

			var rot = Quaternion.identity;
			Instantiate (EnemyPrefab, position, new Quaternion (rot.x, rot.y, Random.rotation.z, rot.w));
		}
	}
}
