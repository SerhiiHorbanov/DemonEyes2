using UnityEngine;

namespace LevelTimeline
{
	[CreateAssetMenu(fileName = "New Empty LevelEvent", menuName = "ScriptableObjects/LevelEvents/Empty")]
	public class EmptyLevelEvent : LevelEvent
	{
		public override void Execute()
		{ }
	}
}
