using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Centipede
{
	public class CentipedeSegment : MonoBehaviour
	{
		[FormerlySerializedAs("_Length")] public float Length;
		[FormerlySerializedAs("_Delay")] public int Delay;
		[FormerlySerializedAs("_Head")] public CentipedeHead Head;

		private void FixedUpdate()
		{
			CentipedeSegmentRecord record = Head.GetRecordForDelay(Delay);
			ApplyRecord(record);
		}

		private void ApplyRecord(CentipedeSegmentRecord record)
		{
			transform.position = record.Position;
			transform.rotation = record.Rotation;
		}
	}
}
