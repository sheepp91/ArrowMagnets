using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowController : MonoBehaviour
{
    public GameObject m_arrowPrefab;

    [SerializeField]
    private float m_power = 0;
    public Slider m_powerBar;
    private GameObject m_arrow;

    private bool m_shotCancelled = false;

    void Start()
    {
        Vector2 position = new Vector2(0f, 0f);
        m_arrow = Instantiate(m_arrowPrefab, position, Quaternion.identity, transform);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !m_shotCancelled) // Preparing to fire
        {
            if (Input.GetMouseButtonDown(1)) // Cancel shot
            {

                m_shotCancelled = true;
                m_power = 0;
                m_powerBar.value = 0;
                return;
            }

            // Calculate power as user drags down
            m_power = Mathf.Abs(Camera.main.WorldToScreenPoint(transform.position).y - Input.mousePosition.y) / GameController.Instance.GetScreenHeight() * 5;
            m_powerBar.value = m_power;
        }
        else if (Input.GetMouseButtonUp(0) && !m_shotCancelled)
        {
            //BUG! YOU CAN CURRENTLY REFIRE THE ARROW!
            //NEED TO SET MAX

            //FIRE!
            m_arrow.transform.parent = null;
            m_arrow.GetComponent<ArrowController>().SetPower(m_power);
        }
        else
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.y > GameController.Instance.GetBoundaryLimit())
            {
                mousePos.y = GameController.Instance.GetBoundaryLimit();
            }
            transform.position = mousePos;

            if (Input.GetMouseButtonUp(0))
            {
                m_shotCancelled = false;
            }
        }
    }
}
