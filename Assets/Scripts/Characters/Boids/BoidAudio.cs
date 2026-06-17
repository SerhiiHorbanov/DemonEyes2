using UnityEngine;

namespace Characters.Boids
{
	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(Boid))]
	public class BoidAudio : MonoBehaviour
	{
		private AudioSource _audioSource;
		private Boid _boid;
		
		[SerializeField] private float _ReservedDistanceForAdjustments;
		[SerializeField] private float _MinSpeedForMaxPitch;
		[SerializeField] private float _MaxPitch;
		[SerializeField] private float _MinSpeedForMaxVolume;
		[SerializeField] private float _MaxVolume;
	
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

			if (distanceToTarget > cutoffDistance + _ReservedDistanceForAdjustments)
				return;

			float pitch01 = Mathf.Clamp01(_boid.Speed / _MinSpeedForMaxPitch);
			_audioSource.pitch = pitch01 * _MaxPitch;
			float volume01 = Mathf.Clamp01(_boid.Speed / _MinSpeedForMaxVolume);
			_audioSource.volume = volume01 * _MaxVolume;
		}
	}
}
