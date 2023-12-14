using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Mushroom,
    Potion,
    Coal,
    Iron,
    Magic,
    Leather
}
public enum ResourceProducerType
{
    MushroomCluster,
    AlchemyTable,
    CoalMine,
    Forge,
    MagicFountain,
    Dragon
}
public enum WeaponType
{
    Axe, 
    Sword, 
    Special
}
public enum WeaponRarity
{
    Common,
    Enchanted,
    Legendary
}
public enum WeaponSize
{
    Small,
    Medium,
    Large
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private CurrencyObjectPool _currencyPool;
    public CurrencyObjectPool CurrencyPool => _currencyPool;

    [SerializeField] private ResourceObjectPool _resourcePool;
    public ResourceObjectPool ResourcePool => _resourcePool;

    [SerializeField] private WeaponObjectPool _weaponPool;
    public WeaponObjectPool WeaponPool => _weaponPool;

    [SerializeField] private Camera _mainCam;
    public Camera MainCam => _mainCam;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        EventManager.InvokeGameLaunched();
    }
}
