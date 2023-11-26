using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float jumpStrength = 1f;
    [SerializeField] float rollCoolDownTime;

    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Transform gunPivot;
    [SerializeField] Joystick joystick;

    Vector2 originalSize;
    Vector2 originalOffset;

    BoxCollider2D playerCollider;
    Rigidbody2D rb;

    public static bool isFlipped = false;
    bool canRoll = true;
    bool isGrounded = true;
    bool canMoveLeft = true;
    bool canMoveRight = true;

    const string groundTag = "Ground";
    const string enemyTag = "Enemy";
    const string borderTagLeft = "BorderLeft";
    const string borderTagRight = "BorderRight";

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();

        originalSize = playerCollider.size;
        originalOffset = playerCollider.offset;
    }

    void Update()
    {
        Move();
        Roll();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    void Move()
    {

        if (joystick.Horizontal < 0 && canMoveLeft)
        {
            isFlipped = true;
            FlipSprites(isFlipped);
            transform.Translate(-joystick.Horizontal * Time.deltaTime * playerSpeed, 0, 0);
        }

        if (joystick.Horizontal > 0 && canMoveRight)
        {
            isFlipped = false;
            FlipSprites(isFlipped);
            transform.Translate(joystick.Horizontal * Time.deltaTime * playerSpeed, 0, 0);
        }

    }

    public void Jump()
    {
        if (joystick.Vertical > 0.7f && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode2D.Impulse);
        }
    }


    void Roll()
    {
        if (joystick.Vertical < -0.7f)
        {
            if (canRoll)
            {
                canRoll = false;
                int direction = isFlipped ? -1 : 1;

                playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y / 2);
                playerCollider.offset = new Vector2(playerCollider.offset.x, -1.26f);

                rb.AddForce(Vector3.right * direction * 10, ForceMode2D.Impulse);

                StartCoroutine(RollCoolDown(rollCoolDownTime));
            }
        }

        if (Mathf.Approximately(rb.velocity.magnitude, 0f))
        {
            playerCollider.size = originalSize;
            playerCollider.offset = originalOffset;
        }
    }

    public void FlipSprites(bool flipDir)
    {
        //playerSprite.flipX = flipDir;
        int direction = flipDir ? 180 : 0;
        //float gunPosition = flipDir ? -0.26f : 0.26f;

        //gunPivot.transform.eulerAngles = new Vector2(0, gunRotationDegree);
        //gunPivot.transform.localPosition = new Vector2(gunPosition, gunPivot.transform.localPosition.y);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, direction, transform.eulerAngles.z);

    }

    IEnumerator RollCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        canRoll = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(groundTag) || collision.transform.CompareTag(enemyTag))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            isGrounded = true;
        }

        if (collision.transform.CompareTag(borderTagLeft))
            canMoveLeft = false;

        if (collision.transform.CompareTag(borderTagRight))
            canMoveRight = false;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(groundTag) || collision.transform.CompareTag(enemyTag))
        {
            isGrounded = false;
        }

        if (collision.transform.CompareTag(borderTagLeft))
            canMoveLeft = true;

        if (collision.transform.CompareTag(borderTagRight))
            canMoveRight = true;
    }
}
