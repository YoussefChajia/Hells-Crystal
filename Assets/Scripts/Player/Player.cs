using UnityEngine;

[RequireComponent(typeof(PlayerController))]

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Player")]
    private PlayerController controller;
    private Vector2 velocity;
    private bool isDead;

    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("Vertical Movement")]
    [SerializeField] private float moveSpeed;
    private Vector2 screenBounds;

    [Header("Dash Move")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDistance;
    [SerializeField] private bool isDashing;
    private bool dashDirection;
    private Vector2 startPosition;

    [Header("Bounce Mechanic")]
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float startJumpTime;
    private float jumpTimer;
    private bool isJumping;
    private int direction;

    [Header("Stop Mechanic")]
    [SerializeField] private float holdTime;
    private float hold;
    private bool isStop = true;

    [Header("Slide Mechanic")]
    private bool isSliding;

    [Header("Player Respawn")]
    [SerializeField] private PlayerRespawn playerRespawn;

    [Header("Level Manager")]
    [SerializeField] private LevelManager levelManager;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("Dragon")]
    private PlatformController tutSpike;

    [Header("Trail Effect")]
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Tutorial")]
    [SerializeField] private Tutorial tutorial;

    [Header("Revive UI")]
    [SerializeField] private GameObject reviveUI;


    public void setIsDead(bool isDead)
    {
        this.isDead = isDead;
    }

    public void setIsStop(bool isStop)
    {
        this.isStop = isStop;
    }

    public bool getIsDead()
    {
        return this.isDead;
    }

    public LevelManager getLevelManager()
    {
        return this.levelManager;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        controller = GetComponent<PlayerController>();
        tutSpike = GameObject.Find("Dragon").GetComponent<PlatformController>();
        reviveUI.SetActive(false);

        //Disabling all the levels, to activate them one at a time with the ActivateLevel method below
        foreach (Level level in levelManager.getLevels())
        {
            level.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Tutorial", 0) == 1)
        {
            tutorial.getUI()[2].gameObject.SetActive(false);
            levelManager.getLevels()[0].gameObject.SetActive(true);
            levelManager.setActiveLevel(levelManager.getLevels()[0]);
            levelManager.getActiveLevel().InitializeLevel();
            //PlayerPrefs.SetInt("Tutorial", 0);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            tutorial.Begin();
        }

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire1") && !isDead)
        {
            startPosition = transform.position;
            //Enabling dash trail
            trail.enabled = true;
            sprite.enabled = false;
            isDashing = true;
            //animator.SetBool("isDashing", true);

            //The player is unable to dash back between chains
            if (transform.position.x < -1.2f)
            {
                dashDirection = true;
            }
            else if (transform.position.x > 1.2f)
            {
                dashDirection = false;
            }
        }
        if (Input.GetButton("Fire1") && !isDead)
        {
            hold += Time.deltaTime;
            if (hold > holdTime && !isSliding)
            {
                isStop = true;
            }
        }
        if (Input.GetButtonUp("Fire1") && !isDead)
        {
            isStop = false;
            hold = 0f;
        }
#else
        if (Input.touchCount > 0  && !isDead)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startPosition = transform.position;
                trail.enabled = true;
                sprite.enabled = false;
                isDashing = true;
                //animator.SetBool("isDashing", true);

                if (transform.position.x < -1.2f)
                {
                    dashDirection = true;
                }
                else if (transform.position.x > 1.2f)
                {
                    dashDirection = false;
                }
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                hold += Time.deltaTime;
                if (hold > holdTime && !isSliding)
                {
                    isStop = true;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isStop = false;
                hold = 0f;
            }
        }
#endif

        if (velocity.y == 0)
        {
            animator.SetBool("isClimbing", false);
        }
        else
        {
            animator.SetBool("isClimbing", true);
        }

        if (isSliding)
        {
            animator.SetBool("isSliding", true);
        }
        else
        {
            animator.SetBool("isSliding", false);
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            //velocity.y = Input.GetAxisRaw("Vertical") * moveSpeed;
            Climb();
        }
        else
        {
            velocity = Vector2.zero;
        }

        if (isDashing)
        {
            Dash();
        }

        if (isJumping)
        {
            Jump(direction);
        }

        if (controller.collisions.right || controller.collisions.left)
        {
            //Disabling dash trail
            trail.enabled = false;
            sprite.enabled = true;
        }

        controller.Move(velocity * Time.fixedDeltaTime);
    }

    void Dash()
    {
        if (Mathf.Abs((startPosition.x - transform.position.x)) >= dashDistance || controller.collisions.right || controller.collisions.left)
        {
            isDashing = false;
            velocity = Vector2.zero;
            //Disabling dash trail
            trail.enabled = false;
            sprite.enabled = true;
            //animator.SetBool("isDashing", false);
        }
        else
        {
            //First line Dashes the player without stoping the vertical movement. The second stops the vertical movement
            velocity = new Vector2((dashDirection) ? dashSpeed : -dashSpeed, velocity.y);
            //velocity = new Vector2((dashDirection) ? dashSpeed : -dashSpeed, 0);

        }
    }

    void Climb()
    {
        if (!isStop)
        {
            if (!isSliding)
            {
                velocity.y = moveSpeed;
            }
            else
            {
                velocity.y = moveSpeed * 2;
            }
        }
        else
        {
            velocity.y = 0;
        }
    }

    void Jump(int direction)
    {
        if (jumpTimer <= 0)
        {
            isJumping = false;
            velocity = Vector2.zero;
        }
        else
        {
            jumpTimer -= Time.fixedDeltaTime;
            switch (direction)
            {
                case 0:
                    velocity = new Vector2(0, jumpSpeed);
                    break;
                case 1:
                    velocity = new Vector2(jumpSpeed, 0);
                    break;
                case -1:
                    velocity = new Vector2(-jumpSpeed, 0);
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Spike":
                isDead = true;
                animator.SetTrigger("isDead");
                playerRespawn.Death();
                break;
            case "BounceUp":
                Bounce(0);
                break;
            case "BounceRight":
                Bounce(1);
                break;
            case "BounceLeft":
                Bounce(-1);
                break;
            case "Hold":
                //This method reset the tutorial spike at the beginning
                ResetSpike(tutSpike);
                break;
            case "Checkpoint":
                levelManager.ActivateLevel(other.GetComponent<CheckPoint>());
                break;
            case "Slide":
                isSliding = true;
                break;
            case "Coin":
                other.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Slide")
        {
            isSliding = false;
        }
    }

    void Bounce(int bounce)
    {
        trail.enabled = true;
        sprite.enabled = false;
        jumpTimer = startJumpTime;
        isJumping = true;
        direction = bounce;
    }

    void ResetSpike(PlatformController spike)
    {
        spike.setFromWayPointIndex(0);
        spike.setPercentWayPoints(0);
    }
}
