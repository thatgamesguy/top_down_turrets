using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{

	public class EnableParentMovement : MonoBehaviour
	{
		private Enemy movement;

		public void EnableMovement ()
		{
			if (!movement)
				movement = transform.parent.GetComponent<Enemy> ();

			movement.CanMove = true;
		}
	}
}
