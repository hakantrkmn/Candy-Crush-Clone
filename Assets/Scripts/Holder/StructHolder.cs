using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Tile
{
    public TileController tileController;
    public Vector3 tilePoint;
}
[Serializable]
public class TileList
{
    public List<Tile> tile;
}

