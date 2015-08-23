using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour
{
    public float throwForce;
    public GameObject fire;

    bool hasSpawned = false;
    Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1000 * Time.deltaTime));

        if (!hasSpawned)
        {
            rigidbody.velocity = new Vector3(-(throwForce / 4), throwForce, 0);
            hasSpawned = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collided)
    {
        FindObjectOfType<SoundManager>().PlayGunshot();
        Instantiate(fire, transform.position, fire.transform.rotation);
        CameraShake.Shake(0.5f, 0.4f);
        Destroy(gameObject);
    }
}
