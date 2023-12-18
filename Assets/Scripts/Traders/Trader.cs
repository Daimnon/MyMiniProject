using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    //[SerializeField] private GameObject _currencyPrefab;
    //public GameObject CurrencyPrefab => _currencyPrefab;
    //
    //[SerializeField] private Transform[] _productsTr;
    //public Transform[] ProductsTr => _productsTr;
    //
    //private CurrencyObjectPool _currencyObjectPool;
    //private PlayerInventory _playerInventory;
    //private List<Adventurer> _adventurers;
    //private const string _playerTag = "Player";
    //private const string _aiTag = "AI";
    //
    //private void Awake()
    //{
    //    _adventurers = new List<Adventurer>();
    //}
    //private void Start()
    //{
    //    _currencyObjectPool = GameManager.Instance.CurrencyPool;
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(_aiTag)) // do entering animation on self
    //        _adventurers.Add(other.GetComponent<Adventurer>());
    //    else if (other.CompareTag(_playerTag) && !_playerInventory)
    //        _playerInventory = other.GetComponent<PlayerInventory>();
    //    else
    //        return;
    //    // do entering animation on self
    //}
}
