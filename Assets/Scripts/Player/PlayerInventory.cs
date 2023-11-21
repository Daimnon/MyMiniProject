using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private int _currency = 0;
    public int Currency => _currency;

    [SerializeField] private int _carryLimit = 3;
    public int CarryLimit => _carryLimit;

    [SerializeField] private List<Transform> _resourcesTr;
    public List<Transform> ResourcesTr => _resourcesTr;

    private List<Resource> _resources = new();
    public List<Resource> Resources => _resources;

    private int _resourceCount = 0;
    public int ResourceCount => _resourceCount;

    [Header("UI")]
    [SerializeField] private RectTransform _canvasRTr;
    [SerializeField] private TextMeshProUGUI _currencyTxt, _resourceTxt;

    public void EarnCurrency(int addedCurrency)
    {
        _currency += addedCurrency;
        _currencyTxt.text = _currency.ToString();
    }
    public int PayCurrency(int price)
    {
        if (_currency <= 0)
        {
            Debug.Log("No more currency");
            return 0;
        }

        _currency -= price;
        _currencyTxt.text = _currency.ToString();
        return price;
    }

    public void TakeResource(Resource newResource)
    {
        if (_resources.Count >= _carryLimit)
        {
            Debug.Log("Carry limit reached");
            return;
        }

        _resources.Add(newResource);
        _resourceCount++;

        newResource.transform.position = _resourcesTr[_resourceCount - 1].position;
        newResource.transform.SetParent(_resourcesTr[_resourceCount - 1]);

        if (_resources.Count == _carryLimit)
        {
            string maxItemCountTxt = _carryLimit.ToString();
            _resourceTxt.text = maxItemCountTxt + "/" + maxItemCountTxt;
            _canvasRTr.anchoredPosition = new Vector2(0, _resources[_resourceCount - 1].transform.position.y);
            _canvasRTr.gameObject.SetActive(true);
        }
    }
    public Resource PayFirstResource()
    {
        if (_resources.Count <= 0)
        {
            Debug.Log("No resource to give");
            return null;
        }

        Resource resourceToPay = _resources[_resources.Count - 1];
        _resources.Remove(resourceToPay);
        _resourceCount--;
        _canvasRTr.gameObject.SetActive(false);
        return resourceToPay;
    }
    public Resource PayResource(ResourceType wantedReseource)
    {
        if (_resources.Count <= 0)
        {
            Debug.Log("No resource to give");
            return null;
        }

        Resource resourceToGive = null;
        for (int i = 0; i < _resources.Count; i++)
        {
            if (_resources[i].Type == wantedReseource)
            {
                resourceToGive = _resources[i];
                break;
            }

            if (i == _resources.Count - 1 && _resources[i].Type != wantedReseource)
                return null;
        }

        _resources.Remove(resourceToGive);
        _resourceCount--;
        _canvasRTr.gameObject.SetActive(false);

        return resourceToGive;
    }
}