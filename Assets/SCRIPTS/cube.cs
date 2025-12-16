using UnityEngine;

public class ClickRaycast3D : MonoBehaviour
{
    public float range = 100f;   // Distância máxima do raio
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))   
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, range))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Cubo clicado: " + hit.collider.name);


                if (hit.collider.CompareTag("Cube"))
                    {

                    

                    Destroy(hit.collider.gameObject); 
                    

                    }
                 
                }
            
        }
    }
}