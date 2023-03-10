using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class Utility
{
    public static readonly List<Tile> LastChangeTiles = new List<Tile>();

    public static void ChangeTiles(Tile firstTile, Tile secondTile)
    {
        LastChangeTiles.Clear();
        
        secondTile.dot.transform.SetParent(firstTile.tileController.transform);
        secondTile.dot.transform.DOLocalMove(Vector3.zero, .5f).OnComplete(() =>
        {
            EventManager.SwipeDone(firstTile.tileController);
        });


        firstTile.dot.transform.SetParent(secondTile.tileController.transform);
        firstTile.dot.transform.DOLocalMove(Vector3.zero, .5f).OnComplete(() =>
        {
            EventManager.SwipeDone(secondTile.tileController);
        });
        secondTile.tileController.tileDot = firstTile.dot;
        firstTile.tileController.tileDot = secondTile.dot;

        var temp = new Tile();
        temp.tileController = firstTile.tileController;
        temp.dot = firstTile.dot;
        temp.tilePoint = firstTile.tilePoint;
        firstTile.dot = secondTile.dot;
        secondTile.dot = temp.dot;
    }

    public static void ReverseLastChange()
    {
        if (LastChangeTiles.Count == 2)
        {
            var firstTile = LastChangeTiles[1];
            var secondTile = LastChangeTiles[0];
            secondTile.dot.transform.parent = firstTile.tileController.transform;
            secondTile.dot.transform.DOLocalMove(Vector3.zero, .5f);

            firstTile.dot.transform.parent = secondTile.tileController.transform;
            firstTile.dot.transform.DOLocalMove(Vector3.zero, .5f);
            secondTile.tileController.tileDot = firstTile.dot;
            firstTile.tileController.tileDot = secondTile.dot;

            var temp = new Tile();
            temp.tileController = firstTile.tileController;
            temp.dot = firstTile.dot;
            temp.tilePoint = firstTile.tilePoint;
            firstTile.dot = secondTile.dot;
            secondTile.dot = temp.dot;
            LastChangeTiles.Clear();
        }
    }

    public static void FillEmptyTile(Tile emptyTile, Tile filledTile,float delay)
    {
        var temp = filledTile;
        emptyTile.dot = temp.dot;
        emptyTile.dot.transform.SetParent(emptyTile.tileController.transform);
        emptyTile.dot.transform.DOLocalMove(Vector3.zero, .5f);
        emptyTile.tileController.tileDot = temp.tileController.tileDot;

        filledTile.dot = null;
    }
}