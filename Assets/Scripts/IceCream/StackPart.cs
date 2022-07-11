using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(HingeJoint))]
public class StackPart : MonoBehaviour
{
    [SerializeField] private MeshRenderer _torRenderer;
    [SerializeField] private MeshRenderer _jointSphereRenderer;

    private HingeJoint _hingeJoint;

    public Rigidbody Rigidbody { get; private set; }

    private void OnEnable()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void SetSettings(float newSpring, float newMass, float massScale)
    {
        JointSpring springJoint = _hingeJoint.spring;
        springJoint.spring = newSpring;

        _hingeJoint.spring = springJoint;
        _hingeJoint.massScale = massScale;
        Rigidbody.mass = newMass;
    }

    public void SetConnectedBody(Rigidbody rigidbody, Vector3 connectedAnchor)
    {
        _hingeJoint.connectedBody = rigidbody;
        _hingeJoint.connectedAnchor = connectedAnchor;
    }

    public void SetNewMaterial(Material newMaterial)
    {
        _torRenderer.material = newMaterial;
        _jointSphereRenderer.material = newMaterial;
    }
}