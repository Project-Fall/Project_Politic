using System;

public class MainController : IController
{

    private DateTime[] _erectDate;
    private bool _isCandidacy = false;

    public MainController()
    {
        _erectDate = new DateTime[1];
        _erectDate[0] = new DateTime(2026, 6, 1);
    }

    // 입후보 날이 되었는지 확인
    public bool IsCandidacyDay()
    {
        return Managers.Data.GameData.PassedTurn % 24 == 23;
    }

    // 입후보 여부
    public void SetCandidacy(bool b)
    {
        _isCandidacy = b;
    }

    // 선거에 참여하는지 = 입후보 하였는지
    public bool IsErect()
    {
        return (Managers.Data.GameData.PassedTurn % 24 == 0 && _isCandidacy);
    }
}