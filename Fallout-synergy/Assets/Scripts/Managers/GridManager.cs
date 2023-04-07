using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int width, height;
 
    [SerializeField] private Tile grassTile, mountainTile;
 
    [SerializeField] private Transform cam;
 
    private Dictionary<Vector2, Tile> _tiles;

    void Awake() {
        Instance = this;
    }
 
    public void GenerateGrid() {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                var randomTile = Random.Range(0, 6) == 3 ? mountainTile : grassTile;
                var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                
                spawnedTile.Init(x, y);
 
 
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
        
        cam.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 - 0.5f,-10);
        StateManager.Instance.ChangeState(GameStateOptions.SpawnChamps);

    }

    public Tile GetHeroSpawnTile()
    {
        return _tiles.Where(t=>t.Key.x < width / 2 && t.Value.Walkable).OrderBy(t=>Random.value).First().Value;
    }
    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t=>t.Key.x > width / 2 && t.Value.Walkable).OrderBy(t=>Random.value).First().Value;
    }
    
    public Tile GetTileAtPosition(Vector2 pos) {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}