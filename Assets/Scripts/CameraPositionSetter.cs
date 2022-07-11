using UnityEngine;

public class CameraPositionSetter : MonoBehaviour
{
    [SerializeField] private float _maxOffsetX;
    [SerializeField] private float _zOffset; 

    private SpringStack _springStack;
    private Vector3 _bacePosition;

    private void Update() => TryMove();

    private void TryMove()
    {
        if (_springStack.transform.localPosition.x >= _maxOffsetX || _springStack.transform.localPosition.x <= -_maxOffsetX)
            return;

        var targetPosition = new Vector3(_springStack.transform.localPosition.x, 
                                         _bacePosition.y, 
                                         _springStack.transform.localPosition.z - _zOffset);
        transform.localPosition = targetPosition;
    }

    public void Init(SpringStack springStack)
    {
        _springStack = springStack;
        _bacePosition = transform.position;
    }
}
