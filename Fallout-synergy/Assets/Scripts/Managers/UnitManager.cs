using System;
using System.Collections.Generic;
using System.Linq;
using Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager Instance;

        private List<Scri> _units;

        public BaseChampion SelectedChampion;
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
                randomPrefab.transform.position = new Vector3(x, y) * 10f + Vector3.one * 5f;
            }
            StateManager.Instance.ChangeState(GameStateOptions.VerifyTiles);
        }
        
        private T GetRandomUnit<T>(Faction faction) where T : BaseUnit
        {
            return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().unitPrefab;
        }

        public void SetSelectedHero(BaseChampion champion)
        {
            SelectedChampion = champion;
            Debug.Log("Champion: " + champion.name);
            //MenuManager.Instance.ShowSelectedChamp(SelectedChampion);
        }
    }
}