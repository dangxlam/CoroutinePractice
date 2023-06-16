using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLocation : MonoBehaviour
{
    public static bool Check(Vector2Int cellLocation)
    {
        if (cellLocation.x >= 0 && cellLocation.x < 8 && cellLocation.y >= 0 && cellLocation.y < 8)
            return true;
        return false;
    }
}
