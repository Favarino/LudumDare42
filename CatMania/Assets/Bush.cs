using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : Interactables, IInteractable{

    public GameObject containedCat;
    public bool full;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            AddCat();
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            RemoveCat();
        }
    }

    void AddCat()
    {
        full = true;
        anim.SetTrigger("Full");
    }
    void RemoveCat()
    {
        full = false;
        anim.SetTrigger("Shake");
    }

    public void OnInteraction()
    {

    }
}
