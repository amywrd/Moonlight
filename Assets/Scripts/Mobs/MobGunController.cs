using UnityEngine;
using System.Collections;

public class MobGunController : MonoBehaviour {

    public Transform shotStart, shotEnd;
    public GameObject bulletTrail;
    public GameObject exclamation;


    bool collision = false;


    public float cooldownMin;
    public float cooldownMax;
    public float chargeTimer;

    private float cooldown;
    Animator animator;

    private bool collisionCheck = false;

    void Start () {
        animator = GetComponent<Animator>();
        cooldown = Random.Range(cooldownMin, cooldownMax);
        StartCoroutine(Shoot());
    }
	
	void Update () {

        collision = Physics2D.Linecast(shotStart.position, shotEnd.position, 1 << LayerMask.NameToLayer("Player"));
        //Debug.DrawLine(shotStart.position, shotEnd.position, Color.green);


        if(collisionCheck)
        {
            if (collision)
            {
                FindObjectOfType<PlayerController>().DamagePlayer();
            }
            collisionCheck = false;
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            // Wait for cooldown
            yield return new WaitForSeconds(cooldown);

            // telegraph the attack!
            animator.SetBool("IsCharging", true);
            yield return new WaitForSeconds(chargeTimer);
            // show exclamation mark
            Instantiate(exclamation, transform.position, transform.rotation);
            FindObjectOfType<SoundManager>().PlayExclamation();
            yield return new WaitForSeconds(1f);

            GetComponent<SpriteRenderer>().enabled = true;

            //Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            Instantiate(bulletTrail, transform.position, Quaternion.Euler(0,0,-8f));
            CameraShake.Shake(0.5f, 0.3f);
            animator.SetBool("IsCharging", false);
            FindObjectOfType<SoundManager>().PlayGunshot();
            Debug.DrawLine(shotStart.position, shotEnd.position, Color.red);
            collisionCheck = true;
        }
    }

    IEnumerator Flashy()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
    }
}
