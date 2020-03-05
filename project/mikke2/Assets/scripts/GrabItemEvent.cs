using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItemEvent : MonoBehaviour {

    private VRTK.VRTK_InteractableObject vrtk_InteractableObject = new VRTK.VRTK_InteractableObject();
    private ItemInformation itemInformation = null;
    private ItemRegistrator itemRegistrator = null;
    private UIManagement uiManagement = null;

    private bool IsCompponentsAttached = false;
    private bool isItemGrabbed;
    private bool IsNotGrrabed;
    private bool IsGrabedSoundRang;

    private SteamVR_Controller.Device controller;

    private int _grabbedItemNumber = 0;
    public int GrabbedItemNumber
    {
        get
        {
            return _grabbedItemNumber;
        }
    }
    public float GripTime;// { get; set; }
    readonly float DecideGripTime = 1.0f;


    private void Start()
    {

        itemRegistrator = GameObject.FindGameObjectWithTag("ItemRegistrator").GetComponent<ItemRegistrator>();
        uiManagement = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManagement>();
    }

    // Update is called once per frame
    private void Update() {

        AddedVrtk_InteractableObjectComponent();
        getItem_InformationComponent();

        isItemGrabbed = GameObjectGrabbedByController();
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
        if (this.GetComponent<VRTK.VRTK_InteractableObject>() != null)
        {
            IsCompponentsAttached = true;
            return;
        }
        else if (this.GetComponent<VRTK.VRTK_InteractableObject>() == null)
        {
            vrtk_InteractableObject = this.gameObject.AddComponent<VRTK.VRTK_InteractableObject>();
            vrtk_InteractableObject.isGrabbable = true;

            //任意の色に変更すること
            vrtk_InteractableObject.touchHighlightColor = Color.blue;

            vrtk_InteractableObject.grabOverrideButton = VRTK.VRTK_ControllerEvents.ButtonAlias.Trigger_Press;
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
        return vrtk_InteractableObject.isGrabbable;
    }

    private int ReturnItemNumber()
    {
        return (itemInformation != null && isItemGrabbed == false) ? itemInformation.ItemNumber : -1;
    }


    //---グリップ時間のカウント---//

    private void GripTimeCounter()
    {
        if (!isItemGrabbed)
        {
            SoundManager.PlayGrubTimeSound();
            IsNotGrrabed = true;
            GripTime += Time.deltaTime;
            if(GripTime > DecideGripTime)
            {
                GripTime = 0;
                this.gameObject.GetComponent<VRTK.VRTK_InteractableObject>().enabled = false;
                itemRegistrator.DestroyItem(_grabbedItemNumber);
            }
        }
        if(IsNotGrrabed == true && vrtk_InteractableObject.enabled == false)
        {
            SoundManager.StopGrubTimeSound();
            GripTime -= Time.deltaTime;
            if(GripTime < 0)
            {
                GripTime = 0;
                IsNotGrrabed = false;
            }
        }
    }

    //---UI周りの処理---//
    private void GrabTimeUIUpdate() 
    {
        if(!isItemGrabbed || IsNotGrrabed)
        {
            uiManagement.GrabbedItemTime = GripTime / DecideGripTime;
        }
    }

    //private void GrabItemNameUIUpdate()
    //{
    //    if(ii != null && !isItemGrabbed)
    //    {
    //        um.GrabItemName = ii.ItemName;
    //    }
    //}

}
