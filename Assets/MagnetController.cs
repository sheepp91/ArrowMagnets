using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    public float strength = 5.0f;
    public int direction = 1;

    private Transform arrow;

    private SphereCollider magneticField;

    private void Start()
    {
        magneticField = transform.GetComponent<SphereCollider>();
    }

    void FixedUpdate()
    {
        if (arrow)
        {
            Vector3 directionToMagnet = transform.position - arrow.position;
            float distance = Vector3.Distance(transform.position, arrow.position);
            float magnetDistanceStrength = (magneticField.radius / distance) * strength;

            Rigidbody arrowRB = arrow.GetComponent<Rigidbody>();
            arrowRB.AddForce(magnetDistanceStrength * (directionToMagnet * direction), ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Arrow"))
        {
            arrow = collider.transform;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.CompareTag("Arrow"))
        {
            arrow = null;
        }
    }
}
