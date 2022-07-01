using DG.Tweening;
using System.Collections;
using UnityEngine;

public class RotationSetter : MonoBehaviour
{
    [SerializeField] private float _durationRotation;

    private Quaternion _targetRotation;

    public void SetTargetRotation(Direction direction)
    {
        float currentRotationY = transform.rotation.y;

        switch (direction)
        {
            case Direction.Right:
                _targetRotation = Quaternion.Euler(0, currentRotationY + 90, 0);
                break;

            case Direction.Left:
                _targetRotation = Quaternion.Euler(0, currentRotationY - 90, 0);
                break;

            case Direction.Forward:
                _targetRotation = Quaternion.Euler(Vector3.zero);
                break;
        }

        TryStartRotation();
    }

    private void TryStartRotation()
    {
        transform.DORotate(_targetRotation.eulerAngles, _durationRotation);
    }
}
