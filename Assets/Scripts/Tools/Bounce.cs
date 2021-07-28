using UnityEngine;

public class Bounce : MonoBehaviour
{
    [Header("Bounce Mechanic")]
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float startJumpTime;
    [SerializeField] private int direction;
    private float jumpTimer;
    private bool isJumping;

    private void Update()
    {
        if (isJumping)
        {
            Jump(direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isBounce(direction);
        }
    }

    void Jump(int direction)
    {
        if (jumpTimer <= 0)
        {
            isJumping = false;
            Player.instance.setVelocity(Vector2.zero);
        }
        else
        {
            jumpTimer -= Time.fixedDeltaTime;
            switch (direction)
            {
                case 0:
                    Player.instance.setVelocity(new Vector2(0, jumpSpeed));
                    break;
                case 1:
                    Player.instance.setVelocity(new Vector2(jumpSpeed, 0));
                    break;
                case -1:
                    Player.instance.setVelocity(new Vector2(-jumpSpeed, 0));
                    break;
                default:
                    break;
            }
        }
    }

    void isBounce(int direction)
    {
        Player.instance.getTrail().enabled = true;
        Player.instance.getSprite().enabled = false;
        jumpTimer = startJumpTime;
        isJumping = true;
        this.direction = direction;
    }
}
