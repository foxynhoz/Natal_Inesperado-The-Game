using UnityEngine;

public class Toy_Box_Script : MonoBehaviour
{
    [SerializeField] Task_Manager taskManager;

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.collider.CompareTag("Toys"))
        {
            Destroy(collision.gameObject);
            taskManager.remainingToys--;
        }
    }
}
