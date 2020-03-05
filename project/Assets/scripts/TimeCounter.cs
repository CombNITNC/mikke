using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/*
 * <概要>
 * 時間の経過を取得しています。
 * 
 * <関係>
 * [TimeCounter]クラスへデータを送信
 * 
 * <property>
 *      GameTimeRangeZeroToOne : float{get;set;}
 *      GameStartTrigger : bool{get;set;}
 *      GameEndTrigger : bool{get;set;}
 * 
 * <public>
 *      timeOutValue : float
 *      
 * <private>
 *      gameTime : float
 *      timeOut : float
 *      Timer() : void
 *      Caluculate_GameTimeRangeZeroToOne() : void
 */

public class TimeCounter : MonoBehaviour {

    private float gameTime;

    /// <summary>
    /// 何秒でゲームが終わるか
    /// </summary>
    private  float timeOut;

    public float timeOutValue = 360.0f;
    /// <summary>
    /// ゲームがスタートしてから終わるまでの経過時間を0～１の範囲で返す
    /// </summary>
    public float GameTimeRangeZeroToOne { get { return _gameTimeRangeZeroToOne; } }
    private  float _gameTimeRangeZeroToOne;


    /// <summary>
    /// ゲーム開始時のフラグ
    /// </summary>
    public bool GameStartTrigger { set { _gameStartTrigger = value; } }
    private  bool _gameStartTrigger;


    /// <summary>
    /// ゲーム終了時のフラグ
    /// </summary>
    public bool GameEndTrigger { get { return this._gameEndTrigger; } }
    public bool _gameEndTrigger;

    // Use this for initialization
    private void Start() {
        gameTime = 0.0f;
        timeOut = timeOutValue;
        _gameStartTrigger = true;
        _gameEndTrigger = false;
    }

    // Update is called once per frame
    private void Update () {

        if (_gameEndTrigger)
        {
            return;
        }
        else if (gameTime >= timeOut)
        {
            _gameEndTrigger = true;
            gameTime = 0.0f;
        }
        else if (_gameStartTrigger)
        {
            Timer();
            Caluculate_GameTimeRangeZeroToOne();
        }

    }

    private void Timer()
    {
        gameTime += Time.deltaTime;
    }

    private void Caluculate_GameTimeRangeZeroToOne()
    {
        if(gameTime == 0)
        {
            _gameTimeRangeZeroToOne = 0;
        }
        else if(timeOutValue - gameTime > 0)
        {
            _gameTimeRangeZeroToOne =  1 - ((timeOutValue - gameTime) / timeOutValue);
        }
        else if(gameTime - timeOutValue >= 0)
        {
            _gameTimeRangeZeroToOne = 1;
        }
    }
}
