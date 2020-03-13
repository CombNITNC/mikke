using UnityEngine;

public class SpreadItem : MonoBehaviour
{
    /*
     * <概要>
     * itemRegistratorクラスから受け取ったアイテムデータを基に、ゲームフィールドにアイテムを散らばらせる。
     * 
     * <関係>
     * [itemRegistrator]クラスからデータを受信
     *   
     * <private>
     *      area : float  [GameObjectをどれほどの量散らばらせるかを表すパラメータ]
     *      itemRegistrator : ItemRegistrator
     *      CenterTransform : Transform
     */

    [SerializeField] private int area = 0;
    [SerializeField] private ItemRegistrator itemRegistrator = null;
    [SerializeField] private Transform CenterTransform = null;

    // Use this for initialization
    void Start()
    {
        if (CenterTransform == null)
        {
            Debug.LogWarning("CenterTransform is not specified, so it will be its Transform.");
            CenterTransform = transform;
        }
        if (itemRegistrator == null)
        {
            Debug.LogError("itemRegistrator is null");
        }
        putItemSpread();
    }

    private void putItemSpread()
    {
        while (itemRegistrator.ItemQ == null);

        foreach (var Q in itemRegistrator.ItemQ)
        {
            if (Q.gameObject.GetComponent<ItemInformation>().ItemRespawnPosition != 0)
            {
                continue;
            }
            Q.transform.position = ItemArrage();
            Q.transform.rotation = Quaternion.Euler(ItemRandomRotation(Q.transform.rotation));
        }
    }

    private Vector3 ItemArrage()
    {
        float x = Random.Range(CenterTransform.position.x - area, CenterTransform.position.x + area);
        float z = Random.Range(CenterTransform.position.z - area, CenterTransform.position.z + area);

        return new Vector3(x, CenterTransform.position.y, z);
    }

    private Vector3 ItemRandomRotation(Quaternion t)
    {
        return new Vector3(t.x, Random.Range(0, 180), t.z);
    }
}