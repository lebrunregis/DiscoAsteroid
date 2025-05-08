using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(LaserShooter))]
    public class PlayerController : MonoBehaviour
    {
        PlayerInput playerInput;
        Rigidbody2D rb;
        public SpriteRenderer bodySprite;
        [SerializeField] UiTextDisplay uiDisplayText;
        private LaserShooter laserShooter;
        public GameObject flame;

        private float rotationInput;
        private float forwardInput;

        public float rotationSpeed = -180;
        public float maxSpeed = 3;
        public float friction = 0.5f;

        public float invincibilityTime = 2;
        private float invincibilityDelta = 0;
        public Color damagedColor = Color.red;

        public float deflectTime = 1f;
        private float deflectDelta = 0;
        public float deflectCooldown = 0;
        public Color deflectColor = Color.cyan;

        public int life = 10;
        public int score = 0;
        public int pushForce = 5;
        public int physicDamage = 1; // Les degat fait aux asteroid apres collision avec le vaisseau
        public bool isInvincible;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody2D>();
            bodySprite = GetComponent<SpriteRenderer>();
            laserShooter = GetComponent<LaserShooter>();
            if (uiDisplayText != null)
            {
                uiDisplayText.UpdateHealthText(life);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (rotationInput != 0)
            {
                rb.rotation += rotationInput * rotationSpeed * Time.deltaTime;
            }
            if (forwardInput != 0)
            {
                Vector2 force = transform.up * pushForce;
                rb.linearVelocity += force * (Time.deltaTime * forwardInput);
                if (rb.linearVelocity.magnitude > maxSpeed)
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
                }
            }
            else
            {
                rb.linearVelocity -= friction * Time.deltaTime * rb.linearVelocity;
            }

            if (isInvincible || invincibilityDelta > 0)
            {
                ProcessInvincibility();
            }

            if (deflectDelta > 0)
            {
                ProcessDeflect();
            }
        }

        private void ProcessDeflect()
        {
            deflectDelta -= Time.deltaTime;
            if (deflectDelta < 0)
            {
                EndDeflect();
            }
        }

        private void ProcessInvincibility()
        {
            invincibilityDelta -= Time.deltaTime;

            float blink = Mathf.Floor(Time.time / .1f) % 2;
            bodySprite.enabled = blink == 0;

            if (invincibilityDelta <= 0)
            {
                EndInvincibility();
            }
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.gameObject.name);
            AsteroidController asteroidController;
            LaserController laserController;
            EnemyController enemyController;

            if (other.gameObject.TryGetComponent<AsteroidController>(out asteroidController))
            {
                if (!isInvincible && invincibilityDelta > 0)
                {
                    TakeDamage(asteroidController.Damage);
                }
            }
            else if (other.gameObject.TryGetComponent<LaserController>(out laserController))
            {
                laserController.homingTarget = laserController.shooter.transform;
                    if (deflectDelta > 0 && laserController.canBeReflected)
                    {
                        Debug.Log("Deflected laser!");
                        laserShooter.ShootAt(laserController.shooter.transform);
                    }
                    else
                    {
                        Debug.Log("Took damage!");
                        TakeDamage(laserController.laserDamage);
                    }
                
            }
            else if (other.gameObject.TryGetComponent<EnemyController>(component: out enemyController))
            {
                Debug.Log("hit an enemy!");
                TakeDamage(1);
            }
        }

        private void TakeDamage(int damage)
        {
            StartInvincibility();
            life -= damage;
            if (uiDisplayText != null)
            {
                uiDisplayText.UpdateHealthText(life);
            }
            CheckIfDead();
        }

        public void StartInvincibility()
        {
            invincibilityDelta = invincibilityTime;
            bodySprite.color = Color.red;
        }

        public void EndInvincibility()
        {
            bodySprite.enabled = true;
            bodySprite.color = Color.white;
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
            if (flame != null)
            {
                bool goingForward = dir.y > 0;
                if (flame.activeInHierarchy != goingForward)
                {
                    flame.SetActive(goingForward);
                }
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed && laserShooter != null)
            {
                laserShooter.Shoot();
            }
        }

        public void OnDeflect(InputAction.CallbackContext context)
        {

            if (context.performed)
            {
                if (deflectCooldown <= 0 && deflectDelta <= 0)
                {
                    Debug.Log("Deflector engaged");
                    StartDeflect();
                }
            }
        }

        public void StartDeflect()
        {
            bodySprite.color = deflectColor;
            deflectDelta = deflectTime;
           
        }

        public void EndDeflect()
        {
            bodySprite.color = Color.white;
        }
    }
}