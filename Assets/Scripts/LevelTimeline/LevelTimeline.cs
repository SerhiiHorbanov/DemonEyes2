using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelTimeline
{
	[CreateAssetMenu(fileName = "New Level Timeline", menuName = "ScriptableObjects/Level Timeline", order = 1)]
	public class LevelTimeline : ScriptableObject
	{
		[FormerlySerializedAs("_Events")] [SerializeField] public List<TimedLevelEvent> Events;

		private void OnValidate()
		{
			EnsureOrdered();
		}
		
		public void EnsureOrdered()
		{
			if (!IsEventsOrdered())
				Events.Sort();
		}

		private bool IsEventsOrdered()
		{
			for (int i = 0; i < Events.Count - 1; i++)
			{
				if (Events[i].CompareTo(Events[i + 1]) < 0)
					return false;
			}
			return true;
		}
	}
}