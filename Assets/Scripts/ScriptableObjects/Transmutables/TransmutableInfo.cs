using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransmutableInfo", menuName = "World/Transmutable")]
public class TransmutableInfo : ScriptableObject
{
    public bool isDestroyable;
    public int destructionNumber;
    public int type;
}
