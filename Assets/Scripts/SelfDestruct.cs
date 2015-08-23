using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    public float timer;

	void Start () { StartCoroutine(Destruct());	}

    IEnumerator Destruct()
    {
        yield return new WaitForSeconds(timer);

        Destroy(gameObject);
    }

}
