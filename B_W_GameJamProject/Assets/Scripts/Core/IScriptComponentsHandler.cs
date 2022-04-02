using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScriptComponentsHandler
{
    public void ChangeScriptStatus(List<Behaviour> scripts, bool isActive);
}
