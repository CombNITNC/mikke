using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * アイテムを並べる場所を決定するためのクラス。
 *  （この処理については未完成）
 */

public interface ItemToFind { }

public class DecideItemPositionOnField : MonoBehaviour
{
    HashSet<ItemToFind> items = new HashSet<ItemToFind>();

    public void putItemDecidedPosition(List<ItemInformationCreate> infos)
    {
        foreach (var info in infos)
        {
            switch (info.positionEnumerate)
            {
                case PositionEnumerate.テーブル:
                    break;
                case PositionEnumerate.棚:
                    break;
                case PositionEnumerate.引き出し:
                    break;
                case PositionEnumerate.森_地面:
                    break;
                case PositionEnumerate.研究室_中央テーブル:
                    break;
                case PositionEnumerate.研究室_サイドテーブル:
                    break;
                case PositionEnumerate.ミニチュア_床:
                    break;
            }
        }

        foreach (var item in items)
        {

        }

    }

    //private Vector3 ArrageOnTable(ItemToFind pos)
    //{
    //    /*
    //        ここに処理を書く
    //     */
    //    pos.id += 1;
    //    return pos.transform.position;
    //}
    //private Vector3 ArrageinDrawer(ItemToFind pos)
    //{
    //
    //    /*
    //        ここに処理を書く
    //     */
    //    pos.id += 1;
    //    return pos.transform.position;
    //}
    //private Vector3 ArrageOnShelf(ItemToFind pos)
    //{
    //    pos.id += 1;
    //    Vector3 vec;
    //    vec = new Vector3(Random.Range(pos.transform.position.x - pos.transform.localScale.x,
    //            pos.transform.position.x + pos.transform.localScale.x), pos.transform.position.y,
    //        Random.Range(pos.transform.position.z - pos.transform.localScale.z,
    //            pos.transform.position.z + pos.transform.localScale.z));
    //    return vec;
    //}

    private int RandFromZeroTo(int value)
    {
        return Random.Range(0, value);
    }
}