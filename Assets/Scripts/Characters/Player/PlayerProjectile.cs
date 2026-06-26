using System.Collections;
using UnityEngine;

namespace Characters.Player
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerProjectile : MonoBehaviour
	{
		[SerializeField] private float _Speed;
		private Rigidbody2D _rigidBody;
		[SerializeField] private float _Lifetime;
		
		private void Awake()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
			_rigidBody.linearVelocity = transform.right * _Speed;
			StartCoroutine(DestroyAfterLifetime());
		}

		private IEnumerator DestroyAfterLifetime()
		{
			yield return new WaitForSeconds(_Lifetime);
			Destroy(gameObject);
		}
	}
}
