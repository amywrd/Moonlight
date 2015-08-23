using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {

    public float moveSpeed;
    public GameObject gib;

	void Start () {	}
	

	void Update () {
        transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
	}

    void OnCollisionEnter2D(Collision2D collided)
    {
        if (collided.gameObject.tag == "Player")
        {
            //Destroy(collided.gameObject);
            FindObjectOfType<PlayerController>().DamagePlayer();
            Destroy(gameObject);
        }

        if(collided.gameObject.tag == "Mob")
        {
            Destroy(collided.gameObject);
            Instantiate(gib, collided.gameObject.transform.position, collided.gameObject.transform.rotation);
        }
    }
}
