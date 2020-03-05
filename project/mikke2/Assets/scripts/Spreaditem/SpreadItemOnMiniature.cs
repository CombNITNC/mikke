using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadItemOnMiniature : MonoBehaviour
{
    [SerializeField] private int area;
    [SerializeField] private ItemRegistrator itemRegistrator;

    // Use this for initialization
    void Start()
    {
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


