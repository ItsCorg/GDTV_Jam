using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManagerr : MonoBehaviour
{
    public AudioClip backgroundMusic; // The audio clip for the background music
    public AudioClip backgroundAmbiance; // The audio clip for the background ambiance

    private AudioSource audioSourceMusic; // Reference to the AudioSource component for music
    private AudioSource audioSourceAmbiance; // Reference to the AudioSource component for ambiance

    private static BackgroundMusicManagerr instance; // Singleton instance

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of the script exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevents the object from being destroyed when loading new scenes

            // Create AudioSources for music and ambiance
            audioSourceMusic = gameObject.AddComponent<AudioSource>();
            audioSourceAmbiance = gameObject.AddComponent<AudioSource>();

            audioSourceMusic.loop = true; // Enable looping for the background music
            audioSourceMusic.clip = backgroundMusic; // Set the audio clip for the background music
            audioSourceMusic.Play(); // Start playing the background music

            audioSourceAmbiance.loop = true; // Enable looping for the background ambiance
            audioSourceAmbiance.clip = backgroundAmbiance; // Set the audio clip for the background ambiance
            audioSourceAmbiance.Play(); // Start playing the background ambiance
        }
        else
        {
            Destroy(gameObject); // Destroy any additional instances of the script
        }
    }

    private void Update()
    {
        // Adjust the volume balance between music and ambiance
        audioSourceMusic.volume = 0.1f; // Set the volume for the background music (adjust as desired)
        audioSourceAmbiance.volume = 0.3f; // Set the volume for the background ambiance (adjust as desired)
    }
}