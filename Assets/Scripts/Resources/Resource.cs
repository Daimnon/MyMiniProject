using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    public ResourceType Type => _type;

    [SerializeField] public Renderer _renderer;

    private void OnTriggerEnter(Collider other)
    {
        
    }
}