using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotGrid : MonoBehaviour
{
    public GameObject slot;

    [Space]
    public GameObject powerSupply;
    public GameObject powerConsumer;
    public GameObject verticalWire;
    public GameObject horizontalWire;

    private Slot[,] slots;

    private const byte xCount = 10;
    private const byte yCount = 10;

    private readonly Vector2 spacing = new Vector2(2f, 2f);
    private readonly Vector2 leftCornerOffset = new Vector2(1, -1);
    private Vector3 cameraTopLeftPos;

    private static SlotGrid instance;

    private void Awake()
    {
        CreateGrid();
        instance = this;
    }

    private void CreateGrid()
    {
        slots = new Slot[xCount, yCount];

        Vector3 position;
        cameraTopLeftPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));

        for (int x = 0; x < xCount; x++)
        {
            for (int y = 0; y < yCount; y++)
            {
                position = new Vector3(leftCornerOffset.x + x * spacing.x, leftCornerOffset.y - y * spacing.y, 10);
                position += cameraTopLeftPos + new Vector3(2, -2, 0);
                slots[x, y] = Instantiate(slot, position, Quaternion.identity, transform).GetComponent<Slot>();
            }
        }
    }

    public void AddItems(List<ItemInfo> appliances)
    {
        Vector3 position;
        GameObject go;
        Appliance appliance;
        Wire wire;

        foreach (ItemInfo info in appliances)
        {
            position = new Vector3(leftCornerOffset.x + info.gridX * spacing.x, leftCornerOffset.y - info.gridY * spacing.y, 10);
            position += cameraTopLeftPos + new Vector3(2, -2, 0);
            go = Instantiate(GetCorespondingItem(info.type), position, Quaternion.identity, transform);

            appliance = go.GetComponent<Appliance>();
            wire = go.GetComponent<Wire>();

            if (appliance != null) // An appliance
            {
                appliance.fixedPosition = true;
                appliance.info = info;

                slots[info.gridX, info.gridY].SetAppliance(appliance);
            }
            else if (wire != null)
            {
                wire.fixedPosition = true;
                wire.info = info;

                if (wire.horizontal)
                {
                    slots[info.gridX, info.gridY].AddWireDirection(WireDirection.Right);
                    slots[info.gridX + 1, info.gridY].AddWireDirection(WireDirection.Left);
                } else
                {
                    slots[info.gridX, info.gridY].AddWireDirection(WireDirection.Down);
                    slots[info.gridX, info.gridY + 1].AddWireDirection(WireDirection.Up);
                }
            } else
            {
                Debug.LogError("Instantiated GameObject does not have script");
            }
        }
    }

    private GameObject GetCorespondingItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.PowerSupply:
                return powerSupply;
            case ItemType.PowerConsumer:
                return powerConsumer;
            case ItemType.VerticalWire:
                return verticalWire;
            case ItemType.HorizontalWire:
                return horizontalWire;
            default:
                Debug.Log("GameGrid: Appliance is not implemented yet (" + type + ")");
                return null;
        }
    }

    public static void AddEnergyTrailToSlot(int xGrid, int yGrid, EnergyTrail trail, Energy caller) => instance.slots[xGrid, yGrid].AddEnergyTrail(trail, caller);
    public static void RemoveEnergyTrailFromSlot(int xGrid, int yGrid, EnergyTrail trail) => instance.slots[xGrid, yGrid].RemoveEnergyTrail(trail);

    public static WireDirection GetWireDirection(int xGrid, int yGrid)
    {
        if (xGrid < 0 || xGrid >= instance.slots.GetLength(0) || yGrid < 0 || yGrid >= instance.slots.GetLength(1))
            return WireDirection.None;

        return instance.slots[xGrid, yGrid].wireDirection;
    }
}
