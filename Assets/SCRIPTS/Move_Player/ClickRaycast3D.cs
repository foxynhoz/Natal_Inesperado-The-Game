using UnityEngine;

public class ClickRaycast3D : MonoBehaviour
{
    public float range = 100f;   // Distância máxima do raio
    [SerializeField] public Task_Manager Task; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))   
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, range))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);


                if (hit.collider.CompareTag("Toys")) //Task Pegar brinquedos
                {
                    Destroy(hit.collider.gameObject);
                    Task.remainingToys--;
                }  
                if(hit.collider.CompareTag("Trash")) //Task Jogar lixo no lixo
                {
                    Destroy(hit.collider.gameObject);
                    Task.remainingTrash--;
                }
                if(hit.collider.CompareTag("Dishes")) //Task Pratos
                {
                    Destroy(hit.collider.gameObject);
                    Task.remainingDishes--;
                }

            }
            
        }
    }
}

// https://youtube.com/shorts/xT5meeMTv6I?si=l0verlFAd8Fmdp6f sound effect toy 

