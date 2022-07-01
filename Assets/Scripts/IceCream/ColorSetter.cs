using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColorSetter : MonoBehaviour
{
    [SerializeField] private Material _material;

    private Collider _collider;

    public Material NewColor => _material;

    private void OnEnable()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }
}
