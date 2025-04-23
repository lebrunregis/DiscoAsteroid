using UnityEngine;

namespace ScreenWrap
{
    public class ScreenWrapController : MonoBehaviour
    {
        public Camera m_camera;
        public ScreenBehaviour mode;
        public Rect screenRect;
        public enum ScreenBehaviour
        {
            None,
            Box,
            Bounce,
            HWrap,
            VWrap,
            Wrap,
            Cleanup
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (m_camera == null)
            {
                m_camera = Camera.main;
            }
        }

        // Update is called once per frame
        void Update()
        {
            screenRect.height = 2f * m_camera.orthographicSize;
            screenRect.width = screenRect.height * m_camera.aspect;
            screenRect.x = m_camera.transform.position.x - screenRect.width/2;
            screenRect.y = m_camera.transform.position.y - screenRect.height/2;
            //m_camera.ViewportToWorldPoint(new Vector3(0, 0, -m_camera.transform.position.z));
            //m_camera.ViewportToWorldPoint(new Vector3(1, 1, -m_camera.transform.position.z));
            //screenRect = m_camera.rect * m_camera.orthographicSize;
            switch (mode)
            {
                case ScreenBehaviour.None:
                    break;
                case ScreenBehaviour.Box:
                    this.transform.position = Box(transform.position, screenRect);
                    break;
                case ScreenBehaviour.HWrap:
                    this.transform.position = HWrap(transform.position, screenRect);
                    this.transform.position = Box(transform.position, screenRect);
                    break;
                case ScreenBehaviour.VWrap:
                    this.transform.position = VWrap(transform.position, screenRect);
                    this.transform.position = Box(transform.position, screenRect);
                    break;
                case ScreenBehaviour.Wrap:
                    this.transform.position = Wrap(transform.position, screenRect);
                    this.transform.position = Box(transform.position, screenRect);
                    break;
                case ScreenBehaviour.Cleanup:
                    Cleanup(transform.position, screenRect);
                    break;
                case ScreenBehaviour.Bounce:
                    break;
            }
        }

        private static Vector2 Box(Vector2 pos, Rect screenRect)
        {
            return new Vector2(Mathf.Clamp(pos.x,screenRect.x,screenRect.x + screenRect.width), 
                Mathf.Clamp(pos.y,screenRect.y,screenRect.y + screenRect.height));
        }

        private static bool IsOutsideView(Vector2 pos, Rect screenRect)
        {
            return IsOutsideBoundary(pos.x,screenRect.x,screenRect.x + screenRect.width) ||
                   IsOutsideBoundary(pos.y,screenRect.y,screenRect.y + screenRect.height);
        }

        private static float Wrap(float pos, float min, float max)
        {
            if (pos < min)
            {
                pos = max;
            } else if (pos > max)
            {
                pos = min;
            }

            return pos;
        }

        private static bool IsOutsideBoundary(float x, float minX, float maxX)
        {
            return x < minX || x > maxX;
        }
    
        private static Vector2 Wrap(Vector2 pos, Rect screenRect)
        {
            pos= HWrap(pos, screenRect);
            pos = VWrap(pos, screenRect);
            return pos;
        }

        private static Vector2 VWrap(Vector2 pos, Rect screenRect)
        {
            pos.x = Wrap(pos.x, screenRect.x, screenRect.x + screenRect.width);
            return pos;
        }

        private static Vector2 HWrap(Vector2 pos, Rect screenRect)
        {
            pos.y = Wrap(pos.y, screenRect.y, screenRect.y + screenRect.height);
            return pos;
        }

        private void Cleanup(Vector2 pos, Rect screenRect)
        {
            if (IsOutsideView(transform.position, screenRect))
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}