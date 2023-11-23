using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    public ResourceType Type => _type;

    [SerializeField] public Renderer _renderer;

    private const string _playerTag = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_playerTag))
            return;
    }
}