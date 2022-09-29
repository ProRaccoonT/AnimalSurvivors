using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaker : MonoBehaviour
{
    public Transform Tile;
    public Vector2 TileSize;
    public Vector2Int TileValue;

    private Transform target;
    private Transform[,] tilesArray;
    
    private Vector2Int prevPos = Vector2Int.zero;

    private void Awake()
    {
        tilesArray = new Transform[TileValue.x, TileValue.y];
    }

    public void SetTarget(Transform tr)
    {
        target = tr;

        SetTile();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector2Int currentPos = new Vector2Int((int)(target.position.x * 0.1f - TileValue.x * 0.5f), (int)(target.position.z * 0.1f - TileValue.y * 0.5f));
        
        Debug.Log("currentPos! : " + currentPos);

        if (!Vector2Int.Equals(prevPos, currentPos))
        {
            MoveTile(currentPos);
        }
    }

    void SetTile()
    {
        prevPos = new Vector2Int((int)(target.position.x * 0.1f - TileValue.x * 0.5f), (int)(target.position.z * 0.1f - TileValue.y * 0.5f));

        Debug.Log("prevPos! : " + prevPos);

        for (int i = 0; i < TileValue.x; i++)
        {
            for (int j = 0; j < TileValue.y; j++)
            {
                GameObject tile = Instantiate(Tile.gameObject, new Vector3((i + prevPos.x) * TileSize.x, 0, (j + prevPos.y) * TileSize.y), Quaternion.identity, transform);
                tile.SetActive(true);

                tilesArray[i, j] = tile.transform;
            }
        }
    }

    void MoveTile(Vector2Int pos)
    {
        Vector2Int vecTemp = pos - prevPos;

        int x = vecTemp.x;
        int y = vecTemp.y;

        if (x > 0) // 캐릭터 우측 한칸
        {
            for (int tY = 0; tY < TileValue.y; tY++)
            {
                Transform temp = tilesArray[0, tY];
                temp.position = tilesArray[TileValue.x - 1, tY].position + new Vector3(TileSize.x, 0, 0);
                for (int tX = 0; tX < TileValue.x; tX++)
                {
                    if (tX.Equals(TileValue.x - 1))
                    {
                        tilesArray[tX, tY] = temp;
                    }
                    else
                    {
                        tilesArray[tX, tY] = tilesArray[tX + 1, tY];
                    }
                }
            }
        }
        else if (x < 0) // 캐릭터 좌측 한칸
        {
            for (int tY = 0; tY < TileValue.y; tY++)
            {
                Transform temp = tilesArray[TileValue.x - 1, tY];
                temp.position = tilesArray[0, tY].position - new Vector3(TileSize.x, 0, 0);
                for (int tX = TileValue.x - 1; tX >= 0; tX--)
                {
                    if (tX.Equals(0))
                    {
                        tilesArray[tX, tY] = temp;
                    }
                    else
                    {
                        tilesArray[tX, tY] = tilesArray[tX - 1, tY];
                    }
                }
            }
        }

        if (y > 0) // 캐릭터 위측 한칸
        {
            for (int tX = 0; tX < TileValue.x; tX++)
            {
                Transform temp = tilesArray[tX, 0];
                temp.position = tilesArray[tX, TileValue.y - 1].position + new Vector3(0, 0, TileSize.y);
                for (int tY = 0; tY < TileValue.y; tY++)
                {
                    if (tY.Equals(TileValue.y - 1))
                    {
                        tilesArray[tX, tY] = temp;
                    }
                    else
                    {
                        tilesArray[tX, tY] = tilesArray[tX, tY + 1];
                    }
                }
            }
        }
        else if (y < 0) // 캐릭터 아래측 한칸
        {
            for (int tX = 0; tX < TileValue.x; tX++)
            {
                Transform temp = tilesArray[tX, TileValue.y - 1];
                temp.position = tilesArray[tX, 0].position - new Vector3(0, 0, TileSize.y);
                for (int tY = TileValue.y - 1; tY >= 0; tY--)
                {
                    if (tY.Equals(0))
                    {
                        tilesArray[tX, tY] = temp;
                    }
                    else
                    {
                        tilesArray[tX, tY] = tilesArray[tX, tY - 1];
                    }
                }
            }
        }

        prevPos = pos;
    }
}