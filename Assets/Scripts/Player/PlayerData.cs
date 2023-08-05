using System;
using Mirror;

public class PlayerData : NetworkBehaviour
{
    [SyncVar]
    private int _money = 50;

    public int Money 
    {  
        get => _money;
        set 
        { 
            _money = value;
            OnMoneyUpdate();
        }
    }

    private const int startMoney = 50;

    public Action OnMoneyUpdate;

    public void ResetMoney()
        => _money = startMoney;
        
    public void AddMoney(int amount)
        => _money += amount;
}
