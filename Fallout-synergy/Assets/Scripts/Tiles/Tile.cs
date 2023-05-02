using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Units;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] public bool isWalkable;
    [SerializeField] private bool isSolid;
    
    public BaseUnit occupiedUnit;
    public bool Walkable => isWalkable && occupiedUnit == null;
    public bool Solid => isSolid && occupiedUnit == null;
    
    public virtual void Init(int x, int y) {}
 
    void OnMouseEnter() {
        _highlight.SetActive(true);
        //MenuManager.Instance.ShowTileInfo(this);
    }
 
    void OnMouseExit()
    {
        _highlight.SetActive(false);
        //MenuManager.Instance.ShowTileInfo(null);
    }

    /*void OnMouseDown() {
        if(StateManager.Instance.GameStateOption != GameStateOptions.ChampTurns) return;

        if (occupiedUnit != null) {
            if (occupiedUnit.Faction == Faction.Champ)
            {
                UnitManager.Instance.SetSelectedHero((BaseChampion)occupiedUnit);
            }
            else {
                if (UnitManager.Instance.SelectedChampion != null) {
                    var enemy = (BaseEnemy)occupiedUnit;
                    Destroy(enemy.gameObject);
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }
        }
        else {
            if (UnitManager.Instance.SelectedChampion != null) {
                SetUnit(UnitManager.Instance.SelectedChampion, );
                UnitManager.Instance.SetSelectedHero(null);
            }
        }

    }*/

    

    public void SetUnit(BaseUnit unit, Vector3 pos)
    {
        if (!isWalkable) return;
        if (unit.OccupiedTile != null) unit.OccupiedTile.occupiedUnit = null;

        unit.transform.position = pos;
        occupiedUnit = unit;
        unit.OccupiedTile = this;
    }
}