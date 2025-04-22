using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        public float maxSpeed = 3;
        public float gracePeriod = 0;
        public float invincibilityTime = 1;
        public int life = 1;
        public int score = 0;
        public int pushForce = 5;
        Vector3 mousePosition;
        PlayerInput playerInput;
        Rigidbody2D rb;
        CircleCollider2D circleCollider;
        public GameObject flame;
        public float rotationInput;
        public float forwardInput;

        public float rotationSpeed = -180;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.rotation += rotationInput * rotationSpeed * Time.deltaTime;
            Vector2 force = transform.up * pushForce;
            rb.linearVelocity += force * (Time.deltaTime * forwardInput); 
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }

        public void OnCollisionEnter(Collision other)
        {

        }

        public void OnLook(InputAction.CallbackContext context)
        {
         //   Vector2 pos = context.ReadValue<Vector2>();
          //  transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 0)),transform.up);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log("move");
                Vector2 dir = context.ReadValue<Vector2>(); 
                forwardInput = dir.y;
                rotationInput = dir.x;

                flame.SetActive(dir.y > 0);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("shoot");    
            }
        }
    }
}
