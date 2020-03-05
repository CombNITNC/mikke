using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * <概要>
 * ゲーム音楽管理クラス
 * ★フェードイン・フェードアウトを実装
 * 
 * <関係>
 *  
 * <public>
 *  めんどくせー割愛じゃー
 * 
 */

public class SoundManager : MonoBehaviour {

    static public AudioSource True;
    static public AudioSource False;
    static public AudioSource GrubTiming;
    static public AudioSource Footsteps;

    [SerializeField]private AudioSource _grubTime;
    [SerializeField]private AudioSource _true;
    [SerializeField]private AudioSource _false;
    [SerializeField]private AudioSource _footsteps;
    [SerializeField]private AudioSource gameSound;
    private int volume_gameMusic = 0;
    private int volume_walkSound = 0;
    private bool IsMusicPlaying = false;
    private bool IsWalking = false;
    public float DownVolumeLate = 0.15f;

    private void Awake()
    {
        True = _true;
        False  = _false;
        GrubTiming = _grubTime;
        Footsteps = _footsteps;
    }

        private void Update()
    {
        if (gameSound.time < 5f && IsMusicPlaying == false)
        {
            IsMusicPlaying = true;
            StopAllCoroutines();
            if (gameObject.activeSelf)
                StartCoroutine(Fadein_GameMusic());
        }
        if (gameSound.time > gameSound.clip.length - 5.0f && IsMusicPlaying == true)
        {
            IsMusicPlaying = false;
            StopAllCoroutines();
            if (gameObject.activeSelf)
            StartCoroutine(Fadeout_GameMusic());
        }


        //if (!Footsteps.isPlaying && IsWalking == false)
        //{
        //    IsWalking = true;
        //    StopAllCoroutines();
        //    if (gameObject.activeSelf)
        //        StartCoroutine(Fadein_walk());
        //}
        //if (Footsteps.isPlaying && IsWalking == true)
        //{
        //    IsWalking = false;
        //    StopAllCoroutines();
        //    if (gameObject.activeSelf)
        //        StartCoroutine(Fadeout_walk());
        //}
    }


    static public void PlayTrueSound()
    {
        True.PlayOneShot(True.clip);
    }

    static public void PlayFalseSound()
    {
        False.PlayOneShot(False.clip);
    }

    static public void PlayGrubTimingSound()
    {
        GrubTiming.PlayOneShot(GrubTiming.clip);
    }

    static public void PlayfootstepsSound()
    {
        if (!Footsteps.isPlaying)
        {
            Footsteps.Play();
        }
    }
    static public void StopfootstepsSound()
    {
        if (Footsteps.isPlaying)
        {
            Footsteps.Pause();
        }
    }

    static public void VolumeChangefootstepsSound(float value)
    {
        Footsteps.volume = value;
    }

    static public void PlayGrubTimeSound()
    {
        if (!GrubTiming.isPlaying)
        {
            GrubTiming.Play();
        }
        
    }

    static public void StopGrubTimeSound()
    {
        if (GrubTiming.isPlaying)
        {
            Footsteps.Pause();
        }

    }

    IEnumerator Fadein_GameMusic()
    {
        gameSound.enabled = true;
        for (; volume_gameMusic < 100; volume_gameMusic++)
        {
            gameSound.volume = (float)volume_gameMusic / 100 * DownVolumeLate;
            yield return null;
        }
        gameSound.volume = DownVolumeLate;
    }

    IEnumerator Fadeout_GameMusic()
    {
        for (; volume_gameMusic > 0; volume_gameMusic--)
        {
            gameSound.volume = (float)(volume_gameMusic) / 100 * DownVolumeLate;
            yield return null;
        }
        gameSound.volume = 0;
        gameSound.enabled = false;
    }

    IEnumerator Fadein_walk()
    {
        Footsteps.enabled = true;
        for (; volume_walkSound < 1000; volume_walkSound++)
        {
            Footsteps.volume = ((float)volume_walkSound / 1000);
            if (Footsteps.volume > DownVolumeLate)
            {
                Footsteps.volume = DownVolumeLate;
                yield return null;
            }
            yield return null;
        }
        Footsteps.volume = DownVolumeLate;
    }

    IEnumerator Fadeout_walk()
    {
        for (; volume_walkSound > 0; volume_walkSound--)
        {
            Footsteps.volume = ((float)(volume_walkSound) / 1000);
            yield return null;
        }
        Footsteps.volume = 0;
        Footsteps.enabled = false;
    }
}
