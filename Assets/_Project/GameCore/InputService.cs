using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour
{
    public InputActionAsset asset;

    private void Awake()
    {
        asset?.Enable();
    }

    private void OnDestroy()
    {
        asset?.Disable();
    }
}
