using UnityEngine;
using System.Collections;

namespace TopDownTurrents
{
	[RequireComponent (typeof(Animator))]
	public class WalkingAnimationController : MonoBehaviour
	{
		private Animator _animator;

		private int speedHash = Animator.StringToHash ("speed");
		
		private Vector2 lastPosition;
		
		void Awake ()
		{
			_animator = GetComponent<Animator> ();

		}
		
		void Start ()
		{
			lastPosition = transform.position;
		}

		void FixedUpdate ()
		{

			var velocity = (((Vector2)transform.position - lastPosition)).magnitude;
			var currentSpeed = Mathf.Abs (velocity) / Time.deltaTime;
			_animator.SetFloat (speedHash, currentSpeed);
			
			//_animator.speed = velocity;
			lastPosition = transform.position;

		}

	}
}
