using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 1f;
    public float jumpStrength = 1f;
    [SerializeField] float rollCoolDownTime;

    SpriteRenderer playerSprite;
    Transform gunPivot;
    Rigidbody2D rb;

    bool isFlipped = false, canRoll = true, isGrounded = true;

    void Start()
    {
        playerSprite = GetChildGameObject(gameObject, "Graphics_Player").GetComponent<SpriteRenderer>();
        gunPivot = GetChildGameObject(gameObject, "GunPivot").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        Roll();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            isFlipped = true;

            FlipSprites(isFlipped);
            transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            isFlipped = false;

            FlipSprites(isFlipped);
            transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
        }

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.AddForce(Vector3.up * jumpStrength, ForceMode2D.Impulse);
    }


    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (canRoll)
            {
                canRoll = false;
                int direction = isFlipped ? -1 : 1;
                rb.AddForce(Vector3.right * direction * 10, ForceMode2D.Impulse);
                StartCoroutine("RollCoolDown", rollCoolDownTime);
            }
        }
    }

    GameObject GetChildGameObject(GameObject parentGameObject, string childName)
    {
        Transform[] objects = parentGameObject.transform.GetComponentsInChildren<Transform>();

        foreach (Transform t in objects)
            if (t.gameObject.name == childName)
                return t.gameObject;

        return null;
    }

    void FlipSprites(bool flipDir)
    {
        playerSprite.flipX = flipDir;
        int gunRotationDegree = flipDir ? 180 : 0;
        float gunPosition = flipDir ? -0.26f : 0.26f;

        gunPivot.transform.eulerAngles = new Vector2(0, gunRotationDegree);
        gunPivot.transform.localPosition = new Vector2(gunPosition, gunPivot.transform.localPosition.y);

        //fixovati gun recoil da ide na gore i kad je flipovan
    }

    IEnumerator RollCoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        canRoll = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
            isGrounded = true;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
            isGrounded = false;
    }
}
