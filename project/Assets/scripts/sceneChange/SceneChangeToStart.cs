using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeToStart : MonoBehaviour
{

    readonly string ScenenName = "Start";
    [SerializeField] private TimeCounter timeCounter = null;

    void Update()
    {
        if (timeCounter.GameEndTrigger)
        {
            ScoreCounter.SetScoreValueToRanking();
            SceneManager.LoadScene(ScenenName);
            return;
        }
    }
}