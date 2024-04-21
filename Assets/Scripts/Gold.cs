using UnityEngine;

public class Gold : MonoBehaviour
{
    public int remainingGold;

    void Awake()
    {
        remainingGold = Random.Range(50, 100); 
    }

    public void mineGold(int numberOfGold)
    {
        remainingGold -= numberOfGold;
        if (remainingGold <= 0)
        {
            Destroy(gameObject);
        }
    }
}
