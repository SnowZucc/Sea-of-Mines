using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float gold;
    public float level;
    public float closestGoldDistance;
    public bool wait = false;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI levelUpText;
    public int randomNumberOfGold;

    // Update is called once per frame
    void Update()
    {
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

        if (gold >=100)
        {
            level += 1;
            gold -= 100;
            StartCoroutine(displayLevelUpText());
        }

        if (closestGoldDistance <= 5 && wait==false)
        {
            Gold goldScript = closestGoldObject.GetComponent<Gold>();
            StartCoroutine(MineGoldOverTime(goldScript));
        }

        goldText.text = "Gold  " + gold.ToString();
        levelText.text = "Level  " + level.ToString();
    }

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

IEnumerator displayLevelUpText()
{
    levelUpText.text = "You have advanced to the level " + level.ToString() + "!";
    levelUpText.gameObject.SetActive(true);
    yield return new WaitForSeconds(5);
    levelUpText.gameObject.SetActive(false);
}
}