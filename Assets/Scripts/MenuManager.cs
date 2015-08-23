using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	void Start () {
	
	}

    bool gameStarting = false;
	void Update () {
	    if(Input.anyKeyDown)
        {
            StartCoroutine(StartGame());
            gameStarting = true;
        }
	}



    IEnumerator StartGame()
    {
        if(!gameStarting)
        {
            var fadeTime = GetComponent<FadeIn>().BeginFade(1);
            yield return new WaitForSeconds(fadeTime);

            Application.LoadLevel("Game");

        }
    }
}
