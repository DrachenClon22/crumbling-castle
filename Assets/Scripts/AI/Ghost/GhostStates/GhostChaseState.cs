using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostChaseState : GhostState
{
    private Transform _player;
    public override void EnterState(GhostController controller)
    {
        Controller = controller;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Tick()
    {
        Controller.NavMeshAgent.SetDestination(_player.position);
    }
}
