using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Boids
{
	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(Boid))]
	public class BoidAudio : MonoBehaviour
	{
		private AudioSource _audioSource;
		private Boid _boid;
		
		[FormerlySerializedAs("_ReservedDistanceForAdjustments")] [SerializeField] private float _reservedDistanceForAdjustments;
		[FormerlySerializedAs("_MinSpeedForMaxPitch")] [SerializeField] private float _minSpeedForMaxPitch;
		[FormerlySerializedAs("_MaxPitch")] [SerializeField] private float _maxPitch;
		[FormerlySerializedAs("_MinSpeedForMaxVolume")] [SerializeField] private float _minSpeedForMaxVolume;
		[FormerlySerializedAs("_MaxVolume")] [SerializeField] private float _maxVolume;
	
		private void Awake()
		{
			_audioSource = GetComponent<AudioSource>();
			_boid = GetComponent<Boid>();
			
			_audioSource.time = Random.Range(0f, _audioSource.clip.length);
		}

		private void FixedUpdate()
		{
			float distanceToTarget = _boid.DistanceToTarget;
			float cutoffDistance = _audioSource.maxDistance;

			if (distanceToTarget > cutoffDistance + _reservedDistanceForAdjustments)
				return;

			float pitch01 = Mathf.Clamp01(_boid.Speed / _minSpeedForMaxPitch);
			_audioSource.pitch = pitch01 * _maxPitch;
			float volume01 = Mathf.Clamp01(_boid.Speed / _minSpeedForMaxVolume);
			_audioSource.volume = volume01 * _maxVolume;
		}
	}
}
