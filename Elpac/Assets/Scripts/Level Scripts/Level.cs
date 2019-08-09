using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level
{
    public List<ItemInfo> appliances;

    public bool isLoaded { get; private set; }

    public Level(string path)
    {
        appliances = new List<ItemInfo>();

        LoadFromFile(path);
    }

    private void LoadFromFile(string path)
    {
        string[] items;
        List<Tuple<int, int>> usedGridCoords = new List<Tuple<int, int>>();

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
            int type = 0;
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
                    int.TryParse(parameters[0], out type);
                    int.TryParse(parameters[1], out gridX);
                    int.TryParse(parameters[2], out gridY);
                    bool.TryParse(parameters[3], out facingRight);
                    
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

            appliances.Add(new ItemInfo(loaded, (ItemType)type, new Vector2Int(gridX, gridY), facingRight, itemData));
        }

        isLoaded = true;
    }
}
