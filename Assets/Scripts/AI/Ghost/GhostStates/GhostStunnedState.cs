using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStunnedState : GhostState
{
    public override void EnterState(GhostController controller)
    {
        Controller = controller;

        Controller.StunnedState = true;
        Controller.StartCoroutine(Stunned());
    }

    public override void Tick() { }

    private IEnumerator Stunned()
    {
        yield return new WaitForSeconds(Controller.StunTimer);
        Controller.ChangeState(Controller.Chase);
        Controller.StunnedState = false;
        Controller.Health = 1f;
    }
}
