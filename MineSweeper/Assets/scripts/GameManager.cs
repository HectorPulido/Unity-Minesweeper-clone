using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    Cell[,] cellMatrix;
    bool ingame;

    public Vector2Int dimension;
    public Cell prefab;

    [Range(0, 100)]
    public int minePercent = 10;

    public Sprite vanilaSprite;
    public Sprite mineSprite;
    public Sprite freeSprite;

	void Start ()
    {
        MatrizInstance();
	}

    void MatrizInstance()
    {
        if (cellMatrix == null)
        {
            cellMatrix = new Cell[dimension.x, dimension.y];
            CellMatrixLoop((i, j) =>
            {
                Cell go = Instantiate(prefab,
                    new Vector3(i - dimension.x / 2f, j - dimension.y / 2f),
                    Quaternion.identity,
                    transform);
                go.name = string.Format("(X:{0},Y:{1})", i, j);
                cellMatrix[i, j] = go;
            });
        }

        CellMatrixLoop((i, j) =>
        {
            cellMatrix[i, j].Init(new Vector2Int(i, j),
            (UnityEngine.Random.Range(0, 100) > minePercent ? false : true),
            Activate);
            cellMatrix[i, j].sprite = vanilaSprite;
        });
    }
    void Activate(int i, int j)
    {
        if (cellMatrix[i, j].showed)
            return;
        cellMatrix[i, j].showed = true;

        if (cellMatrix[i, j].mine)
        {
            // FAIL STATE
            cellMatrix[i, j].sprite = mineSprite;
            MatrizInstance();
        }
        else
        {
            cellMatrix[i, j].sprite = freeSprite;

            if (ArroundCount(i, j) == 0)
            {
                ActivateArround(i,j);
            }
            else
            {
                cellMatrix[i, j].text = ArroundCount(i, j).ToString();
            }
        }
    }
    void ActivateArround(int i, int j)
    {
        if (PointIsInsideMatrix(i + 1, j))
            Activate(i + 1, j);
        if (PointIsInsideMatrix(i, j + 1))
            Activate(i, j + 1);
        if (PointIsInsideMatrix(i + 1, j + 1))
            Activate(i + 1, j + 1);
        if (PointIsInsideMatrix(i - 1, j))
            Activate(i - 1, j);
        if (PointIsInsideMatrix(i, j - 1))
            Activate(i, j - 1);
        if (PointIsInsideMatrix(i - 1, j - 1))
            Activate(i - 1, j - 1);
        if (PointIsInsideMatrix(i - 1, j + 1))
            Activate(i - 1, j + 1);
        if (PointIsInsideMatrix(i + 1, j - 1))
            Activate(i + 1, j - 1);
    }   
    void CellMatrixLoop(Action<int, int> e)
    {
        for (int i = 0; i < cellMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cellMatrix.GetLength(1); j++)
            {
                e(i, j);
            }
        }
    }

    bool PointIsInsideMatrix(int i, int j)
    {
        if (i >= cellMatrix.GetLength(0))
            return false;
        if (i < 0)
            return false;
        if (j >= cellMatrix.GetLength(1))
            return false;
        if (j < 0)
            return false;

        return true;
    }
    int ArroundCount(int i, int j)
    {
        int arround = 0;

        if (PointIsInsideMatrix(i + 1, j) && cellMatrix[i + 1, j].mine)
            arround++;
        if (PointIsInsideMatrix(i, j + 1) && cellMatrix[i, j + 1].mine)
            arround++;
        if (PointIsInsideMatrix(i + 1, j + 1) && cellMatrix[i + 1, j + 1].mine)
            arround++;
        if (PointIsInsideMatrix(i - 1, j) && cellMatrix[i - 1, j].mine)
            arround++;
        if (PointIsInsideMatrix(i, j - 1) && cellMatrix[i, j - 1].mine)
            arround++;
        if (PointIsInsideMatrix(i - 1, j - 1) && cellMatrix[i - 1, j - 1].mine)
            arround++;
        if (PointIsInsideMatrix(i - 1, j + 1) && cellMatrix[i - 1, j + 1].mine)
            arround++;
        if (PointIsInsideMatrix(i + 1, j - 1) && cellMatrix[i + 1, j - 1].mine)
            arround++;

        return arround;
    }
}
