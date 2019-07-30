using System;
using System.Collections.Generic;

public class HorizontalWire : Wire
{
    protected override void OnPowered(bool powered)
    {
        if (powered)
        {
            SlotGrid.instance.PowerSlotOn(info.gridX, (byte)(info.gridY + 1));
        } else
        {
            SlotGrid.instance.PowerSlotOff(info.gridX, (byte)(info.gridY + 1));
        }
    }
}
