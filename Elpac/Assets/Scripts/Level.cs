using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level
{
    public HashSet<ApplianceInfo> appliances;

    public bool isLoaded { get; private set; }

    public Level(string path)
    {
        appliances = new HashSet<ApplianceInfo>();

        LoadFromFile(path);
    }

    private void LoadFromFile(string path)
    {
        string[] parameters;
        Dictionary<byte, byte> usedGridCoords = new Dictionary<byte, byte>();

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

        Appliance.Type type;
        byte gridX, gridY;
        bool facingRight;

        for (int i = 0; i < parameters.Length / 4; i += 4)
        {
            try
            {
                type = (Appliance.Type)int.Parse(parameters[i]);
                gridX = byte.Parse(parameters[i + 1]);
                gridY = byte.Parse(parameters[i + 2]);

                //if (usedGridCoords.ContainsKey(gridX) && usedGridCoords[gridX] == gridY) // Check if the position in grid is already occupied. If so file is corrupted
                    //return;

                //usedGridCoords.Add(gridX, gridY);

                facingRight = bool.Parse(parameters[i + 3]);

                appliances.Add(new ApplianceInfo(type, gridX, gridY, facingRight));
            }
            catch // Don't care what type of exception is thrown. File is corrupted
            {
                return;
            }
        }

        isLoaded = true;
    }
}
