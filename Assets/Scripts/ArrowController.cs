using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float m_speed = 20;
    private float m_power = 0;

    Vector3 previousPos = Vector3.zero;
    Vector3 startPos = Vector3.zero;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (m_power > 0)
        {
            Vector3 pos = transform.position;

            if (startPos == Vector3.zero)
            {
                startPos = pos;
            }



            pos.y += m_speed * m_power * Time.deltaTime;
            transform.position = pos;



            
        }

    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        //float angle = Vector2.Angle(previousPos.normalized, transform.position.normalized);
        //if (angle > 0)
        //{
        //    print("angle: " + angle);
        //    float currentAngle = transform.rotation.eulerAngles.normalized.z;
        //    float newAngle = currentAngle + angle;
        //    //transform.rotation = Quaternion.Euler(new Vector3(0,0, newAngle));
        //    transform.Rotate(new Vector3(0, 0, currentAngle));
        //}

        if (rb.velocity.y > 0)
        {
            //float angle = Quaternion.LookRotation(rb.velocity).eulerAngles.x +90f;
            //print(angle);
            //transform.rotation = Quaternion.Slerp(Quaternion.Euler(previousPos), Quaternion.Euler(transform.position), Time.deltaTime*50);

            Vector3 newVec = transform.position - startPos;
            //float angle = Vector3.Angle(Vector3.zero, newVec);
            //print(newVec.x + ", " + newVec.y + ", " + newVec.z);
            //print(angle);
            //transform.rotation = Quaternion.LookRotation(newVec);

            //https://answers.unity.com/questions/630670/rotate-2d-sprite-towards-moving-direction.html
            float angle = (Mathf.Atan2(newVec.y, newVec.x) * Mathf.Rad2Deg) - 45;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



            Debug.DrawLine(startPos, transform.position, Color.blue, Mathf.Infinity);
            //Debug.DrawLine(Vector3.zero, previousPos, Color.cyan, Mathf.Infinity);
            //Debug.DrawLine(startPos, newVec, Color.green, Mathf.Infinity);

        }

        previousPos = transform.position;
    }

    IEnumerator HitWall()
    {
        m_power = 0;
        transform.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(2);
        GameController.Instance.ResetLevel();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            StartCoroutine(HitWall());
        }
    }

    public void SetPower(float power)
    {
        m_power = power;
    }
}
