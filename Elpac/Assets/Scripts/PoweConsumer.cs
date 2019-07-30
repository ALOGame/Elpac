using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweConsumer : Appliance
{
    protected override void OnPowered(bool powered)
    {
        Debug.Log("power suppply powered");
    }
}
