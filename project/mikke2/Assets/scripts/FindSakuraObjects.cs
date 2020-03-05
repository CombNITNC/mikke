using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

/*
 * <概要>
 * サクラ（俗にいう張りぼて）のアイテムを生成するクラス。
 * 今回、フィールドに並べるアイテム数が心苦しかったために即席で作りました。
 * [FindScriptableObject]クラスと同じことをしています。
 * 
 * <関係>
 * [ItemRegistrator]クラスへデータを送信
 * 
 * <public>
 *      seiseiset : int
 *      PathName : string
 *      find_scriptableobjects_in_folda() : void
 *      
 * <property>
 *      SakuraItemsList : List<GameObject> {get;set;}
 * 
 */
public class FindSakuraObjects : MonoBehaviour {

    private List<GameObject> _sakuraitemsList = new List<GameObject>();

    public List<GameObject> SakuraItemsList
    {
        get { return _sakuraitemsList; }
        set { _sakuraitemsList = value; }
    }

    //アイテム生成数
    public int seiseiset = 3;

    //Directory名は任意で変更可
    [Tooltip("\nディレクトリのパスを記入\n")]
    [Header("ディレクトリパスを記入")]
    [Multiline(3)]
    public string PathName;

    //フォルダの内部からスクリプタブルオブジェクトを取得します
    public void find_scriptableobjects_in_folda()
    {

        DirectoryInfo dir = new DirectoryInfo(PathName);
        FileInfo[] info = dir.GetFiles("*.prefab");


        if (info.Length == 0)
        {
            return;
        }

        if (_sakuraitemsList.Count != 0)
        {
            _sakuraitemsList.Clear();
        }


        foreach (FileInfo f in info)
        {
            var element = AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine(PathName, f.Name));
            _sakuraitemsList.Add(element);
            //Debug.Log(f.Name);

        }
    }

}
