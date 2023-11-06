using DG.Tweening; // nepotreban using
using System.Collections;  
using System.Collections.Generic; // nepotreban using
using Unity.VisualScripting; // nepotreban using
using UnityEditor; // nepotreban using
using UnityEngine;

/*
 * generalno: gledaj da ne dodajes using statments ako ih ne koristis, gledaj da to bude cisto
 * Ako nema potrebe da variabla bude public, tj ako je koristis samo u ovoj skripti a hoces da bude vidljiva u editoru iskoristi  [SerializeField]
 * Ja vise volim explicitno da oznacim da je field private i da svaku variablu zasebno definisem pr: bool isFlipped pa u novom redu bool canRoll..
 * ja vise volim da sve stavim unutar {} (konkretno mislim kad if ima jednu liniju, petlja u petlji...)
 * Gledaj da nadjes naming semu koja ti se svidja
 */
public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 1f; /*umesto public mozes da stavis [SerializeField], idalje ce biti vidljivo u editoru ali nece biti dostupno ostalim skriptama*/
    public float jumpStrength = 1f;  /*umesto public mozes da stavis [SerializeField], idalje ce biti vidljivo u editoru ali nece biti dostupno ostalim skriptama*/
    [SerializeField] float rollCoolDownTime;

    SpriteRenderer playerSprite;
    Transform gunPivot;
    Rigidbody2D rb;

    bool isFlipped = false, canRoll = true, isGrounded = true;

    void Start()
    {
        /*uzimanje komponenti se po nepisanom pravilu uvek radi u Awake
         Nikad nije dobrbo da se vezes za stringove, tako da bi ovde bilo bolje da definises
        promenjive kojima ces moci da prevuces ove komponente koje ti trebaju
        [SerializeField] private SpriteRenderer graphics; i prevuces iz editora umesto 
        GetChildGameObject(gameObject, "Graphics_Player").GetComponent<SpriteRenderer>();
         */
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
                StartCoroutine("RollCoolDown", rollCoolDownTime); /* vezivanje za stringove generalno nije dobra praksa
                mozes lako da uzmes ime funkcije uz pomoc nameof(funkcija)
                */
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
        if (collision.transform.CompareTag("Ground")) /* vezanje za stringove generalno nije dobra praksa, posebno za "magicne stringove"
            uvedi konstantu koja ce da ima vrednost Ground
            */
            isGrounded = true;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
            isGrounded = false;
    }
}
