using UnityEngine;

public abstract class SpringPartsPreset : ScriptableObject
{
    [SerializeField] private float _wheitStep = 0.2f;
    [SerializeField] private float _baseWheit = 1;
    [SerializeField] private float _minMassScale = 1;
    [SerializeField] private float _springPowerStep;
    [SerializeField] private float _baseSpring;

    public float SpringStep => _springPowerStep;
    public float MinMassScale => _minMassScale;
    public float BaseSpring => _baseSpring;
    public float BaseWheit => _baseWheit;
    public float WheitStep => _wheitStep;
}
