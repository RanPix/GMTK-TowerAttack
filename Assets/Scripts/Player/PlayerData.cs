using System;

public static class PlayerData
{
    private static int _money = 50;
    public static int Money 
    {  
        get => _money;
        set 
        { 
            _money = value;
            OnMoneyUpdate();
        }
    }
    public static Action OnMoneyUpdate;
}
