using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public float remainingGold = 100;

    public void mineGold()
    {
        remainingGold -= 1;
        if (remainingGold <= 0)
        {
            Destroy(gameObject);
        }
    }
}
