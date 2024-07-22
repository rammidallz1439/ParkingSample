using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vault;

public class AppEvents
{
}

public struct MakeLevelEvent : GameEvent
{

}

public struct FindParkingSpotEvent : GameEvent
{
    public Transform ParkingSlot;
    public int Id;

    public FindParkingSpotEvent(Transform parkingSlot, int id)
    {
        ParkingSlot = parkingSlot;
        Id = id;
    }
}