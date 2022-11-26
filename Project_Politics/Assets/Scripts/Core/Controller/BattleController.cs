

public class BattleController : IController
{
    public Character[] Candidates { get;}

    public BattleController()
    {
        Candidates = new Character[2];
    }

    public Character GetResultCandidate()
    {
        if (Candidates[0].Awareness > Candidates[1].Awareness)
        {
            return Candidates[0];
        }
        return Candidates[1];
    }
}