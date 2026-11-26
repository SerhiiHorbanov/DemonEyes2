using UnityEngine;
using UnityEngine.Serialization;

namespace Tools
{
    public class AudioSourcePool : MonoBehaviour
    {
        [FormerlySerializedAs("_Sources")] [SerializeField] public AudioSource[] Sources;

        public bool PlayEmptySource()
        {
            foreach (AudioSource source in Sources)
            {
                if (source.isPlaying)
                    continue;
                
                source.Play();
                return true;
            }
            
            Debug.LogWarning("No empty audio source found. Consider increasing pool size.");
            return false;
        }
    }
}
