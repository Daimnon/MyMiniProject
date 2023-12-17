using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIObjectPool : MonoBehaviour
{
    [SerializeField] private Adventurer[] _adventurerPrefabs;
    [SerializeField] private int _initialPoolSize = 100;

    private List<Adventurer> _aiPool;

    private void Awake()
    {
        _aiPool = new List<Adventurer>();
    }
    private void OnEnable()
    {
        EventManager.OnLevelLaunched += OnLevelLaunched;
    }
    private void OnDisable()
    {
        EventManager.OnLevelLaunched -= OnLevelLaunched;
    }

    private void Initialize()
    {
        for (int i = 0; i < _adventurerPrefabs.Length; i++)
        {
            for (int j = 0; j < _initialPoolSize; j++)
            {
                Adventurer adventurer = Instantiate(_adventurerPrefabs[i], transform);
                adventurer.gameObject.SetActive(false);
                _aiPool.Add(adventurer);
            }
        }
    }

    public Adventurer GetResourceFromPool(int adventurerTypeIndex)
    {
        for (int i = 0; i < _aiPool.Count; i++)
        {
            if (_aiPool[i].AdventurerType != (AdventurerType)adventurerTypeIndex)
                continue;

            Adventurer adventurer = _aiPool[i];
            if (!adventurer.gameObject.activeSelf)
            {
                adventurer.gameObject.SetActive(true);
                return adventurer;
            }
        }

        // If no inactive money objects are available, create a new one
        Adventurer newAdventurer = Instantiate(_adventurerPrefabs[adventurerTypeIndex], transform);
        _aiPool.Add(newAdventurer);
        newAdventurer.gameObject.SetActive(true);
        return newAdventurer;
    }
    public void ReturnAdventurerToPool(Adventurer resource)
    {
        resource.gameObject.SetActive(false);
        resource.transform.SetParent(transform);
        resource.transform.position = Vector3.zero;
    }

    private void OnLevelLaunched()
    {
        Initialize();
    }

}
