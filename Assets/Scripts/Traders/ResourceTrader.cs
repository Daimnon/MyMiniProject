using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTrader : Trader
{
    [SerializeField] private GameObject _currencyPrefab;
    public GameObject CurrencyPrefab => _currencyPrefab;

    [SerializeField] private Transform[] _productsTr;
    public Transform[] ProductsTr => _productsTr;

    [SerializeField] private int[] _conversionRates = new int[System.Enum.GetValues(typeof(ResourceType)).Length];
    
    private CurrencyObjectPool _currencyObjectPool;
    private PlayerInventory _playerInventory;
    private List<Adventurer> _adventurers;
    private const string _resourceCustomer = "ResourceCustomer";
    private const string _playerTag = "Player";
    private const string _aiTag = "AI";

    private void Awake()
    {
        _adventurers = new List<Adventurer>();
    }
    private void Start()
    {
        _currencyObjectPool = GameManager.Instance.CurrencyPool;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_aiTag)) // do entering animation on self
            _adventurers.Add(other.GetComponent<Adventurer>());
        else if (other.CompareTag(_playerTag) && !_playerInventory)
            _playerInventory = other.GetComponent<PlayerInventory>();
        else
            return;
        // do entering animation on self
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_resourceCustomer) || _adventurers.Count < 1)
        {
            Adventurer adventurer = other.GetComponent<Adventurer>();

            if (!adventurer.HasBoughtItem && _playerInventory)
            {
                adventurer.BuyItem(_playerInventory);
                return;
            }
        }

        if (!other.CompareTag(_playerTag) || _adventurers.Count < 1)
            return;

        if (_playerInventory != null && _playerInventory.ResourceCount < 1)
            return;

        Resource convertedResource = _playerInventory.PayFirstResource();
        ConvertCurrency(convertedResource.Type);
        EventManager.InvokePayFirstResource(convertedResource);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_aiTag))
            _adventurers.Remove(other.GetComponent<Adventurer>());
        else if (other.CompareTag(_playerTag)) // do exiting animation on self
            _playerInventory = null;
        else
            return;
        // do exiting animation on self
    }
    private void ConvertCurrency(ResourceType resourceToConvert)
    {
        int conversionRate = _conversionRates[(int)resourceToConvert];
        int availableIndex = 0;
        int amountConverted = 0;

        while (amountConverted <= conversionRate && availableIndex < _productsTr.Length)
        {
            if (_productsTr[availableIndex].childCount > 0)
            {
                availableIndex++;
                continue;
            }

            Transform newCurrencyTr = _currencyObjectPool.GetCurrencyFromPool().transform;
            newCurrencyTr.SetParent(_productsTr[availableIndex]);
            newCurrencyTr.localPosition = Vector3.zero;
            amountConverted++;
            availableIndex++;
        }
    }
}
