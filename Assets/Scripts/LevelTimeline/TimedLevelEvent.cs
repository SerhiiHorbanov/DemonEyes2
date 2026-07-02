using System;

namespace LevelTimeline
{
	[Serializable]
	public struct TimedLevelEvent : IComparable<TimedLevelEvent>
	{
		public float _TimeSeconds;
		public LevelEvent _Event;

		public int CompareTo(TimedLevelEvent other)
			=> _TimeSeconds.CompareTo(other._TimeSeconds);
	}
}