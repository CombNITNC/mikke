using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/*
 * <概要>
 * スクリプタブルオブジェクトにまとめた、３Dオブジェクト名、名前、ポイントなどの情報を扱いやすいListの形に変換する。
 * [ItemInformationCreate]クラスはスクリプタブルオブジェクトの内容を決定している
 * 
 * <関係>
 * [ItemInformationCreate]クラスからデータを受信
 * [ItemRegistrator]クラスへデータを送信
 * 
 * <public>
 *      PathName : string
 *      find_scriptableobjects_in_folda() : void
 *      
 * <property>
 *      ItemsList : List<GameObject> {get;set;}
 * 
 */

public class FindScriptableobjects : MonoBehaviour
{
    [Tooltip("\n☆使い方☆\n" +
        "1,今回だけobjects/scriptableobjects(sample)ディレクトリ上で右クリックで[create]=>[アイテムを生成する！]" +
        "2,scriptableobjectをobjectsディレクトリ上に置く\n" +
        "3,ヒエラルキー上のFIND_OBJECT_MANAGERのインスペクタに飛び適用ボタンを押す\n\n")]
    [Header("scriptableobjects格納list")]
    [SerializeField]
    private List<ItemInformationCreate> _itemsList = new List<ItemInformationCreate>();

    public List<ItemInformationCreate> ItemsList
    {
        get { return _itemsList; }
        set { _itemsList = value; }
    }

    //Directory名は任意で変更可
    [Tooltip("\nディレクトリのパスを記入\n")]
    [Header("ディレクトリパスを記入")]
    [Multiline(3)]
    public string PathName;

    public void find_scriptableobjects_in_folda()
    {

        DirectoryInfo dir = new DirectoryInfo(PathName);
        FileInfo[] info = dir.GetFiles("*.asset");

        if (info.Length == 0)
        {
            return;
        }

        if (_itemsList.Count != 0)
        {
            _itemsList.Clear();
        }

        foreach (FileInfo f in info)
        {
            var element = AssetDatabase.LoadAssetAtPath<ItemInformationCreate>(Path.Combine(PathName, f.Name));
            _itemsList.Add(element);
            //Debug.Log(f.Name);

        }
    }

}