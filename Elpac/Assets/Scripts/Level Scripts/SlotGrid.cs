using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void AddItems(List<ItemData> appliances)
    {
        Vector3 position;
        GameObject go;
        Appliance appliance;
        Wire wire;

        foreach (ItemData info in appliances)
        {
            if (info.loaded)
            {
                position = new Vector3(leftCornerOffset.x + info.gridPos.x * spacing.x, leftCornerOffset.y - info.gridPos.y * spacing.y, 10);
                position += cameraTopLeftPos + new Vector3(2, -2, 0);
                go = Instantiate(ItemManager.GetCorespondingItem(info.type), position, Quaternion.identity, transform);

                appliance = go.GetComponent<Appliance>();
                wire = go.GetComponent<Wire>();

                if (appliance != null) // An appliance
                {
                    appliance.fixedPosition = true;
                    appliance.data = info;

                    slots[info.gridPos.x, info.gridPos.y].SetAppliance(appliance);
                }
                else if (wire != null)
                {
                    wire.fixedPosition = true;
                    wire.info = info;

                    if (wire.horizontal)
                    {
                        slots[info.gridPos.x, info.gridPos.y].AddWireDirection(Direction.Right);
                        slots[info.gridPos.x + 1, info.gridPos.y].AddWireDirection(Direction.Left);
                    }
                    else
                    {
                        slots[info.gridPos.x, info.gridPos.y].AddWireDirection(Direction.Down);
                        slots[info.gridPos.x, info.gridPos.y + 1].AddWireDirection(Direction.Up);
                    }
                    slots[info.gridPos.x, info.gridPos.y].AddWire(wire);
                }
                else
                {
                    Debug.LogError("Instantiated GameObject does not have script");
                }
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                foreach (object param in info.itemData)
                    builder.Append(" ").Append(param.ToString());

                Debug.LogError("Insufficient or erroneous Item parameters - " + builder.ToString());
            }
        }
    }
    
    public static List<EnergyTrail> GetEnergyTrails(int xGrid, int yGrid) => instance.slots[xGrid, yGrid].energyTrails;

    public static void AddEnergyTrails(List<EnergyTrail> trails)
    {
        foreach (EnergyTrail trail in trails)
        {
            instance.slots[trail.gridPos.x, trail.gridPos.y].AddEnergyTrail(trail);
        }

        UpdateSlots(trails);
    }
    public static void RemoveEnergyTrails(List<EnergyTrail> trails)
    {
        foreach (EnergyTrail trail in trails)
        {
            instance.slots[trail.gridPos.x, trail.gridPos.y].RemoveEnergyTrail(trail);
        }

        UpdateSlots(trails);
    }

    private static void UpdateSlots(List<EnergyTrail> trails)
    {
        foreach (EnergyTrail trail in trails)
        {
            instance.slots[trail.gridPos.x, trail.gridPos.y].UpdateItems(trail.energy);
        }
    }

    public static Direction GetWireDirection(int xGrid, int yGrid)
    {
        if (!PositionInsideGrid(xGrid, yGrid))
            return Direction.None;

        return instance.slots[xGrid, yGrid].wireDirection;
    }
    public static bool IsSlotOccupied(int xGrid, int yGrid)
    {
        if (!PositionInsideGrid(xGrid, yGrid))
            return true;

        return instance.slots[xGrid, yGrid].isOccupied;
    }
    public static bool PositionInsideGrid(int xGrid, int yGrid)
    {
        return !(xGrid < 0 || xGrid >= instance.slots.GetLength(0) || yGrid < 0 || yGrid >= instance.slots.GetLength(1));
    }
}
