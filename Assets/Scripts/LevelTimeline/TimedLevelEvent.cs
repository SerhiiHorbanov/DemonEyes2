using System;
using UnityEngine.Serialization;

namespace LevelTimeline
{
	[Serializable]
	public struct TimedLevelEvent : IComparable<TimedLevelEvent>
	{
		[FormerlySerializedAs("_TimeSeconds")] public float TimeSeconds;
		[FormerlySerializedAs("_Event")] public LevelEvent Event;

		public int CompareTo(TimedLevelEvent other)
			=> TimeSeconds.CompareTo(other.TimeSeconds);
	}
}