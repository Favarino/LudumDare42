using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour {

    [SerializeField] TextMeshProUGUI completionText;

    Vector2 startScale = new Vector2(.3f, .3f);
    Vector2 endScale = Vector2.one;

    public static UiController instance;

    bool animate = false;

	void Start () {
       if(instance==null)
        {
            instance = this;
        }
       else if(instance!=this)
        {
            Destroy(gameObject);
        }
	}

    private void Update()
    {
        if(animate)
        {
            StartCoroutine(ScaleTo(completionText.gameObject,endScale,startScale,1.5f));
        }
    }

    public void SetCompletionText(string newText)
    {
        completionText.text = newText;
        animate = true;
    }

    IEnumerator ScaleTo(GameObject obj, Vector2 endScale, Vector2 originalScale, float rate)
    {
        obj.gameObject.SetActive(true);
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime*rate;
            obj.gameObject.transform.localScale = Vector2.Lerp(originalScale, endScale, i);
            if(i>= .9f)
            {
                obj.SetActive(false);
            }
            yield return null;
        }
    }
}
