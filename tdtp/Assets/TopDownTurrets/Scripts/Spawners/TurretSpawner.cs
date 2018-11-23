using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TopDownTurrets
{
	public class TurretSpawner : MonoBehaviour
	{

		public GameObject[] Turrets;

		private List<GameObject> usedTiles;
		
		void Awake ()
		{
			usedTiles = new List<GameObject> ();
		}


		void Update ()
		{
			if (Input.GetKeyUp (KeyCode.Alpha1)) {
				SpawnTurret (0);
			} else if (Input.GetKeyUp (KeyCode.Alpha2)) {
				SpawnTurret (1);
			} else if (Input.GetKeyUp (KeyCode.Alpha3)) {
				SpawnTurret (2);
			} else if (Input.GetKeyUp (KeyCode.Alpha4)) {
				SpawnTurret (3);
			} else if (Input.GetKeyUp (KeyCode.Alpha5)) {
				SpawnTurret (4);
			}
		}
	
		private void SpawnTurret (int index)
		{
			if (usedTiles.Count == Environment.instance.FloorTiles.Count)
				return;
					
			GameObject tile = null;
			
			do {
				tile = Environment.instance.RandomFloorTile;
			} while (usedTiles.Contains (tile));
			
			if (tile != null) {
				usedTiles.Add (tile);
			
			
				var position = tile.transform.position;
				var rot = Quaternion.identity;
				Instantiate (Turrets [index], position, new Quaternion (rot.x, rot.y, Random.rotation.z, rot.w));
			}
		}
	}
}
