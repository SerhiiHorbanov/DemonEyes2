using System.Collections;
using UnityEngine;

namespace Tools
{
	public static class AudioSourceTools
	{
		public static IEnumerator FadeOut(AudioSource audioSource, float duration)
		{
			float startVolume = audioSource.volume;

			while (audioSource.volume > 0)
			{
				audioSource.volume -= startVolume * Time.deltaTime / duration;
				yield return null;
			}

			audioSource.volume = 0;
			audioSource.Stop();
		}

		public static void PlayAtRandomPosition(this AudioSource audioSource)
		{
			audioSource.time = Random.Range(0, audioSource.clip.length);
			audioSource.Play();
		}
	}
}
