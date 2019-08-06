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
        string[] parameters;
        Dictionary<int, int> usedGridCoords = new Dictionary<int, int>();

        try
        {
            parameters = File.ReadAllText(path).Split(';');
        }

        catch
        {
            return;
        }

        if (parameters.Length % 4 != 0)
            return;

        ItemType type;
        int gridX, gridY;
        bool facingRight;

        for (int i = 0; i < parameters.Length; i += 4)
        {
            try
            {
                type = (ItemType)int.Parse(parameters[i]);
                gridX = int.Parse(parameters[i + 1]);
                gridY = int.Parse(parameters[i + 2]);

                //if (usedGridCoords.ContainsKey(gridX) && usedGridCoords[gridX] == gridY) // Check if the position in grid is already occupied. If so file is corrupted
                    //return;

                //usedGridCoords.Add(gridX, gridY);

                facingRight = bool.Parse(parameters[i + 3]);

                appliances.Add(new ItemInfo(type, gridX, gridY, facingRight));
            }
            catch // Don't care what type of exception is thrown. File is corrupted
            {
                return;
            }
        }

        isLoaded = true;
    }
}
