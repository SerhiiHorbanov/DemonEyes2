using UnityEngine;

public class ArenaArea : MonoBehaviour
{
	[SerializeField] private float _Radius;

	public float GetSignedDistanceTo(Vector2 position, out Vector2 relativeArenaPosition)
	{
		relativeArenaPosition = (Vector2)transform.position - position;
		float distanceToCenter = relativeArenaPosition.magnitude;
		return distanceToCenter - _Radius;
	}
	
	public Vector2 GetRandomEdgePosition()
	{
		float angle = Random.value * Mathf.PI * 2;
		Vector2 randomPositionOnUnitCircle = new(Mathf.Cos(angle), Mathf.Sin(angle));
		return MapFromUnitCircleToWorld(randomPositionOnUnitCircle);
	}
	
	public Vector2 GetRandomPositionInside()
		=> MapFromUnitCircleToWorld(Random.insideUnitCircle);
	
	private Vector2 MapFromUnitCircleToWorld(Vector2 input)
		=> input * _Radius + (Vector2)transform.position;

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, _Radius);
	}
}
