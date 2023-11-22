using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyConverter : MonoBehaviour
{
    [SerializeField] private GameObject _currencyPrefab;
    public GameObject CurrencyPrefab => _currencyPrefab;

    [SerializeField] private Transform[] _productsTr;
    public Transform[] ProductsTr => _productsTr;

    [SerializeField] private int[] _conversionRates = new int[System.Enum.GetValues(typeof(ResourceType)).Length];
    
    private CurrencyObjectPool _currencyObjectPool;
    private PlayerInventory _playerInventory;

    private void Start()
    {
        _currencyObjectPool = GameManager.Instance.CurrencyPool;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_playerInventory == null)
            _playerInventory = other.GetComponent<PlayerInventory>();

        // do entering animation on self
    }
    private void OnTriggerStay(Collider other)
    {
        if (_playerInventory.ResourceCount <= 0)
            return;

        Resource convertedResource = _playerInventory.PayFirstResource();
        ConvertCurrency(convertedResource.Type);
        EventManager.InvokePayFirstResource(convertedResource);
    }
    private void OnTriggerExit(Collider other)
    {
        _playerInventory = null;

        // do exiting animation on self
    }
    private void ConvertCurrency(ResourceType resourceToConvert)
    {
        int conversionRate = _conversionRates[(int)resourceToConvert];
        for (int i = 0; i < conversionRate; i++)
        {
            Currency newCurrency = _currencyObjectPool.GetCurrencyFromPool();
            newCurrency.transform.position = _productsTr[i].position;
        }
    }
}
