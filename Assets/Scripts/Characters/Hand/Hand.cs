using Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Hand
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Hand : MonoBehaviour
	{
		private Vector2 _targetPosition;
		private Rigidbody2D _rigidBody;
		[FormerlySerializedAs("_ForceMultiplier")] [SerializeField] private float _forceMultiplier;
		[FormerlySerializedAs("_MaxSpeedMultiplier")] [SerializeField] private float _maxSpeedMultiplier;

		[FormerlySerializedAs("_SlowMoveSpeed")] [SerializeField] private float _slowMoveSpeed;
		
		[FormerlySerializedAs("_MoveAudio")] [SerializeField] private AudioSource _moveAudio;
		[FormerlySerializedAs("_AudioFadeTime")] [SerializeField] private float _audioFadeTime;
		[FormerlySerializedAs("_DefaultVolume")]
		[Range(0, 1)]
		[SerializeField] private float _defaultVolume;
		
		private bool _moveSlowly;
		
		public void MoveTo(Vector2 position)
		{
			_targetPosition = position;
			
			if (!_moveSlowly)
				return;
			
			_moveSlowly = false;
			_moveAudio.volume = _defaultVolume;
			_moveAudio.PlayAtRandomPosition();
		}
		public void MoveSlowlyTo(Vector2 position)
		{
			_targetPosition = position;
			if (_moveSlowly)
				return;
			
			_moveSlowly = true;
			StartCoroutine(AudioSourceTools.FadeOut(_moveAudio, _audioFadeTime));
		}
		
		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
		}
		
		private void FixedUpdate()
		{
			Vector2 deltaPosition = _targetPosition - (Vector2)transform.position;

			if (_moveSlowly)
			{
				_rigidBody.linearVelocity = deltaPosition.normalized * _slowMoveSpeed;
			}
			_rigidBody.AddForce(deltaPosition * _forceMultiplier, ForceMode2D.Force);
			float maxSpeed = deltaPosition.magnitude * 2 + 1;
			if (_rigidBody.linearVelocity.magnitude > maxSpeed)
			{
				_rigidBody.linearVelocity = deltaPosition.normalized * maxSpeed;
			}
		}
	}
}
