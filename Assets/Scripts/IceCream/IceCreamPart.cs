using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IceCreamPart : MonoBehaviour
{
    [SerializeField] private MeshRenderer _torRenderer;
    [SerializeField] private MeshRenderer _jointSphereRenderer;
    [SerializeField] private AnimationClip _bounceAnimation;
    [SerializeField] private float _moveSpeed;

    private Animator _animator;
    private Vector3 _targetPosition;
    private Transform _trget;
    private float _distanceBetweenParts;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_trget == null)
            return;

        transform.LookAt(_trget.transform);
        _targetPosition = _trget.transform.position;
        _targetPosition.y = _trget.transform.position.y + _distanceBetweenParts;

        if (_targetPosition != transform.position)
        {
            var targetPosition = new Vector3(_targetPosition.x, _targetPosition.y, _targetPosition.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
        }
    }

    public void SetNewMaterial(Material newMaterial)
    {
        _torRenderer.material = newMaterial;
        _jointSphereRenderer.material = newMaterial;
    }

    public void Init(Transform target, float distanceBetweenParts)
    {
        _trget = target;
        _distanceBetweenParts = distanceBetweenParts;
    }

    public void Bounce()
    {
        _animator.Play(_bounceAnimation.name);
    }
}