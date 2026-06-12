using System;
using UnityEngine;

namespace Tools
{
	public static class GameObjectExtensions
	{
		public static void AddListenerToOnDestroy(this GameObject gameObject, Action<GameObject> action)
		{
			DestroyEventComponent destroyEventComponent = gameObject.AddComponent<DestroyEventComponent>();
			
			destroyEventComponent.OnDestroyEvent += action;
		}

		public static void AddListenerToOnDestroy(this GameObject gameObject, Action action)
		{
			DestroyEventComponent destroyEventComponent = gameObject.AddComponent<DestroyEventComponent>();
			
			destroyEventComponent.OnDestroyParameterless += action;
		}
	}
}
