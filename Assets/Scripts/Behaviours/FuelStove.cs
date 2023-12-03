using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FuelStove : MonoBehaviour
{
    [SerializeField] private ResourceObjectPool _resourceObjectPool;
    [SerializeField] private IronProducer _forge;

    private const string _playerTag = "Player";
    private PlayerInventory _playerInventory;

    [Header("Coal")]
    [SerializeField] private int _maxCoal = 3;
    public int MaxCoal { get => _maxCoal; set => _maxCoal = value; }

    [SerializeField] private bool _isFull = false;
    public bool IsFull { get => _isFull; set => _isFull = value; }

    private List<Resource> _coal;
    public List<Resource> Coal => _coal;

    [Header("Converted Coal")]
    [SerializeField] private int _convertedCoal = 0;
    public int ConvertedCoal => _convertedCoal;

    [SerializeField] private float _productionTime = 0.5f;
    public float ProductionTime => _productionTime;

    private void Awake()
    {
        _coal = new List<Resource>();
    }
    private void Start()
    {
        Initialize();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
            _playerInventory = other.GetComponent<PlayerInventory>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!_playerInventory)
            return;

        if (_coal.Count < _maxCoal)
            TakeCoal();
        else
            Debug.Log("Max convered coal reached!");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
            _playerInventory = null;
    }

    private void Initialize()
    {
        if (!_resourceObjectPool && GameManager.Instance.ResourcePool)
            _resourceObjectPool = GameManager.Instance.ResourcePool;
    }
    private void TakeCoal()
    {
        Resource coal = _playerInventory.PayResource(ResourceType.Coal);
        _coal.Add(coal);
        _resourceObjectPool.ReturnResourceToPool(coal);
    }

    public void ConvertCoal() // need to modify for fuel usage
    {
        if (_isFull || _convertedCoal >= _maxCoal)
            return;

        _convertedCoal++;
        // do burning vfx

        if (_convertedCoal == _maxCoal)
            _isFull = true;
        else
            Invoke(nameof(ConvertCoal), _productionTime);
    }
}
