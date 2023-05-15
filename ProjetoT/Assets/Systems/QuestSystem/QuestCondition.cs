using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestCondition : ScriptableObject
{    
    public abstract void StartCondition(string quest);
    public abstract bool Completed();
}

public class ConditionExecuter
{
    
}

public class Condition_LimparTerreno : ConditionExecuter
{
    public int TerrenosASeremLimpos = 12;
    public int TerrenosLimpos { get; private set; }
}

//public class Condition_ColetarItem : QuestCondition
//{

//}