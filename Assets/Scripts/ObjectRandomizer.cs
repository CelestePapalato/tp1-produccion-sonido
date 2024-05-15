using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectRandomizer
{
    public static object GetRandom(object[] objects)
    {
        int r = Random.Range(0, objects.Length);
        return objects[r];
    }
}
