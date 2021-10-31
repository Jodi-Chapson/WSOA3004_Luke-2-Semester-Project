using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private BoxCollider2D FishTankTrigger;
    private GameObject MoveSpot;
    public Transform MoveSpotTransform;

    public float minX;
    public float maxX;
    public float maxY;
    public float minY;

    public float minspeed;
    public float maxspeed;
    public float currentSpeed;

    public float speedTime;
    public float MinSpeedTime;
    public float MaxSpeedTime; //just to vary the fishes speed every once in a while
    public float waitTime;
    public float StartWaitTime;

    public float scaleX;
    public bool canMove;

    public GameObject HappinessLevel;            //This is the happiness level indicator

    //Save File stuff
    public int Level;
    public float Happiness;
    public string Species;



    void Awake()
    {
        SaveManager.Fish.Add(this);
    }

    void Start()
    {
        canMove = true;
        HappinessLevel.SetActive(false);

        FishTankTrigger = GameObject.Find("Tank01").GetComponent<BoxCollider2D>();
        minX = FishTankTrigger.bounds.min.x;
        maxX = FishTankTrigger.bounds.max.x;
        minY = FishTankTrigger.bounds.min.y;
        maxY = FishTankTrigger.bounds.max.y;
        speedTime = Random.Range(MinSpeedTime, MaxSpeedTime);
        waitTime = StartWaitTime;
        currentSpeed = Random.Range(minspeed, maxspeed);
        MoveSpot = new GameObject();
        MoveSpotTransform = MoveSpot.transform;
        scaleX = this.transform.localScale.x;
        Happiness = HappinessLevel.GetComponent<Slider>().value;

        DetermineMoveSpot(MoveSpotTransform);
        FlipFish(MoveSpotTransform);
        

    }

    void OnMouseOver()
    {
        HappinessLevel.SetActive(true);         // Only Appear when mouse hovers over the fish
    }

    void OnMouseExit()
    {
        HappinessLevel.SetActive(false);
    }

    void Update()
    {
        
        if (speedTime <= 0)
        {
            VarySpeed();
        }
        else
        {
            speedTime -= Time.deltaTime;
        }


        //moves fish
        MoveFish(MoveSpotTransform);


        //checks if fish is new movespot position
        if (Vector2.Distance(transform.position, MoveSpotTransform.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                Destroy(MoveSpot);
                
                MoveSpot = new GameObject();
                MoveSpotTransform = MoveSpot.transform;

                DetermineMoveSpot(MoveSpotTransform);

                FlipFish(MoveSpotTransform);


                float random = Random.Range(StartWaitTime - 2, StartWaitTime);
                waitTime = random;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }


        CheckCursor();
    }

    private void MoveFish(Transform movespot)
    {
        //moves the fish towards the movespot transform position

        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, movespot.position, currentSpeed * Time.deltaTime);
        }
    }

    private Transform DetermineMoveSpot(Transform movespot)
    {
       //choose a position within the range of the home tank
       
        float xPos = Random.Range(minX, maxX);
        float yPos = Random.Range(minY, maxY);
        movespot.position = new Vector2(xPos, yPos);
        return movespot;
    }

    public void FlipFish(Transform movespot)
    {
        //flips the fish gameobject based on their position in relation to the movespot position
        
        if (movespot.position.x < this.transform.position.x) //the new location is towards the left of the fish
        {
            this.transform.localScale = new Vector3(scaleX, this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (movespot.position.x > this.transform.position.x) //the new location is towards the right of the fish
        {
            this.transform.localScale = new Vector3(-scaleX, this.transform.localScale.y, this.transform.localScale.z);
        }
    }

    public void CheckCursor()
    {
        //checks the position of the players cursor

        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentfishpos = this.transform.position;

        float distance = Vector2.Distance(mousepos, currentfishpos);

        if (distance < 1.5f)
        {
            MoveSpotTransform.position = mousepos;
            FlipFish(MoveSpotTransform);
        }


    }

    public void VarySpeed()
    {
        //ramdomly increasing or decreasing speed
        
        
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            float speed = currentSpeed + 0.05f;
        }
        else if (random == 1)
        {
            float speed = currentSpeed - 0.05f;
        }
        
        

        speedTime = Random.Range(MinSpeedTime, MaxSpeedTime);
    }



 
}
