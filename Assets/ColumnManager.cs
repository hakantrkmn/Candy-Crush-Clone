using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class ColumnManager : MonoBehaviour
{
    
    public List<TileList> tileList;
    int idCount;
    public GameObject dotPrefab;
    public void CheckColumnForFill()
    {
        for (int i = 0; i < tileList.Count; i++)
        {
            for (int j = 0; j < tileList[i].tile.Count; j++)
            {
                if (tileList[i].tile[j].dot!=null)
                {
                    

                    if (idCount!=j)
                    {
                        
                        var temp = tileList[i].tile[j];
                        tileList[i].tile[idCount].dot = temp.dot;
                        tileList[i].tile[idCount].dot.transform.parent = tileList[i].tile[idCount].tileController.transform;
                        tileList[i].tile[idCount].dot.transform.DOLocalMove(Vector3.zero,2f);

                        tileList[i].tile[j].dot = null;


                    }
                    idCount++;

                }
            }
           
            for (int k = idCount; k < tileList[i].tile.Count; k++)
            {
                var obj = Instantiate(dotPrefab, tileList[i].tile[k].tilePoint, quaternion.identity,tileList[i].tile[k].tileController.transform);
                tileList[i].tile[k].dot = obj.GetComponent<DotController>();
            }

            idCount = 0;
        }

        
    }
}
