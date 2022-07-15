using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, BrigeStackManager> dictBrigeStack = new Dictionary<Collider, BrigeStackManager>();
    public static BrigeStackManager GenCollectItems(Collider col)
    {
        if (!dictBrigeStack.ContainsKey(col))
        {
            BrigeStackManager brm = col.GetComponent<BrigeStackManager>();
            dictBrigeStack.Add(col, brm);
        }
        return dictBrigeStack[col];
    }


}
