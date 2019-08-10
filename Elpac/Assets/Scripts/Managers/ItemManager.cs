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
    public GameObject battery;

    public static GameObject PowerSupply() => instance.powerSupply;
    public static GameObject Target() => instance.target;
    public static GameObject VerticalWire() => instance.verticalWire;
    public static GameObject HorizontalWire() => instance.horizontalWire;
    public static GameObject Fan() => instance.fan;
    public static GameObject Battery() => instance.battery;


    private static ItemManager instance;

    private void Awake()
    {
        instance = this;
    }
}
