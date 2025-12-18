using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TasksUI_Script : MonoBehaviour
{
    [SerializeField] Task_Manager Task_Manager;

    [SerializeField] Text TasksUI;
    [SerializeField] Text GoodLuckText;
    bool flip = true;

    // Update is called once per frame
    void Update()
    {
        if (Task_Manager.remainingToys <= 20 && Task_Manager.remainingTrash <= 15 && flip)
        {
            flip = false;
            StartCoroutine(GoodLuck());
        }
        TasksUI.text = "Be a Good Boy + \nRemaining Toys: " + Task_Manager.remainingToys + "\nRemaining Trash: " + Task_Manager.remainingTrash;
    }

    IEnumerator GoodLuck()
    {
        GoodLuckText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        TasksUI.gameObject.SetActive(true);
        GoodLuckText.gameObject.SetActive(false);
    }
}

