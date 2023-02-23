using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public int columnPlace;
    public int columnId;
    public ColumnManager columnManager;

    public TileController leftTileController;
    public TileController rightTileController;
    public TileController upTileController;
    public TileController downTileController;

    // Start is called before the first frame update
    void Start()
    {
        columnManager = GetComponentInParent<ColumnManager>();
        SetAroundTiles();
    }

    public void SetAroundTiles()
    {
        if (columnId!=0)
        {
            leftTileController = columnManager.tileList[columnId - 1].tile[columnPlace].tileController;
            
        }

        if (columnId!=9)
        {
            rightTileController = columnManager.tileList[columnId + 1].tile[columnPlace].tileController;
        }

        if (columnPlace!=0)
        {
            downTileController = columnManager.tileList[columnId ].tile[columnPlace-1].tileController;

        }

        if (columnPlace!=9)
        {
            upTileController = columnManager.tileList[columnId ].tile[columnPlace+1].tileController;

        }
    }
    
    
}
