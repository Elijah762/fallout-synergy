using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Elements;

public class Test : MonoBehaviour
{
    private Grid<HeatMapGridObject> grid;
    [SerializeField] private Camera _camera;
    private PathFinding pathFinding;
    private TileMap _tileMap;
    [SerializeField] private List<Tile> tiles;
    private void Start()
    {
        _tileMap = new TileMap(5, 5);
        _tileMap.SetMap(tiles);
        _camera.transform.position = new Vector3((float)5/2 -0.5f, (float)5 / 2 - 0.5f,-10);
        pathFinding = new PathFinding(5, 5);
        LineGrids();
        
    }

    private void Update()
    {
        Vector3 pos = UserInput.GetMouseWorldPosition(_camera);
        if (Input.GetMouseButtonDown(0))
        {
            pathFinding.GetGrid().GetXY(pos, out int x, out int y);
            List<PathNode> path= pathFinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            pathFinding.GetGrid().GetXY(pos, out int x, out int y);
            pathFinding.GetNode(x, y).isWalkable = !pathFinding.GetNode(x, y).isWalkable;
            _tileMap.SetTileMapTile(pos, tiles[1]);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _tileMap.Save();
            Debug.Log("Saved");
        }
    }

    private void LineGrids()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                Debug.Log(pathFinding.GetNode(x, y).isWalkable);
                pathFinding.GetNode(x, y).isWalkable = _tileMap.GetTile(x, y).Walkable;
            }
        }
    }
}

public class HeatMapGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;
    private int value;
    private Grid<HeatMapGridObject> grid;
    private int x, y;
    public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
