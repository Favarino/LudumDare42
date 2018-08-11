using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour {
    enum Types { CHORE, CAT }
    [SerializeField] Types type;
}

interface IInteractable
{
    void OnInteraction();
}
