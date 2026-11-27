using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ArenaArea : MonoBehaviour
{
	public static ArenaArea Instance { get; private set; }
	
	[FormerlySerializedAs("_Radius")] [SerializeField] private float _radius;

	private void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError("ArenaArea already exists!");
			return;
		}
		Instance = this;
	}

	public float GetSignedDistanceTo(Vector2 position, out Vector2 relativeArenaPosition)
	{
		relativeArenaPosition = (Vector2)transform.position - position;
		float distanceToCenter = relativeArenaPosition.magnitude;
		return distanceToCenter - _radius;
	}
	
	public Vector2 GetRandomEdgePosition()
	{
		float angle = Random.value * Mathf.PI * 2;
		Vector2 randomPositionOnUnitCircle = new(Mathf.Cos(angle), Mathf.Sin(angle));
		return MapFromUnitCircleToWorld(randomPositionOnUnitCircle);
	}
	
	public Vector2 GetRandomPositionInside()
		=> MapFromUnitCircleToWorld(Random.insideUnitCircle);
	
	public Vector2 MapFromUnitCircleToWorld(Vector2 input)
		=> input * _radius + (Vector2)transform.position;

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, _radius);
	}

	private void OnDestroy()
	{
		if (Instance == this)
			Instance = null;
	}
}
