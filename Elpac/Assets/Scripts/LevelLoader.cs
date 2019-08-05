using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader
{
    private const string defaultLevelDir = "levels/default";
    private const string userLevelDir = "levels/user";

    public string[] defaultLevelsNames;
    public string[] userLevelsNames;

    public Level loadedLevel { get; private set; }

    public LevelLoader()
    {
        PrepareSelf();
    }

    private void PrepareSelf()
    {
        if (!Directory.Exists(defaultLevelDir))
            Directory.CreateDirectory(defaultLevelDir);
        if (!Directory.Exists(userLevelDir))
            Directory.CreateDirectory(userLevelDir);

        defaultLevelsNames = Directory.GetFiles(defaultLevelDir);
        userLevelsNames = Directory.GetFiles(userLevelDir);
    }

    public void LoadLevel(string levelPath)
    {
        loadedLevel = new Level(levelPath);
        Debug.Log(loadedLevel.appliances.Count);
    }
}
