using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubblesGenerated : MonoBehaviour
{
    public float seconds = 0f;
    public static double bubbles = 0;
   

    private bool ResetTimer;

    public Transform SpawnPoint;
    public GameObject BubblePrefab;

    public int bubbleproduction;
    public int baseproduction;
    public int level;
    public int levelmodifier = 1;
    public float happinessmodifier;
    public int hometankID;
    public InfoTankManager hometank;
    public float tankmodifier;

    public Fish fish;

    void Start()
    {
        ResetTimer = false;
        fish = this.GetComponent<Fish>();
        hometank = GameObject.Find("Tank01").GetComponent<InfoTankManager>();
        //tankmodifier = hometank.TankProductionModifer;

        if (fish.Level != 0)
        {
            level = fish.Level;
            levelmodifier = fish.Level + 1;
        }

      
    }


    void Update()
    {
        happinessmodifier = fish.Happiness / 10;
        //tankmodifier = hometank.TankProductionModifer;
        

        
        if (seconds >= 5)
        {
            bubbles += bubbleproduction;      //+1 each time a bubble spawns
            
            ResetTimer = true;
            StartCoroutine(GenerateBubble(bubbleproduction));
        }

        if (ResetTimer)
        {
            seconds = 0f;
            ResetTimer = false;
        }


        //If tank 01 is dirty, fish in that tank stop producing bubbles
        if(GameObject.Find("Dirt01").GetComponent<Dirt>().Dirty == true)
        {
            string name = GetComponent<Fish>().TankName;                //accessing string variable in fish script
            if (name == "Tank01")
            {
                bubbleproduction = 0;
                seconds = 0;

                Image SliderFill = GetComponent<Fish>().SliderFill;         //accessing slider image in fish script and turning it red when in dirt
                SliderFill.color = Color.red;
            }  
        }
       else if (GameObject.Find("Dirt01").GetComponent<Dirt>().Dirty == false)
        {
            string name = GetComponent<Fish>().TankName;
            if (name == "Tank01")
            {
                //continue
                bubbleproduction = (int)((float)baseproduction * levelmodifier * tankmodifier * happinessmodifier * GameManager.GameSpeed);
                seconds += Time.deltaTime;

                Image SliderFill = GetComponent<Fish>().SliderFill;
                SliderFill.color = Color.green;
            }
        }



        //If tank 02 is dirty, fish in that tank stop producing bubbles
        if (GameObject.Find("Dirt02").GetComponent<Dirt2>().Dirty == true)
        {
            string name = GetComponent<Fish>().TankName;
            if (name == "Tank02")
            {
                bubbleproduction = 0;
                seconds = 0;

                Image SliderFill = GetComponent<Fish>().SliderFill;
                SliderFill.color = Color.red;
            }
        }
        else if (GameObject.Find("Dirt02").GetComponent<Dirt2>().Dirty == false)
        {

            string name = GetComponent<Fish>().TankName;
            if (name == "Tank02")
            {
                //continue
                bubbleproduction = (int)((float)baseproduction * levelmodifier * tankmodifier * happinessmodifier * GameManager.GameSpeed);
                seconds += Time.deltaTime;

                Image SliderFill = GetComponent<Fish>().SliderFill;
                SliderFill.color = Color.green;
            }
          
        }

    }



    
    public IEnumerator GenerateBubble(int production)
    {
        float random = Random.Range(0,1);
        
        yield return new WaitForSeconds(random);
        
        GameObject bubble = Instantiate(BubblePrefab, SpawnPoint.position, SpawnPoint.rotation);

        if (GameManager.numbers)
        {
            bubble.GetComponentInChildren<Text>().text = "+" + production;
        }
       
    }
}
