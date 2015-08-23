using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float slideTime;
    public int health = 3;
    public GameObject gib;
    public GameObject playerHitGib;

    public Text healthText;
    public GameObject restartText;

    bool isFalling = false;
    bool isSliding = false;

    public bool playerDead = false;
    public bool isInvincible = false;

    bool playingDeathScene = false;

    Rigidbody2D rigidbody;
    Animator animator;
    SpriteRenderer renderer;
    BoxCollider2D collider;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    public void DamagePlayer()
    {
        if (isInvincible) return;
        isInvincible = true;
        FindObjectOfType<SoundManager>().PlayPlayerHit();
        health--;
        StartCoroutine(Flash());
        StartCoroutine(InvincibleTimer());
        Instantiate(playerHitGib, transform.position, transform.rotation);
        Debug.Log(health);
    }

    IEnumerator Flash()
    {
        while (isInvincible)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            renderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator InvincibleTimer()
    {
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }


    void Update()
    {
        healthText.text = health.ToString();

        if(transform.position.x < -3.5)
        {
            transform.position += new Vector3(3f, 0, 0) * Time.deltaTime;
        }

        if (Input.GetButton("Jump") && !isFalling)
        {
            //transform.position += new Vector3(0, jumpForce, 0);
            rigidbody.velocity = new Vector3(0, jumpForce, 0);
            isFalling = true;
            animator.SetBool("PlayerSlide", false);
        }

        if(Input.GetButton("Slide") && !isSliding && !isFalling)
        {
            StartCoroutine(Slide());
        }

        if(health == 0)
        {
            Time.timeScale = 0.1f;
            Time.fixedDeltaTime = 0.05f * Time.timeScale;

            FindObjectOfType<SoundManager>().PlayPlayerDead();
            FindObjectOfType<SoundManager>().StopBgm();
            FindObjectOfType<SoundManager>().PlayTheme();

            Instantiate(gib, transform.position, transform.rotation);
            Instantiate(gib, transform.position, transform.rotation);
            Instantiate(gib, transform.position, transform.rotation);
            Instantiate(gib, transform.position, transform.rotation);
            Instantiate(gib, transform.position, transform.rotation);
            Instantiate(gib, transform.position, transform.rotation);
            Instantiate(gib, transform.position, transform.rotation);

            FindObjectOfType<GameManager>().playerDead = true;
            CameraShake.Shake(0.3f, 0.1f);
            playerDead = true;
            restartText.SetActive(true);
            Destroy(gameObject);

        }
    }

    IEnumerator Slide()
    {
        isSliding = true;
        collider.size = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
        collider.offset = new Vector3(0, -transform.localScale.y / 2, 0);
        animator.SetBool("PlayerSlide", true);

        yield return new WaitForSeconds(slideTime);

        isSliding = false;
        animator.SetBool("PlayerSlide", false);

        collider.size = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
        collider.offset = new Vector3(0, 0, 0);

    }

    IEnumerator PlayerDeath()
    {
            yield return new WaitForSeconds(0.5f);
        if (!playingDeathScene)
        {
            playingDeathScene = true;





        }

    }

    void OnCollisionStay2D()
    {
        isFalling = false;
    }

    void OnCollisionEnter2D(Collision2D collided)
    {
        if(collided.gameObject.tag == "Mob")
        {
            CameraShake.Shake(0.1f, 0.2f);
            Instantiate(gib, collided.transform.position, collided.transform.rotation);
            Destroy(collided.gameObject);
            FindObjectOfType<GameManager>().SetScore(100);
            FindObjectOfType<SoundManager>().PlaySquish();
        }

        if (collided.gameObject.tag == "Killzone")
        {
            health = 0;
        }
    }
}
