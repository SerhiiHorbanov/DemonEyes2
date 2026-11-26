using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Squids
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Squid : MonoBehaviour
	{
		[NonSerialized] public float TargetRotationDeg;
		
		[FormerlySerializedAs("_Speed")] [SerializeField] private float _speed;
		[FormerlySerializedAs("_RotationSpeed")] [SerializeField] private float _rotationSpeed;
		[FormerlySerializedAs("_MinRotationSpeed")] [SerializeField] private float _minRotationSpeed;
		
		[FormerlySerializedAs("_BoostForce")] [SerializeField] private float _boostForce;

		private Rigidbody2D _rigidBody;
		public bool IsRotatedTowardsTarget 
			=> Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, TargetRotationDeg)) < 1f;

		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
		}
		
		private void FixedUpdate()
		{
			float deltaTime = Time.fixedDeltaTime;

			RotateTowardsTarget(deltaTime);
		}

		private void RotateTowardsTarget(float deltaTime)
		{
			float difference = Mathf.DeltaAngle(transform.eulerAngles.z, TargetRotationDeg);

			float rotationSpeed = Mathf.Max(_minRotationSpeed, Mathf.Abs(_rotationSpeed * difference));
			float rotate = rotationSpeed * Mathf.Sign(difference) * deltaTime;
			
			transform.Rotate(0, 0, rotate);
		}

		public void Boost()
		{
			_rigidBody.AddForce(transform.right * _boostForce, ForceMode2D.Impulse);
		}
	}
}
