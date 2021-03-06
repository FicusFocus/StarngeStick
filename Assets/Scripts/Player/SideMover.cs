using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SideMover : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private SpringStack _springStack;
    private Vector3 _right = Vector3.right;
    private Vector3 _left = Vector3.left;
    private float _sideSpeed;

    private void SideMove(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x > 0)
                _springStack.transform.position += _right * _sideSpeed * Time.deltaTime;
            else
                _springStack.transform.position += _left * _sideSpeed * Time.deltaTime;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) { }

    public void OnDrag(PointerEventData eventData) => SideMove(eventData);

    public void Init(float sideSpeedValue, SpringStack springStack)
    {
        _sideSpeed = sideSpeedValue;
        _springStack = springStack;
    }

    public void SetNewDirectionVectors(Vector3 right, Vector3 left)
    {
        _right = right;
        _left = left;
    }
}
