using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static class EventManager
{
    public static Action OnGameLaunched, OnLevelLaunched, OnOpenMenu;
    public static Action OnBakeNavMesh;
    public static Action<bool> OnHoldResource, OnHoldWeapon;
    public static Action<int> OnEarnCurrency;
    public static Action<int> OnPayCurrency;
    public static Action<Resource> OnTakeResource, OnPayFirstResource, OnPayResource;

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

    public static void InvokeBakeNavMesh()
    {
        OnBakeNavMesh?.Invoke();
        UnityEngine.Debug.Log("Event: BakeNavMesh");
    }

    public static void InvokeHoldResource(bool isHoldingResource)
    {
        OnHoldResource?.Invoke(isHoldingResource);
        UnityEngine.Debug.Log("Event: HoldResource changed");
    }
    public static void InvokeHoldWeapon(bool isHoldingWeapon)
    {
        OnHoldWeapon?.Invoke(isHoldingWeapon);
        UnityEngine.Debug.Log("Event: HoldWeapon changed");
    }

    public static void InvokeEarnCurrency(int amount)
    {
        if (amount > 0)
        {
            OnEarnCurrency?.Invoke(amount);
            UnityEngine.Debug.Log($"Event: EarnCurrency, Amount: {amount}");
        }
    }
    public static void InvokePayCurrency(int price)
    {
        OnPayCurrency?.Invoke(price);
        UnityEngine.Debug.Log($"Event: PayCurrency, Price: {price}");
    }
    public static void InvokeTakeResource(Resource resource)
    {
        if (resource != null)
        {
            OnTakeResource?.Invoke(resource);
            UnityEngine.Debug.Log($"Event: TakeResource, Resource: {resource.Type.ToString()}");
        }
    }
    public static void InvokePayFirstResource(Resource resource)
    {
        if (resource != null)
        {
            OnPayFirstResource?.Invoke(resource);
            UnityEngine.Debug.Log($"Event: PayResource, ResourceType: {resource.Type.ToString()}");
        }
    }
    public static void InvokePayResource(Resource resource)
    {
        if (resource != null)
        {
            OnPayResource?.Invoke(resource);
            UnityEngine.Debug.Log($"Event: PayResource, ResourceType: {resource.Type.ToString()}");
        }
    }
}
