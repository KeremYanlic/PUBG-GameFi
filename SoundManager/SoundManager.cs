using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource shootingChannel;
    public AudioSource emptyMagazineSound;
    public AudioSource reloadingSound1911;

    public AudioSource reloadingSoundM16;

    public AudioClip M16Shot;
    public AudioClip P1911Shot;

    private void Awake()
    {
        Instance = this;

    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch(weapon)
        {
            case WeaponModel.Pistol1911:
                shootingChannel.PlayOneShot(P1911Shot);
                break;
            case WeaponModel.M16:
                shootingChannel.PlayOneShot(M16Shot);
                break;
        }
    }

    public void PlayReloadSound(WeaponModel weapon)
    {
        switch(weapon)
        {
            case WeaponModel.Pistol1911:
                reloadingSound1911.Play();
                break;
            case WeaponModel.M16:
                reloadingSoundM16.Play();
                break;
        }
    }
}
