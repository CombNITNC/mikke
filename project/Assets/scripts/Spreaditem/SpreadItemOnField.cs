using System.Collections.Generic;
using UnityEngine;

public class SpreadItemOnField : MonoBehaviour
{

    public GameObject[] objects;

    List<int> numbers = new List<int>();

    public string FilePath;

    public int Amount;
    public int area;

    [SerializeField] ItemRegistrator IR = new ItemRegistrator();

    public List<GameObject> items_GameObjct = new List<GameObject>(); //[SerializeField] private List<GameObject> items_GameObjct = new List<GameObject>();
    [SerializeField] private List<string> items_name = new List<string>();

    private Queue<GameObject> _itemObjectQ = new Queue<GameObject>();
    public Queue<GameObject> ItemObjectQ
    {
        get { return this._itemObjectQ; }
    }

    void Awake()
    {

        ///適当にスクリプトを綺麗にまとめてほしい
        ///こちらからRIgidBodyとCollidrは勝手につけるようにした

        var Find_GameObject = GameObject.Find("FIND_OBJECT_MANAGER");
        var tmp_FS = Find_GameObject.GetComponent<FindScriptableobjects>();

        var I_Name = Find_GameObject.GetComponent<ItemRegistrator>();

        foreach (var element in tmp_FS.ItemsList)
        {
            items_GameObjct.Add(element.ItemObject);

            if (element.ItemName.Length == 0)
            {
                items_name.Add("☆---名前を追加してください---☆");
            }
            else
            {
                items_name.Add(element.ItemName);
            }

            //element.ItemObject.AddComponent<CapsuleCollider>();
            //element.ItemObject.AddComponent<Rigidbody>();

            System.Array.Resize(ref objects, objects.Length + 1);
            objects[objects.Length - 1] = element.ItemObject;

            //byte[] data = System.Text.Encoding.ASCII.GetBytes(element.ItemName);
            //string hoge = null;
            //for (int i = 0; i < data.Length; i++)
            //{
            //    Debug.Log(Convert.ToString(data[i], 16) + " - ");
            //}
            //Debug.Log("-------------------------------------------");

        }
    }

    // Use this for initialization
    void Start()
    {
        putItemSpread();

    }

    // Update is called once per frame
    void Update()
    { }

    private void putItemSpread()
    {

        //FilePath = Application.dataPath + "/objects/Test";
        //Object[] gameObjectArray = Resources.LoadAll(FilePath, typeof(GameObject));

        //for (int i = 0; i < objects.Length; i++)
        //{
        //    objects[i] = gameObjectArray[i];
        //    numbers.Add(i);

        //}
        //while (numbers.Count > 0)
        for (int i = 0; i < 1; i++)
        {
            //foreach (var Q in IR.ItemInformationQ)
            //{

            //    //int index = Random.Range(0, numbers.Count);

            //    //int ransu = numbers[index];

            //    float x = Random.Range(-area, area);
            //    float y = Random.Range(0, 30.0f);
            //    float z = Random.Range(-area, area);
            //    GameObject instancedItem = Instantiate(Q.ItemObject, new Vector3(x, y, z), transform.rotation);
            //    instancedItem.AddComponent<ItemInformation>();

            //    _itemObjectQ.Enqueue(instancedItem);

            //    // Debug.Log("Remove");
            //    // Obje.Enqueue(objects[index]);

            //    //numbers.RemoveAt(index);
            //}
        }
        //Enqueue Looking objects 
        for (int i = 0; i < objects.Length; i++)
        {
            numbers.Add(i);
        }

        while (numbers.Count > 0)
        {

            int index = Random.Range(0, numbers.Count);

            int ransu = numbers[index];

            //  Find.Enqueue(objects[ransu]);

            numbers.RemoveAt(index);
        }
    }
}