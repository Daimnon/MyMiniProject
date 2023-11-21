using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyObjectPool : MonoBehaviour
{
    [SerializeField] private Currency _currency;
    [SerializeField] private int _initialPoolSize = 20000;

    private List<Currency> _currencyPool;

    private void Start()
    {
        _currencyPool = new List<Currency>();

        for (int i = 0; i < _initialPoolSize; i++)
        {
            Currency currency = Instantiate(_currency, transform);
            currency.gameObject.SetActive(false);
            _currencyPool.Add(currency);
        }
    }

    public Currency GetCurrencyFromPool()
    {
        for (int i = 0; i < _currencyPool.Count; i++)
        {
            Currency currency = _currencyPool[i];
            if (!currency.gameObject.activeSelf)
            {
                currency.gameObject.SetActive(true);
                ReturnResourceToPool(currency);
                return currency;
            }
        }

        // If no inactive money objects are available, create a new one
        Currency newCurrency = Instantiate(_currency);
        _currencyPool.Add(newCurrency);
        newCurrency.gameObject.SetActive(true);
        ReturnResourceToPool(newCurrency);
        return newCurrency;
    }
    public void ReturnResourceToPool(Currency currency)
    {
        currency.gameObject.SetActive(false);
        currency.transform.SetParent(transform);
        currency.transform.position = Vector3.zero;
    }
}