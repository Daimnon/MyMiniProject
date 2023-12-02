using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FuelStove : MonoBehaviour
{
    [SerializeField] private int _convertedCoal = 3;
    public int ConvertedCoal { get => _convertedCoal; set => _convertedCoal = value; }

    [SerializeField] private float _productionTime = 0.5f;
    public float ProductionTime => _productionTime;

    [SerializeField] private bool _isFull = false;
    public bool IsFull { get => _isFull; set => _isFull = value; }

    private List<Resource> _coal;
    public List<Resource> Coal => _coal;

    /*public override void ConvertCoal() // need to modify for fuel usage
    {
        if (_isFull)
            return;

        int productCount = _products.Count;
        if (productCount < _maxProducts && _productsTr[productCount].childCount < 1)
        {
            int resourceIndex = (int)_type;
            Resource newResource = _resourcePool.GetResourceFromPool(resourceIndex);
            newResource.transform.position = _productsTr[productCount].position;
            _products.Add(newResource);

            if (productCount == _maxProducts)
                _isFull = true;
        }

        if (!_isFull)
            Invoke(nameof(Produce), _productionTime);
    }*/
}
