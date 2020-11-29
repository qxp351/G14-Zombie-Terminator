using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : Item
{
    public Tool(string name)
    {
        named = name;
        amount = 1;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
