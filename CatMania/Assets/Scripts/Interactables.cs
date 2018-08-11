using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour {
    public enum Types { CHORE, CAT, BUSH }
    public Types type;
}

interface IInteractable
{
    void OnInteraction();
}
