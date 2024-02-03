using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SoundEffectManager : SingletonMonobehavior<SoundEffectManager>
{
    public int soundsVolume = 8;

    private void Start()
    {
        SetSoundsVolume(soundsVolume);
    }

    // <summary>
    // Play the sound effect
    // </summary>
    public void PlaySoundEffect(SoundEffectSO soundEffectSO,Vector3 soundPosition)
    {
        // Play sound using a sound gameobject and component from the object pool
        SoundEffect sound = (SoundEffect)PoolManager.Instance.ReuseComponent(soundEffectSO.soundPrefab, soundPosition, Quaternion.identity);
        sound.SetSound(soundEffectSO);
        sound.gameObject.SetActive(true);

        StartCoroutine(DisableSound(sound, soundEffectSO.soundEffectClip.length));
    }

    // <summary>
    // Disable sound effect object after it has played thus returning it to the object pool
    // </summary>
    private IEnumerator DisableSound(SoundEffect sound, float durationToDisable)
    {
        yield return new WaitForSeconds(durationToDisable);
        sound.gameObject.SetActive(false);
    }


    // <summary>
    // Set sounds volume
    // </summary>
    private void SetSoundsVolume(int soundsVolume)
    {
        float muteDecibels = -80f;

        if(soundsVolume == 0)
        {
            GameManager.Instance.gameResources.soundsMasterMixerGroup.audioMixer.SetFloat("soundsVolume", muteDecibels);
        }
        else
        {
            GameManager.Instance.gameResources.soundsMasterMixerGroup.audioMixer.SetFloat("soundsVolume", UtilsClass.LinearToDecibels(soundsVolume));
        }
    }

    
}
