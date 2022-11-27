using System;

public class MainController : IController
{
    private DateTime[] _erectDate;

    public MainController()
    {
        _erectDate = new DateTime[1];
        _erectDate[0] = new DateTime(2026, 6, 1);
    }
    public bool IsErect()
    {
        return Managers.Data.GameData.PassedTurn % 24 == 23;
    }
}