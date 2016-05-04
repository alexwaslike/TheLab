using UnityEngine;

public class SoundController : MonoBehaviour {

    public AudioSource MainAudioSource;
    
    public bool PlayMenuSong = false;
    public bool PlayAmbientMusic = false;

    public AudioClip[] AmbientMusic;

    public AudioClip ButtonClicked;

    void Start()
    {
        if (PlayMenuSong) {
            MainAudioSource.time = 21;
            MainAudioSource.Play();
        } else if (PlayAmbientMusic) {
            MainAudioSource.volume = 0.7f;
            MainAudioSource.clip = AmbientMusic[Random.Range(0, AmbientMusic.Length)];
            MainAudioSource.Play();
        }
    }

    void Update()
    {
        if (!MainAudioSource.isPlaying) {
            MainAudioSource.clip = AmbientMusic[Random.Range(0, AmbientMusic.Length)];
            MainAudioSource.Play();
        }
    }
    
    public void PlayButtonClick()
    {
        MainAudioSource.pitch = GetRandomPitch();
        MainAudioSource.PlayOneShot(ButtonClicked, 1);
    }

    public float GetRandomPitch()
    {
        return Random.Range(-0.5f,1.5f);
    }
	
}
