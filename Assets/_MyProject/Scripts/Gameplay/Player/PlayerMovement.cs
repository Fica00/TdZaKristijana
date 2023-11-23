using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float jumpStrength = 1f;
    [SerializeField] float rollCoolDownTime;

    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Transform gunPivot;

    Vector2 originalSize;
    Vector2 originalOffset;

    BoxCollider2D playerCollider;
    Rigidbody2D rb;

    public static bool isFlipped = false;
    bool canRoll = true;
    bool isGrounded = true;

    const string groundTag = "Ground";
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
        Jump();
        Roll();
    }

    void Move()
    {
        if (SimpleInput.GetAxis("Horizontal") < 0)
        {
            isFlipped = true;

            FlipSprites(isFlipped);
            transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
        }

        if (SimpleInput.GetAxis("Horizontal") > 0)
        {
            isFlipped = false;

            FlipSprites(isFlipped);
            transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
        }

    }

    public void Jump()
    {
        if (JumpButton.isJumpPressed && isGrounded)
        {
            JumpButton.isJumpPressed = false;
            rb.AddForce(Vector3.up * jumpStrength, ForceMode2D.Impulse);
        }
    }


    void Roll()
    {
        if (RollButton.isRollPressed)
        {
            RollButton.isRollPressed = false;
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

    void FlipSprites(bool flipDir)
    {
        playerSprite.flipX = flipDir;
        int gunRotationDegree = flipDir ? 180 : 0;
        float gunPosition = flipDir ? -0.26f : 0.26f;

        gunPivot.transform.eulerAngles = new Vector2(0, gunRotationDegree);
        gunPivot.transform.localPosition = new Vector2(gunPosition, gunPivot.transform.localPosition.y);
    }

    IEnumerator RollCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        canRoll = true;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(groundTag))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(groundTag))
        {
            isGrounded = false;
        }
    }
}
