using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class King : MonoBehaviour
{
    public Transform target;
    public string GameOverScenes;

    NavMeshAgent agent;
    Animator animator;

    private bool sceneLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponentInChildren<Animator>();

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        if (animator != null)
        {
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            if (!string.IsNullOrEmpty(GameOverScenes))
            {
                SceneManager.LoadScene(GameOverScenes);
            }

            else
            {

            }
        }
    }
}
