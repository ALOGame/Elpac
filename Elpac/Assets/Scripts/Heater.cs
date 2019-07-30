﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : Appliance
{
    public Entity steam;
    public Vector3 steamSpawnPoint;
    public byte steamReleaseTime;

    private byte lastSteamReleased;

    protected override void OnPowered(bool powered)
    {
        if (lastSteamReleased == steamReleaseTime)
        {
            Instantiate(steam.Prefab, steamSpawnPoint, Quaternion.identity);
            lastSteamReleased = 0;
        }
        else
            lastSteamReleased++;
    }
}