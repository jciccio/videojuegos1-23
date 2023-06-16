using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem 
{

    private int _height;
    private int _width;

    private float _cellSize;
    public GridSystem(int width, int height, float cellSize){
        _width = width;
        _height = height;
        _cellSize = cellSize;
        for (int i = 0 ; i < width ; i++){
            for (int j = 0; j < height ; j++){
                Debug.DrawLine(GetWorldPosition(i,j), GetWorldPosition(i,j)+ Vector3.right*0.35f, Color.white, 1000);
            }
        }
    }

    public Vector3 GetWorldPosition(int x , int z){
        return new Vector3(x, 0,z) * _cellSize;
    }

    public GridPosition FromWorldToGridPosition(Vector3 worldPosition){
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / _cellSize),
            Mathf.RoundToInt(worldPosition.z /_cellSize)
        );
    }
}
