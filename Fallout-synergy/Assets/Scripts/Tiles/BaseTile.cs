using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

public class BaseTile
{
    public int x;
    public int y;
    private Tile _tile;

    public BaseTile(Grid<BaseTile> grid, int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Tile SetTileSprite(Tile tileSprite, Tile tile)
    {
        Tile tempTile = GameObject.Instantiate(tileSprite, new Vector3(x, y)* 10f + Vector3.one * 5f, Quaternion.identity) as Tile;
        _tile = tempTile;
        return tempTile;
    }

    public override string ToString()
    {
        return "";
    }

    public enum TileSprite
    {
        None, 
        Grass,
    }
    
    public SaveTiles SaveTile()
    {
        Debug.Log("Saving " + x + " " + y);
        return new SaveTiles()
        {
            x = x,
            y = y,
        };
    }
}

[Serializable]
public class SaveTiles
{
    public int x;
    public int y;
}

