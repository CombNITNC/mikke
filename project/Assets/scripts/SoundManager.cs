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

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _footsteps = null;
    [SerializeField] private AudioSource gameSound = null;

    private int volume_gameMusic = 0;
    private int volume_walkSound = 0;
    private bool IsMusicPlaying = false;
    private Coroutine walkSoundTask = null;
    public float DownVolumeLate = 0.15f;

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

        if (!_footsteps.isPlaying && walkSoundTask == null)
        {
            StopAllCoroutines();
            if (gameObject.activeSelf)
                walkSoundTask = StartCoroutine(Fadein_walk());
        }
        if (_footsteps.isPlaying && walkSoundTask == null)
        {
            StopAllCoroutines();
            if (gameObject.activeSelf)
                walkSoundTask = StartCoroutine(Fadeout_walk());
        }
    }

    public void PlayfootstepsSound()
    {
        if (!_footsteps.isPlaying)
        {
            _footsteps.Play();
        }
    }
    public void StopfootstepsSound()
    {
        if (_footsteps.isPlaying)
        {
            _footsteps.Pause();
        }
    }

    IEnumerator Fadein_GameMusic()
    {
        gameSound.enabled = true;
        for (; volume_gameMusic < 100; volume_gameMusic++)
        {
            gameSound.volume = (float) volume_gameMusic / 100 * DownVolumeLate;
            yield return null;
        }
        gameSound.volume = DownVolumeLate;
    }

    IEnumerator Fadeout_GameMusic()
    {
        for (; volume_gameMusic > 0; volume_gameMusic--)
        {
            gameSound.volume = (float) (volume_gameMusic) / 100 * DownVolumeLate;
            yield return null;
        }
        gameSound.volume = 0;
        gameSound.enabled = false;
    }

    IEnumerator Fadein_walk()
    {
        _footsteps.enabled = true;
        for (; volume_walkSound < 1000; volume_walkSound++)
        {
            _footsteps.volume = ((float) volume_walkSound / 1000);
            if (_footsteps.volume > DownVolumeLate)
            {
                _footsteps.volume = DownVolumeLate;
                yield return null;
            }
            yield return null;
        }
        _footsteps.volume = DownVolumeLate;
    }

    IEnumerator Fadeout_walk()
    {
        for (; volume_walkSound > 0; volume_walkSound--)
        {
            _footsteps.volume = ((float) (volume_walkSound) / 1000);
            yield return null;
        }
        _footsteps.volume = 0;
        _footsteps.enabled = false;
    }
}