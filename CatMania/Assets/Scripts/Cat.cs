using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour, IInteractable {
    [SerializeField] float xWanderArea;
    [SerializeField] float yWanderArea;
    [SerializeField] Transform wanderAreaOrigin;
    [SerializeField] float speed;
    [SerializeField] float interactionCircleRadius;
    [SerializeField] LayerMask interactableLayer;
    [RangeAttribute(1, 100)][SerializeField] int bushLikelyHood;
    [RangeAttribute(1, 100)][SerializeField] int catLikelyHood;

    Rect wanderArea;

    Vector2 goalArea;

    Vector2 currentPos;
    Vector2 lastPos;

    enum Behaviors { WANDERING, BUSH }
    Behaviors behavior;

    float timer;

    Bush bushToHideIn;

    void Start()
    {
        Init();
    }

    void OnEnable()
    {
        behavior = Behaviors.WANDERING;
        Init();
    }

    void Init()
    {
        wanderArea = new Rect(wanderAreaOrigin.position, new Vector2(xWanderArea, yWanderArea));
        GetRandomPosInWanderArea();
    }

    private void Update()
    {
        switch (behavior)
        {
            case Behaviors.WANDERING:
                timer += Time.deltaTime;
                if (goalArea != null)
                {
                    RandomlyWander();
                }
                if(timer>=4)
                DecideToInteract(Physics2D.OverlapCircleAll(transform.position, interactionCircleRadius, interactableLayer));
                break;
            case Behaviors.BUSH:
                float deltaSpeed = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, goalArea, deltaSpeed);
                if (Vector2.Distance(transform.position, goalArea) < .2f)
                {
                    bushToHideIn.AddCat();
                    gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
       
    }
    
    void DecideToInteract(Collider2D[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            Interactables interactable = colliders[i].gameObject.GetComponent<Interactables>();

            float roll = Random.Range(1, 101);

            switch (interactable.type)
            {
                case Interactables.Types.BUSH:
                    {
                        if (roll <= bushLikelyHood)
                        {
                            EnterBush(colliders[i].GetComponent<Bush>());
                            break;
                        }
                        else
                        {
                            continue;
                        }
                        break;
                    }
                case Interactables.Types.CAT:
                    {
                        if (roll <= catLikelyHood)
                        {

                        }
                        else
                        {
                            continue;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }

    void EnterBush(Bush bush)
    {
        behavior = Behaviors.BUSH;
        goalArea = bush.transform.position;
        bushToHideIn = bush;
    }

    void RandomlyWander()
    {
        float deltaSpeed = speed * Time.deltaTime;
        lastPos = currentPos;
        currentPos = transform.position;
        float velocity = ((currentPos - lastPos).magnitude) / Time.deltaTime;
        if (velocity < .5f)
        {
            goalArea = GetRandomPosInWanderArea();
        }
        if (Vector2.Distance(transform.position, goalArea) >= .2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, goalArea, deltaSpeed);
        }
        else
        {
            goalArea = GetRandomPosInWanderArea();
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

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionCircleRadius);
    }
}
