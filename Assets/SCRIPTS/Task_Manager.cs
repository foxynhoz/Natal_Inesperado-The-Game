using UnityEngine;

public class Task_Manager : MonoBehaviour
{
    public int remainingToys = 20;
    public int remainingTrash = 20;
    public int remainingDishes = 20;

    void Update()
    {
        if (remainingToys == 0 && remainingDishes == 0 && remainingTrash == 0)
        {

        }
    }
}
