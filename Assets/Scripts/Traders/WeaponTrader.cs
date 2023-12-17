using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrader : Trader
{
    [SerializeField] private GameObject _currencyPrefab;
    public GameObject CurrencyPrefab => _currencyPrefab;

    [SerializeField] private Transform[] _productsTr;
    public Transform[] ProductsTr => _productsTr;

    [SerializeField] private int[] _typePriceFactor = new int[3], _rarityPriceFactor = new int[3], _sizePriceFactor = new int[3];

    private CurrencyObjectPool _currencyObjectPool;
    private PlayerInventory _playerInventory;
    private List<Adventurer> _adventurers;
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
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_playerTag) && _playerInventory.Weapon[0] && _adventurers.Count > 0)
        {
            SellWeapon(_playerInventory.GiveWeapon());
            _adventurers.RemoveAt(0);
            // move weapon to ai
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_aiTag))
            _adventurers.Remove(other.GetComponent<Adventurer>());
        else if (other.CompareTag(_playerTag)) // do exiting animation on self
            _playerInventory = null;
        else
            return;
    }
    private void SellWeapon(Weapon weaponToSell)
    {
        int conversionRate = _typePriceFactor[(int)weaponToSell.Type] + _rarityPriceFactor[(int)weaponToSell.Rarity] + _sizePriceFactor[(int)weaponToSell.Size];
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
