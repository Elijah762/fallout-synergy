using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Units;
using UnityEngine;
using UI_Elements;

namespace Managers
{
    public class CombatMoveManager: MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private string action;
        [SerializeField] private BaseUnit unit;

        public void Update()
        {
            if (StateManager.Instance.GameStateOption == GameStateOptions.ChampTurns)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Vector3 clickedPos = UserInput.GetMouseWorldPosition(_camera);
                    if (StateManager.Instance.GameStateOption == GameStateOptions.ChampTurns)
                    {
                        UnitManager.Instance.MoveUnit(clickedPos, unit);

                        StateManager.Instance.GameStateOption = GameStateOptions.EnemyTurns;
                    }
                }
            }
            else if ((StateManager.Instance.GameStateOption == GameStateOptions.EnemyTurns))
            {
                print("Not your turn");
                StateManager.Instance.GameStateOption = GameStateOptions.ChampTurns;
            }
            
        }
    }
}