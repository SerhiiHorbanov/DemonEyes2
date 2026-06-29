using System;

namespace Tools
{
	public static class EventBus<T>
	{
		public static event Action<T> Event;

		public static void Invoke(T payload)
			=> Event?.Invoke(payload);

		public static void Invoke<U>() where U : T, new() 
			=> Invoke(new U());
	}
}
