using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnlockerType
{
    MushroomCluster,
    Tree,
    Quarry,
    Forge
}

public class Unlocker : MonoBehaviour
{
    [SerializeField] private UnlockerType _type;
    public UnlockerType Type => _type;

    [SerializeField] private GameObject[] _unlockablePrefabs;
    [SerializeField] private Transform _spawnPos;

    private const string _playerTag = "Player";

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {

        }
    }
    private void Unlock()
    {
        int prefabIndex = (int)_type;
        Instantiate(_unlockablePrefabs[prefabIndex], _spawnPos.position, Quaternion.identity);
    }
}
