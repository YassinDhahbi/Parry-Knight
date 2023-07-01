using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource walkingAudioSource;

    [SerializeField]
    private AudioClip walkingClipV1;

    [SerializeField]
    private AudioClip walkingClipV2;

    public void PlayWalkingSound(int index)
    {
        AudioClip selectedAudio = index == 0 ? walkingClipV1 : walkingClipV2;
        walkingAudioSource.PlayOneShot(selectedAudio);
    }
}