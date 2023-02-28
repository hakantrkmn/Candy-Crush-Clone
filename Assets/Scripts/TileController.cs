using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public DotController tileDot;
    public int columnPlace;
    public int columnId;
    public ColumnManager columnManager;

    public TileController leftTileController;
    public TileController rightTileController;
    public TileController upTileController;
    public TileController downTileController;

    public List<DotController> horizontalComboDots;
    public List<DotController> verticalComboDots;


    private void OnEnable()
    {
        EventManager.SwipeDone += SwipeDone;
    }

    private void OnDisable()
    {
        EventManager.SwipeDone -= SwipeDone;
    }

    private void SwipeDone(TileController tile)
    {
        if (tile==this)
        {
            CheckCombo();
        }
    }

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


    public void SwipeAction(SwipeDirections swipeDir)
    {
        var thisTile = columnManager.tileList[columnId].tile[columnPlace];

        if (swipeDir==SwipeDirections.Left)
        {
            if (leftTileController!=null)
            {
                var swipedTile = columnManager.tileList[columnId-1].tile[columnPlace];
                Utility.ChangeTiles(thisTile,swipedTile);
            }
            

        }
        else if (swipeDir==SwipeDirections.Right)
        {
            if (rightTileController!=null)
            {
                var swipedTile = columnManager.tileList[columnId+1].tile[columnPlace];
                Utility.ChangeTiles(thisTile,swipedTile);

            }

        }
        else if (swipeDir==SwipeDirections.Up)
        {
            if (upTileController!=null)
            {
                var swipedTile = columnManager.tileList[columnId].tile[columnPlace+1];
                Utility.ChangeTiles(thisTile,swipedTile);

            }

        }
        else if (swipeDir==SwipeDirections.Down)
        {
            if (downTileController!=null)
            {
                var swipedTile = columnManager.tileList[columnId].tile[columnPlace-1];
                Utility.ChangeTiles(thisTile,swipedTile);

            }

        }
    }

    public void CheckSideForCombo(SwipeDirections side,List<DotController> dotList)
    {
        if (side==SwipeDirections.Left)
        {
            if (leftTileController)
            {
                if (leftTileController.tileDot.dotColor==tileDot.dotColor)
                {
                    dotList.Add(leftTileController.tileDot);
                    leftTileController.CheckSideForCombo(SwipeDirections.Left,dotList);

                }
            }
        }
        else if (side==SwipeDirections.Right)
        {
            if (rightTileController)
            {
                if (rightTileController.tileDot.dotColor==tileDot.dotColor)
                {
                    dotList.Add(rightTileController.tileDot);
                    rightTileController.CheckSideForCombo(SwipeDirections.Right,dotList);

                }
            }
        }
        else if (side==SwipeDirections.Up)
        {
            if (upTileController)
            {
                if (upTileController.tileDot.dotColor==tileDot.dotColor)
                {
                    dotList.Add(upTileController.tileDot);
                    upTileController.CheckSideForCombo(SwipeDirections.Up,dotList);

                }
            }
        }
        else if (side==SwipeDirections.Down)
        {
            if (downTileController)
            {
                if (downTileController.tileDot.dotColor==tileDot.dotColor)
                {
                    dotList.Add(downTileController.tileDot);
                    downTileController.CheckSideForCombo(SwipeDirections.Down,dotList);

                }
            }
        }
    }

    [Button]
    public void CheckCombo()
    {
        horizontalComboDots.Add(tileDot);
        verticalComboDots.Add(tileDot);

        if (leftTileController)
        {
            if (leftTileController.tileDot.dotColor==tileDot.dotColor)
            {
                horizontalComboDots.Add(leftTileController.tileDot);
                leftTileController.CheckSideForCombo(SwipeDirections.Left,horizontalComboDots);
            }
        }
        if (rightTileController)
        {
            if (rightTileController.tileDot.dotColor==tileDot.dotColor)
            {
                horizontalComboDots.Add(rightTileController.tileDot);
                rightTileController.CheckSideForCombo(SwipeDirections.Right,horizontalComboDots);
            }
        }

        if (upTileController)
        {
            if (upTileController.tileDot.dotColor==tileDot.dotColor)
            {
                verticalComboDots.Add(upTileController.tileDot);
                upTileController.CheckSideForCombo(SwipeDirections.Up,verticalComboDots);
            }
        }
        if (downTileController)
        {
            if (downTileController.tileDot.dotColor==tileDot.dotColor)
            {
                verticalComboDots.Add(downTileController.tileDot);
                downTileController.CheckSideForCombo(SwipeDirections.Down,verticalComboDots);
            }
        }

        if (horizontalComboDots.Count<3&& verticalComboDots.Count<3)
        {
            Utility.lastChangeTiles.Add(columnManager.tileList[columnId].tile[columnPlace]);
            Utility.ReverseLastChange();
        }
        else
        {
            ClearCombos();

        }
        
    }
    [Button]
    public void ClearCombos()
    {
        if (horizontalComboDots.Count>=3)
        {
            foreach (var dot in horizontalComboDots)
            {
                Destroy(dot.gameObject);
            }
        }
        if (verticalComboDots.Count>=3)
        {
            foreach (var dot in verticalComboDots)
            {
                Destroy(dot.gameObject);
            }
        }
        horizontalComboDots.Clear();
        verticalComboDots.Clear();
        DOVirtual.Float(0, 1, .5f, (x) =>
        {

        }).OnComplete(() =>
        {
            EventManager.FillTheColumns();

        });
    }
    
}
