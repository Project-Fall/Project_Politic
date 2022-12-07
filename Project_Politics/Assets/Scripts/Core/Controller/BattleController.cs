using UnityEngine;

public class BattleController : IController
{

    public Character[] Candidates { get; }

    public BattleController()
    {
        Candidates = new Character[2];
        Candidates[0] = Managers.Resource.Load<Character>($"ScriptObjects/Character/NPC/NPC{Random.Range(0, 3)+1}");
        Candidates[1] = Managers.Resource.Load<Character>("ScriptObjects/Character/Player");
    }

    public Character GetResultCandidate()
    {
        if (Candidates[0].Awareness > Candidates[1].Awareness)
        {
            return Candidates[0];
        }
        return Candidates[1];
    }

    public Character GetCandidate(int idx)
    {
        return Candidates[idx];
    }
}