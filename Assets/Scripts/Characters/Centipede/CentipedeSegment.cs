using System;
using UnityEngine;

namespace Characters.Centipede
{
	public class CentipedeSegment : MonoBehaviour
	{
		public float _Length;
		public int _Delay;
		public CentipedeHead _Head;

		private void FixedUpdate()
		{
			CentipedeSegmentRecord record = _Head.GetRecordForDelay(_Delay);
			ApplyRecord(record);
		}

		private void ApplyRecord(CentipedeSegmentRecord record)
		{
			transform.position = record.Position;
			transform.rotation = record.Rotation;
		}
	}
}
