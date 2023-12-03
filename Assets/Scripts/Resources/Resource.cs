using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    public ResourceType Type => _type;

    [SerializeField] private Renderer _renderer;
    [SerializeField] private bool _isInInventory = false;

    private const string _playerTag = "Player";
    private void OnTriggerEnter(Collider other) // might conflict with producer's trigger - take notice.
    {
        if (!_isInInventory && other.CompareTag(_playerTag))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            Pickup(playerInventory);
        }
    }

    private void Pickup(PlayerInventory playerInventory)
    {
        playerInventory.TakeResource(this);
        _isInInventory = true;
    }
}