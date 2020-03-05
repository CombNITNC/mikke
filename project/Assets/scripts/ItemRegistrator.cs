using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * <概要>
 * スクリプタブルオブジェクトを一元管理するクラス
 * 
 * <関係>
 * [FindScriptableobjects][FindSakuraObjects]クラスからデータを受信
 * 
 * <public>
 *      seiseisuu : int
 *      PathName : string
 *      find_scriptableobjects_in_folda() : void
 *      
 * <property>
 *      SakuraItemsList : List<GameObject> {get;set;}
 * 
 */


public class ItemRegistrator : MonoBehaviour {

    ///<summary>
    ///Scriptableobjectsを一元管理しており、
    ///実際に使用するデータを格納したクラスが,
    ///FindScriptableObject
    ///</summary>
    private FindScriptableobjects findScriptableObject;
    private FindSakuraObjects findSakuraObjects;

    private Queue<GameObject> _itemQ = new Queue<GameObject>();
    public Queue<GameObject> ItemQ
    {
        get { return this._itemQ; }
        set { this._itemQ = value; }
    }

     private string _odaiName;
    public string OdaiName
    {
        get { return _odaiName; }
    }
    private string _odaiHint;
    public string OdaiHint
    {
        get { return _odaiHint; }
    }
    private int _odaiLeftAmount;
    public int OdaiLeftAmount
    {
        get { return _odaiLeftAmount; }
    }

    ScoreCounter scoreCounter; 

    void Awake()
    {
        InstanceItemGameObjects();
        InstanceSakuraItemGameObjects();
    }


    //お題は常に番号で管理される。プレイヤーがお題を１クリアするごとに１繰り上がる。
    private int NowOdaiNumber = 0;

    /// <summary>
    /// ゲーム開始時にスクリプタブルオブジェクトから読み取ったアイテム情報をQueueに格納する。
    /// ここでいうアイテム情報とは、
    /// [1]ＵＩなどに表示するアイテムの名前
    /// [2]フィールドに配置するゲームオブジェクト（プレファブ)
    /// [3]格納する順番
    /// [4]アイテムのポイント
    /// [5]アイテムをどこに配置するか
    /// [6]アイテムのヒント
    /// である
    /// </summary>
    /// 

        private void InstanceItemGameObjects()
    {
        _odaiLeftAmount = 0;
        findScriptableObject = this.GetComponent<FindScriptableobjects>();

        //お題を混ぜる
        queue_shuffle();

        int kakunoujyun = 0;
        //以下は元データ(scriptableobjects)の数だけカウントされる
        for (int i = 0; i < findScriptableObject.ItemsList.Count; i++)
        {
            //☆[重要]☆
            //ItemsList自体は生成するアイテム情報だけを持ったクラスである。
            //そのため、以下でgameObjectを生成する
            var sourceinformation = findScriptableObject.ItemsList[i];

            if (sourceinformation.ItemObject == null)
            {
                continue;
            }
            else
            {
                GameObject instancedGameObject = Instantiate(sourceinformation.ItemObject);
                var iteminformation = instancedGameObject.AddComponent<ItemInformation>();

                //実際にアイテムをＱに配置してよいか調べる変数
                //情報の記入漏れがあるとアイテムは格納されない
                bool itemGeneratedAlsoIsAllowedTo;
                itemGeneratedAlsoIsAllowedTo = CheckNoMissFromItemInformation(sourceinformation);

                if(itemGeneratedAlsoIsAllowedTo == false)
                {
                    Destroy(instancedGameObject);
                    continue;
                }
                else
                {
                 iteminformation.itemInformation(sourceinformation.ItemName,
                                                 sourceinformation.ItemObject,
                                                 kakunoujyun,
                                                 sourceinformation.ItemPoint, 
                                                 (int)sourceinformation.positionEnumerate, 
                                                 sourceinformation.ItemHint
                                                );
                    kakunoujyun += 1;
                }
                //アイテムをＱに格納する
                _itemQ.Enqueue(instancedGameObject);
            }

        }
        _odaiLeftAmount = _itemQ.Count + 1;
        OdaiUpadte();

        //byte[] data = System.Text.Encoding.ASCII.GetBytes(element.ItemName);
        //string hoge = null;
        //for (int i = 0; i < data.Length; i++)
        //{
        //    Debug.Log(Convert.ToString(data[i], 16) + " - ");
        //}
        //Debug.Log("-------------------------------------------");

    }

    private void InstanceSakuraItemGameObjects()
    {
        findSakuraObjects = this.GetComponent<FindSakuraObjects>();
        findSakuraObjects.find_scriptableobjects_in_folda();

        for (int i = 0; i < findSakuraObjects.SakuraItemsList.Count; i++)
        {
            for (int j = 0; j < findSakuraObjects.seiseiset; j++)
            {
                GameObject instancedGameObject = Instantiate(findSakuraObjects.SakuraItemsList[i]);
                var iteminformation = instancedGameObject.AddComponent<ItemInformation>();

                iteminformation.itemInformation("SakuraObjects",
                                                instancedGameObject,
                                                -1,
                                                0,
                                                0
                                               );
                _itemQ.Enqueue(instancedGameObject);
            }

        }
    }

    private bool CheckNoMissFromItemInformation(ItemInformationCreate sourceinformation)
    {
        string errorMessage = "";

        if (sourceinformation.ItemName.Length == 0)
        {
            errorMessage += "名前,";
        }
        if (sourceinformation.ItemHint.Length == 0)
        {
            errorMessage += "ヒント,";
        }
        if (sourceinformation.ItemPoint == 0)
        {
            errorMessage += "ポイント";
        }

        if (errorMessage.Length != 0)
        {
            Debug.Log(errorMessage + "の情報を追加してください。" + sourceinformation.ItemObject.name);
        }

        return errorMessage.Length == 0 ? true : false;
    }

    private void queue_shuffle()
    {

        int n = findScriptableObject.ItemsList.Count;

        List<int> numbers = new List<int>();

        for (int i = 0; i < findScriptableObject.ItemsList.Count; i++)
        {
            numbers.Add(i);
        }

        for (int i = 0; i < n; i++)
        {
            int index = Random.Range(0, numbers.Count);

            int ransu = numbers[index];

            numbers.RemoveAt(index);

            int r = ransu;

            var rand = findScriptableObject.ItemsList[r];
            findScriptableObject.ItemsList[r] = findScriptableObject.ItemsList[i];
            findScriptableObject.ItemsList[i] = rand;
        }

    }

    public void DestroyItem(int grabbedItemNumber)
    {
        if(grabbedItemNumber == NowOdaiNumber)
        {
            SoundManager.PlayTrueSound();

            ScoreCounter.AddScoreValue(_itemQ.Peek().GetComponent<ItemInformation>().ItemPoint);
            Destroy(_itemQ.Peek());
            _itemQ.Dequeue();
            _odaiLeftAmount = _itemQ.Count + 1;

            OdaiUpadte();
            NowOdaiNumber++;
        }
        else
        {
            SoundManager.PlayFalseSound();

            NowOdaiNumber += 0;
        }

        //Debug.Log(NowOdaiNumber);
    }

    private void OdaiUpadte()
    {
        _odaiName = _itemQ.Peek().GetComponent<ItemInformation>().ItemName;
        _odaiHint = _itemQ.Peek().GetComponent<ItemInformation>().ItemHint;
    }
}

