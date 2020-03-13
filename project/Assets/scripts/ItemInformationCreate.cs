using UnityEngine;

[CreateAssetMenu(menuName = "アイテムを生成する！")]
public class ItemInformationCreate : ScriptableObject
{
    [SerializeField] private string _itemName;
    public string ItemName
    {
        get { return _itemName; }
        set
        {
            _itemName = value;
        }
    }

    [SerializeField] private int _itemPoint;
    public int ItemPoint
    {
        get { return _itemPoint; }
        set { _itemPoint = value; }
    }

    [SerializeField, MultilineAttribute(5)] private string _itemHint;
    public string ItemHint
    {
        get { return _itemHint; }
        set { _itemHint = value; }
    }

    [SerializeField] private GameObject _itemObject;
    public GameObject ItemObject
    {
        get { return _itemObject; }
        set { _itemObject = value; }
    }

    [SerializeField] private bool _atOneFloor;
    public bool AtOneFloor
    {
        get { return _atOneFloor; }
        set { _atOneFloor = value; }
    }

    [SerializeField] private bool _atTwoFloor;
    public bool AtTwoFloor
    {
        get { return _atTwoFloor; }
        set { _atTwoFloor = value; }
    }

    public PositionEnumerate positionEnumerate;

    void OnValidate()
    {
        if (_atTwoFloor)
        {
            _atOneFloor = false;
            _atTwoFloor = true;
            return;
        }
        if (_atOneFloor)
        {
            _atTwoFloor = false;
            _atOneFloor = true;
            return;
        }

    }
}