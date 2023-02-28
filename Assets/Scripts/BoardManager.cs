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

    private float canvasWidth;
    private float canvasHeight;


    
    private void OnEnable()
    {
        EventManager.FillTheColumns += FillTheColumns;
    }

    private void FillTheColumns()
    {
        FillColumns();
    }

    private void OnDisable()
    {
        EventManager.FillTheColumns -= FillTheColumns;

    }

    [Button]
    public void CreateBoard()
    {
        
        foreach (var tile in tiles)
        {
            Destroy(tile);
        }
        tiles.Clear();
        var tileXSize = canvasWidth / (float)rowAmount;
        var tileYSize = canvasHeight / (float)columnAmount;
        tilePrefab.transform.localScale = new Vector3(tileXSize / (rowAmount*columnAmount), tileYSize/(rowAmount*columnAmount), 1);

        var height = canvasHeight - (tilePrefab.GetComponentInChildren<RectTransform>().rect.height*tilePrefab.transform.localScale.y);
        var width = canvasWidth - (tilePrefab.GetComponentInChildren<RectTransform>().rect.width*tilePrefab.transform.localScale.x);
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
    
    void Start()
    {
        canvasHeight = transform.parent.GetComponent<RectTransform>().rect.height;
        canvasWidth = transform.parent.GetComponent<RectTransform>().rect.width;

        for (int i = 0; i < columnAmount; i++)
        {
            var tempTileList = new TileList();

            columnManager.tileList.Add(tempTileList);
        }
        for (int i = 0; i < rowAmount; i++)
        {
            var tempTileList2 = new TileList();

            rowManager.tileList.Add(tempTileList2);
        }

    }

    [Button]
    public void FillColumns()
    {
        columnManager.CheckColumnForFill();
        transform.parent.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;

    }

}
