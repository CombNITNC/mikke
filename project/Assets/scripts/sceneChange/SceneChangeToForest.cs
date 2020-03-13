using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class SceneChangeToForest : MonoBehaviour
{
    public string SceneName = "";
    private Interactable interactable = null;
    private float GripTime;
    readonly float DecideGripTime = 3.0f;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    private void HandHoverUpdate(Hand hand)
    {
        var grabType = hand.GetGrabStarting();
        var endedGrabbing = hand.IsGrabEnding(gameObject);
        if (endedGrabbing)
        {
            GripTime = 0;
        }
        if (interactable.attachedToHand == null && grabType == GrabTypes.Grip)
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