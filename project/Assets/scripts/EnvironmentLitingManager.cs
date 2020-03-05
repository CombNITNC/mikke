using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * 時間の経過を取得して夕焼けを実装しています。
 * [TimeCounter]クラスからデータを取得
 * 
 * ※注意
 * [TimeCounter]クラスを用いて時間経過を取得しています。
 * ゲームオブジェクトに[TimeCounter]クラスもアタッチされていることを確認。
 * 
 * <public>
 *      sky : Material
 *      timeCounter : GetComponent<TimeCounter>()
 *      
 * <private>
 *      firstSkyboxColor : color
 *      endSkyboxColor : color
 */

public class EnvironmentLitingManager : MonoBehaviour {

    [SerializeField]private  Material sky;
    // Use this for initialization

    Color32 firstSkyboxColor = new Color32(141, 189, 255, 1);
    Color32 endSkyboxColor = new Color32(124, 53, 37, 1);

    TimeCounter timeCounter = null;

    void Start () {
        RenderSettings.skybox = sky;

        timeCounter = this.GetComponent<TimeCounter>();
        if (timeCounter == null)
        {
            return;
        }

        RenderSettings.skybox.SetColor("_Tint", firstSkyboxColor);
        RenderSettings.skybox.SetFloat("_Exposure", 1.0f);
    }
	
	// Update is called once per frame
	void Update () {

        if (timeCounter == null)
        {
            return;
        }

        Color32 lerpedColor = Color32.Lerp(firstSkyboxColor, endSkyboxColor,timeCounter.GameTimeRangeZeroToOne);
        sky.SetColor("_Tint", lerpedColor);

        float lerpedExposureValue = Mathf.Lerp(1.0f, 0.8f, timeCounter.GameTimeRangeZeroToOne);
        RenderSettings.skybox.SetFloat("_Exposure", lerpedExposureValue);
    }

    void OnApplicationQuit()
    {
        RenderSettings.skybox.SetColor("_Tint", firstSkyboxColor);
        RenderSettings.skybox.SetFloat("_Exposure", 1.0f);
    }
}
