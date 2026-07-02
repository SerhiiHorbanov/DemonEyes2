using UnityEngine;

namespace LevelTimeline
{
	public abstract class LevelEvent : ScriptableObject
	{
		public abstract void Execute();
	}
}