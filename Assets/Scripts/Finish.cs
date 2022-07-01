using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Finish : MonoBehaviour
{
    public event UnityAction FinishPassed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out  IceCream hand))
            FinishPassed?.Invoke();
    }
}
