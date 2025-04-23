using UnityEngine;

public class MapWrapper : MonoBehaviour
{
    private Vector2 bounds;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        Vector2 camPos = cam.transform.position;
        bounds = new Vector2(width / 2f, height / 2f) + new Vector2(camPos.x, camPos.y);
    }


    private void Update()
    {
        Vector3 pos = transform.position;

        if (pos.x > bounds.x)
        {
            pos.x = -bounds.x;
        }
        else if (pos.x < -bounds.x)
        {
            pos.x = bounds.x;
        }


        if (pos.y > bounds.y)
        {
            pos.y = -bounds.y;
        }

        else if (pos.y < -bounds.y)
        {
            pos.y = bounds.y;
        }

        transform.position = pos;
    }
}