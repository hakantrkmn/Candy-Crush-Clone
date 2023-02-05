using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public RowManager rowManager;
    public ColumnManager columnManager;
    public GameObject tilePrefab;

    public int rowAmount;

    public int columnAmount;

    public List<GameObject> tiles;

    [Button]
    public void CreateBoard()
    {
        foreach (var tile in tiles)
        {
            Destroy(tile);
        }
        tiles.Clear();
        var tileXSize = 1080 / (float)rowAmount;
        var tileYSize = 1920 / (float)columnAmount;
        tilePrefab.transform.localScale = new Vector3(tileXSize / 100, tileYSize/100, 1);

        var height = 1920 - (tilePrefab.GetComponentInChildren<BoxCollider2D>().size.y*tilePrefab.transform.localScale.y);
        var width = 1080 - (tilePrefab.GetComponentInChildren<BoxCollider2D>().size.x*tilePrefab.transform.localScale.x);
        var offset = new Vector3((tilePrefab.GetComponentInChildren<BoxCollider2D>().size.x*tilePrefab.transform.localScale.x) / 2,
            (tilePrefab.GetComponentInChildren<BoxCollider2D>().size.y*tilePrefab.transform.localScale.y) / 2, 0);
        

        for (int i = 0; i < columnAmount; i++)
        {
            for (int j = 0; j < rowAmount; j++)
            {
                var pos = offset+ new Vector3(j * (width / (rowAmount-1)), i * (height / (columnAmount-1)), 0);
                var tile = Instantiate(tilePrefab, pos, quaternion.identity,transform);
                rowManager.tileList[j].tile[i].tileController = tile.GetComponent<TileController>();
                rowManager.tileList[j].tile[i].tilePoint = tile.transform.position;
                columnManager.tileList[i].tile[j].tileController = tile.GetComponent<TileController>();
                columnManager.tileList[i].tile[j].tilePoint = tile.transform.position;
                tiles.Add(tile);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
