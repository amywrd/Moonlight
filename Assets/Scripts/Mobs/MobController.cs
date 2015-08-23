using UnityEngine;
using System.Collections;

public class MobController : MonoBehaviour {

    public Transform sightStart, sightEnd;
    public float jumpForce = .5f;

    private Rigidbody2D rigidbody;
    private bool collision = false;


    // 0 - random, 1 - left, 2 - right, 3 - none
    public int movementDirection;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        if (movementDirection == 0) movementDirection = Random.Range(1, 3);
    }

    void Update()
    {
        HandleMovement();
        OffScreenCheck();

        collision = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Solid"));
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);

        if (collision)
        {
            rigidbody.velocity = new Vector3(0, Random.Range(jumpForce, jumpForce + 3f), 0);
        }
    }


    void HandleMovement()
    {
        float randomX = Random.Range(0f, 4f);
        Vector3 movementVector = new Vector3(randomX, 0, 0);

        if (movementDirection == 0) return;

        else if (movementDirection == 1)
        {
            transform.position += movementVector * Time.deltaTime;
        }

        else if (movementDirection == 2)
        {
            transform.position += movementVector * Time.deltaTime;
        }
    }

    void OffScreenCheck()
    {
       // if (transform.position.x - transform.lossyScale.x > GetScreenWidth()) Destroy(gameObject);
        if (transform.position.x + transform.lossyScale.x < -GetScreenWidth()) Destroy(gameObject);
    }

    public float GetScreenWidth()
    {
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0);
        Vector3 targetWidth = Camera.main.ScreenToWorldPoint(upperCorner);
        return targetWidth.x;
    }

}
