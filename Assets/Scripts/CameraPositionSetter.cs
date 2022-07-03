using UnityEngine;

public class CameraPositionSetter : MonoBehaviour
{
    [SerializeField] private float _maxOffsetX;
    [SerializeField] private float _zOffset; 

    private IceCream _iceCream;
    private Vector3 _bacePosition;

    private void Update()
    {
        TryMove();
    }

    private void TryMove()
    {
        if (_iceCream.transform.localPosition.x >= _maxOffsetX || _iceCream.transform.localPosition.x <= -_maxOffsetX)
            return;

        var targetPosition = new Vector3(_iceCream.transform.localPosition.x, 
                                         _bacePosition.y, 
                                         _iceCream.transform.localPosition.z - _zOffset);
        transform.localPosition = targetPosition;
    }

    public void Init(IceCream targetIceCream)
    {
        _iceCream = targetIceCream;
        _bacePosition = transform.position;
    }
}
