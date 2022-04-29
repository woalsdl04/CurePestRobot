using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX_SOUND //사운드 종류
{
    SHOOT,
    LASER,
    HIT,
    CLAP,
    WARNING,
    BOMB,
    EXPLOSION,
}
public class Sound : MonoBehaviour //사운드 매니저
{
    public void Awake()
    {
        V.Sound = this;
    }

    public void SFX_Play(SFX_SOUND sound, float volume = 1f) //효과음 재생
    {
        var obj = new GameObject();
        obj.AddComponent<AudioSource>().clip = GetSound(sound);
        obj.GetComponent<AudioSource>().volume = volume;
        obj.GetComponent<AudioSource>().Play();
        

        Destroy(obj, obj.GetComponent<AudioSource>().clip.length);
    }


    public AudioClip GetSound(SFX_SOUND sound) //사운드 찾기
    {
        AudioClip clip = null;

        switch (sound) 
        {
            case SFX_SOUND.SHOOT:
                clip = Resources.Load<AudioClip>("Sound/Shoot");
                break;
            case SFX_SOUND.CLAP:
                clip = Resources.Load<AudioClip>("Sound/Clapping");
                break;
            case SFX_SOUND.EXPLOSION:
                clip = Resources.Load<AudioClip>("Sound/Explosion");
                break;
            case SFX_SOUND.HIT:
                clip = Resources.Load<AudioClip>("Sound/Hit");
                break;
            case SFX_SOUND.LASER:
                clip = Resources.Load<AudioClip>("Sound/Laser");
                break;
            case SFX_SOUND.BOMB:
                clip = Resources.Load<AudioClip>("Sound/Bomb");
                break;
            case SFX_SOUND.WARNING:
                clip = Resources.Load<AudioClip>("Sound/Warning");
                break;
        }

        return clip;
    }
}
