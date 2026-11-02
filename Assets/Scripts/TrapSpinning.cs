using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class TrapSpinning : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private Rigidbody rb;
    void Start() { rb = GetComponent<Rigidbody>(); }
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}