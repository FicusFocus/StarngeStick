using UnityEngine;

public class RotateTrigger : MonoBehaviour
{
    [SerializeField] private Direction _direction;

    public Direction Direction => _direction;

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
