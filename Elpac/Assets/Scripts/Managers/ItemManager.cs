using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject powerSupply;
    public GameObject target;
    public GameObject verticalWire;
    public GameObject horizontalWire;
    public GameObject fan;
    public GameObject windTurbine;
    public GameObject battery;
    public GameObject heater;
    public GameObject heatsink;
    public GameObject laserGun;
    public GameObject laserFeeder;
    public GameObject laserMirror;
    public GameObject waterDispenser;

    private static GameObject PowerSupply() => instance.powerSupply;
    private static GameObject Target() => instance.target;
    private static GameObject VerticalWire() => instance.verticalWire;
    private static GameObject HorizontalWire() => instance.horizontalWire;
    private static GameObject Fan() => instance.fan;
    private static GameObject WindTurbine() => instance.windTurbine;
    private static GameObject Battery() => instance.battery;
    private static GameObject Heater() => instance.heater;
    private static GameObject HeatSink() => instance.heatsink;
    private static GameObject LaserGun() => instance.laserGun;
    private static GameObject LaserFeeder() => instance.laserFeeder;
    private static GameObject LaserMirror() => instance.laserMirror;
    private static GameObject WaterDispenser() => instance.waterDispenser;


    private static ItemManager instance;

    private void Awake()
    {
        instance = this;
    }
    public static GameObject GetCorespondingItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.PowerSupply:
                return PowerSupply();
            case ItemType.PowerConsumer:
                return Target();
            case ItemType.VerticalWire:
                return VerticalWire();
            case ItemType.HorizontalWire:
                return HorizontalWire();
            case ItemType.Fan:
                return Fan();
            case ItemType.WindTurbine:
                return WindTurbine();
            case ItemType.Battery:
                return Battery();
            case ItemType.Heater:
                return Heater();
            case ItemType.Heatsink:
                return HeatSink();
            case ItemType.LaserGun:
                return LaserGun();
            case ItemType.LaserFeeder:
                return LaserFeeder();
            case ItemType.LaserMirror:
                return LaserMirror();
            case ItemType.WaterDispenser:
                return WaterDispenser();
            default:
                Debug.Log("GameGrid: Appliance is not implemented yet (" + type + ")");
                return null;
        }
    }
}
