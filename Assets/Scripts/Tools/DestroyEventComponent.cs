using System;
using UnityEngine;

namespace Tools
{
	public class DestroyEventComponent : MonoBehaviour
	{
		public Action<GameObject> OnDestroyEvent;
		public Action OnDestroyParameterless;
		
		private void OnDestroy()
		{
			OnDestroyEvent?.Invoke(gameObject);
			OnDestroyParameterless?.Invoke();
		}
		
		public void DestroyThis()
			=> Destroy(gameObject);
	}
}
