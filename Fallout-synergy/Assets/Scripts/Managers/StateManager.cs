using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StateManager : MonoBehaviour
    {
        public static StateManager Instance;
        public static GameStateOptions GameStateOption;

        void Awake()
        {
            Instance = this;
        }

        void start()
        {
            ChangeState(GameStateOptions.GenerateGrid);
        }
    
        public static void ChangeState(GameStateOptions newState)
        {
        
            GameStateOption = newState;
            switch (newState)
            {
                case GameStateOptions.GenerateGrid:
                    GridManager.Instance.GenerateGrid();
                    break;
                case GameStateOptions.SpawnEnemy:
                    UnitManager.Instance.SpawnChamps();
                    break;
                case GameStateOptions.SpawnChamps:
                    UnitManager.Instance.SpawnEnemies();
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
        ChampTurns = 3,
        EnemyTurns = 4
    }
}