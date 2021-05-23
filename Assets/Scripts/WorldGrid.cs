using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid 
{
   public Cell [,,] World;

    public WorldGrid(int size)
    {
        World = new Cell[size, size, size];
    }
}
