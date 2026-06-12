using System;
using UnityEngine;

namespace Characters.Squids
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Squid : MonoBehaviour
	{
		[NonSerialized] public float TargetRotationDeg;
		
		[SerializeField] private float _Speed;
		[SerializeField] private float _RotationSpeed;
		[SerializeField] private float _MinRotationSpeed;
		
		[SerializeField] private float _BoostForce;

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

			float rotationSpeed = Mathf.Max(_MinRotationSpeed, Mathf.Abs(_RotationSpeed * difference));
			float rotate = rotationSpeed * Mathf.Sign(difference) * deltaTime;
			
			transform.Rotate(0, 0, rotate);
		}

		public void Boost()
		{
			_rigidBody.AddForce(transform.right * _BoostForce, ForceMode2D.Impulse);
		}
	}
}
