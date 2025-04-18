using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound_Manager : MonoBehaviour
{
    public AudioClip[] soundClips; // Array to hold sound clips
    private AudioSource audioSource; // Reference to the AudioSource component

    public float fadeDuration = 1f; // Time to fade in/out
    public float maxVolume = 1f; // Maximum volume for the audio source
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to this GameObject
        StartCoroutine(playListSound()); // Play a random sound at the start
    }

    IEnumerator playListSound()
    {
        for(int i = 0; i < soundClips.Length; i++)
        {   
            yield return StartCoroutine(FadeIn(audioSource, fadeDuration)); // fade in
            audioSource.clip = soundClips[i];
            audioSource.Play();

            yield return new WaitForSeconds(soundClips[i].length); // wait for the sound to finish playing
            yield return StartCoroutine(FadeOut(audioSource, fadeDuration)); // fade out
        }   
        
    }
    IEnumerator FadeIn(AudioSource source, float duration)
    {
        source.volume = 0f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, maxVolume, timer / duration);
            yield return null;
        }
        
        source.volume = maxVolume; // eusure the volume is set to max
    }

    IEnumerator FadeOut(AudioSource source, float duration)
    {
        float startVolume = source.volume;
        float timer = 0f;
        Debug.Log("FadeOut: " + startVolume + " " + duration);
        while (timer < duration)
        {
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, timer / duration);
            yield return null;
        }
        
        source.volume = 0f; // eusure the volume is set to 0
        source.Stop(); // stop the audio
    }
    
}
