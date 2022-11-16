using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField] private int value = 1;

    public int PickUp()
    {
        Destroy(gameObject);
        return value;
    }
}
