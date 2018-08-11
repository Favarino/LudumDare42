using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] float speed;

    [SerializeField] float interactOffset;
    [SerializeField] float interactRadius;
    public LayerMask interactableLayer;


    enum Directions { LEFT,RIGHT,UP,DOWN }
    Directions currentDirection;

    Vector2 interactCirlceDirection;

    bool interacting;

    List<Cat> carriedCats = new List<Cat>();
	
	// Update is called once per frame
	void Update () {
        #region BasicMovement
        float deltaSpeed = speed * Time.deltaTime;

        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 1 * deltaSpeed, 0);
            currentDirection = Directions.UP;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -1 * deltaSpeed, 0);
            currentDirection = Directions.DOWN;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-1 * deltaSpeed, 0, 0);
            currentDirection = Directions.LEFT;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(1 * deltaSpeed, 0, 0);
            currentDirection = Directions.RIGHT;
        }
        #endregion
        #region InteractInput
        if(Input.GetKey(KeyCode.E))
        {
            interacting = true;
            switch (currentDirection)
            {
                case Directions.LEFT:
                    
                    interactCirlceDirection = new Vector2(transform.position.x - interactOffset, transform.position.y);
                    break;
                case Directions.RIGHT:
                   
                    interactCirlceDirection = new Vector2(transform.position.x + interactOffset, transform.position.y);
                    break;
                case Directions.UP:
                   
                    interactCirlceDirection = new Vector2(transform.position.x, transform.position.y + interactOffset);
                    break;
                case Directions.DOWN:
                   
                    interactCirlceDirection = new Vector2(transform.position.x, transform.position.y - interactOffset);
                    break;
                default:
                    break;
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(interactCirlceDirection, interactRadius, interactableLayer);
            if (colliders != null)
            {
                DelegateHowToInteract(colliders);
            }
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            interacting = false;
        }
        #endregion
    }

    void DelegateHowToInteract(Collider2D[] colliders)
    {
        //For now use the first collider we get
        if (colliders[0].gameObject.GetComponent<Chore>() && Physics2D.OverlapCircle(interactCirlceDirection, interactRadius, interactableLayer))
        {
            Chore chore = colliders[0].gameObject.GetComponent<Chore>();
            chore.OnInteraction();
        }
        else
        {
            return;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(interactCirlceDirection!=null)
        Gizmos.DrawWireSphere(interactCirlceDirection, interactRadius);
    }

}
