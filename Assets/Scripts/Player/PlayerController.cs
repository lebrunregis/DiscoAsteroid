using Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        public float maxSpeed = 3;
        public float gracePeriod = 0;
        public float invincibilityTime = 2;
        public int life = 10;
        public int score = 0;
        public int pushForce = 5;
        public int physicDamage = 1; // Les degat fait aux asteroid apres collision avec le vaisseau
        public bool isInvincible;
        Vector3 mousePosition;
        PlayerInput playerInput;
        Rigidbody2D rb;
        CircleCollider2D circleCollider;
        SpriteRenderer spriteRenderer;
        [SerializeField] UiTextDisplay uiDisplayText;
        public GameObjectPool gameObjectPool;
        public GameObject flame;
        private float rotationInput;
        private float forwardInput;

        public float rotationSpeed = -180;

        //cpt
        private float currentInvincibilityTimer = 0;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            uiDisplayText.UpdateHealthText(life);
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

            //cptInvinsible
            StartInvincibleFrame();
        }

        private void StartInvincibleFrame()
        {
            if (!isInvincible) return;

            currentInvincibilityTimer += Time.deltaTime;

            float blink = Mathf.Floor(currentInvincibilityTimer / .1f) % 2;
            spriteRenderer.enabled = blink == 0;

            if (currentInvincibilityTimer >= invincibilityTime)
            {
                isInvincible = false;
                currentInvincibilityTimer = 0f;
                spriteRenderer.enabled = true;
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.gameObject.name);
            AsteroidBase asteroidScript = other.gameObject.GetComponent<AsteroidBase>();

            if (asteroidScript == null) return;

            asteroidScript.TakeDamage(physicDamage);
            TakeDamage(asteroidScript.Damage);
        }

        private void TakeDamage(int damage)
        {
            if (isInvincible) return;
            isInvincible = true;
            life -= damage;
            uiDisplayText.UpdateHealthText(life);
            CheckIfDead();
        }

        private void CheckIfDead()
        {
            if (life <= 0)
            {
                SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
                playerInput.enabled = false;
                gameObject.SetActive(false);
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            //   Vector2 pos = context.ReadValue<Vector2>();
            //  transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 0)),transform.up);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 dir = context.ReadValue<Vector2>();
            forwardInput = dir.y;
            rotationInput = dir.x;

            flame.SetActive(dir.y > 0);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                GameObject laser = gameObjectPool.GetFistAvailableObject();
                laser.SetActive(true);
                laser.transform.position = transform.position;
                laser.transform.rotation = transform.rotation;
            }
        }
    }
}