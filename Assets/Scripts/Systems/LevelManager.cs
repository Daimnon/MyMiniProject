using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance => _instance;

    [SerializeField] private FuelStove _fuelStove = null;
    public FuelStove FuelStove { get => _fuelStove; set => _fuelStove = value; }
}
