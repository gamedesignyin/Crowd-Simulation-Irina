using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        goalLocations = GameObject.FindGameObjectsWithTag("goal");

        // Executa apenas se o agente estiver posicionado corretamente no NavMesh
        if (agent != null && agent.isOnNavMesh && goalLocations.Length > 0)
        {
            SetRandomDestination();

            // Variação de velocidade de caminhada individual
            float sm = Random.Range(0.5f, 2f);
            agent.speed *= sm;

            if (anim != null)
            {
                anim.SetBool("isWalking", true); // Corrigido para SetBool
                anim.SetFloat("wOffset", Random.Range(0.0f, 1.0f));
                anim.SetFloat("speedMult", sm);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent == null || !agent.isOnNavMesh || goalLocations.Length == 0) return;

        // Aguarda a rota ser calculada antes de verificar a distância restante
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);
    }
}