using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class KrampusAI : MonoBehaviour

{
    NavMeshAgent agent;
    [SerializeField] GameObject Player;
    int InsideMask, OutsideMask;

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
    }

    // Update is called once per frame
    void Update()
    {
        agent.updateRotation = true;

        if (NowState == AIstates.Lurking)
        {
            StartCoroutine(LurkRoutine());
        }

        if (NowState == AIstates.Attacking)
        {
            agent.SetDestination(Player.transform.position);
        }

        
    }
    IEnumerator LurkRoutine()
    {
        agent.areaMask = OutsideMask;

        agent.SetDestination(new Vector3(56,2,-21)); //Mudar pra aleatorizar pontos escolhidos dentro da Navmesh
        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f);
        yield return new WaitForSeconds(2f);
        NowState = AIstates.Attacking;

    }
}
