// This is the script that is attached to every player or AI.
// It calculates various things such as the distance to the closest gold, the distance to the closest map part, the total gold in the map, etc...
// It also contains the coroutine to mine gold over time, and the coroutines to display the level up and treasure found texts.
// It has all the UI elements

using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float gold;
    public float level;
    public float mapPart;

    public float distanceToMine;
    public float closestGoldDistance;
    public float closestMapPartDistance;

    public int randomNumberOfGold;
    public bool wait = false;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI mapPartText;

    public TextMeshProUGUI levelUpText;
    public TextMeshProUGUI mapPartFoundText;
    public TextMeshProUGUI allMapsPartsFoundText;

        public TextMeshProUGUI totalGoldInMapText;

    public GameObject CenterMapImage1;
    public GameObject CenterMapImage2;
    public GameObject CenterMapImage3;
    public GameObject CenterMapImage4;

    public GameObject RightMapImage1;
    public GameObject RightMapImage2;
    public GameObject RightMapImage3;
    public GameObject RightMapImage4;
    public GameObject Waypoint;
    public GameObject closestGoldObject { get; private set; }

    public float totalGoldInMap;


    // Update is called once per frame
    void Update()
    {
        // Force the player to stay vertially straight (water bug)
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        // Gold part
        // Function to get the closest gold object
        GameObject[] goldObjects = GameObject.FindGameObjectsWithTag("Gold");
        closestGoldObject = null;
        closestGoldDistance = float.MaxValue;

        foreach (GameObject goldObject in goldObjects)
        {
            float distance = Vector3.Distance(transform.position, goldObject.transform.position);
            if (distance < closestGoldDistance)
            {
                closestGoldDistance = distance;
                closestGoldObject = goldObject;
            }
        }

        // Levelup
        if (gold >=100)
        {
            level += 1;
            gold -= 100;
            StartCoroutine(displayLevelUpText());
        }

        // Mine gold coroutine
        if (closestGoldDistance <= distanceToMine && wait==false)
        {
            Gold goldScript = closestGoldObject.GetComponent<Gold>();
            StartCoroutine(MineGoldOverTime(goldScript));
        }

        // Displaying the gold and level
        goldText.text = "Gold  " + gold.ToString();
        levelText.text = "Level  " + level.ToString();

        // Map parts
        GameObject[] mapPartObjects = GameObject.FindGameObjectsWithTag("Map part");
        GameObject closestMapPartObject = null;
        closestMapPartDistance = float.MaxValue;

        // Function to get the closest treasure object
        foreach (GameObject mapPartObject in mapPartObjects)
        {
            float distance = Vector3.Distance(transform.position, mapPartObject.transform.position);
            if (distance < closestMapPartDistance)
            {
                closestMapPartDistance = distance;
                closestMapPartObject = mapPartObject;
            }
        }

        // Found map part function
        if (closestMapPartDistance <= distanceToMine)
        {
            if (level >= 3)
                {
                Destroy(closestMapPartObject);
                mapPart += 1;
                StartCoroutine(displayTreasureFoundText());
                mapPartText.text = "Map parts " + mapPart.ToString();
                }
            else
            {
                StartCoroutine(displayInsufficientLevel());
            }
        }

        
        // Total gold in map calculator with every exception
        totalGoldInMap = 0;

        goldObjects = GameObject.FindGameObjectsWithTag("Gold");

        foreach (GameObject goldObject in goldObjects) {
            Gold goldScript = goldObject.GetComponent<Gold>();
            if (goldScript != null) {
                totalGoldInMap += goldScript.remainingGold;
            }
        }

        if (goldObjects.Length == 0) {
            totalGoldInMap = 0;
        }

        if (totalGoldInMapText != null) {
            totalGoldInMapText.text = "Gold left in Map : " + totalGoldInMap.ToString();
            if (totalGoldInMap == 0)
            {
                if (level < 3)
                {
                    totalGoldInMapText.text = "Game over : no more gold in the map! You cannot levelup and collect map parts anymore";
                }
            }
        }
    }

// The coroutine to mine gold over time (I struggled a lot with this one)
IEnumerator MineGoldOverTime(Gold goldScript)
{
    while (true)
    {
        wait = true;
        if (goldScript != null && goldScript.gameObject != null)
        {
            float distanceToGold = Vector3.Distance(transform.position, goldScript.gameObject.transform.position);
            if (distanceToGold <= distanceToMine)
            {
                randomNumberOfGold = Random.Range(5, 16);
                goldScript.mineGold(randomNumberOfGold);
                gold += randomNumberOfGold;
            }
            else
            {
                wait = false;
                break; 
            }
        }
        else
        {
            wait = false;
            break; 
        }
        yield return new WaitForSeconds(1);
        wait = false;
    }
}

// Display level up / treasure found texte coroutines
IEnumerator displayInsufficientLevel()
{
    levelUpText.text = "You need to be at least level 3 to collect map parts!";
    levelUpText.gameObject.SetActive(true);
    yield return new WaitForSeconds(5);
    levelUpText.gameObject.SetActive(false);
}

IEnumerator displayLevelUpText()
{
    levelUpText.text = "You have advanced to the level " + level.ToString() + "!";
    levelUpText.gameObject.SetActive(true);
    yield return new WaitForSeconds(5);
    levelUpText.gameObject.SetActive(false);
}

IEnumerator displayTreasureFoundText()
{
    if (mapPart !=4)
    {
    mapPartFoundText.text = "You have found " + mapPart.ToString() + " map parts out of 4 !";
    mapPartFoundText.gameObject.SetActive(true);
    }

    if (mapPart == 1)
    {
        CenterMapImage1.SetActive(true);
    }
    else if (mapPart == 2)
    {
        CenterMapImage1.SetActive(true);
        CenterMapImage2.SetActive(true);
    }
    else if (mapPart == 3)
    {
        CenterMapImage1.SetActive(true);
        CenterMapImage2.SetActive(true);
        CenterMapImage3.SetActive(true);
    }
    else if (mapPart == 4)
    {
        CenterMapImage1.SetActive(true);
        CenterMapImage2.SetActive(true);
        CenterMapImage3.SetActive(true);
        CenterMapImage4.SetActive(true);

        allMapsPartsFoundText.gameObject.SetActive(true);
    }
    
    yield return new WaitForSeconds(5);
    mapPartFoundText.gameObject.SetActive(false);

    if (mapPart == 1)
    {
        CenterMapImage1.SetActive(false);
        RightMapImage1.SetActive(true);
    }
    else if (mapPart == 2)
    {
        CenterMapImage1.SetActive(false);
        CenterMapImage2.SetActive(false);
        RightMapImage1.SetActive(true);
        RightMapImage2.SetActive(true);
    }
    else if (mapPart == 3)
    {
        CenterMapImage1.SetActive(false);
        CenterMapImage2.SetActive(false);
        CenterMapImage3.SetActive(false);
        RightMapImage1.SetActive(true);
        RightMapImage2.SetActive(true);
        RightMapImage3.SetActive(true);
    }
    else if (mapPart == 4)

    {
        CenterMapImage1.SetActive(false);
        CenterMapImage2.SetActive(false);
        CenterMapImage3.SetActive(false);
        CenterMapImage4.SetActive(false);
        RightMapImage1.SetActive(true);
        RightMapImage2.SetActive(true);
        RightMapImage3.SetActive(true);
        RightMapImage4.SetActive(true);

        allMapsPartsFoundText.gameObject.SetActive(false);
        Waypoint.SetActive(true);
    }

}
}