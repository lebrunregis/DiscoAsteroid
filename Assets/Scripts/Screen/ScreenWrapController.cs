using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenWrapController : MonoBehaviour
{
    public Camera m_camera;

    public GameObject _gameObjectContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       Debug.Log(m_camera.rect.x + " " + m_camera.rect.y);
       ScreenWrapAble[] screenWrapAbles= _gameObjectContainer.transform.GetComponents<ScreenWrapAble>();
    }
}