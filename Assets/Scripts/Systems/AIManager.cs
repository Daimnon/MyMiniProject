using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] private AIObjectPool _aiPool;
    [SerializeField] private Transform _resourceTradingTr, _resourceExitTr;
    [SerializeField] private Transform _weaponTradingTr, _weaponExitTr;
    [SerializeField] private List<Adventurer> _adventurers;

    [SerializeField] private Adventurer[] testingAdventurers;

    private const string _resourceCustomerTag = "ResourceCustomer", _weaponCustomer = "WeaponCustomer";

    private void OnEnable()
    {
        EventManager.OnWeaponTraderUnlocked += OnWeaponTraderUnlocked;
        EventManager.OnAdventurerRespawned += OnAdventurerRespawned;
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
    private void OnDisable()
    {
        EventManager.OnWeaponTraderUnlocked -= OnWeaponTraderUnlocked;
        EventManager.OnAdventurerRespawned -= OnAdventurerRespawned;
    }

    private void OnWeaponTraderUnlocked(WeaponTrader weaponTrader)
    {
       _weaponTradingTr = weaponTrader.TradingPos;
        // start spawning 
    }
    private void OnAdventurerRespawned(Adventurer adventurer)
    {
        if (adventurer.CompareTag(_resourceCustomerTag))
        {
            adventurer.TradingTr = _resourceTradingTr;
            adventurer.ExitTr = _resourceExitTr;
        }
        else if (adventurer.CompareTag(_weaponCustomer))
        {
            adventurer.TradingTr = _weaponTradingTr;
            adventurer.ExitTr = _weaponExitTr;
        }

        _adventurers.Add(adventurer);
    }
}
