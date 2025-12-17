using UnityEngine;
using UnityEngine.UI;

public class Interaction_Script : MonoBehaviour
{
    public float range = 1f;   // Distância máxima do raio

    [SerializeField] public Task_Manager Task; 
    [SerializeField] GameObject Hand;
    [SerializeField] public GameObject heldOBJ = null;
    [SerializeField] public GameObject Player;

    [SerializeField] public Canvas UI;
    Text interactionText;
    public LayerMask interactableLayer;

    public bool isHidden = false;

    private void Start()
    {
        interactionText = UI.GetComponentInChildren<Text>(true);
    }
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.blue);
        

            if (Physics.Raycast(ray, out hit, 1, interactableLayer))
            {
                interactionText.text = "Press [E] to Interact";
                interactionText.gameObject.SetActive(true);
            }
            else
            {
                interactionText.gameObject.SetActive(false);
                interactionText.text = "";
            }


        

        if (Input.GetKeyDown(KeyCode.E))  //Pega ou solta coisas ou Interage com algo
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

                if (Physics.Raycast(ray, out hit, range))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                    if(hit.collider.CompareTag("HideSpot"))
                    {
                        Transform exitPoint = hit.transform.Find("Exit_Point");
                        Debug.Log(exitPoint.position);

                        switch (isHidden)
                        {
                            case false:
                                Debug.Log("Hidden");
                                isHidden = true;
                                Player.transform.position = hit.transform.position;
                                break;
                            case true:
                                Player.GetComponent<CharacterController>().enabled = false; //Gambiarra a gente aceita
                                Player.transform.position = exitPoint.position;
                                Player.GetComponent<CharacterController>().enabled = true; //A derrota nao
                                isHidden = false;
                                break;
                        }
                        
                    }

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
                    
                    if (hit.collider.CompareTag("Dishes")) //Task Jogar lixo no lixo
                    {
                        interactionText.gameObject.SetActive(true);
                        interactionText.text = "Cleaning Dishes...";
                    }
                    /*
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

