using UnityEngine;

public class BattleController : IController
{
    public Character[] Candidates { get; }

    public BattleController()
    {
        Candidates = new Character[2];
        Managers.Data.Init();
        Candidates[0] = Managers.Data.NPCs[Random.Range(0, Managers.Data.NPCs.Count)];
        Candidates[1] = Managers.Data.Player;
    }

    public Character GetWinner()
    {
        if (Candidates[0].Awareness > Candidates[1].Awareness)
        {
            return Candidates[0];
        }
        return Candidates[1];
    }
}