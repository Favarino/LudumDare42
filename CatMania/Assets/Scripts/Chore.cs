using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Chore : MonoBehaviour, IInteractable {
    [SerializeField] Image progressBar;
    [SerializeField] string completionText;

    public float timeToFinish;
    float currentTime;

	public void OnInteraction()
    {
        currentTime += Time.deltaTime;

        progressBar.fillAmount = currentTime / timeToFinish;

        if(currentTime>=timeToFinish)
        {
            Finished();
        }
    }

    void Finished()
    {
        UiController.instance.SetCompletionText(completionText);
    }
}
