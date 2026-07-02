using System.Collections.Generic;
using UnityEngine;

namespace LevelTimeline
{
	[CreateAssetMenu(fileName = "New Level Timeline", menuName = "ScriptableObjects/Level Timeline", order = 1)]
	public class LevelTimeline : ScriptableObject
	{
		[SerializeField] public List<TimedLevelEvent> _Events;

		private void OnValidate()
		{
			EnsureOrdered();
		}
		
		public void EnsureOrdered()
		{
			if (!IsEventsOrdered())
				_Events.Sort();
		}

		private bool IsEventsOrdered()
		{
			for (int i = 0; i < _Events.Count - 1; i++)
			{
				if (_Events[i].CompareTo(_Events[i + 1]) < 0)
					return false;
			}
			return true;
		}
	}
}