using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScriptComponentsHandler
{
    public List<Behaviour> ChangeScriptStatus(List<Behaviour> scripts, bool isActive);
}
