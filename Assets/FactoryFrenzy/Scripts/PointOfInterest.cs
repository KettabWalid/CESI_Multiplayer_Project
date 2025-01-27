using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public PointOfInterestStruct PointOfInterestData;

    public void Initialize(PointOfInterestStruct data)
    {
        PointOfInterestData = data;
    }
}
