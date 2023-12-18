using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] private AIObjectPool _aiPool;
    [SerializeField] private Transform _resourceTradingTr, _resourceExitTr;
    [SerializeField] private Transform _weaponTradingTr, _weaponExitTr;
    [SerializeField] private int _spawnAmount;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private AdventurerType[] _spawnTypes;

    [SerializeField] private Adventurer[] testingAdventurers;

    private const string _resourceCustomerTag = "ResourceCustomer", _weaponCustomer = "WeaponCustomer";

    private void OnEnable()
    {
        EventManager.OnWeaponTraderUnlocked += OnWeaponTraderUnlocked;
    }
    private void Start()
    {
        for (int i = 0; i < testingAdventurers.Length; i++)
        {
            Adventurer adventurer = testingAdventurers[i];
            if (adventurer.CompareTag(_resourceCustomerTag))
            {
                testingAdventurers[i].TradingTr = _resourceTradingTr;
                testingAdventurers[i].ExitTr = _resourceExitTr;
            }
            else if (adventurer.CompareTag(_weaponCustomer))
            {
                testingAdventurers[i].TradingTr = _weaponTradingTr;
                testingAdventurers[i].ExitTr = _weaponExitTr;
            }
        }
    }

    public void OnWeaponTraderUnlocked(WeaponTrader weaponTrader)
    {
        //_weaponTradingTr = weaponTrader.tr
    }
}
