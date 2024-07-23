using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostState
{
    public GhostController Controller;
    public abstract void EnterState(GhostController controller);
    public abstract void Tick();
}
