using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FuelStove : MonoBehaviour
{
    [SerializeField] private int _convertedCoal = 0;
    public int ConvertedCoal => _convertedCoal;

    [SerializeField] private int _maxConvertedCoal = 3;
    public int MaxConvertedCoal { get => _maxConvertedCoal; set => _maxConvertedCoal = value; }

    [SerializeField] private float _productionTime = 0.5f;
    public float ProductionTime => _productionTime;

    [SerializeField] private bool _isFull = false;
    public bool IsFull { get => _isFull; set => _isFull = value; }

    private List<Resource> _coal;
    public List<Resource> Coal => _coal;

    [SerializeField] private ResourceObjectPool _resourceObjectPool;

    private const string _playerTag = "Player";

    private void Awake()
    {
        _coal = new List<Resource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            TakeCoal(inventory);
        }
    }
    private void TakeCoal(PlayerInventory inventory)
    {
        Resource coal = inventory.PayResource(ResourceType.Coal);
        _coal.Add(coal);
        _resourceObjectPool.ReturnResourceToPool(coal);
    }

    public void ConvertCoal() // need to modify for fuel usage
    {
        if (_isFull || _convertedCoal >= _maxConvertedCoal)
            return;

        _convertedCoal++;
        // do burning vfx

        if (_convertedCoal == _maxConvertedCoal)
            _isFull = true;
        else
            Invoke(nameof(ConvertCoal), _productionTime);
    }
}
