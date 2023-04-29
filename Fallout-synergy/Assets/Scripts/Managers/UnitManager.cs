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
                var spawnedHero = Instantiate(randomPrefab);
                var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();
                randomSpawnTile.SetUnit(spawnedHero);
            }
            StateManager.Instance.ChangeState(GameStateOptions.SpawnEnemy);
        }

        public void SpawnEnemies()
        {
            var enemyCount = 1;
            for (int i = 0; i < enemyCount; i++)
            {
                var randomPrefab = GetRandomUnit<BaseEnemy>(Faction.Enemy);
                var spawnedEnemy = Instantiate(randomPrefab);
                var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();
                randomSpawnTile.SetUnit(spawnedEnemy);
            }
            StateManager.Instance.ChangeState(GameStateOptions.ChampTurns);
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