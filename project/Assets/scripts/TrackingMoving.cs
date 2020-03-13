using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * <概要>
 * プレイヤーが動いているかどうかを過去の位置関係と比較する
 * 
 * <関係>
 * [SoundManager]クラスへデータを送信
 * 
 * <public>
 *      moveMargin : float
 *      delayTime : int
 *      
 * <property>
 *      currentFramePos : Vector3
 *      latestFramePos : Queue<Vector3>
 */

public class TrackingMoving : MonoBehaviour
{

    // Use this for initialization

    private Queue<Vector3> latestFramePos = new Queue<Vector3>();
    private Vector3 currentFramePos;
    public int delayTime = 200;
    public float moveMargin = 0.1f;

    [SerializeField] SoundManager soundManager = null;

    void Start()
    {
        if (soundManager == null)
        {
            Debug.LogError("soundManager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {

        //
        float currentFramePosSqrt = Mathf.Sqrt((float) Math.Pow(Mathf.Abs(currentFramePos.x - this.transform.position.x), 2) +
            (float) Math.Pow(Mathf.Abs(currentFramePos.z - this.transform.position.z), 2));

        //
        float avaragelatestFramePosSqrt = 0;

        Queue<Vector3> latestFramePosCopy = new Queue<Vector3>(latestFramePos);
        while (latestFramePosCopy.Count != 0)
        {
            Vector3 first = latestFramePosCopy.Dequeue();
            if (latestFramePosCopy.Count == 0)
            {
                break;
            }

            Vector3 second = latestFramePosCopy.Peek();
            avaragelatestFramePosSqrt += Mathf.Sqrt((float) Math.Pow(Mathf.Abs(first.x - second.x), 2) +
                (float) Math.Pow(Mathf.Abs(first.z - second.z), 2));
        }
        avaragelatestFramePosSqrt /= delayTime;

        //
        if (Mathf.Abs(currentFramePosSqrt - avaragelatestFramePosSqrt) > moveMargin)
        {
            soundManager.PlayfootstepsSound();
        }
        if (Mathf.Abs(currentFramePosSqrt - avaragelatestFramePosSqrt) <= moveMargin)
        {
            soundManager.StopfootstepsSound();
        }

        //
        if (latestFramePos.Count > delayTime)
        {
            latestFramePos.Dequeue();
        }
        latestFramePos.Enqueue(this.transform.position);

        //
        currentFramePos = this.transform.position;
    }
}