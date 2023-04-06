using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
 
public abstract class Tile : MonoBehaviour {
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool isWalkable;//NOT DISPLAYING :(

    public BaseUnit occupiedUnit;
    public bool Walkable => isWalkable && occupiedUnit == null;
    
    public virtual void Init(int x, int y) {}
 
    void OnMouseEnter() {
        _highlight.SetActive(true);
    }
 
    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupiedTile != null) unit.OccupiedTile.occupiedUnit = null;
            unit.transform.position = transform.position;
            occupiedUnit = unit;
            unit.OccupiedTile = this;
    }
}