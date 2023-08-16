using System;
using Mirror;

public class PlayerData : NetworkBehaviour
{
    [SyncVar]
    private int _money = 50;
    public int Money 
    {  
        get => _money;
        [Server]
        set 
        { 
            _money = value;
            OnMoneyUpdate();
        }
    }

    [SyncVar] 
    public string PlayerName = "user";
    [SyncVar] 
    public string PlayerID;
    
    
    private const int startMoney = 50;

    public Action OnMoneyUpdate;

    public void ResetMoney()
        => _money = startMoney;
        
    public void AddMoney(int amount)
        => _money += amount;
    
    public void RemoveMoney(int amount)
        => _money -= amount;
}
