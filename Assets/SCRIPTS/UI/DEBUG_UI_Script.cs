using UnityEngine;
using UnityEngine.UI;

public class DEBUG_UI_Script : MonoBehaviour
{
    Text Text;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Krampus;
    [SerializeField] Task_Manager TaskManager;
    bool ShowUI = false;

    private void Start()
    {
        Text = GetComponentInChildren<Text>();
    }
    void Update()
    {
        if (ShowUI)
        {
            Text.text = (
            "\n\n====ENTITIES====\n\n" +
            "PlayerPos: " + Player.transform.position +
            "\nKrampusPos: " + Krampus.transform.position +
            "\n\n====STATES====\n\n" +
            "Krampus State: " + Krampus.GetComponent<KrampusAI>().NowState.ToString() +
            "\nPlayer is Hidden: " + Player.GetComponent<PlayerMove>().isHidden +
            "\nPlayer is holding: " + Camera.main.GetComponent<ClickRaycast3D>().heldOBJ +
            "\n\n====VARIABLES====\n\n" +
            "Remaining Toys: " + TaskManager.remainingToys +
            "\nRemaining Trash: " + TaskManager.remainingTrash +
            "\nRemaining Dishes: " + TaskManager.remainingDishes
            );
        }
        else
        {
            Text.text = ("");
        }
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ShowUI = !ShowUI;
        }
    }
}
