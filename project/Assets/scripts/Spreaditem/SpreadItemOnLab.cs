using UnityEngine;

public class SpreadItemOnLab : MonoBehaviour
{
    [SerializeField] private int area = 0;
    [SerializeField] private ItemRegistrator itemRegistrator = null;

    // Use this for initialization
    void Start()
    {
        if (itemRegistrator == null)
        {
            Debug.LogError("itemRegistrator is null");
        }
        putItemSpread();
    }

    private void putItemSpread()
    {
        foreach (var Q in itemRegistrator.ItemQ)
        {
            if (Q.gameObject.GetComponent<ItemInformation>().ItemRespawnPosition != 0)
            {
                continue;
            }

            /**/
            float x = Random.Range(-area, area);
            float z = Random.Range(-area, area);

            Vector2 pos1 = new Vector2(x, z);
            Vector2 pos2 = new Vector2(0, 0);

            while (Vector2.Distance(pos1, pos2) < 15 || Vector2.Distance(pos1, pos2) > 45)
            {
                x = Random.Range(-area, area);
                z = Random.Range(-area, area);

                pos1 = new Vector2(x, z);
                pos2 = new Vector2(0, 0);
            }

            float y = Random.Range(0, 30.0f);
            Q.transform.position = new Vector3(x, y, z);

            /**/
        }

    }
}