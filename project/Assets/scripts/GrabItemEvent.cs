using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class GrabItemEvent : MonoBehaviour
{

    private Interactable interactable = null;
    private ItemInformation itemInformation = null;
    private ItemRegistrator itemRegistrator = null;
    private UIManagement uiManagement = null;

    private bool IsCompponentsAttached = false;
    private bool IsNotGrrabed;
    private bool IsGrabedSoundRang;

    private int _grabbedItemNumber = 0;
    public int GrabbedItemNumber
    {
        get
        {
            return _grabbedItemNumber;
        }
    }
    public float GripTime; // { get; set; }
    readonly float DecideGripTime = 1.0f;

    private void Start()
    {
        interactable = GetComponent<Interactable>();

        itemRegistrator = GameObject.FindGameObjectWithTag("ItemRegistrator").GetComponent<ItemRegistrator>();
        uiManagement = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManagement>();
    }

    // Update is called once per frame
    private void Update()
    {

        AddedVrtk_InteractableObjectComponent();
        getItem_InformationComponent();

        _grabbedItemNumber = ReturnItemNumber();

        GripTimeCounter();

        GrabTimeUIUpdate();
        //GrabItemNameUIUpdate();

        //controller = SteamVR_Controller.Input((int)trackedObj.index);

        //if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    controller.TriggerHapticPulse(2000);
        //}
    }

    //---コンポーネントの追加---//
    private void AddedVrtk_InteractableObjectComponent()
    {
        if (IsCompponentsAttached)
        {
            return;
        }
        if (interactable != null)
        {
            IsCompponentsAttached = true;
            return;
        }
        else
        {
            interactable = gameObject.AddComponent<Interactable>();

            //任意の色に変更すること
            // vrtk_InteractableObject.touchHighlightColor = Color.blue;

            // vrtk_InteractableObject.grabOverrideButton = VRTK.VRTK_ControllerEvents.ButtonAlias.TriggerPress;
        }
    }

    private void getItem_InformationComponent()
    {
        if (itemInformation == null)
        {
            itemInformation = this.GetComponent<ItemInformation>();
        }
    }

    //---グリップ時の処理---//
    private bool GameObjectGrabbedByController()
    {
        return interactable.attachedToHand != null;
    }

    private int ReturnItemNumber()
    {
        return (itemInformation != null && GameObjectGrabbedByController() == false) ? itemInformation.ItemNumber : -1;
    }

    //---グリップ時間のカウント---//

    private void GripTimeCounter()
    {
        if (!GameObjectGrabbedByController())
        {
            SoundManager.PlayGrubTimeSound();
            IsNotGrrabed = true;
            GripTime += Time.deltaTime;
            if (GripTime > DecideGripTime)
            {
                GripTime = 0;
                interactable.enabled = false;
                itemRegistrator.DestroyItem(_grabbedItemNumber);
            }
        }
        if (IsNotGrrabed == true && interactable.enabled == false)
        {
            SoundManager.StopGrubTimeSound();
            GripTime -= Time.deltaTime;
            if (GripTime < 0)
            {
                GripTime = 0;
                IsNotGrrabed = false;
            }
        }
    }

    //---UI周りの処理---//
    private void GrabTimeUIUpdate()
    {
        if (!GameObjectGrabbedByController() || IsNotGrrabed)
        {
            uiManagement.GrabbedItemTime = GripTime / DecideGripTime;
        }
    }

    //private void GrabItemNameUIUpdate()
    //{
    //    if(ii != null && !GameObjectGrabbedByController())
    //    {
    //        um.GrabItemName = ii.ItemName;
    //    }
    //}

}