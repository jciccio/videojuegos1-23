using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq.Expressions;
using System.ComponentModel;


[System.Serializable]
public class Sound {

    [Header("General")]
    public string Name;
    public AudioClip Clip;
    public AudioSource Source;

    [Header("Properties")]
    //[Range(0f, 1f)] public float Volume = 0.5f;
    [Range(0f, 2f)] public float Pitch = 1f;
    [Range(0f, 1f)] public float Volume = 1f; // field
    public bool Loop = false;

    public void SetSource(AudioSource _source){
        Source = _source;
        Source.clip = Clip;
    }

    public void Play(bool playOver = false){
        if(!Source.isPlaying || playOver){
            UpdateAudioSettings();
            Source.Play();
        }
    }

    public bool IsPlaying(){
        return Source.isPlaying;
    }

    public void Stop(){
         Source.Stop();
        if(Source.isPlaying){
            Source.Stop();
        }
        
    }

    public void Pause(bool state = true){
        if(state)
            Source.Pause();
        else
            Source.UnPause();
    }

    public void UpdateAudioSettings(){
        if(Source != null){
            Source.pitch = Pitch;
            Source.volume = Volume;
            Source.loop = Loop;
        }
    }

    public void UpdateAudioSettings(Dictionary<string, float> props){
        if(Source != null){
            Source.pitch = props.ContainsKey("Pitch") ? props["Pitch"] : Source.pitch;
            Source.volume = props.ContainsKey("Volume") ? props["Volume"] : Source.volume;
            Source.loop = props.ContainsKey("Loop") ? props["Loop"] == 1.0 : Source.loop;
        }
    }
    public void UpdateWithUI(){
        UpdateAudioSettings();
    }

}

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance;

   [SerializeField] private Sound [] Sfx;
   [SerializeField] private Sound [] Music;
    [SerializeField] List<Coroutine> Coroutines;
    void Awake(){
        if(instance == null){
            instance = this;
            GameObject.DontDestroyOnLoad(this);
        }
        else if(instance != this){
            Destroy(gameObject);
        }
        InitAudioArrays(Sfx, "SFX");
        InitAudioArrays(Music, "Music");
        Coroutines = new List<Coroutine>();
    }

    public void SetGlobalSfxVolume(float volume){
        SetGlobalVolume(volume, Sfx);
    }

    public void SetGlobalMusicVolume(float volume){
        SetGlobalVolume(volume, Music);
    }

    void SetGlobalVolume(float volume, Sound[] elements){
        foreach(Sound element in elements){
            element.Volume = volume;
            element.UpdateAudioSettings();
        }
    }

    public void PauseAll(){
        foreach(Sound element in Sfx){
            element.Pause(true);
            StopCoroutines();
        }
        foreach(Sound element in Music){
            element.Pause(true);
            StopCoroutines();
        }
    }

    public void ResumeAll(){
        foreach(Sound element in Sfx){
            element.Pause(false);
        }
        foreach(Sound element in Music){
            element.Pause(false);
        }
    }

    public void StopAll(){
        foreach(Sound element in Sfx){
            element.Stop();
            StopCoroutines();
        }
        foreach(Sound element in Music){
            element.Stop();
            StopCoroutines();
        }
    }

   void InitAudioArrays(Sound [] array, string prefix){
        float music = PlayerPrefs.GetFloat("MusicVolume",1f);
        float sfx = PlayerPrefs.GetFloat("SfxVolume",1f);

        for(int i = 0; i < array.Length ; i++){
            GameObject _audio = new GameObject(prefix +"_" + i + "_" + array[i].Name);
            _audio.transform.parent = transform;
            array[i].SetSource(_audio.AddComponent<AudioSource>());
            if(prefix == "Music"){
                array[i].Volume = music;
            }
            if(prefix == "SFX"){
                array[i].Volume = sfx;
            }
        }
   }

    public void PlaySfx(string name){
        Sound audio = SearchSound(name, Sfx);
        if(audio != null){
            audio.Play();
        }
    }

   public void PlayMusic(string name, bool playOver = false){
        Sound audio = SearchSound(name, Music);
        if(audio != null){
            audio.Play(playOver);
        }
   }

    public void StopSound(string name){
        Sound audio = SearchSound(name);
        if(audio != null)
            audio.Stop();
    }

   public void UpdateSoundProperties(string name, Dictionary<string, float> props){
        Sound audio = SearchSound(name);
        audio.UpdateAudioSettings(props);
   }

   private Sound SearchSound(string name, Sound[] audios){
        foreach(Sound audio in audios){
            if(audio.Name == name){
                return audio;
            }
        }
        //Debug.LogError("AudioManager: audio "+ name + " was not found");
        return null;
   }

   private Sound SearchSound(string name){
        Sound audio = SearchSound(name, Sfx);
        audio = (audio == null) ? SearchSound(name, Music) : audio; 
        if(audio == null){
            Debug.LogError("AudioManager: audio "+ name + " was not found");
        }
        return audio;
   }

    public void FadeOutSound(string name, float time = 0.2f){
        Debug.Log("Fading " + name);
        Sound audio = SearchSound(name);
        if(audio != null){
            Coroutines.Add(StartCoroutine(FadeOutCoroutine(audio,time)));
        }
    }

    void StopCoroutines(){
        foreach(Coroutine coroutine in Coroutines){
            StopCoroutine(coroutine);
        }
    }

    protected IEnumerator FadeOutCoroutine(Sound audio , float time)
    {
        float currVol = audio.Volume;
        while (audio.Volume > 0.01f && audio.IsPlaying())
        {
           yield return new WaitForSeconds(time);
           float newVol = Mathf.Lerp(audio.Volume, 0, time);
           audio.Volume = newVol;
           audio.Source.volume = newVol;
        }
        if(audio.IsPlaying())
            audio.Stop();
        audio.Volume = currVol;
    }

    void OnValidate(){
        foreach(Sound audio in Sfx){
            audio.UpdateWithUI();
        }

        foreach(Sound audio in Music){
            audio.UpdateWithUI();
        }
    }

}
