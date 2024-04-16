using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float gold;
    public float level;
    public float closestGoldDistance;

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
        }
    }
}
