using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject selectedHeroObject, tileObject, tileUnitObject;
    void Awake()
    {
        Instance = this;
    }

    public void ShowTileInfo(Tile tile)
    {
        if (tile == null)
        {
            tileObject.SetActive(false);
            tileUnitObject.SetActive(false);
            return;
        }
        
        tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        tileObject.SetActive(true);

        if (tile.occupiedUnit)
        {
            tileUnitObject.GetComponentInChildren<Text>().text = tile.occupiedUnit.UnitName;
            tileUnitObject.SetActive(true);
        }
    }
    public void ShowSelectedChamp(BaseChampion champ)
    {
        if (champ == null)
        {
            selectedHeroObject.SetActive(false);
            return;
        }
        selectedHeroObject.GetComponentInChildren<Text>().text = champ.UnitName;
        selectedHeroObject.SetActive(true);
    }
}
