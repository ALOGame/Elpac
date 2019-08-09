using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotGrid : MonoBehaviour
{
    public GameObject slot;    
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
            if (info.loaded)
            {
                position = new Vector3(leftCornerOffset.x + info.gridPos.x * spacing.x, leftCornerOffset.y - info.gridPos.y * spacing.y, 10);
                position += cameraTopLeftPos + new Vector3(2, -2, 0);
                go = Instantiate(GetCorespondingItem(info.type), position, Quaternion.identity, transform);

                appliance = go.GetComponent<Appliance>();
                wire = go.GetComponent<Wire>();

                if (appliance != null) // An appliance
                {
                    appliance.fixedPosition = true;
                    appliance.info = info;

                    slots[info.gridPos.x, info.gridPos.y].SetAppliance(appliance);
                }
                else if (wire != null)
                {
                    wire.fixedPosition = true;
                    wire.info = info;

                    if (wire.horizontal)
                    {
                        slots[info.gridPos.x, info.gridPos.y].AddWireDirection(WireDirection.Right);
                        slots[info.gridPos.x + 1, info.gridPos.y].AddWireDirection(WireDirection.Left);
                    }
                    else
                    {
                        slots[info.gridPos.x, info.gridPos.y].AddWireDirection(WireDirection.Down);
                        slots[info.gridPos.x, info.gridPos.y + 1].AddWireDirection(WireDirection.Up);
                    }
                }
                else
                {
                    Debug.LogError("Instantiated GameObject does not have script");
                }
            }

            else
            {
                string paramdump = "";
                foreach (object param in info.itemData)
                    paramdump += (" " + param.ToString());

                Debug.LogError("Isufficient or errorneous Item parameters -" + paramdump);
            }
        }
    }

    private GameObject GetCorespondingItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.PowerSupply:
                return ItemManager.PowerSupply();
            case ItemType.PowerConsumer:
                return ItemManager.Target();
            case ItemType.VerticalWire:
                return ItemManager.VerticalWire();
            case ItemType.HorizontalWire:
                return ItemManager.HorizontalWire();
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
