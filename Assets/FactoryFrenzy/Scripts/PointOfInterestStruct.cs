using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct PointOfInterestStruct
{
    public int Id;
    public string Type; // Type of the POI (e.g., "Shop", "Landmark", "NPC")
    public string Title;
    public string Description;
    public PositionStruct Position;
}

[System.Serializable]
public struct PositionStruct
{
    public float x;
    public float y;

    public PositionStruct(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}


