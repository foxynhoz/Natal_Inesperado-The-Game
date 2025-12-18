using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] Task_Manager taskManager;

    void Update()
    {
        if(taskManager.remainingToys <= 20 &&  taskManager.remainingTrash <= 15)
        {
            gameObject.SetActive(false);
        }
    }
}
