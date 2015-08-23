using UnityEngine;
using System.Collections;

public class MolotovMobController : MonoBehaviour
{

    public GameObject bomb;
    public GameObject exclamation;
    public float cooldownMin;
    public float cooldownMax;
    public float chargeTimer;

    private float cooldown;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        cooldown = Random.Range(cooldownMin, cooldownMax);
        StartCoroutine(ThrowBomb());
    }

    IEnumerator ThrowBomb()
    {
        while(true)
        {
            // Wait for cooldown
            yield return new WaitForSeconds(cooldown);

            // telegraph the attack!
            animator.SetBool("IsCharging", true);
            yield return new WaitForSeconds(chargeTimer);
            // show exclamation mark
            FindObjectOfType<SoundManager>().PlayExclamation();
            Instantiate(exclamation, transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
            FindObjectOfType<SoundManager>().PlayMoltovThrow();
            Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            Instantiate(bomb, spawnLocation, transform.rotation);
            animator.SetBool("IsCharging", false);
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
