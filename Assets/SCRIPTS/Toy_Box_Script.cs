using UnityEngine;

public class Toy_Box_Script : MonoBehaviour
{
    [SerializeField] Task_Manager taskManager;

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.collider.CompareTag("Toys") && this.gameObject.name == "Toy_Box")
        {
            Destroy(collision.gameObject);
            taskManager.remainingToys--;
        }

        if (collision.collider.CompareTag("Trash") && this.gameObject.name == "Trash_Bin")
        {
            Destroy(collision.gameObject);
            taskManager.remainingTrash--;
        }

    }
}
