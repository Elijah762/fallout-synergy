using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap
{
    private Grid<BaseTile> grid;
    private Tile grassTile;
    private Tile[, ] tileGrid;
    public TileMap(int width, int height)
    {
        grid = new Grid<BaseTile>(width, height, 10f, Vector3.zero,
            (Grid<BaseTile> g, int x, int y) => new BaseTile(g, x, y));

        tileGrid = new Tile[width, height];
    }

    public void SetTileMapTile(Vector3 pos, Tile tileSprite)
    {
        BaseTile baseTile = grid.GetGridObject(pos);
        Vector3 temp = pos / 10f - Vector3.one / 5f;
        if (baseTile != null)
        {
            Tile newTile = baseTile.SetTileSprite(tileSprite, grassTile);
            Debug.Log(newTile + " " + (int)temp.x + " " + (int)temp.y);
            tileGrid[(int)temp.x, (int)temp.y] = newTile;

        }
    }

    public void SetMap(List<Tile> tiles)
    {
        for (int x = 0; x < grid.GetHeight(); x++)
        {
            for (int y = 0; y < grid.GetWidth(); y++)
            {
                int randomIndex = Random.Range(0, tiles.Count);
                SetTileMapTile(new Vector3(x, y) * 10f + Vector3.one * 5f, tiles[randomIndex]);
            }
        }
    }
    
    public Tile GetTile(int x, int y)
    {
        return tileGrid[x, y];
    }
    
}
