using System;

public class PlayerData
{
    private static int _money = 2500;
    public static int Money 
    {  
        get { return _money; }
        set 
        { 
            _money = value;
            OnMoneyUpdate();
        }
    }
    public static Action OnMoneyUpdate;
}