using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * <概要>
 * プレイヤーがゲームでアイテムを一致させて得点にしたものを保管するクラス
 * 
 * <関係>
 * 
 * <static>
 *      scoreValue : int
 *      ScoreRanking : List<int> 
 *      AddScoreValue(int) : void
 *      SetScoreValueToZero() : void
 *      SetScoreValueToRanking() : void
 *      
 */


public class ScoreCounter : MonoBehaviour
{
    public static int scoreValue = 0;
    public static List<int> ScoreRanking = new List<int>();

    public static void AddScoreValue(int _scoreValue)
    {
        scoreValue += _scoreValue;
    }

    public static void SetScoreValueToZero()
    {
        scoreValue = 0;
    }

    public static void SetScoreValueToRanking()
    {
        //ゲーム終了時に呼ばれる関数
        ScoreRanking.Add(scoreValue);
    }
}
