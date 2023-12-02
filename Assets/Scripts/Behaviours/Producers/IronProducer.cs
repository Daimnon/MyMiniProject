using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronProducer : Producer
{
    [Header("Product Source")]
    [SerializeField] private ResourceObjectPool _resourcePool;
    public override ResourceObjectPool ResourcePool { get => _resourcePool; set => _resourcePool = value; }

    [SerializeField] private FuelStove _engine;
    public FuelStove Engine => _engine;

    [Header("Production Details")]
    [SerializeField] private ProducerType _type;
    public override ProducerType Type => _type;

    [SerializeField] private int _maxProducts = 3;
    public override int MaxProducts { get => _maxProducts; set => _maxProducts = value; }

    [SerializeField] private float _productionTime = 0.1f;
    public override float ProductionTime => _productionTime;

    [SerializeField] private bool _isFull = false;
    public override bool IsFull { get => _isFull; set => _isFull = value; }

    [Header("Product placements")]
    [SerializeField] private Transform[] _productsTr;
    public override Transform[] ProductsTr => _productsTr;

    private List<Resource> _products;
    public override List<Resource> Products => _products;

    private void Awake()
    {
        _products = new List<Resource>();
    }
    private void Start()
    {
        Initialize();
    }
    public override void Produce() // need to modify for fuel usage
    {
        int convertedCoal = _engine.ConvertedCoal;
        if (_isFull || convertedCoal < 1)
            return;

        int productCount = _products.Count;
        if (productCount < _maxProducts && _productsTr[productCount].childCount < 1)
        {
            int resourceIndex = (int)_type;
            Resource newResource = _resourcePool.GetResourceFromPool(resourceIndex);
            newResource.transform.position = _productsTr[productCount].position;
            _products.Add(newResource);
            convertedCoal--;

            if (productCount == _maxProducts)
                _isFull = true;
        }

        if (!_isFull)
            Invoke(nameof(Produce), _productionTime);
    }

    private IEnumerator WaitForProduction()
    {
        yield return new WaitUntil(() => _engine.ConvertedCoal > 0);
        Produce();
    }
}
