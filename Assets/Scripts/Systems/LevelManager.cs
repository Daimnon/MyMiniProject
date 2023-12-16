using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance => _instance;

    [SerializeField] private Unlocker[] _allUnlockers;

    private int _unlockersUsed = 0;

    private void OnEnable()
    {
        EventManager.OnUnlock += OnUnlock;
    }
    private void OnDisable()
    {
        EventManager.OnUnlock -= OnUnlock;
    }

    private void OnUnlock()
    {
        _unlockersUsed++;
        _allUnlockers[_unlockersUsed].gameObject.SetActive(true);
    }

    /*private IEnumerator UnlockWithAnimation()
    {

    }*/
}
