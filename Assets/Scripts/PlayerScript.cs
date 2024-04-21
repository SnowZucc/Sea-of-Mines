using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float gold;
    public float level;
    public float treasure;

    public float closestGoldDistance;
    public float closestTreasureDistance;

    public int randomNumberOfGold;
    public bool wait = false;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI treasureText;

    public TextMeshProUGUI levelUpText;
    public TextMeshProUGUI treasureFoundText;

    // Update is called once per frame
    void Update()
    {
        // Gold part
        // Function to get the closest gold object
        GameObject[] goldObjects = GameObject.FindGameObjectsWithTag("Gold");
        GameObject closestGoldObject = null;
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
        if (closestGoldDistance <= 5 && wait==false)
        {
            Gold goldScript = closestGoldObject.GetComponent<Gold>();
            StartCoroutine(MineGoldOverTime(goldScript));
        }

        // Displaying the gold and level
        goldText.text = "Gold  " + gold.ToString();
        levelText.text = "Level  " + level.ToString();

        // Treasure part
        GameObject[] treasureObjects = GameObject.FindGameObjectsWithTag("Treasure");
        GameObject closestTreasureObject = null;
        closestTreasureDistance = float.MaxValue;

        // Function to get the closest treasure object
        foreach (GameObject treasureObject in treasureObjects)
        {
            float distance = Vector3.Distance(transform.position, treasureObject.transform.position);
            if (distance < closestTreasureDistance)
            {
                closestTreasureDistance = distance;
                closestTreasureObject = treasureObject;
            }
        }

        // Found treasure function
        if (closestTreasureDistance <= 5)
        {
            Destroy(closestTreasureObject);
            treasure += 1;
            StartCoroutine(displayTreasureFoundText());
            treasureText.text = "Treasure  " + treasure.ToString();
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
            if (distanceToGold <= 5)
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
IEnumerator displayLevelUpText()
{
    levelUpText.text = "You have advanced to the level " + level.ToString() + "!";
    levelUpText.gameObject.SetActive(true);
    yield return new WaitForSeconds(5);
    levelUpText.gameObject.SetActive(false);
}

IEnumerator displayTreasureFoundText()
{
    levelUpText.text = "You have found " + treasure.ToString() + "treasure part out of 4 !";
    levelUpText.gameObject.SetActive(true);
    yield return new WaitForSeconds(5);
    levelUpText.gameObject.SetActive(false);
}
}