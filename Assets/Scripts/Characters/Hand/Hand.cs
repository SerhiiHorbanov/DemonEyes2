using Tools;
using UnityEngine;

namespace Characters.Hand
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Hand : MonoBehaviour
	{
		private Vector2 TargetPosition;
		private Rigidbody2D _rigidBody;
		[SerializeField] private float _ForceMultiplier;
		[SerializeField] private float _MaxSpeedMultiplier;

		[SerializeField] private float _SlowMoveSpeed;
		
		[SerializeField] private AudioSource _MoveAudio;
		[SerializeField] private float _AudioFadeTime;
		[Range(0, 1)]
		[SerializeField] private float _DefaultVolume;
		
		private bool _moveSlowly;
		
		public void MoveTo(Vector2 position)
		{
			TargetPosition = position;
			
			if (!_moveSlowly)
				return;
			
			_moveSlowly = false;
			_MoveAudio.volume = _DefaultVolume;
			_MoveAudio.PlayAtRandomPosition();
		}
		public void MoveSlowlyTo(Vector2 position)
		{
			TargetPosition = position;
			if (_moveSlowly)
				return;
			
			_moveSlowly = true;
			StartCoroutine(AudioSourceTools.FadeOut(_MoveAudio, _AudioFadeTime));
		}
		
		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
		}
		
		private void FixedUpdate()
		{
			Vector2 deltaPosition = TargetPosition - (Vector2)transform.position;

			if (_moveSlowly)
			{
				_rigidBody.linearVelocity = deltaPosition.normalized * _SlowMoveSpeed;
			}
			_rigidBody.AddForce(deltaPosition * _ForceMultiplier, ForceMode2D.Force);
			float maxSpeed = deltaPosition.magnitude * 2 + 1;
			if (_rigidBody.linearVelocity.magnitude > maxSpeed)
			{
				_rigidBody.linearVelocity = deltaPosition.normalized * maxSpeed;
			}
		}
	}
}
