using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private BoxCollider2D FishTankTrigger;
    private BoxCollider2D FishTankTrigger02;
    private BoxCollider2D FishTankTrigger03;
    private BoxCollider2D FishTankTrigger04;

    public BoxCollider2D SwimmingArea;

    private GameObject MoveSpot;
    public Transform MoveSpotTransform;

    public float minX;
    public float maxX;
    public float maxY;
    public float minY;

    public float fishheight;
    public float fishlength;

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
    public int hometankID;

    void Awake()
    {
        SaveManager.Fish.Add(this);
        GameManager.fish.Add(this.gameObject);
    }

    void Start()
    {
        canMove = true;
        HappinessLevel.SetActive(false);

        FishTankTrigger = GameObject.Find("Tank01").GetComponent<BoxCollider2D>();
        FishTankTrigger02 = GameObject.Find("Tank02").GetComponent<BoxCollider2D>();
        FishTankTrigger03 = GameObject.Find("Tank03").GetComponent<BoxCollider2D>();
        FishTankTrigger04 = GameObject.Find("Tank04").GetComponent<BoxCollider2D>();



        speedTime = Random.Range(MinSpeedTime, MaxSpeedTime);
        waitTime = StartWaitTime;
        currentSpeed = Random.Range(minspeed, maxspeed);
        MoveSpot = new GameObject();
        MoveSpotTransform = MoveSpot.transform;
        scaleX = this.transform.localScale.x;
        HappinessLevel.GetComponent<Slider>().value = Happiness;

        //DetermineMoveSpot(MoveSpotTransform);
        //FlipFish(MoveSpotTransform);
    }

    private void LateUpdate()
    {
        if (Species == "Gold Fish" && hometankID == 1)
        {
            SwimmingArea = GameObject.Find("Gold Fish Swimming Area 1").GetComponent<BoxCollider2D>();
        }
        else if (Species == "Red Tailed Shark" && hometankID == 1)
        {
            SwimmingArea = GameObject.Find("Red Tailed Shark Swimming Area 1").GetComponent<BoxCollider2D>();
        }
        else if (Species == "Neon Tetra" && hometankID == 1)
        {
            SwimmingArea = GameObject.Find("Neon Tetra Swimming Area 1").GetComponent<BoxCollider2D>();
        }
        else if (Species == "Gold Fish" && hometankID == 2)
        {
            SwimmingArea = GameObject.Find("Gold Fish Swimming Area 2").GetComponent<BoxCollider2D>();
        }
        else if (Species == "Red Tailed Shark" && hometankID == 2)
        {
            SwimmingArea = GameObject.Find("Red Tailed Shark Swimming Area 2").GetComponent<BoxCollider2D>();
        }
        else if (Species == "Neon Tetra" && hometankID == 2)
        {
            SwimmingArea = GameObject.Find("Neon Tetra Swimming Area 2").GetComponent<BoxCollider2D>();
        }
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
        if (Vector2.Distance(transform.position, MoveSpotTransform.position) < 0.3f)
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
                currentSpeed = minspeed;
            }

        }

        HappinessLevel.GetComponent<Slider>().value = Happiness;

        CheckCursor();

        if (this.transform.position.x >= -7.5 && this.transform.position.x <= 7.3)
        {

            minX = FishTankTrigger.bounds.min.x + fishlength;
            maxX = FishTankTrigger.bounds.max.x - fishlength;
            minY = FishTankTrigger.bounds.min.y + fishheight;
            maxY = FishTankTrigger.bounds.max.y - fishheight;
        }

        else if (this.transform.position.x >= 9 && this.transform.position.x <= 23)
        {

            minX = FishTankTrigger02.bounds.min.x + fishlength;
            maxX = FishTankTrigger02.bounds.max.x - fishlength;
            minY = FishTankTrigger02.bounds.min.y + fishheight;
            maxY = FishTankTrigger02.bounds.max.y - fishheight;
        }

        else if (this.transform.position.x >= 25 && this.transform.position.x <= 39)
        {

            minX = FishTankTrigger03.bounds.min.x + fishlength;
            maxX = FishTankTrigger03.bounds.max.x - fishlength;
            minY = FishTankTrigger03.bounds.min.y + fishheight;
            maxY = FishTankTrigger03.bounds.max.y - fishheight;
        }

        else if (this.transform.position.x >= 42 && this.transform.position.x <= 55)
        {

            minX = FishTankTrigger04.bounds.min.x + fishlength;
            maxX = FishTankTrigger04.bounds.max.x - fishlength;
            minY = FishTankTrigger04.bounds.min.y + fishheight;
            maxY = FishTankTrigger04.bounds.max.y - fishheight;
        }

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
        float xPos = Random.Range(SwimmingArea.bounds.min.x, SwimmingArea.bounds.max.x);
        float yPos = Random.Range(SwimmingArea.bounds.min.y, SwimmingArea.bounds.max.y);
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

        if (mousepos.x > maxX || mousepos.x < minX || mousepos.y > maxY || mousepos.y < minY)
        {
            return;
        }


        Vector2 currentfishpos = this.transform.position;

        float distance = Vector2.Distance(mousepos, currentfishpos);

        if (distance < 2f)
        {
            MoveSpotTransform.position = mousepos;
            currentSpeed = maxspeed;
            FlipFish(MoveSpotTransform);
        }
    }

    public void VarySpeed()
    {
        //ramdomly increasing or decreasing speed

        int random;
        float speed;

        if (currentSpeed == maxspeed)
        {
            random = 1;
        }
        else if (currentSpeed == minspeed)
        {
            random = 0;
        }
        else
        {
            random = Random.Range(0, 2);
        }

        if (random == 0)
        {
            speed = currentSpeed + 0.1f;
        }
        else
        {
            speed = currentSpeed - 0.1f;
        }

        if (speed > maxspeed)
        {
            speed = maxspeed;
        }
        else if (speed < minspeed)
        {
            speed = minspeed;
        }

        currentSpeed = speed;

        speedTime = Random.Range(MinSpeedTime, MaxSpeedTime);
    }
}
