using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonTetra : MonoBehaviour
{
    public static List<GameObject> NeonTetraInTank = new List<GameObject>();
    public GameObject NeonTetraPrefab;
    public Fish fish;
    private void Start()
    {
        fish = this.GetComponent<Fish>();
        CheckForOtherNeonTetras();
    }

    private void Update()
    {
        Debug.Log(NeonTetraInTank.Count);

        if (NeonTetraInTank.Count >= 2)
        {
            //if (fish.Happiness < 10f)
            //{
            //    fish.Happiness += 0.00005f;
            //}
            //else
            //{
            //    fish.Happiness = 10f;
            //}

            //Code to change their movement so that they swim together.
        }
        else
        {
            if (fish.Happiness > 0.2f)
            {
                fish.Happiness -= -0.00004f;
            }
            else
            {
                fish.Happiness = 0.2f;
            }
        }
    }

    public void CheckForOtherNeonTetras()
    {
        for (int i = 0; i < GameManager.fish.Count; i++)
        {
            if (GameManager.fish[i] == NeonTetraPrefab)
            {
                NeonTetraInTank.Add(this.gameObject);
                Debug.Log(NeonTetraInTank.Count);
            }
        }
    }
}
