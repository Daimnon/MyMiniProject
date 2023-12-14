using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;

public class Unlocker : MonoBehaviour
{
    [SerializeField] private GameObject _unlockablePrefab;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private TextMeshProUGUI _currentPriceTxt;
    [SerializeField] private int _priceToUnlock = 3, _amountToCharge = 1;

    private const string _playerTag = "Player";
    private PlayerInventory _playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (_playerInventory == null && other.CompareTag(_playerTag))
            _playerInventory = other.GetComponent<PlayerInventory>();
    }
    private void OnTriggerStay(Collider other)
    {
        int paidAmount = _playerInventory.PayCurrency(_amountToCharge);
        EventManager.InvokePayCurrency(paidAmount);
        _priceToUnlock -= paidAmount;
        _currentPriceTxt.text = _priceToUnlock.ToString();

        if (_priceToUnlock <= 0)
            Unlock();
    }
    private void Unlock()
    {
        Instantiate(_unlockablePrefab, _spawnPos.position, _spawnPos.rotation);
        EventManager.InvokeBakeNavMesh();
        Destroy(gameObject);
    }

    /*[SerializeField] private Transform[] _edgeSprites;
[SerializeField] private bool _isIdle = true;*/
    /*private void Update()
    {
        if (!_isIdle)
            return;

        // breathing animation for edge pieces
    }*/
}
