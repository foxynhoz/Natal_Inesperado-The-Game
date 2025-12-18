using System.Collections;
using System.Linq;
using Unity.Transforms;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class KrampusAI : MonoBehaviour

{
    NavMeshAgent agent;
    [SerializeField] GameObject Player;
    int InsideMask, OutsideMask;

    [SerializeField] GameObject[] breachPointsList;

    bool isHidden;
    public enum AIstates
    {
        Lurking , Breaching , Attacking , Searching , Retreating
    }

    public AIstates NowState = AIstates.Lurking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InsideMask = 1 << NavMesh.GetAreaFromName("Inside");
        OutsideMask = 1 << NavMesh.GetAreaFromName("Outside");

        agent.areaMask = OutsideMask;
        RoutineManager();
        isHidden = Camera.main.GetComponent<Interaction_Script>().isHidden;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Player.transform.position) < 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Gameover");
            SceneManager.LoadScene("GameOver");
        }
    }

    void RoutineManager()
    {
        StopAllCoroutines();
        agent.updateRotation = true;

        switch (NowState)
        {
            case AIstates.Lurking:
                StartCoroutine(LurkRoutine());
                break;
            case AIstates.Breaching:
                StartCoroutine(BreachingRoutine());
                break;
            case AIstates.Attacking:
                StartCoroutine(AttackingRoutine());
                break;
            case AIstates.Searching:
                StartCoroutine(SearchingRoutine());
                break;
            case AIstates.Retreating:
                StartCoroutine(RetreatingRoutine());
                break;

        }
    }

    IEnumerator LurkRoutine()
    {
        agent.areaMask = OutsideMask;
        agent.SetDestination(new Vector3(Random.Range(35f, 59f),1.6f, Random.Range(-29f,3f))); //Mudar pra aleatorizar pontos escolhidos dentro da Navmesh
        yield return new WaitForSeconds(3f);
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);
        yield return new WaitForSeconds(3f);

        int rand = Random.Range(0, 2);  Debug.Log(rand);

        switch (rand)
        {
            case 0:
                NowState = AIstates.Breaching;
                RoutineManager();
                break;
            default:
                RoutineManager();
                break;
        }
    }

    IEnumerator BreachingRoutine()
    {
        GameObject breachPoint = breachPointsList[Random.Range(0, breachPointsList.Length)];
        agent.SetDestination(breachPoint.transform.position);           Debug.Log("Indo ate" + breachPoint.name);
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.1f);
        agent.updateRotation = false;
        agent.transform.LookAt(breachPoint.transform.position);

        if(breachPoint.GetComponent<BP_States>().isBlocked)
        {
            Debug.Log("Ta bloqueado");
            yield return new WaitForSeconds(5f);
        }
        else
        {
            Debug.Log("Nao ta bloqueado");
            yield return new WaitForSeconds(2f);
        }

        agent.areaMask = InsideMask;
        transform.position = breachPoint.transform.position;

        NowState = AIstates.Searching;
        RoutineManager();
        //StopCoroutine(BreachingRoutine());
    }

    IEnumerator SearchingRoutine()
    {
        agent.SetDestination(new Vector3(Random.Range(55f, 40f), Random.Range(2, 5), Random.Range(-17f, -4f)));
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);
        yield return new WaitForSeconds(3f);

        int rand = Random.Range(0, 3); Debug.Log(rand);

        switch (rand)
        {
            case 0:
                NowState = AIstates.Retreating;
                RoutineManager();
                break;
            default:
                RoutineManager();
                break;
        }
    }

    IEnumerator AttackingRoutine()
    {
        for (int i = 0; i < 4; i++)
        {
            agent.SetDestination(Player.transform.position);
            yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);
        }
        
        NowState = AIstates.Searching;
        //StopCoroutine(AttackingRoutine());
        RoutineManager();
    }

    IEnumerator RetreatingRoutine()
    {
        GameObject RetreatbreachPoint = breachPointsList[Random.Range(0, breachPointsList.Length)];
        agent.SetDestination(RetreatbreachPoint.transform.position); Debug.Log("Indo ate" + RetreatbreachPoint.name);
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.1f);
        agent.updateRotation = false;
        agent.transform.LookAt(RetreatbreachPoint.transform.position);
        yield return new WaitForSeconds(1f);

        NowState = AIstates.Lurking;
        RoutineManager();
        //StopCoroutine (RetreatingRoutine());
    }

    private void OnTriggerEnter(Collider other) 
    {
            if(other.name == "Player" && NowState == AIstates.Searching && !isHidden)
            {
                Debug.Log("Vendo Jogador");
                NowState = AIstates.Attacking;
                //StopCoroutine(SearchingRoutine());
                RoutineManager();
            }
    }

    
    public bool Raycasting2Player(Collider other)
    {
        Ray ray = new Ray(transform.localPosition, other.transform.localPosition);
        RaycastHit hit;
        Debug.DrawRay(transform.localPosition, other.transform.localPosition, Color.red);

        if(Physics.Raycast(ray, out hit))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
    