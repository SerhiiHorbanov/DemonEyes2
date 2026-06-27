using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Centipede
{
	public class CentipedeHead : MonoBehaviour
	{
		private List<CentipedeSegmentRecord> _records = new();
		[SerializeField] private List<CentipedeSegment> _Segments;
		[SerializeField] private float _Speed;
		[SerializeField] private int _ReservedRecordsCount;
		private Vector2 _direction;
		
		private int _currentRecordIdx; 
		public float _Length;
		
		private void Awake()
		{
			InitializeRecords();
		}
		
		private void InitializeRecords()
		{
			float totalLength = _Length * 0.5f;
			
			foreach (CentipedeSegment segment in _Segments)
				totalLength += segment._Length;
			
			int recordsCount = Mathf.RoundToInt(totalLength / _Speed / Time.fixedDeltaTime) + _ReservedRecordsCount;

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
			
			transform.position += (Vector3)(_direction * (_Speed * Time.fixedDeltaTime));
			
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
			=> Mathf.RoundToInt(length / _Speed / Time.fixedDeltaTime);

		public void ReserveSegmentsMemory(int count)
			=> _Segments.Capacity = count;

		public void AddSegment(CentipedeSegment segment)
			=> _Segments.Add(segment);

		public void ClearAndDestroySegments()
		{
			foreach (CentipedeSegment segment in _Segments)
			{
				if (segment != null)
					DestroyImmediate(segment.gameObject);
			}
			_Segments.Clear();
		}
	}
}
