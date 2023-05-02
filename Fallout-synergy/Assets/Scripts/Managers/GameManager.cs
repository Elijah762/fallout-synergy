using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Elements;
using Units;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public string firstLevel;
    public static GameManager Instance;
    [SerializeField] private Camera _camera;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private List<Tile> tiles;

    private int heroLocX;
    private int heroLocY;
    private int enemyLocX;
    private int enemyLocY;
    private PathFinding pathFinding;
    private TileMap _tileMap;
    
    
    void Awake() {
        Instance = this;
    }

    public void GenerateGrid()
    {
        _tileMap = new TileMap(width, height);
        _tileMap.SetMap(tiles);
        _camera.transform.position = new Vector3((float)5/2 -0.5f, (float)5 / 2 - 0.5f,-10);
        pathFinding = new PathFinding(width, height);

        StateManager.Instance.ChangeState(GameStateOptions.SpawnChamps);
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

        if (Input.GetKeyDown(KeyCode.S))
        {
            _tileMap.Save();
            Debug.Log("Saved");
        }
    }

    public void SetHeroSpawnTile(BaseUnit unit, out int x, out int y)
    {
        x = Random.Range(0, width / 2);
        y = Random.Range(0, height);
        
        _tileMap.SetTileUnit(unit, x, y);
        _tileMap.SetTileMapTile(new Vector3(x, y) * 10f + Vector3.one * 5f, tiles[0]);
        heroLocX = x;
        heroLocY = y;
        Debug.Log("OUT " + x + ", " + y);
    }
    public void SetEnemySpawnTile(BaseUnit unit, out int x, out int y)
    {
        x = Random.Range(width / 2 + 1, width);
        y = Random.Range(0, height);
        
        _tileMap.SetTileUnit(unit, x, y);
        _tileMap.SetTileMapTile(new Vector3(x, y) * 10f + Vector3.one * 5f, tiles[0]);
        enemyLocX = x;
        enemyLocY = y;
        Debug.Log("OUT " + x + ", " + y);   
    }

    public void VerifyTiles()
    {
        LineGrids();
        var result = pathFinding.FindPath(heroLocX, heroLocY, enemyLocX, enemyLocY);
        if (result == null)
        {
            SceneManager.LoadScene(firstLevel);
        }
        else
            StateManager.Instance.ChangeState(GameStateOptions.ChampTurns);
    }
    
    private void LineGrids()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                pathFinding.GetNode(x, y).isWalkable = _tileMap.GetTile(x, y).isWalkable;
            }
        }
    }
} 
