using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chore : Interactables, IInteractable {
    [SerializeField] Image progressBar;

    public float timeToFinish;
    float currentTime;

	public void OnInteraction()
    {
        currentTime += Time.deltaTime;

        progressBar.fillAmount = currentTime / timeToFinish;
    }
}
