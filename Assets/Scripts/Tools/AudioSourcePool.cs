using UnityEngine;

namespace Tools
{
    public class AudioSourcePool : MonoBehaviour
    {
        [SerializeField] public AudioSource[] _Sources;

        public bool PlayEmptySource()
        {
            foreach (AudioSource source in _Sources)
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
