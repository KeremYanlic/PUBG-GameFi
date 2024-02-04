using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameResources : SingletonMonobehavior<GameResources>
{
    #region Header SOUNDS
    [Space(10)]
    [Header("SOUNDS")]
    #endregion
    #region Tooltip
    [Tooltip("Populate with the sounds master mixer group")]
    #endregion
    public AudioMixerGroup soundsMasterMixerGroup;
}
