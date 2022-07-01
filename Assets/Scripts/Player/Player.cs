using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private RotationSetter _rotationSetter;
    [SerializeField] private NewIceCream _icecream;
    [SerializeField] private SideMover _sideMover;
    [SerializeField] private CameraPositionSetter _cameraPositionSetter;
    [SerializeField] private float _sideSpeed;
    [SerializeField] private float _forvardSpeed;

    private void OnEnable()
    {
        _sideMover.Init(_sideSpeed, _icecream);
        _cameraPositionSetter.Init(_icecream);
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PartTotake icecreamPartToTake))
            TakeNewIceCreamPart(icecreamPartToTake);
        else if (other.TryGetComponent(out RotateTrigger rotate))
            ChangeDirection(rotate.Direction);
        else if (other.TryGetComponent(out ColorSetter iceCreamColoreSetter))
            ChangeIceCreamMaterial(iceCreamColoreSetter.NewColor);
    }

    private void ChangeIceCreamMaterial(Material material)
    {
        _icecream.ChangeMaterial(material);
    }

    private void TakeNewIceCreamPart(PartTotake icecreamPartToTake)
    {
        _icecream.InstantiateNewIceCreamPart();
        icecreamPartToTake.DestroyPart();
    }

    private void ChangeDirection(Direction rotate)
    {
        switch (rotate)
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

        _rotationSetter.SetTargetRotation(rotate);
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _forvardSpeed * Time.deltaTime);
    }

    public void SetForvardSpeedValue(float value)
    {
        if (value < 0 || value > 10)
            return;

        _forvardSpeed = value;
    }
}
