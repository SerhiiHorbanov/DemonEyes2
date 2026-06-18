using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Characters.Boids
{
	public class Boid : MonoBehaviour
	{
		private readonly static List<Boid> Boids = new();
		
		[SerializeField] private Vector2 _Velocity;

		[SerializeField] private float _MaxSignedDistanceToArena;
		[SerializeField] private float _ArenaPullForce;
		[SerializeField] public ArenaArea _Arena;
		
		[SerializeField] private float _TargetingWeight;
		[SerializeField] public Transform _Target;
		
		[SerializeField] private float _BoidForcesRadius;
		[SerializeField] private float _CohesionWeight;
		[SerializeField] private float _AlignmentWeight;
		[SerializeField] private float _SeparationWeight;

		[SerializeField] private float _MaxSpeed;
		[SerializeField] private float _TargetSpeed;
		[SerializeField] private float _SpeedAdjustmentForce;
		
		private float ForcesRadiusSquared => _BoidForcesRadius * _BoidForcesRadius;
		public float DistanceToTarget { get; private set; }
		public float Speed { get; private set; }

		private void Awake()
		{
			Boids.Add(this);
		}

		private void OnDestroy()
		{
			Boids.RemoveSwapBack(this);
		}

		private void FixedUpdate()
		{
			float deltaTime = Time.fixedDeltaTime;
			
			if (_Target != null)
				ApplyTargeting(deltaTime);
			ApplyBoidInteractions(deltaTime);
			
			if (ShouldApplyArenaPullForce(out Vector2 relativeArenaPosition))
				ApplyArenaPullForce(deltaTime, relativeArenaPosition);
			
			UpdateSpeed(deltaTime);
			
			transform.position += (Vector3)(_Velocity * deltaTime);
			UpdateRotation();
		}

		private void ApplyTargeting(float fixedDeltaTime)
		{
			Vector2 target = _Target.position;
			Vector2 position = transform.position;
			
			Vector2 relativePosition = target - position;
			DistanceToTarget = relativePosition.magnitude;
			Vector2 directionToTarget = relativePosition / DistanceToTarget;
			_Velocity += directionToTarget * (_TargetingWeight * fixedDeltaTime);
		}

		private void UpdateRotation()
		{
			float desiredRotation = Mathf.Atan2(_Velocity.y, _Velocity.x) * Mathf.Rad2Deg;
			transform.eulerAngles = new(0, 0, desiredRotation);
		}

		private void ApplyBoidInteractions(float deltaTime)
		{
			int boidsInRadius = 0;
			
			Vector2 positionsSum = Vector2.zero;
			Vector2 separationForce = Vector2.zero;
			Vector2 velocitiesSum = Vector2.zero;
			
			foreach (Boid boid in Boids)
			{
				Vector2 relativePosition = boid.transform.position - transform.position;
				float distanceSquared = relativePosition.sqrMagnitude;
				
				if (distanceSquared > ForcesRadiusSquared)
					continue;
				if (boid == this)
					continue;
				if (distanceSquared == 0)
					continue;

				boidsInRadius++;
				
				positionsSum += (Vector2)boid.transform.position;
				velocitiesSum += boid._Velocity;
				separationForce += -relativePosition / distanceSquared * (_SeparationWeight * deltaTime);
			}

			if (boidsInRadius == 0)
				return;
			
			Vector2 averagePosition = positionsSum / boidsInRadius;
			Vector2 averageVelocity = velocitiesSum / boidsInRadius;

			Vector2 directionToAveragePosition = (averagePosition - (Vector2)transform.position).normalized;
			
			Vector2 cohesionForce = directionToAveragePosition * (_CohesionWeight * deltaTime);
			Vector2 alignmentForce = (averageVelocity - _Velocity) * (_AlignmentWeight * deltaTime);

			_Velocity += cohesionForce + alignmentForce + separationForce;
		}

		private void UpdateSpeed(float deltaTime)
		{
			Speed = _Velocity.magnitude;
			if (Speed < _MaxSpeed)
				return;
				
			if (Speed > _MaxSpeed)
				Speed = _MaxSpeed;

			if (Mathf.Abs(Speed - _TargetSpeed) < _SpeedAdjustmentForce * deltaTime)
			{
				_Velocity = _Velocity.normalized * _TargetSpeed;
				return;
			}

			Speed += _SpeedAdjustmentForce * Mathf.Sign(_TargetSpeed - Speed) * deltaTime;
			_Velocity = _Velocity.normalized * Speed;
		}
		
		private bool ShouldApplyArenaPullForce(out Vector2 relativeArenaPosition)
		{
			float distance = _Arena.GetSignedDistanceTo(transform.position, out relativeArenaPosition);

			return distance > _MaxSignedDistanceToArena;
		}
		
		private void ApplyArenaPullForce(float deltaTime, Vector2 relativeArenaPosition)
		{
			Vector2 direction = relativeArenaPosition.normalized;
			Vector2 force = direction * _ArenaPullForce;
			_Velocity += force * deltaTime;
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, _BoidForcesRadius);
		}
	}
}
