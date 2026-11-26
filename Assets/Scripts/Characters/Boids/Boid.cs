using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Boids
{
	public class Boid : MonoBehaviour
	{
		private readonly static List<Boid> Boids = new();
		
		[FormerlySerializedAs("_Velocity")] [SerializeField] private Vector2 _velocity;

		[FormerlySerializedAs("_MaxSignedDistanceToArena")] [SerializeField] private float _maxSignedDistanceToArena;
		[FormerlySerializedAs("_ArenaPullForce")] [SerializeField] private float _arenaPullForce;
		[FormerlySerializedAs("_Arena")] [SerializeField] private ArenaArea _arena;
		
		[FormerlySerializedAs("_TargetingWeight")] [SerializeField] private float _targetingWeight;
		[FormerlySerializedAs("_Target")] [SerializeField] private Transform _target;
		
		[FormerlySerializedAs("_BoidForcesRadius")] [SerializeField] private float _boidForcesRadius;
		[FormerlySerializedAs("_CohesionWeight")] [SerializeField] private float _cohesionWeight;
		[FormerlySerializedAs("_AlignmentWeight")] [SerializeField] private float _alignmentWeight;
		[FormerlySerializedAs("_SeparationWeight")] [SerializeField] private float _separationWeight;

		[FormerlySerializedAs("_MaxSpeed")] [SerializeField] private float _maxSpeed;
		[FormerlySerializedAs("_TargetSpeed")] [SerializeField] private float _targetSpeed;
		[FormerlySerializedAs("_SpeedAdjustmentForce")] [SerializeField] private float _speedAdjustmentForce;
		
		private float ForcesRadiusSquared => _boidForcesRadius * _boidForcesRadius;
		public float DistanceToTarget { get; private set; }
		public float Speed { get; private set; }

		private void Awake()
		{
			Boids.Add(this);
		}

		public void Initialize(Transform target, ArenaArea arena)
		{
			_target = target;
			_arena = arena;
		}
		
		private void OnDestroy()
		{
			Boids.RemoveSwapBack(this);
		}

		private void FixedUpdate()
		{
			float deltaTime = Time.fixedDeltaTime;
			
			if (_target != null)
				ApplyTargeting(deltaTime);
			ApplyBoidInteractions(deltaTime);
			
			if (ShouldApplyArenaPullForce(out Vector2 relativeArenaPosition))
				ApplyArenaPullForce(deltaTime, relativeArenaPosition);
			
			UpdateSpeed(deltaTime);
			
			transform.position += (Vector3)(_velocity * deltaTime);
			UpdateRotation();
		}

		private void ApplyTargeting(float fixedDeltaTime)
		{
			Vector2 target = _target.position;
			Vector2 position = transform.position;
			
			Vector2 relativePosition = target - position;
			DistanceToTarget = relativePosition.magnitude;
			Vector2 directionToTarget = relativePosition / DistanceToTarget;
			_velocity += directionToTarget * (_targetingWeight * fixedDeltaTime);
		}

		private void UpdateRotation()
		{
			float desiredRotation = Mathf.Atan2(_velocity.y, _velocity.x) * Mathf.Rad2Deg;
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
				velocitiesSum += boid._velocity;
				separationForce += -relativePosition / distanceSquared * (_separationWeight * deltaTime);
			}

			if (boidsInRadius == 0)
				return;
			
			Vector2 averagePosition = positionsSum / boidsInRadius;
			Vector2 averageVelocity = velocitiesSum / boidsInRadius;

			Vector2 directionToAveragePosition = (averagePosition - (Vector2)transform.position).normalized;
			
			Vector2 cohesionForce = directionToAveragePosition * (_cohesionWeight * deltaTime);
			Vector2 alignmentForce = (averageVelocity - _velocity) * (_alignmentWeight * deltaTime);

			_velocity += cohesionForce + alignmentForce + separationForce;
		}

		private void UpdateSpeed(float deltaTime)
		{
			Speed = _velocity.magnitude;
			if (Speed < _maxSpeed)
				return;
				
			if (Speed > _maxSpeed)
				Speed = _maxSpeed;

			if (Mathf.Abs(Speed - _targetSpeed) < _speedAdjustmentForce * deltaTime)
			{
				_velocity = _velocity.normalized * _targetSpeed;
				return;
			}

			Speed += _speedAdjustmentForce * Mathf.Sign(_targetSpeed - Speed) * deltaTime;
			_velocity = _velocity.normalized * Speed;
		}
		
		private bool ShouldApplyArenaPullForce(out Vector2 relativeArenaPosition)
		{
			float distance = _arena.GetSignedDistanceTo(transform.position, out relativeArenaPosition);

			return distance > _maxSignedDistanceToArena;
		}
		
		private void ApplyArenaPullForce(float deltaTime, Vector2 relativeArenaPosition)
		{
			Vector2 direction = relativeArenaPosition.normalized;
			Vector2 force = direction * _arenaPullForce;
			_velocity += force * deltaTime;
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, _boidForcesRadius);
		}
	}
}
