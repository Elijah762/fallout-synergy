using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StateManager : MonoBehaviour
    {
        public static StateManager Instance;
        public static GameStateOptions currentState;

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
        
            currentState = newState;
            switch (newState)
            {
                case GameStateOptions.GenerateGrid:
                    GridManager.Instance.GenerateGrid();
                    break;
                case GameStateOptions.SpawnEnemy:
                    break;
                case GameStateOptions.SpawnChamps:
                    break;
                case GameStateOptions.ChampTurns:
                    break;
                case GameStateOptions.EnemyTurns:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
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
}