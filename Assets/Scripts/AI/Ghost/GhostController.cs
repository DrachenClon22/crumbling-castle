using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GhostController : MonoBehaviour, IInteractable
{
    public GhostStunnedState Stunned = new GhostStunnedState();
    public GhostChaseState Chase = new GhostChaseState();

    public float StunTimer = 10f;
    public float Health = 1f;
    public float HealthDamage = 0.3f;
    public float Speed = 2f;

    public GameObject Cubes;
    public AudioClip stunned;

    public bool StunnedState = false;

    [HideInInspector]
    public NavMeshAgent NavMeshAgent;

    private GhostState _currentState;

    private float _damageTimer = 1f;
    private float _timer = 0f;

    private void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        _currentState = Chase;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_currentState is not null)
        {
            _currentState.Tick();
        }
    }

    public void ChangeState(GhostState newState)
    {
        _currentState = newState;
        NavMeshAgent.SetDestination(transform.position);
        _currentState.EnterState(this);
    }

    public void BeginInteract()
    {
        if (StunnedState)
        {
            if (PlayerController.GhostsHeld < 4)
            {
                PlayerController.GhostsHeld += 1;
                PlayerController.Score += 100;
                GameObject.Destroy(gameObject);
            }
        }
    }

    public void ContinueInteract()
    {
        if (_timer > _damageTimer)
        {
            _timer = 0f;
            Health -= HealthDamage;
            if (Health <= 0f && !StunnedState)
            {
                ChangeState(Stunned);
                SoundManager.PlayAudio(stunned);
            }
        }
    }

    public void EndInteract() { }

    private void OnCollisionEnter(Collision collision)
    {
        if (!StunnedState)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (PlayerController.Score <= 100)
                {
                    PlayerController.Score = 0;
                }
                else
                {
                    PlayerController.Score -= 100;
                }
                CastleManager.Integrity -= 0.1f;
                Destroy(gameObject);
            }
        }
    }
}
