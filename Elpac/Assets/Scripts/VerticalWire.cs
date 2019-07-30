using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWire : Wire
{ 
    protected override void OnPowered(bool powered)
    {
        if (powered)
        {
            SlotGrid.instance.PowerSlotOn((byte)(info.gridX + 1), info.gridY);
        }
        else
        {
            SlotGrid.instance.PowerSlotOff((byte)(info.gridX + 1), info.gridY);
        }
    }
}
