using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TopDownTurrets
{
	public class Environment : MonoBehaviour
	{

		public GameObject CellPrefab;
		public Sprite Floor;
		public Sprite Wall_N;
		public Sprite Wall_NE;
		public Sprite Wall_E;
		public Sprite Wall_SE;
		public Sprite Wall_S;
		public Sprite Wall_SW;
		public Sprite Wall_W;
		public Sprite Wall_NW;
	
		public Vector2 RoomSize = new Vector2 (15, 15);

		private GameObject container;

		private List<GameObject> floorTiles;
		public List<GameObject> FloorTiles { get { return floorTiles; } }
		public GameObject RandomFloorTile {
			get {
				return floorTiles [Random.Range (0, floorTiles.Count)];
			}
		}
		
		private static Environment _instance;
		public static Environment instance { get { return _instance; } }

		void Awake ()
		{
			_instance = this;
			container = new GameObject ("Tiles");
			floorTiles = new List<GameObject> ();

			float floorWidth = GetTileWidth (Floor);
			float floorHeight = GetTileHeight (Floor);
		
			for (int i = 0; i < RoomSize.x; i++) {
				var x = i * floorWidth;
				for (int j = 0; j < RoomSize.y; j++) {
					var y = j * floorHeight;
				
					var position = new Vector2 (x, y);
					var tileClone = (GameObject)Instantiate (CellPrefab, position, Quaternion.identity);
					tileClone.transform.SetParent (container.transform);
					
					var tileSprite = GetTileSprite (new Vector2 (i, j));
					
					if (IsFloor (tileSprite)) {
						tileClone.GetComponent<Collider2D> ().isTrigger = true;
						floorTiles.Add (tileClone);
					} else {
						tileClone.tag = "Wall";
						tileClone.layer = LayerMask.NameToLayer ("Wall");
					}
					
					tileClone.GetComponent<SpriteRenderer> ().sprite = tileSprite;
				}
			}
		}
	
		private Sprite GetTileSprite (Vector2 pos)
		{
			if (pos.x == 0 && pos.y == 0) {
				return Wall_SW;
			} else if (pos.x == RoomSize.x - 1 && pos.y == RoomSize.y - 1) {
				return Wall_NE;
			} else if (pos.x == 0 && pos.y == RoomSize.y - 1) {
				return Wall_NW;
			} else if (pos.x == RoomSize.x - 1 && pos.y == 0) {
				return Wall_SE;
			} else if (pos.x == RoomSize.x - 1) {
				return Wall_E;
			} else if (pos.y == RoomSize.y - 1) {
				return Wall_N;
			} else if (pos.x == 0) {
				return Wall_W;
			} else if (pos.y == 0) {
				return Wall_S;
			}
		
			return Floor;
		}
		
		private bool IsFloor (Sprite sprite)
		{
			return sprite == Floor;
		}
	
		private float GetTileWidth (Sprite tile)
		{
			return tile.bounds.size.x;
		}
	
		private float GetTileHeight (Sprite tile)
		{
			return tile.bounds.size.y;
		}
		
	}
}
