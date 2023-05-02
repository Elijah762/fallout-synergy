using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Elements;

public class Test : MonoBehaviour
{
    private Grid<HeatMapGridObject> grid;
    private Grid<StringGridObject> stringGrid;
    [SerializeField] private Camera _camera;
    private PathFinding pathFinding;
    private void Start()
    {
        pathFinding = new PathFinding(3, 3);
    }

    private void Update()
    {
        Vector3 pos = UserInput.GetMouseWorldPosition(_camera);
        if (Input.GetMouseButtonDown(0))
        {
            pathFinding.GetGrid().GetXY(pos, out int x, out int y);
            List<PathNode> path= pathFinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            pathFinding.GetGrid().GetXY(pos, out int x, out int y);
            pathFinding.GetNode(x, y).isWalkable = !pathFinding.GetNode(x, y).isWalkable;
        }
    }
}

public class HeatMapGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;
    private int value;
    private Grid<HeatMapGridObject> grid;
    private int x, y;
    public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}

public class StringGridObject
{
    private Grid<StringGridObject> grid;
    private int x;
    private int y;
    private string letters;
    private string numbers;

    public StringGridObject(Grid<StringGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        letters = "";
        numbers = "";
    }
    
    public void AddLetter(String letter)
    {
        letters += letter;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void AddNumber(String number)
    {
        numbers += number;
        grid.TriggerGridObjectChanged(x, y);
        
    }
    public override string ToString()
    {
        return letters + "\n" + numbers;
    }
}
