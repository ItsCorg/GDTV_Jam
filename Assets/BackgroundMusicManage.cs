using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // The audio clip for the background music
    private AudioSource audioSource; // Reference to the AudioSource component

    private static BackgroundMusicManager instance; // Singleton instance

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of the script exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevents the object from being destroyed when loading new scenes
            audioSource = GetComponent<AudioSource>(); // Get the reference to the AudioSource component
        }
        else
        {
            Destroy(gameObject); // Destroy any additional instances of the script
        }
    }

    private void Start() 
    {
        audioSource.loop = true; // Enable looping for the background music
        audioSource.clip = backgroundMusic; // Set the audio clip for the background music
        audioSource.Play(); // Start playing the background music
    }

    private void OnEnable()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is not the main menu scene
        if (scene.buildIndex != 0)
        {
            audioSource.Stop(); // Stop playing the background music
            Destroy(gameObject); // Destroy the object as it's no longer needed in other scenes
        }
    }
}
