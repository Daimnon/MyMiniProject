using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ResourceObjectPool : MonoBehaviour
{
    [SerializeField] private Resource[] _resourcePrefabs;
    [SerializeField] private int _initialPoolSize = 200;
    [SerializeField] float DespawnDelay = 10;

    private List<Resource> _resourcePool;

    private void Start()
    {
        _resourcePool = new List<Resource>();

        for (int i = 0; i < _resourcePrefabs.Length; i++)
        {
            for (int j = 0; j < _initialPoolSize; j++)
            {
                Resource resource = Instantiate(_resourcePrefabs[i], transform);
                resource.gameObject.SetActive(false);
                _resourcePool.Add(resource);
            }
        }
    }

    public Resource GetResourceFromPool(int resourceType)
    {
        if (resourceType == 0) { Debug.LogError($"Sent resource was {(ResourceType)resourceType}"); return null; }

        for (int i = 0; i < _resourcePool.Count; i++)
        {
            if (_resourcePool[i].Type != (ResourceType)resourceType)
                continue;

            Resource resource = _resourcePool[i];
            if (!resource.gameObject.activeSelf)
            {
                resource.gameObject.SetActive(true);
                ReturnResourceToPool(resource);
                return resource;
            }
        }

        // If no inactive money objects are available, create a new one
        Resource newResource = Instantiate(_resourcePrefabs[resourceType]);
        _resourcePool.Add(newResource);
        newResource.gameObject.SetActive(true);
        ReturnResourceToPool(newResource);
        return newResource;
    }
    public void ReturnResourceToPool(Resource resource)
    {
        resource.gameObject.SetActive(false);
        resource.transform.SetParent(transform);
        resource.transform.position = Vector3.zero;
    }
}