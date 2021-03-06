using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFish : MonoBehaviour
{

    public GameObject OneStar;
    public GameObject TwoStar;
    public GameObject ThreeStar;
    public GameObject FourStar;
    public GameObject FiveStar;
    public GameObject targetfish;

    public Text UpgradePrice;

    public int Clicks = 0;

    public float UpgradeFishPrice = 100f;

    public void Start()
    {
        UpgradePrice.text = "Price: " + UpgradeFishPrice;
    }

    public void Upgrade()
    {
        if (BubbleManager.Count >= UpgradeFishPrice)
        {
            BubblesGenerated.bubbles -= (int)UpgradeFishPrice;

            UpgradeFishPrice = (int)(UpgradeFishPrice * 1.5f);
            OneStar.SetActive(true);

            UpgradePrice.text = "Price: " + UpgradeFishPrice;
            targetfish.GetComponent<BubblesGenerated>().levelmodifier += 1;
            targetfish.GetComponent<BubblesGenerated>().level += 1;

            Clicks++;
        }

    }

    void Update()
    {
        if(Clicks == 1)
        {
            OneStar.SetActive(true);
        }

        if (Clicks == 2)
        {
            TwoStar.SetActive(true);
        }

        if (Clicks == 3)
        {
            ThreeStar.SetActive(true);
        }

        if (Clicks == 4)
        {
            FourStar.SetActive(true);
        }

        if (Clicks == 5)
        {
            FiveStar.SetActive(true);
        }
    }
}
