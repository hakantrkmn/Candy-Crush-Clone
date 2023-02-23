using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using Unity.VisualScripting;
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

        var height = 1920 - (tilePrefab.GetComponentInChildren<RectTransform>().rect.height*tilePrefab.transform.localScale.y);
        var width = 1080 - (tilePrefab.GetComponentInChildren<RectTransform>().rect.width*tilePrefab.transform.localScale.x);
        var offset = new Vector3((tilePrefab.GetComponentInChildren<RectTransform>().rect.width*tilePrefab.transform.localScale.x) / 2,
            (tilePrefab.GetComponentInChildren<RectTransform>().rect.height*tilePrefab.transform.localScale.y) / 2, 0);
        

        for (int i = 0; i < columnAmount; i++)
        {
            for (int j = 0; j < rowAmount; j++)
            {
                var pos = offset+ new Vector3(j * (width / (rowAmount-1)), i * (height / (columnAmount-1)), 0);
                var tile = Instantiate(tilePrefab, pos, quaternion.identity,transform);
                var tempTile = new Tile();
                tempTile.tilePoint = tile.transform.position;
                tempTile.tileController = tile.GetComponent<TileController>();
                tempTile.tileController.columnPlace = i;
                tempTile.tileController.columnId = j;
                columnManager.tileList[j].tile.Add(tempTile);
                rowManager.tileList[i].tile.Add(tempTile);
                
                tiles.Add(tile);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < columnAmount; i++)
        {
            var tempTileList = new TileList();

            columnManager.tileList.Add(tempTileList);
        }
        for (int i = 0; i < rowAmount; i++)
        {
            var tempTileList = new TileList();

            rowManager.tileList.Add(tempTileList);
        }

    }

    [Button]
    public void FillColumns()
    {
        columnManager.CheckColumnForFill();
        
    }

}
