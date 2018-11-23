using UnityEngine;
using System.Collections;

namespace TopDownTurrents
{
	[RequireComponent (typeof(LineRenderer))]
	public class LaserSight : MonoBehaviour
	{

		public float Range = 5f;
	
		private LayerMask mask;
		private LineRenderer lineRenderer;

		public void Awake ()
		{
			mask = ~(1 << LayerMask.NameToLayer ("Default")); // Anything but default mask.
		
			lineRenderer = GetComponent<LineRenderer> ();
		
			lineRenderer.SetPosition (1, new Vector3 (0, Range, 0));
		}

		void Update ()
		{
			if (lineRenderer.enabled) {
				var hit = Physics2D.Raycast (transform.position, transform.up, Range, mask);
		
		
				if (hit.collider) {
					lineRenderer.SetPosition (1, new Vector3 (0, hit.distance, 0));
				} else {
					lineRenderer.SetPosition (1, new Vector3 (0, Range, 0));
				}
			}
		
		}

	}
}
