using UnityEngine;
using System.Collections;

namespace TopDownTurrets
{
	public class Destroy : MonoBehaviour
	{

		public void ExecuteDestroy ()
		{
			Destroy (this.gameObject);
		}
	}
}
