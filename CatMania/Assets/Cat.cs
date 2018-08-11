using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Interactables, IInteractable {
    [SerializeField] float xWanderArea;
    [SerializeField] float yWanderArea;
    [SerializeField] Transform wanderAreaOrigin;
    [SerializeField] float speed;

    Rect wanderArea;

    Vector2 goalArea;

    void Start()
    {
        wanderArea = new Rect(wanderAreaOrigin.position, new Vector2(xWanderArea, yWanderArea));
    }


    private void Update()
    {
        if (goalArea != null)
        {
            float deltaSpeed = speed * Time.deltaTime;
            if (Vector2.Distance(transform.position, goalArea) >= .2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, goalArea, deltaSpeed);
            }
            else
            {
                goalArea = GetRandomPosInWanderArea();
            }
        }
    }

    public void OnInteraction()
    {

    }

    Vector2 GetRandomPosInWanderArea()
    {
        float x = Random.Range(wanderAreaOrigin.position.x, wanderAreaOrigin.position.x + xWanderArea);
        float y = Random.Range(wanderAreaOrigin.position.y, wanderAreaOrigin.position.y - yWanderArea);

        return new Vector2(x, y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //horizontal lines
        Gizmos.DrawLine(wanderAreaOrigin.position, new Vector2(wanderAreaOrigin.position.x + xWanderArea, wanderAreaOrigin.position.y));
        Gizmos.DrawLine(new Vector2(wanderAreaOrigin.position.x + xWanderArea, wanderAreaOrigin.position.y - yWanderArea), new Vector2(wanderAreaOrigin.position.x,wanderAreaOrigin.position.y-yWanderArea));
        //vertical lines
        Gizmos.DrawLine(wanderAreaOrigin.position, new Vector2(wanderAreaOrigin.position.x, wanderAreaOrigin.position.y - yWanderArea));
        Gizmos.DrawLine(new Vector2(wanderAreaOrigin.position.x + xWanderArea, wanderAreaOrigin.position.y - yWanderArea), new Vector2(wanderAreaOrigin.position.x + xWanderArea, wanderAreaOrigin.position.y));
    }
}
