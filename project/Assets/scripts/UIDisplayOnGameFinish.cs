using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*
 * <概要>
 * ロビー画面でのランキングを表示するためのもの
 * 
 * <関係>
 * [ScoreCounter]クラスからデータを受信
 * 
 * <public>
 *      ScoreText : Text
 *      RankingText : Text[]
 *      
 * <private>
 *      ScoreDisplay() : void
 *      RankingDisplay() : void
 *      
 */

public class UIDisplayOnGameFinish : MonoBehaviour
{

    public Text ScoreText;

    [SerializeField] public Text[] RankingText = new Text[10];

    void Start()
    {
        ScoreDisplay();
        RankingDisplay();
    }
    private void ScoreDisplay()
    {
        ScoreText.text = ScoreCounter.scoreValue.ToString() + "  point";
    }

    private void RankingDisplay()
    {
        var toQueueRankingValue = new Queue<int>();
        toQueueRankingValue.Enqueue(0);

        if (ScoreCounter.ScoreRanking != null)
        {
            ScoreCounter.ScoreRanking.Sort((a, b) => b - a);
            toQueueRankingValue = new Queue<int>(ScoreCounter.ScoreRanking);
        }

        foreach (var elm in RankingText)
        {
            if (toQueueRankingValue.Count == 0)
            {
                elm.text = "0  points";
                continue;
            }

            elm.text = toQueueRankingValue.Peek().ToString() + "  point";
            toQueueRankingValue.Dequeue();
        }
    }
}