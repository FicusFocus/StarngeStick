using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Hand : MonoBehaviour
{
    private NewIceCream _icecream;

    public event UnityAction<Direction> RotateTriggerTaked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PartTotake icecreamPartToTake))
            TakeIcecream(icecreamPartToTake);
        else if (other.TryGetComponent(out RotateTrigger rotate))
            RotateTriggerTaked?.Invoke(rotate.Direction);
        else if (other.TryGetComponent(out ColorSetter iceCreamColoreSetter))
            ChangeIceCreamMaterial(iceCreamColoreSetter.NewColor);
    }

    public void TakeIcecream(PartTotake icecreamPartToTake)
    {
        _icecream.InstantiateNewIceCreamPart();
        icecreamPartToTake.DestroyPart();
    }

    public void ChangeIceCreamMaterial(Material material)
    {
        _icecream.ChangeMaterial(material);
    }

    public void Init(NewIceCream icecream)
    {
        _icecream = icecream;
    }
}
