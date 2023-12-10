using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionProducer : ResourceProducer
{
    [Header("Product Source")]
    [SerializeField] private ResourceObjectPool _resourcePool;
    public override ResourceObjectPool ResourcePool { get => _resourcePool; set => _resourcePool = value; }

    [SerializeField] private MushroomProducer _mushroomCluster;
    public MushroomProducer MushroomCluster => _mushroomCluster;

    [Header("Production Details")]
    [SerializeField] private ResourceProducerType _type = ResourceProducerType.AlchemyTable;
    public override ResourceProducerType Type => _type;

    [SerializeField] private int _upgradeFactor = 3;
    public override int UpgradeFactor { get => _upgradeFactor; }

    [SerializeField] private int _maxProducts = 3;
    public override int MaxProducts { get => _maxProducts; set => _maxProducts = value; }

    [SerializeField] private float _productionTime = 1.0f;
    public override float ProductionTime => _productionTime;

    [SerializeField] private bool _isFull = false;
    public override bool IsFull { get => _isFull; set => _isFull = value; }

    [Header("Product placements")]
    [SerializeField] private Transform[] _productsTr;
    public override Transform[] ProductsTr => _productsTr;

    private List<Resource> _products;
    public override List<Resource> Products => _products;

    private const string _playerTag = "Player";
    private PlayerInventory _playerInventory;

    private void Awake()
    {
        _products = new List<Resource>();
    }
    private void Start()
    {
        Initialize();
        Produce();
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

        if (_products.Count > 0)
            GiveIron();
        else
            Debug.Log("Gave all iron!");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
            _playerInventory = null;
    }

    public override void Produce() // need to modify for fuel usage
    {
        int availableMushrooms = _mushroomCluster.Products.Count;

        /*if (_isFull)
        {
            StartCoroutine(WaitForProductionSpace());
            return;
        }
        else if (availableMushrooms < 1)
        {
            StartCoroutine(WaitForProduction());
            return;
        }*/

        int productCount = _products.Count;
        if (productCount < _maxProducts && _productsTr[productCount].childCount < 1)
        {
            int resourceIndex = (int)_type;
            Resource newResource = _resourcePool.GetResourceFromPool(resourceIndex);
            newResource.transform.position = _productsTr[productCount].position;
            //_mushroomCluster.Complete();
            _products.Add(newResource);

            if (productCount == _maxProducts)
                _isFull = true;
        }

        Invoke(nameof(Produce), _productionTime);
    }

    /*private IEnumerator WaitForProduction()
    {
        yield return new WaitUntil(() => _engine.ConvertedCoal > 0);
        Produce();
    }
    private IEnumerator WaitForProductionSpace()
    {
        yield return new WaitUntil(() => _engine.ConvertedCoal < _maxProducts);
        StartCoroutine(WaitForProduction());
    }*/

    private void GiveIron()
    {
        if (_products.Count > 0)
        {
            _playerInventory.TakeResource(_products[0]);
            _products.RemoveAt(0);
        }
    }
}
