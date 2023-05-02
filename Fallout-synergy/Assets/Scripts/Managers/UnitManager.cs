using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Units;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager Instance;

        private List<Scri> _units;

        public BaseUnit SelectedUnit;
        private void Awake()
        {
            Instance = this;

            _units = Resources.LoadAll<Scri>("Units").ToList();
        }

        public void SpawnChamps()
        {
            var heroCount = 1;
            for (int i = 0; i < heroCount; i++)
            {
                var randomPrefab = GetRandomUnit<BaseChampion>(Faction.Champ);
                int x, y;
                GameManager.Instance.SetHeroSpawnTile(randomPrefab, out x, out y);
                
                randomPrefab = Instantiate(randomPrefab);
                randomPrefab.x = x;
                randomPrefab.y = y;
                randomPrefab.transform.position = new Vector3(x, y) * 10f + Vector3.one * 5f;
            }
            StateManager.Instance.ChangeState(GameStateOptions.SpawnEnemy);
        }

        public void SpawnEnemies()
        {
            var enemyCount = 1;
            for (int i = 0; i < enemyCount; i++)
            {
                var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
                int x, y;
                GameManager.Instance.SetEnemySpawnTile(randomPrefab, out x, out y);
                
                randomPrefab = Instantiate(randomPrefab);
                randomPrefab.x = x;
                randomPrefab.y = y;
                randomPrefab.transform.position = new Vector3(x, y) * 10f + Vector3.one * 5f;
            }
            StateManager.Instance.ChangeState(GameStateOptions.VerifyTiles);
        }

        public void MoveUnit(Vector3 clickedPos, BaseUnit unit)
        {
            SelectedUnit = unit;
            List<PathNode> moveTo = GameManager.Instance.GetPath(clickedPos, SelectedUnit.x, SelectedUnit.y);
            foreach (var node in moveTo)
            {
                unit.transform.position = new Vector3(node.x, node.y) * 10f + Vector3.one * 5f;
            }
            
        }
        
        private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
        {
            return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().unitPrefab;
        }

        public void SetSelectedHero(BaseUnit unit)
        {
            SelectedUnit = unit;
            Debug.Log("Unit: " + unit.name);
            //MenuManager.Instance.ShowSelectedChamp(SelectedChampion);
        }
    }
}
