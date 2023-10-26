using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfTutorialCollider : MonoBehaviour
{


    public Image fadeImage;
    bool hasStarted;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("DF");

            if (hasStarted) { return; }

            hasStarted = true;
            
            StartCoroutine(nameof(FadeImage));
        }
    }


    IEnumerator FadeImage()
    {
        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            fadeImage.color = new Color(0, 0, 0, i);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
        Debug.Log("Finished");
    }

}
