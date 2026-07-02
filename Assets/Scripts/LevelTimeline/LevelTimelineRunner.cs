using System.Collections.Generic;
using UnityEngine;

namespace LevelTimeline
{
	public class LevelTimelineRunner : MonoBehaviour
	{
		[SerializeField] private LevelTimeline _Timeline;
		[SerializeField] private float _TimeScale = 1;
		[SerializeField] private bool _IsPaused;
		[SerializeField] private bool _LogEventExecutions;

		private float _time;
		private int _nextEventToExpire;
		
		private void Start()
		{
			_Timeline.EnsureOrdered();
		}
		
		private void FixedUpdate()
		{
			_time += Time.fixedDeltaTime * _TimeScale;
			ExecuteExpiredEvents();
		}

		private void ExecuteExpiredEvents()
		{
			List<TimedLevelEvent> events = _Timeline._Events;
		
			for (int i = _nextEventToExpire; events[i]._TimeSeconds < _time && i < events.Count; i++)
			{
				ExecuteEvent(i);
				_nextEventToExpire = i + 1;
			}
		}
	
		private void ExecuteEvent(int idx)
		{
			LevelEvent @event = _Timeline._Events[idx]._Event;

			if (@event == null)
			{
				Debug.LogError($"Timeline event at index {idx} is null");
				return;
			}
			
			if (_LogEventExecutions)
				Debug.Log($"Executing timeline event of type {@event.GetType()} with index {idx}. At time: {_time}");

			@event.Execute();
		}
	}
}