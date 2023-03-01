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

    TileController leftTileController;
    TileController rightTileController;
    TileController upTileController;
    TileController downTileController;

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
        if (tile == this)
        {
            CheckCombo(true);
        }
    }

    void Start()
    {
        columnManager = GetComponentInParent<ColumnManager>();
        SetAroundTiles();
    }

    void SetAroundTiles()
    {
        if (columnId != 0)
        {
            leftTileController = columnManager.tileList[columnId - 1].tile[columnPlace].tileController;
        }

        if (columnId != 9)
        {
            rightTileController = columnManager.tileList[columnId + 1].tile[columnPlace].tileController;
        }

        if (columnPlace != 0)
        {
            downTileController = columnManager.tileList[columnId].tile[columnPlace - 1].tileController;
        }

        if (columnPlace != 9)
        {
            upTileController = columnManager.tileList[columnId].tile[columnPlace + 1].tileController;
        }
    }


    public void SwipeAction(SwipeDirections swipeDir)
    {
        var thisTile = columnManager.tileList[columnId].tile[columnPlace];

        if (swipeDir == SwipeDirections.Left)
        {
            if (leftTileController != null)
            {
                var swipedTile = columnManager.tileList[columnId - 1].tile[columnPlace];
                Utility.ChangeTiles(thisTile, swipedTile);
            }
        }
        else if (swipeDir == SwipeDirections.Right)
        {
            if (rightTileController != null)
            {
                var swipedTile = columnManager.tileList[columnId + 1].tile[columnPlace];
                Utility.ChangeTiles(thisTile, swipedTile);
            }
        }
        else if (swipeDir == SwipeDirections.Up)
        {
            if (upTileController != null)
            {
                var swipedTile = columnManager.tileList[columnId].tile[columnPlace + 1];
                Utility.ChangeTiles(thisTile, swipedTile);
            }
        }
        else if (swipeDir == SwipeDirections.Down)
        {
            if (downTileController != null)
            {
                var swipedTile = columnManager.tileList[columnId].tile[columnPlace - 1];
                Utility.ChangeTiles(thisTile, swipedTile);
            }
        }
    }

    void CheckSideForCombo(SwipeDirections side, List<DotController> dotList)
    {
        if (side == SwipeDirections.Left)
        {
            if (leftTileController)
            {
                if (leftTileController.tileDot.dotColor == tileDot.dotColor)
                {
                    dotList.Add(leftTileController.tileDot);
                    leftTileController.CheckSideForCombo(SwipeDirections.Left, dotList);
                }
            }
        }
        else if (side == SwipeDirections.Right)
        {
            if (rightTileController)
            {
                if (rightTileController.tileDot.dotColor == tileDot.dotColor)
                {
                    dotList.Add(rightTileController.tileDot);
                    rightTileController.CheckSideForCombo(SwipeDirections.Right, dotList);
                }
            }
        }
        else if (side == SwipeDirections.Up)
        {
            if (upTileController)
            {
                if (upTileController.tileDot.dotColor == tileDot.dotColor)
                {
                    dotList.Add(upTileController.tileDot);
                    upTileController.CheckSideForCombo(SwipeDirections.Up, dotList);
                }
            }
        }
        else if (side == SwipeDirections.Down)
        {
            if (downTileController)
            {
                if (downTileController.tileDot.dotColor == tileDot.dotColor)
                {
                    dotList.Add(downTileController.tileDot);
                    downTileController.CheckSideForCombo(SwipeDirections.Down, dotList);
                }
            }
        }
    }

    public void CheckCombo(bool isSwipe)
    {
        horizontalComboDots.Add(tileDot);
        verticalComboDots.Add(tileDot);

        if (leftTileController)
        {
            if (leftTileController.tileDot.dotColor == tileDot.dotColor)
            {
                horizontalComboDots.Add(leftTileController.tileDot);
                leftTileController.CheckSideForCombo(SwipeDirections.Left, horizontalComboDots);
            }
        }

        if (rightTileController)
        {
            if (rightTileController.tileDot.dotColor == tileDot.dotColor)
            {
                horizontalComboDots.Add(rightTileController.tileDot);
                rightTileController.CheckSideForCombo(SwipeDirections.Right, horizontalComboDots);
            }
        }

        if (upTileController)
        {
            if (upTileController.tileDot.dotColor == tileDot.dotColor)
            {
                verticalComboDots.Add(upTileController.tileDot);
                upTileController.CheckSideForCombo(SwipeDirections.Up, verticalComboDots);
            }
        }

        if (downTileController)
        {
            if (downTileController.tileDot.dotColor == tileDot.dotColor)
            {
                verticalComboDots.Add(downTileController.tileDot);
                downTileController.CheckSideForCombo(SwipeDirections.Down, verticalComboDots);
            }
        }

        if (horizontalComboDots.Count < 3 && verticalComboDots.Count < 3)
        {
            if (isSwipe)
            {
                Utility.LastChangeTiles.Add(columnManager.tileList[columnId].tile[columnPlace]);
                Utility.ReverseLastChange();
            }

            horizontalComboDots.Clear();
            verticalComboDots.Clear();
        }
        else
        {
            ClearCombos();
        }
    }

    void ClearCombos()
    {
        if (horizontalComboDots.Count >= 3)
        {
            foreach (var dot in horizontalComboDots)
            {
                Destroy(dot.gameObject);
            }
        }

        if (verticalComboDots.Count >= 3)
        {
            foreach (var dot in verticalComboDots)
            {
                Destroy(dot.gameObject);
            }
        }

        horizontalComboDots.Clear();
        verticalComboDots.Clear();
        DOVirtual.Float(0, 1, .2f, (x) => { }).OnComplete(() => { EventManager.FillTheColumns(); });
    }
}