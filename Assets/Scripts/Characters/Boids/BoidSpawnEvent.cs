using LevelTimeline;
using Tools;
using UnityEngine;

namespace Characters.Boids
{
	[CreateAssetMenu(fileName = "New Boid Spawn LevelEvent", menuName = "ScriptableObjects/LevelEvents/Boid Spawn")]
	public class BoidSpawnEvent : LevelEvent
	{
		public override void Execute()
			=> EventBus<BoidSpawnEvent>.Invoke(this);
	}
}