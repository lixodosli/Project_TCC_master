using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionChecker : ScriptableObject
{
}

public class ConditionEvent : ConditionChecker
{
    public virtual void StartChecking()
    {

    }
}

public class ItemCondition : ConditionChecker
{

}