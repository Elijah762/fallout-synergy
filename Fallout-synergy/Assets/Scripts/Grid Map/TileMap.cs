using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Elements;
using Units;

public class TileMap
{
    private Grid<BaseTile> grid;
    private Tile grassTile;
    public TileMap(int width, int height)
    {
        grid = new Grid<BaseTile>(width, height, 10f, Vector3.zero,
            (Grid<BaseTile> g, int x, int y) => new BaseTile(g, x, y));
    }
    
    public void SetMap(List<Tile> tiles)
    {
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                int randomIndex = Random.Range(0, tiles.Count);
                SetTileMapTile(new Vector3(x, y) * 10f + Vector3.one * 5f, tiles[randomIndex]);
            }
        }
    }
    
    public void DestroyMap()
    {
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                DestroyTile(new Vector3(x, y) * 10f + Vector3.one * 5f);
            }
        }
    }
    
    public void SetTileMapTile(Vector3 pos, Tile tileSprite)
    {
        BaseTile baseTile = grid.GetGridObject(pos);
        if (baseTile != null)
        {
            baseTile.SetTileSprite(tileSprite);
        }
    }
    
    public void DestroyTile(Vector3 pos)
    {
        BaseTile baseTile = grid.GetGridObject(pos);
        baseTile.Delete();
    }
    
    public Tile GetTile(int x, int y)
    {
        BaseTile baseTile = grid.GetGridObject(x, y);
        return baseTile.GetTile();
    }

    public void SetTileUnit(BaseUnit unit, int x, int y)
    {
        Debug.Log("Setting hero at " + x + ", " + y);
        BaseTile baseTile = grid.GetGridObject(x, y);
        baseTile.SetTileUnit(unit);
    }

    public void Save()
    {
        List<SaveTiles> tileSaveList = new List<SaveTiles>();
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                BaseTile baseTile = grid.GetGridObject(x, y);
                tileSaveList.Add(baseTile.SaveTile());
            }
        }
        
        Save_Load.SaveObject(tileSaveList.ToArray());
    }
}
