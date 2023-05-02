using System;
using Managers;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;
    public  GameStateOptions GameStateOption;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ChangeState(GameStateOptions.GenerateGrid);
    }

    public void ChangeState(GameStateOptions newState)
    {
    
        GameStateOption = newState;
        switch (newState)
        {
            case GameStateOptions.GenerateGrid:
                GameManager.Instance.GenerateGrid();
                break;
            case GameStateOptions.SpawnChamps:
                UnitManager.Instance.SpawnChamps();
                break;
            case GameStateOptions.SpawnEnemy:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameStateOptions.VerifyTiles:
                GameManager.Instance.VerifyTiles();
                break;
            case GameStateOptions.ChampTurns:
                break;
            case GameStateOptions.EnemyTurns:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}
public enum GameStateOptions
{
    GenerateGrid = 0,
    SpawnEnemy = 1,
    SpawnChamps = 2,
    VerifyTiles = 3,
    ChampTurns = 4,
    EnemyTurns = 5
}