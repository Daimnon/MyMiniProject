using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MushroomToPotionConverter : MonoBehaviour
{
    [SerializeField] private MushroomProducer _mushroomClaster;
    [SerializeField] private MeshRenderer[] _potionRenderes;
    [SerializeField] private Material _clearMat, _completeMat;
    [SerializeField] private int _potionCount = 3, _clearPotions = 3, _completePotions = 0;

    [SerializeField] private bool _isFull = false;
    public  bool IsFull { get => _isFull; set => _isFull = value; }

    private ResourceObjectPool _resourceObjectPool;
    private PlayerInventory _playerInventory;
    private const string _playerTag = "Player";

    private void Awake()
    {
        _clearMat = new(_clearMat);
        _completeMat = new(_completeMat);
    }
    private void Start()
    {
        _resourceObjectPool = GameManager.Instance.ResourcePool;
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

        if (_mushroomClaster.Products.Count > 0)
            CompletePotion();
        else
            Debug.Log("Gave all iron!");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
            _playerInventory = null;
    }

    /*public void CompletePotion() // need to modify for fuel usage
    {
        int convertedCoal = _engine.ConvertedCoal;

        if (_isFull)
        {
            StartCoroutine(WaitForProductionSpace());
            return;
        }
        else if (convertedCoal < 1)
        {
            StartCoroutine(WaitForProduction());
            return;
        }

        int productCount = _products.Count;
        if (productCount < _maxProducts && _productsTr[productCount].childCount < 1)
        {
            int resourceIndex = (int)_type;
            Resource newResource = _resourcePool.GetResourceFromPool(resourceIndex);
            newResource.transform.position = _productsTr[productCount].position;
            _engine.UseConvertedCoal();
            _products.Add(newResource);

            if (productCount == _maxProducts)
                _isFull = true;
        }

        Invoke(nameof(Produce), _productionTime);
    }

    private IEnumerator WaitForProduction()
    {
        yield return new WaitUntil(() => _engine.ConvertedCoal > 0);
        Produce();
    }
    private IEnumerator WaitForProductionSpace()
    {
        yield return new WaitUntil(() => _engine.ConvertedCoal < _maxProducts);
        StartCoroutine(WaitForProduction());
    }

    private void GiveIron()
    {
        if (_products.Count > 0)
        {
            _playerInventory.TakeResource(_products[0]);
            _products.RemoveAt(0);
        }
    }*/

    private void CompletePotion()
    {
        if (_completePotions >= _potionCount)
            return;

        _potionRenderes[_completePotions].material = _completeMat;
        _clearPotions--;
        _completePotions++;
    }
    private void EmptyPotion()
    {
        if (_completePotions < 1)
            return;

        _potionRenderes[_clearPotions].material = _clearMat;
        _completePotions--;
        _clearPotions++;
    }
}
