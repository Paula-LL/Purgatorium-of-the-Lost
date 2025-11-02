using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public Vector2Int size;
    public int numSalas;
    public Vector2Int midaSalas = new Vector2Int(2, 3);
    [Range(0, 100)]
    public float relativeSquareSize = 10;

    public GameObject walls;
    public GameObject decorators;
    public GameObject fpsController;

    int n;

    private void Start()
    {
        int[,] dungeon = Dungeon.InstanciarDungeon(size, numSalas, 100 / relativeSquareSize, midaSalas);
        InstantiateDungeon(dungeon);
        Debug.Log("Dungeon Instanciada");

    }
    void InstantiateDungeon(int[,] dungeon)
    {
        bool _i = false;
        for (int i = 0; i < dungeon.GetLength(0); i++) {
            for (int j = 0; j < dungeon.GetLength(1); j++) {
                if (dungeon[i, j] == 0)
                {
                    if (CheckPosition(dungeon,new Vector2Int(i, j))){
                        GameObject go = Instantiate(walls, new Vector3(i, 1.5f, j), Quaternion.identity);
                        go.transform.SetParent(transform);

                    }
                }
                else
                {
                    if (!_i)
                    {
                        Instantiate(fpsController, new Vector3(i, 1, j), Quaternion.identity);
                        _i = true;
                    }
                    else
                    {
                        int r = Random.Range(0, 100);
                        if (r < 1)
                        {
                            Instantiate(decorators, new Vector3(i, 1, j), Quaternion.identity);
                        }
                    }
                }
            }
            bool CheckPosition(int[,] dungeon, Vector2Int position)
            {
                if (position.x >= 1 &&
                    position.y >= 1 &&
                    position.y < dungeon.GetLength(1) - 1 &&
                    position.x < dungeon.GetLength(0) - 1)
                {
                    if (dungeon[position.x + 1, position.y] != 0)
                        return true;
                    if (dungeon[position.x - 1, position.y] != 0)
                        return true;
                    if (dungeon[position.x, position.y + 1] != 0)
                        return true;
                    if (dungeon[position.x, position.y - 1] != 0)
                        return true;
                }
                return false; ;

            }

        }
    }
}
