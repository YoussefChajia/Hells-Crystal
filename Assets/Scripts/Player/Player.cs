using UnityEngine;

[RequireComponent(typeof(PlayerController))]

public class Player : MonoBehaviour
{
    [Header("Player")]
    private PlayerController controller;
    [HideInInspector]
    public Vector2 velocity;
    public bool isDead;

    [Header("Camera")]
    public Camera mainCamera;

    [Header("Vertical Movement")]
    public float moveSpeed;
    private Vector2 screenBounds;

    [Header("Dash Move")]
    public float dashSpeed;
    public float dashDistance;
    public bool isDashing;
    private bool dashDirection;
    private Vector2 startPosition;

    [Header("Bounce Mechanic")]
    public float jumpSpeed;
    public float startJumpTime;
    private float jumpTimer;
    private bool isJumping;
    private int direction;

    [Header("Stop Mechanic")]
    public float holdTime;
    private float hold;
    public bool isStop;

    [Header("Slide Mechanic")]
    private bool isSliding;

    [Header("Player Respawn")]
    public PlayerRespawn levelManager;

    [Header("Levels")]
    public GameObject[] levels;
    public GameObject activeLevel;

    [Header("Animator")]
    public Animator animator;

    [Header("Dragon")]
    private PlatformController tutSpike;

    [Header("Trail Effect")]
    public TrailRenderer trail;
    public SpriteRenderer sprite;

    [Header("Tutorial")]
    public Tutorial tutorial;


    void Start()
    {
        controller = GetComponent<PlayerController>();
        tutSpike = GameObject.Find("Dragon").GetComponent<PlatformController>();
        tutorial.UI[3].SetActive(false);

        //Disabling all the levels, to activate them one at a time with the ActivateLevel method below
        foreach (GameObject level in levels)
        {
            level.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Tutorial", 0) == 1)
        {
            tutorial.UI[2].gameObject.SetActive(false);
            levels[0].gameObject.SetActive(true);
            //PlayerPrefs.SetInt("Tutorial", 0);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            tutorial.getOut();
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
                //levelManager.Respawn();
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
                levelManager.respawnPoint.transform.position = new Vector2(-1.5f, other.transform.position.y + 1.5f);
                ActivateLevel(int.Parse(other.name));
                break;
            case "Slide":
                isSliding = true;
                break;
            case "Coin":
                Destroy(other.gameObject);
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

    //This method activates the level after the checkpoint and diactivates the previous level
    void ActivateLevel(int checkpoint)
    {
        levels[checkpoint].gameObject.SetActive(false);
        levels[checkpoint + 1].gameObject.SetActive(true);
        activeLevel = levels[checkpoint + 1];
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
        spike.fromWayPointIndex = 0;
        spike.percentWayPoints = 0;
    }

    /* void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0.5f, 0.0f);
        DrawRect(dashRightButton);
    }

    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    } */


}
