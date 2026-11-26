using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Centipede
{
	public class CentipedeHead : MonoBehaviour
	{
		private List<CentipedeSegmentRecord> _records = new();
		[FormerlySerializedAs("_Segments")] [SerializeField] private List<CentipedeSegment> _segments;
		[FormerlySerializedAs("_Speed")] [SerializeField] private float _speed;
		[FormerlySerializedAs("_ReservedRecordsCount")] [SerializeField] private int _reservedRecordsCount;
		private Vector2 _direction;
		
		private int _currentRecordIdx; 
		[FormerlySerializedAs("_Length")] public float Length;
		
		private void Awake()
		{
			InitializeRecords();
		}
		
		private void InitializeRecords()
		{
			float totalLength = Length * 0.5f;
			
			foreach (CentipedeSegment segment in _segments)
				totalLength += segment.Length;
			
			int recordsCount = Mathf.RoundToInt(totalLength / _speed / Time.fixedDeltaTime) + _reservedRecordsCount;

			_records.Capacity = recordsCount;
			
			_currentRecordIdx = _records.Count - 1;
			for (int i = 0; i < recordsCount; i++)
			{
				CentipedeSegmentRecord record = new(transform.position, transform.rotation);
				_records.Add(record);
			}
		}

		private void FixedUpdate()
		{
			_currentRecordIdx -= 1;
			if (_currentRecordIdx < 0)
				_currentRecordIdx = _records.Count - 1;
			
			CentipedeSegmentRecord record = new(transform.position, transform.rotation);
			_records[_currentRecordIdx] = record;
			
			transform.position += (Vector3)(_direction * (_speed * Time.fixedDeltaTime));
			
			// Temporary walking in circles behavior. It's for debugging purposes
			float angleOfDirectionRad = Mathf.Atan2(_direction.y, _direction.x);
			float newAngleOfDirectionRad = angleOfDirectionRad + Time.fixedDeltaTime;
			_direction = new(Mathf.Cos(newAngleOfDirectionRad), Mathf.Sin(newAngleOfDirectionRad));
			transform.eulerAngles = new(0, 0, angleOfDirectionRad * Mathf.Rad2Deg);
		}

		public CentipedeSegmentRecord GetRecordForDelay(int delay)
		{
			int idx = (_currentRecordIdx + delay) % _records.Count;
			return _records[idx];
		}
		
		public int CalculateDelayForSegmentOnLength(float length)
			=> Mathf.RoundToInt(length / _speed / Time.fixedDeltaTime);

		public void ReserveSegmentsMemory(int count)
			=> _segments.Capacity = count;

		public void AddSegment(CentipedeSegment segment)
			=> _segments.Add(segment);

		public void ClearAndDestroySegments()
		{
			foreach (CentipedeSegment segment in _segments)
			{
				if (segment != null)
					DestroyImmediate(segment.gameObject);
			}
			_segments.Clear();
		}
	}
}
