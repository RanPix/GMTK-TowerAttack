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

    private const int startMoney = 50;

    public static Action OnMoneyUpdate;

    public static void ResetMoney()
        => _money = startMoney;
}
