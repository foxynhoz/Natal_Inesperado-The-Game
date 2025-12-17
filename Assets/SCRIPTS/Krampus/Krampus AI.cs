using System.Collections;
using System.Linq;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.AI;

public class KrampusAI : MonoBehaviour

{
    NavMeshAgent agent;
    [SerializeField] GameObject Player;
    int InsideMask, OutsideMask;

    [SerializeField] GameObject[] breachPointsList;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RoutineManager()
    {
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
                agent.SetDestination(Player.transform.position);
                break;
            case AIstates.Searching:
                StartCoroutine(SearchingRoutine());
                break;
            case AIstates.Retreating:
                break;

        }
    }

    IEnumerator LurkRoutine()
    {  

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
        StopCoroutine(BreachingRoutine());
    }

    IEnumerator SearchingRoutine()
    {
        agent.SetDestination(new Vector3(Random.Range(55f, 40f), Random.Range(2, 5), Random.Range(-17f, -4f))); //Mudar pra aleatorizar pontos escolhidos dentro da Navmesh
        yield return new WaitForSeconds(3f);
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);
        yield return new WaitForSeconds(3f);
        RoutineManager();
        StopCoroutine(SearchingRoutine());

        /*
        int rand = Random.Range(0, 6); Debug.Log(rand);

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
        */
    }
}
    