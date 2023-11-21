using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static class EventManager
{
    public static Action OnGameLaunched, OnLevelLaunched, OnOpenMenu;
    public static Action<int> OnEarnCurrency;
    public static Action<PlayerInventory, int> OnPayCurrency;
    public static Action<Resource> OnTakeResource;
    public static Action<PlayerInventory> OnPayFirstResource;
    public static Action<PlayerInventory, ResourceType> OnPayResource;

    public static void InvokeGameLaunched()
    {
        OnGameLaunched?.Invoke();
        UnityEngine.Debug.Log("Event: GameLaunched");
    }
    public static void InvokeLevelLaunched()
    {
        OnLevelLaunched?.Invoke();
        UnityEngine.Debug.Log("Event: LevelLaunched");
    }
    public static void InvokeOpenMenu()
    {
        OnOpenMenu?.Invoke();
        UnityEngine.Debug.Log("Event: OpenMenu");
    }

    public static void InvokeEarnCurrency(int amount)
    {
        if (amount > 0)
        {
            OnEarnCurrency?.Invoke(amount);
            UnityEngine.Debug.Log($"Event: EarnCurrency, Amount: {amount}");
        }
    }
    public static void InvokePayCurrency(PlayerInventory inventory, int price)
    {
        if (inventory.Currency >= price)
        {
            OnPayCurrency?.Invoke(inventory, price);
            UnityEngine.Debug.Log($"Event: PayCurrency, Price: {price}, NewBalance: {inventory.Currency}");
        }
    }
    public static void InvokeTakeResource(Resource resource)
    {
        if (resource != null)
        {
            OnTakeResource?.Invoke(resource);
            UnityEngine.Debug.Log($"Event: TakeResource, Resource: {resource.Type.ToString()}");
        }
    }
    public static void InvokePayFirstResource(PlayerInventory inventory)
    {
        if (inventory.Resources != null && inventory.Resources.Count > 0)
        {
            OnPayFirstResource?.Invoke(inventory);
            UnityEngine.Debug.Log($"Event: PayResource, ResourceType: {inventory.Resources[0].Type.ToString()}");
        }
    }
    public static void InvokePayResource(PlayerInventory inventory, ResourceType wantedResource)
    {
        for (int i = 0; i < inventory.Resources.Count; i++)
        {
            Resource currentResource = inventory.Resources[i];
            if (currentResource.Type != wantedResource)
                continue;

            OnPayResource?.Invoke(inventory, wantedResource);
            UnityEngine.Debug.Log($"Event: PayResource, ResourceType: {wantedResource}");
        }
    }
}
