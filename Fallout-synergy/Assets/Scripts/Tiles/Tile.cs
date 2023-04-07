using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Units;
using Units.Champions;
using Units.Enemies;
using UnityEngine;
 
public abstract class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool isWalkable;//NOT DISPLAYING :(

    public BaseUnit occupiedUnit;
    public bool Walkable => isWalkable && occupiedUnit == null;
    
    public virtual void Init(int x, int y) {}
 
    void OnMouseEnter() {
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }
 
    void OnMouseExit()
    {
        _highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.GameState != GameStateOptions.ChampTurns) return;
        if (occupiedUnit != null)
        {
            if (occupiedUnit.Faction == Faction.Champ)
            {
                UnitManager.Instance.SetSelectedHero((BaseChampion)occupiedUnit);
            }
            else
            {
                if (UnitManager.Instance.SelectedChampion != null)//champ clicks another unit (enemy)
                {
                    var enemy = (BaseEnemy)occupiedUnit;
                    //Action on Enemy for being clicked
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SetSelectedHero(null);
                }
                else
                {
                    if (UnitManager.Instance.SelectedChampion != null)
                    {
                        SetUnit(UnitManager.Instance.SelectedChampion);
                        UnitManager.Instance.SetSelectedHero(null);//35:45
                    }
                }
            }
        }
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.occupiedUnit = null;
            unit.transform.position = transform.position;
            occupiedUnit = unit;
            unit.OccupiedTile = this;
    }
}