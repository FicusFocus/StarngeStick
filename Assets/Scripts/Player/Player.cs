using System;
using UnityEngine;

[RequireComponent(typeof(Animator))] 
public class Player : MonoBehaviour
{
    [SerializeField] private CameraPositionSetter _cameraPositionSetter;
    [SerializeField] private RotationSetter _rotationSetter;
    [SerializeField] private SpringStack _springStack;
    [SerializeField] private SideMover _sideMover;
    [SerializeField] private float _forvardSpeed;
    [SerializeField] private float _sideSpeed;

    private Animator _animator;
    private bool _canMove = true;
    private string _stopTrigger = "Stop";

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _sideMover.Init(_sideSpeed, _springStack);
        _cameraPositionSetter.Init(_springStack);
        _springStack.RotateTriggerTaked += OnRotatetriggerTaket;
    }

    private void OnDisable() => _springStack.RotateTriggerTaked -= OnRotatetriggerTaket;

    private void Update()
    {
        if (_canMove)
            Move();
    }

    private void Move() => transform.Translate(Vector3.forward * _forvardSpeed * Time.deltaTime);

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
    
    private void ChangeIceCreamMaterial(Material material) => _springStack.ChangeMaterial(material);

    public void Stop()
    {
        _canMove = false;
        _animator.SetTrigger(_stopTrigger);
    }
}
