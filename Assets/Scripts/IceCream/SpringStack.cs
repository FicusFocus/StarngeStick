using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class SpringStack : MonoBehaviour
{
    [SerializeField] private float _distanceBetweenParts = 0.25f;

    [SerializeField] private Transform _stackpartsContainer;
    [SerializeField] private LowerPreset _lowerPreset;
    [SerializeField] private MidlePreset _midlePreset;
    [SerializeField] private UperPreset _uperPreset;
    [SerializeField] private Material _startMaterial;
    [SerializeField] private StackPart _tamplate;
    [SerializeField] private MeshRenderer _stick;
    [SerializeField] private int _startSteckSize;

    private List<StackPart> _stack = new List<StackPart>();
    private StackPart _lastSpawnedPart;
    private Material _currentMaterial;
    private Rigidbody _rigidbody;

    public event UnityAction<Direction> RotateTriggerTaked;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentMaterial = _startMaterial;
    }

    private void Start()
    {
        for (int i = 0; i < _startSteckSize; i++)
            AddSteckPart();

        FindeCorrectSpringPartsPreset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PartTotake icecreamPartToTake))
        {
            AddSteckPart();
            FindeCorrectSpringPartsPreset();
            icecreamPartToTake.DestroyPart();
            return;
        }

        if (other.TryGetComponent(out RotateTrigger rotate))
        {
            RotateTriggerTaked?.Invoke(rotate.Direction);
            rotate.Disable();
        }
        else if (other.TryGetComponent(out ColorSetter iceCreamColoreSetter))
        {
            ChangeMaterial(iceCreamColoreSetter.NewColor);
        }
    }

    private void AddSteckPart()
    {
        Vector3 newPosition = _stackpartsContainer.position;
        newPosition.y += _distanceBetweenParts * _stack.Count;

        StackPart newStackPart = Instantiate(_tamplate, newPosition, _stackpartsContainer.rotation, _stackpartsContainer); ;

        if (_stack.Count == 0)
            newStackPart.SetConnectedBody(this._rigidbody, new Vector3(0, _distanceBetweenParts, 0));
        else
            newStackPart.SetConnectedBody(_lastSpawnedPart.Rigidbody, new Vector3(0, _distanceBetweenParts, 0));

        newStackPart.SetNewMaterial(_currentMaterial);
        _lastSpawnedPart = newStackPart;
        _stack.Add(newStackPart);
    }

    private void FindeCorrectSpringPartsPreset()
    {
        if (_stack.Count <= 6)
            SetSpringParsSettings(_lowerPreset);
        else if (_stack.Count > 6 && _stack.Count <= 10)
            SetSpringParsSettings(_midlePreset);
        else if (_stack.Count > 10)
            SetSpringParsSettings(_uperPreset);
    }

    private void SetSpringParsSettings(SpringPartsPreset preset)
    {
        int iterationNumber = 0;

        for (int i = _stack.Count - 1; i >= 0; i--)
        {
            float spring = preset.BaseSpring + (preset.SpringStep * iterationNumber);
            float mass = preset.BaseWheit + (preset.WheitStep * iterationNumber);
            float massScale = ((preset.MinMassScale / _stack.Count) * iterationNumber) + preset.MinMassScale;

            _stack[i].SetSettings(spring, mass, massScale);
            iterationNumber++;
        }
    }

    public void ChangeMaterial(Material newMaterial)
    {
        foreach (StackPart stackPart in _stack)
            stackPart.SetNewMaterial(newMaterial);

        _stick.material = newMaterial;
        _currentMaterial = newMaterial;
    }
}
