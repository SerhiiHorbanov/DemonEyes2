using System;

namespace Tools
{
	public static class EventBus<T>
	{
		public static event Action<T> Event;

		public static void Invoke(T payload)
			=> Event?.Invoke(payload);

		public static void Invoke<TU>() where TU : T, new() 
			=> Invoke(new TU());
	}
}
