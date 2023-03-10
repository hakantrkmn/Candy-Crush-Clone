using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColumnManager : MonoBehaviour
{
    public List<TileList> tileList;
    private int idCount;
    public List<GameObject> dotPrefabs;

    public void FillTileIfEmpty()
    {
        for (int i = 0; i < tileList.Count; i++)
        {
            for (int j = 0; j < tileList[i].tile.Count; j++)
            {
                if (tileList[i].tile[j].dot != null)
                {
                    if (idCount != j)
                    {
                        Utility.FillEmptyTile(tileList[i].tile[idCount], tileList[i].tile[j],j*.2f);
                    }

                    idCount++;
                }
            }

            for (int k = idCount; k < tileList[i].tile.Count; k++)
            {
                var obj = Instantiate(dotPrefabs[Random.Range(0, dotPrefabs.Count)],
                    tileList[i].tile[tileList[i].tile.Count - 1].tilePoint + new Vector3(0, .5f, 0), quaternion.identity,
                    tileList[i].tile[k].tileController.transform);
                obj.transform.DOLocalMove(Vector3.zero, .5f);
                tileList[i].tile[k].tileController.tileDot = obj.GetComponent<DotController>();
                tileList[i].tile[k].dot = obj.GetComponent<DotController>();
            }

            idCount = 0;
        }
    }
}