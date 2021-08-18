using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController m_instance;

    public static GameController Instance { get { return m_instance; } }

    public Transform m_boundary;
    public float m_boundaryScreenPercent = 0.3f;

    private int m_screenHeight;

    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
    }

    void Start()
    {
        m_screenHeight = Screen.height;
        Vector2 boundaryPos = Camera.main.ScreenToWorldPoint(new Vector2(0, m_screenHeight * m_boundaryScreenPercent));
        boundaryPos.x = 0;
        m_boundary.position = (Vector2)boundaryPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetLevel();
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(0); // HOW DOES THIS WORK?!
    }

    public int GetScreenHeight()
    {
        return m_screenHeight;
    }

    public float GetBoundaryLimit()
    {
        return m_boundary.position.y;
    }
}
