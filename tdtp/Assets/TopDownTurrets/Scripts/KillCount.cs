using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TopDownTurrets
{
	[RequireComponent (typeof(Text))]
	public class KillCount : MonoBehaviour
	{
		private Text text;
		private int currentKillCount = 0;
		private static readonly string TEXT_PREPEND = "Kills: ";
		
		private static KillCount _instance;
		public static KillCount instance { get { return _instance; } }

		void Awake ()
		{
			_instance = this;
		}

		void Start ()
		{
			text = GetComponent<Text> ();
			text.text = TEXT_PREPEND + currentKillCount.ToString ();
		}
	
		public void EnemyKilled ()
		{
			currentKillCount++;
			text.text = TEXT_PREPEND + currentKillCount.ToString ();
	
		}
	}
}
