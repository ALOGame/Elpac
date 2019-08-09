using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level
{
    public List<ItemData> appliances;

    public bool isLoaded { get; private set; }

    public Level()
    {
        appliances = new List<ItemData>();
    }

    public void LoadFromFile(string path)
    {
        appliances.Clear();
        isLoaded = false;

        string[] items;
        List<Tuple<int, int>> usedGridCoords = new List<Tuple<int, int>>(); // ?? Neni nahodou lepsi Dictionary? 

        try
        {
            items = File.ReadAllLines(path);
        }

        catch
        {
            return;
        }


        foreach (string item in items)
        {
            ItemType type = 0;
            int gridX = -1, gridY = -1;
            bool facingRight = false;
            bool loaded = false;
            object[] itemData = new object[0];

            //if (usedGridCoords.ContainsKey(gridX) && usedGridCoords[gridX] == gridY) // Check if the position in grid is already occupied. If so file is corrupted
            //return;
            //usedGridCoords.Add(gridX, gridY);

            string[] parameters = item.Split(';');
            if (parameters.Length >= 4)
            {
                loaded = true;
                try
                {
                    type = (ItemType)int.Parse(parameters[0]);
                    gridX = int.Parse(parameters[1]);
                    gridY = int.Parse(parameters[2]);
                    facingRight = bool.Parse(parameters[3]);
                    
                    if (parameters.Length > 4)
                    {
                        int lendiff = parameters.Length - 4;
                        itemData = new object[lendiff];
                        for (int i = 0; i < itemData.Length; i++)
                        {
                            itemData[i] = parameters[i + 4];
                        }
                    }
                }
                catch
                {
                    loaded = false;
                }
            }

            if (!loaded)
            {
                itemData = parameters;
            }

            appliances.Add(new ItemData(loaded, type, new Vector2Int(gridX, gridY), facingRight, itemData));
        }

        isLoaded = true;
    }
}
