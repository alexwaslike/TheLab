using UnityEngine;

public class SoundController : MonoBehaviour {

    public AudioSource MainAudioSource;
    private float _defaultPitch;
    
    public bool PlayMenuSong = false;

    public AudioClip ButtonClicked;

    void Start()
    {
        _defaultPitch = MainAudioSource.pitch;
        if (PlayMenuSong) {
            MainAudioSource.time = 21;
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
