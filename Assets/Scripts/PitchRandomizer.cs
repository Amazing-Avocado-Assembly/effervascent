using UnityEngine;

public class PitchRandomizer : MonoBehaviour
{
    private void Awake()
    {
        var audio = GetComponent<AudioSource>();
        audio.pitch += Random.value * 0.3f - 0.15f;
        audio.time = audio.clip.length * Random.value;
    }
}
