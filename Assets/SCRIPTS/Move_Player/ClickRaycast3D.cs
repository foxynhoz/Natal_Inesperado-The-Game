using UnityEngine;

public class ClickRaycast3D : MonoBehaviour
{
    public float range = 100f;   // Distância máxima do raio
    [SerializeField] public Task_Manager Task; 

    [SerializeField] GameObject Hand;
    [SerializeField] public GameObject heldOBJ = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))  //Pega ou solta coisas
        {
            if (heldOBJ != null) //Ja ta segurando algo
            {
                Debug.Log("Soltou");
                heldOBJ.transform.SetParent(null);
                heldOBJ.GetComponent<Rigidbody>().isKinematic = false;
                heldOBJ.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward*3,ForceMode.Impulse);
                heldOBJ = null;
            }

            if (heldOBJ == null) //Nao ta segurando nada
            {
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, range))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                    if (hit.collider.CompareTag("Toys")) //Task Pegar brinquedos
                    {
                        if (heldOBJ == null)
                        {
                            Debug.Log("Segurou");
                            heldOBJ = hit.collider.attachedRigidbody.gameObject;
                            heldOBJ.transform.position = Hand.transform.position;
                            heldOBJ.transform.SetParent(Hand.transform.parent, true);
                            heldOBJ.GetComponent<Rigidbody>().isKinematic = true;
                        }

                    }
                    /*
                    if (hit.collider.CompareTag("Trash")) //Task Jogar lixo no lixo
                    {
                        Destroy(hit.collider.gameObject);
                        Task.remainingTrash--;
                    }
                    if (hit.collider.CompareTag("Dishes")) //Task Pratos
                    {
                        Destroy(hit.collider.gameObject);
                        Task.remainingDishes--;
                    }
                    if (hit.collider.CompareTag("Door")) //Task Fechar porta
                    {
                        hit.collider.gameObject.SetActive(true);
                    }
                    */
                }
            }
            
            
        }
    }
}

// https://youtube.com/shorts/xT5meeMTv6I?si=l0verlFAd8Fmdp6f sound effect toy 

