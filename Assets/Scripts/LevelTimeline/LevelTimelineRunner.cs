using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelTimeline
{
	public class LevelTimelineRunner : MonoBehaviour
	{
		[FormerlySerializedAs("_Timeline")] [SerializeField] private LevelTimeline _timeline;
		[FormerlySerializedAs("_TimeScale")] [SerializeField] private float _timeScale = 1;
		[FormerlySerializedAs("_IsPaused")] [SerializeField] private bool _isPaused;
		[FormerlySerializedAs("_LogEventExecutions")] [SerializeField] private bool _logEventExecutions;

		private float _time;
		private int _nextEventToExpire;
		
		private void Start()
		{
			_timeline.EnsureOrdered();
		}
		
		private void FixedUpdate()
		{
			_time += Time.fixedDeltaTime * _timeScale;
			ExecuteExpiredEvents();
		}

		private void ExecuteExpiredEvents()
		{
			List<TimedLevelEvent> events = _timeline.Events;
		
			for (int i = _nextEventToExpire; events[i].TimeSeconds < _time && i < events.Count; i++)
			{
				ExecuteEvent(i);
				_nextEventToExpire = i + 1;
			}
		}
	
		private void ExecuteEvent(int idx)
		{
			LevelEvent @event = _timeline.Events[idx].Event;

			if (@event == null)
			{
				Debug.LogError($"Timeline event at index {idx} is null");
				return;
			}
			
			if (_logEventExecutions)
				Debug.Log($"Executing timeline event of type {@event.GetType()} with index {idx}. At time: {_time}");

			@event.Execute();
		}
	}
}