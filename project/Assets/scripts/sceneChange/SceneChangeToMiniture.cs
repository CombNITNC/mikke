using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangeToMiniture : MonoBehaviour
{
    public string SceneName = "";
    private VRTK.VRTK_InteractableObject vrtk_InteractableObject = new VRTK.VRTK_InteractableObject();
    private float GripTime;
    readonly float DecideGripTime = 3.0f;

    private void Start()
    {
        vrtk_InteractableObject = this.gameObject.GetComponent<VRTK.VRTK_InteractableObject>();
        vrtk_InteractableObject.isGrabbable = true;
    }

    private void Update()
    {
        if (vrtk_InteractableObject.isGrabbable)
        {
            GripTime = 0;
        }
        else
        {
            GripTimeCounter();
        }
    }

    private void GripTimeCounter()
    {
        GripTime += Time.deltaTime;

        if (GripTime > DecideGripTime)
        {
            SceneManager.LoadScene(SceneName);
            ScoreCounter.SetScoreValueToZero();
            GripTime = 0;
        }
    }

}


