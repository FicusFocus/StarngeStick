using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackpartsConteinerPositionSetter : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _speed = 5;

    void Update()
    {
        if (_target == null)
            return;

        Move();
    }

    private void Move()
    {
        Vector3 targetPosition = _target.transform.position;
        targetPosition.y = transform.position.y;
        targetPosition.z = _target.transform.position.z - 1.4f;

        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}
