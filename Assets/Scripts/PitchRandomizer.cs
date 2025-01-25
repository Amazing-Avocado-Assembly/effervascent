using UnityEngine;

public class PitchRandomizer : MonoBehaviour
{
    private void Awake()
    {
        var audio = GetComponent<AudioSource>();
        audio.pitch = Random.Range(1f - 0.15f, 1 + 0.15f);
        audio.time = audio.clip.length * Random.value;
    }
}
