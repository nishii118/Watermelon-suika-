using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Start()
    {
        //musicSounds = 
        PlayMusic(GetRandomeMusic());
    }

    private void OnEnable()
    {
        Messenger.AddListener<string>(EventKey.OnToggleSound, ToggleState);
        Messenger.AddListener<string>(EventKey.DROPFRUITSOUND, PlaySFX);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<string>(EventKey.OnToggleSound, ToggleState);
        Messenger.RemoveListener<string>(EventKey.DROPFRUITSOUND, PlaySFX);
    }

    public bool GetToggleState(String key) {
        return PlayerPrefs.GetInt(key, 1) == 1 ? true : false;    
    }

    public void ToggleState(String key) {
        int currentState = PlayerPrefs.GetInt(key, 1);

        int newState = currentState == 1 ? 0 : 1;
        PlayerPrefs.SetInt(key, newState);

        if (key == "Music") {
            if(newState == 0) {
                StopMusic();
            } else {
                PlayMusic(GetCurrentMusic());
            }
        } 
    }

    public void PlayMusic(String name) {
        if(!GetToggleState("Music")) return;

        Sound s = Array.Find(musicSounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        } else {
            musicSource.clip = s.clip;
            musicSource.loop = true;

            musicSource.Play();
        }
    }

    private String GetRandomeMusic() {
        int randomIndex = UnityEngine.Random.Range(0, musicSounds.Length);
        musicSource.clip = musicSounds[randomIndex].clip;
        return musicSounds[randomIndex].name;
    }
    public void StopMusic() {
        musicSource.Stop();
    }

    public void Vibrate() {
        if(!GetToggleState("Vibration")) return;
        Handheld.Vibrate();
    }

    public void PlaySFX(string name) {
        if(!GetToggleState("SFX")) return;

        Sound s = Array.Find(sfxSounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        } else {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public String GetCurrentMusic() {
        Debug.Log("music name: " + musicSource.clip.name);
        return musicSource.clip.name;
    }
}
