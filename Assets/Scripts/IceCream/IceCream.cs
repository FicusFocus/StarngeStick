using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : MonoBehaviour
{
    [SerializeField] private Transform _partsContainer;
    [SerializeField] private NewPart _newPartTemplate;
    [SerializeField] private BacePart _bacePartTemplate;
    [SerializeField] private AnimationClip _bounceAnimation;
    [SerializeField] private float _distanceBetweenParts;
    [SerializeField] private Material _startMaterial;

    private int _initialIceCreamSize = 1;
    private List<IceCreamPart> _iceCreamParts = new List<IceCreamPart>();
    private IceCreamPart _lastSpawnedPart;
    private Material _currentMaterial;
    private BacePart _bacePart;
    private IEnumerator _bounceCurotine;

    private void OnEnable()
    {
        _currentMaterial = _startMaterial;

        for (int i = 0; i < _initialIceCreamSize; i++)
            InstantiateNewIceCreamPart();
    }

    public void InstantiateNewIceCreamPart()
    {
        if (_iceCreamParts.Count == 0)
        {
            _bacePart = Instantiate(_bacePartTemplate, FindNewIceCreamPartPosition(), _newPartTemplate.transform.rotation, _partsContainer);
            _bacePart.Init(this.transform, _distanceBetweenParts);
            _bacePart.SetNewMaterial(_currentMaterial);
            _iceCreamParts.Add(_bacePart);
            _lastSpawnedPart = _bacePart;
        }
        else
        {
            NewPart newPart = Instantiate(_newPartTemplate, FindNewIceCreamPartPosition(), _newPartTemplate.transform.rotation, _partsContainer);
            newPart.Init(_lastSpawnedPart.transform, _distanceBetweenParts);
            newPart.SetNewMaterial(_currentMaterial);
            _iceCreamParts.Add(newPart);
            _lastSpawnedPart = newPart;
            //StartBounce();
        }
    }

    private Vector3 FindNewIceCreamPartPosition()
    {
        if (_iceCreamParts.Count == 0)
            return _partsContainer.position;

        return new Vector3(_lastSpawnedPart.transform.position.x, 
                           _lastSpawnedPart.transform.position.y + _distanceBetweenParts, 
                           _lastSpawnedPart.transform.position.z);
    }

    public void ChangeMaterial(Material newMaterial)
    {
        foreach (IceCreamPart iceCreamPart in _iceCreamParts)
            iceCreamPart.SetNewMaterial(newMaterial);

        _currentMaterial = newMaterial;
    }

    private IEnumerator BounceIceCream()
    {
        WaitForSeconds bounceAnimationLenth = new WaitForSeconds(0.5f);

        Debug.Log(_iceCreamParts.Count);

        foreach (IceCreamPart iceCreamPart in _iceCreamParts)
        {
            iceCreamPart.Bounce();
            yield return bounceAnimationLenth;
        }
    }

    private void StartBounce()
    {
        if (_bounceCurotine != null)
        {
            StopCoroutine(_bounceCurotine);
            _bounceCurotine = null;
        }

        _bounceCurotine = BounceIceCream();
        StartCoroutine(_bounceCurotine);
    }
}
