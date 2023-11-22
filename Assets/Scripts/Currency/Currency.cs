using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] private int _worth = 1;
    [SerializeField] private Renderer _renderer;

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        Pickup(playerInventory);
    }

    private void Pickup(PlayerInventory playerInventory)
    {
        playerInventory.EarnCurrency(_worth);
        playerInventory.CurrencyObjectPool.ReturnCurrencyToPool(this);
    }
}
