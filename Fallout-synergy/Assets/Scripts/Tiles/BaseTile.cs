using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

public class BaseTile
{
    private Grid<BaseTile> grid;
    public int x;
    public int y;
    private Tile _tile;

    public BaseTile(Grid<BaseTile> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public Tile SetTileSprite(Tile tileSprite, Tile tile)
    {
        Tile tempTile = GameObject.Instantiate(tileSprite, new Vector3(x, y)* 10f + Vector3.one * 5f, Quaternion.identity) as Tile;
        grid.TriggerGridObjectChanged(x, y);
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
}
