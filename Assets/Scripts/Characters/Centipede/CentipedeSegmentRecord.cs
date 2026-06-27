using UnityEngine;

namespace Characters.Centipede
{
	public struct CentipedeSegmentRecord
	{
		public Vector2 Position;
		public Quaternion Rotation;
		
		public CentipedeSegmentRecord(Vector2 position, Quaternion rotation)
		{
			Position = position;
			Rotation = rotation;
		}
	}
}
