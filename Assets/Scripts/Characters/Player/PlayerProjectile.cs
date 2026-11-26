using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Player
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerProjectile : MonoBehaviour
	{
		[FormerlySerializedAs("_Speed")] [SerializeField] private float _speed;
		private Rigidbody2D _rigidBody;
		[FormerlySerializedAs("_Lifetime")] [SerializeField] private float _lifetime;
		
		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
			_rigidBody.linearVelocity = transform.right * _speed;
			StartCoroutine(DestroyAfterLifetime());
		}

		private IEnumerator DestroyAfterLifetime()
		{
			yield return new WaitForSeconds(_lifetime);
			Destroy(gameObject);
		}
	}
}
