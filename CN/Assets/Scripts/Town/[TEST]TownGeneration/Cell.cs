using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    SpaceType spaceType;
    bool isOccupied;

    public Cell(SpaceType spaceType, bool isOccupied)
    {
        this.spaceType = spaceType;
        this.isOccupied = isOccupied;

        //Debug.Log("Cell is occupied?" + this.isOccupied + "Cell is type: " + this.spaceType);
    }
}
