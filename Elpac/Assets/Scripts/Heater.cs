using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : Appliance
{
    public Vector3 steamSpawnPoint;
    public byte steamReleaseTime;

    private byte lastSteamReleased;
}
