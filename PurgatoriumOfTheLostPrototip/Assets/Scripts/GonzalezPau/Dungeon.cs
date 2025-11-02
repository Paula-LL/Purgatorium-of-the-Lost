using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;
[System.Serializable]
public class Vector2Int
{
    public int x;
    public int y; 

    public Vector2Int(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}
static class Dungeon
{
    public static int[,] InstanciarDungeon(Vector2Int size, int SquareForGeneration, float dpc, Vector2Int tamañoSala)
    {
        int[,] dungeonMapa;
        dungeonMapa = CreateDungeonSalas(size.x, size.y, SquareForGeneration, dpc);
        int nbrRoom = encuentraSalas(dungeonMapa, size.x, size.y);
        if (nbrRoom > 1)
        {
            dungeonMapa = createDungeonHalls(dungeonMapa, size.x, size.y, nbrRoom, tamañoSala);
        }
        else
        {
            return InstanciarDungeon(size, SquareForGeneration, dpc, tamañoSala);
        }
        return dungeonMapa;
    }
    public static int[,] CreateDungeonSalas(int sizeX, int sizeZ, int numberOfsala, float dpc)
    {
        int[,] newMapa = new int[sizeX, sizeZ];

        int maxRoomSizeX = Mathf.Max(10, Mathf.RoundToInt(sizeX / dpc));
        int maxRoomSizeZ = Mathf.Max(10, Mathf.RoundToInt(sizeZ / dpc));

        int roomSizeX = Mathf.Min(maxRoomSizeX, sizeX - 4); // deja margen
        int roomSizeZ = Mathf.Min(maxRoomSizeZ, sizeZ - 4);

        int roomPosX = Random.Range(2, sizeX - roomSizeX - 1);
        int roomPosZ = Random.Range(2, sizeZ - roomSizeZ - 1);

        for (int j = roomPosX; j < roomPosX + roomSizeX && j < sizeX; j++)
        {
            for (int k = roomPosZ; k < roomPosZ + roomSizeZ && k < sizeZ; k++)
            {
                newMapa[j, k] = 1;
            }
        }

        return newMapa;
    }
    public static int encuentraSalas(int[,] dungeonMapa, int sizeX, int sizeZ)
    {
        List<Vector2> ListaDeCoordenadas = new List<Vector2>();
        int[,] mapaModificado = new int[sizeX, sizeZ];
        int salasEncontradas = 0;

        System.Array.Copy(dungeonMapa, mapaModificado, sizeX * sizeZ);
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                if (mapaModificado[i, j] == 1)
                {
                    ListaDeCoordenadas.Add(new Vector2(i, j));
                    while (ListaDeCoordenadas.Count > 0)
                    {
                        int x = (int)ListaDeCoordenadas[0].x;
                        int z = (int)ListaDeCoordenadas[0].y;
                        ListaDeCoordenadas.RemoveAt(0);
                        dungeonMapa[x, z] = salasEncontradas + 1;
                        for (int xAround = x - 1; xAround <= x + 1; xAround++)
                        {
                            for (int zAround = z - 1; zAround <= z + 1; zAround++)
                            {
                                if (mapaModificado[xAround, zAround] == 1)
                                {
                                    ListaDeCoordenadas.Add(new Vector2(xAround, zAround));
                                    mapaModificado[xAround, zAround] = 0;
                                }
                            }
                        }
                    }
                    salasEncontradas++;
                }
            }
        }
        return salasEncontradas;
    }
    public static int[,] createDungeonHalls(int[,] dungeonMap, int sizeX, int sizeZ, int numSalas, Vector2Int midaSalas)
    {
        int x1, z1, x2, z2;
        for (int currentRoomNumber = 1; currentRoomNumber <= numSalas; currentRoomNumber++)
        {
            int numeroSalasTry = 0;
            int numeroSalasTryMax = 2000;
            x1 = Random.Range(1, sizeX - 1);
            z1 = Random.Range(1, sizeZ - 1);
            while (dungeonMap[x1, z1] != currentRoomNumber && numeroSalasTry < numeroSalasTryMax)
            {
                x1 = Random.Range(1, sizeX - 1);
                z1 = Random.Range(1, sizeZ - 1);
                numeroSalasTry++;
            }
            numeroSalasTry = 0;

            x2 = Random.Range(1, sizeX - 1);
            z2 = Random.Range(1, sizeZ - 1);
            while ((dungeonMap[x2, z2] == 0 || dungeonMap[x2, z2] == currentRoomNumber) && numeroSalasTry > numeroSalasTryMax)
            {
                x2 = Random.Range(1, sizeX - 1);
                z2 = Random.Range(1, sizeZ - 1);
                numeroSalasTry++;
            }
            int diffX = x2 - x1;
            int diffZ = z2 - z1;

            int xDirection = 1;
            int zDirection = 1;

            if (diffX != 0)
            {
                xDirection = diffX / Mathf.Abs(diffX);
            }
            else
            {
                xDirection = 0;
            }

            if (diffZ != 0)
            {
                zDirection = diffZ / Mathf.Abs(diffZ);
            }
            else
            {
                zDirection = 0;
            }
            int hallWidth = Random.Range(midaSalas.x, midaSalas.y);
            int hallHeight = Random.Range(midaSalas.x, midaSalas.y);
            for (int i = x1; i != x1 + xDirection * hallWidth; i += xDirection)
            {
                for (int j = z1; j != z2; j += zDirection)
                {
                    if (i >= 0 && i < sizeX && j >= 0 && j < +sizeZ)
                    {
                        if (dungeonMap[i, j] == 0)
                        {
                            dungeonMap[i, j] = -1;
                        }
                    }
                }
            }

            for (int i = x1; i != x2; i += xDirection)
            {
                for (int j = z2; j != z2 + zDirection * hallHeight; j += zDirection)
                {
                    if (i >= 0 && i < sizeX && j >= 0 && j < +sizeZ)
                    {
                        if (dungeonMap[i, j] == 0)
                        {
                            dungeonMap[i, j] = -1;
                        }
                    }
                }
            }
        }
        return dungeonMap;

    }
}
