using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class NewIceCream : MonoBehaviour
{
    [SerializeField] private IceCreamPart _newPartTemplate;
    [SerializeField] private IceCreamPart _topPartTemplate;
    [SerializeField] private Transform _partsContainer;
    [SerializeField] private Material _startMaterial;
    [SerializeField] private MeshRenderer _stick;
    [SerializeField] private float _distanceBetweenParts;

    private List<IceCreamPart> _iceCreamParts = new List<IceCreamPart>();
    private Material _currentMaterial;

    public event UnityAction<Direction> RotateTriggerTaked;

    private void OnEnable()
    {
        _currentMaterial = _startMaterial;
        InstantiateNewIceCreamPart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PartTotake icecreamPartToTake))
        {
            InstantiateNewIceCreamPart();
            icecreamPartToTake.DestroyPart();
            return;
        }

        if (other.TryGetComponent(out RotateTrigger rotate))
            RotateTriggerTaked?.Invoke(rotate.Direction);
        else if (other.TryGetComponent(out ColorSetter iceCreamColoreSetter))
            ChangeMaterial(iceCreamColoreSetter.NewColor);
    }

    private void ChangeIceCreamPartsTargets()
    {
        for (int i = 0; i < _iceCreamParts.Count; i++)
        {
            if (i == _iceCreamParts.Count - 1)
                _iceCreamParts[i].Init(this.transform, _distanceBetweenParts);
            else
                _iceCreamParts[i].Init(_iceCreamParts[i + 1].transform, _distanceBetweenParts);
        }
    }

    private void InstantiateNewIceCreamPart()
    {
        IceCreamPart newPart;

        if (_iceCreamParts.Count == 0)
            newPart = Instantiate(_topPartTemplate, _partsContainer.position, Quaternion.identity, _partsContainer);
        else
            newPart = Instantiate(_newPartTemplate, _partsContainer.position, Quaternion.identity, _partsContainer);

        newPart.SetNewMaterial(_currentMaterial);
        _iceCreamParts.Add(newPart);
        ChangeIceCreamPartsTargets();
    }

    public void ChangeMaterial(Material newMaterial)
    {
        foreach (IceCreamPart iceCreamPart in _iceCreamParts)
            iceCreamPart.SetNewMaterial(newMaterial);

        _stick.material = newMaterial;
        _currentMaterial = newMaterial;
    }
}
