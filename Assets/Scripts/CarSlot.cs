using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarSlot : MonoBehaviour
{
    public int Id;
    public bool Available;
    public TMP_Text TextId;
    public VechileType VechileType;
    public ParkingSlot ParkingSlot = null;
}
