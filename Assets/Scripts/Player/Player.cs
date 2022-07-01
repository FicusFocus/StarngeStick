using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RotationSetter _rotationSetter;
    [SerializeField] private IceCream _icecream;
    [SerializeField] private SideMover _sideMover;
    [SerializeField] private CameraPositionSetter _cameraPositionSetter;
    [SerializeField] private float _sideSpeed;
    [SerializeField] private float _forvardSpeed;

    private void OnEnable()
    {
        _sideMover.Init(_sideSpeed, _icecream);
        _cameraPositionSetter.Init(_icecream);
        _icecream.RotateTriggerTaked += OnRotatetriggerTaket;
    }

    private void OnDisable()
    {
        _icecream.RotateTriggerTaked -= OnRotatetriggerTaket;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _forvardSpeed * Time.deltaTime);
    }

    
    
    private void OnRotatetriggerTaket(Direction direction)
    {
        switch (direction)
        {
            case Direction.Right:
                _sideMover.SetNewDirectionVectors(Vector3.back, Vector3.forward);
                break;

            case Direction.Left:
                _sideMover.SetNewDirectionVectors(Vector3.forward, Vector3.back);
                break;

            case Direction.Forward:
                _sideMover.SetNewDirectionVectors(Vector3.right, Vector3.left);
                break;
        }

        _rotationSetter.SetTargetRotation(direction);
    }

    private void ChangeIceCreamMaterial(Material material)
    {
        _icecream.ChangeMaterial(material);
    }

    public void SetForvardSpeedValue(float value)
    {
        if (value < 0 || value > 10)
            return;

        _forvardSpeed = value;
    }
}
