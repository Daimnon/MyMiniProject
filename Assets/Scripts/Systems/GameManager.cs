using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Mushroom, 
    WoodPlank, 
    Stone, 
    IronChain
}

public enum ProducerType
{
    MushroomCluster,
    Tree,
    Quarry,
    Forge
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private CurrencyObjectPool _currencyPool;
    public CurrencyObjectPool CurrencyPool => _currencyPool;

    [SerializeField] private ResourceObjectPool _resourcePool;
    public ResourceObjectPool ResourcePool => _resourcePool;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
}