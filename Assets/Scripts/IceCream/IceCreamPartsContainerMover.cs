using UnityEngine;

public class IceCreamPartsContainerMover : MonoBehaviour
{
    [SerializeField] private Transform _iceCream;

    private void Update()
    {
        transform.position = _iceCream.transform.position;
    }
}
