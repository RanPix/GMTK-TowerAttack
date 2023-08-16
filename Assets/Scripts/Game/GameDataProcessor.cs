using Mirror;

public class GameDataProcessor : NetworkBehaviour
{
    private static GameDataProcessor _Instance;
    public static GameDataProcessor Instance
    {
        get => _Instance;
        set
        {
            if (!Instance)
                _Instance = value;
        }
    }
    
    [Server]
    public void ResetMoneyForAllPlayers()
    {
        ResetMoneyForPlayer(GameData.Instance.Attacker);
        ResetMoneyForPlayer(GameData.Instance.Defender);
    }
    [Server]
    public void ResetMoneyForPlayer(Player player)
    {
        player.PlayerData.ResetMoney();
    }
    
    [Server]
    public void AddMoneyForAllPlayers(int amount)
    {
        AddMoneyToPlayer(amount, GameData.Instance.Attacker);
        AddMoneyToPlayer(amount, GameData.Instance.Defender);
    }
    [Server]
    public void AddMoneyToPlayer(int amount, Player player)
    {
        player.PlayerData.RemoveMoney(amount);
    }
    [Server]
    public void RemoveMoneyForAllPlayers(int amount)
    {
        AddMoneyToPlayer(amount, GameData.Instance.Attacker);
        AddMoneyToPlayer(amount, GameData.Instance.Defender);
    }
    [Server]
    public void RemoveMoneyToPlayer(int amount, Player player)
    {
        player.PlayerData.RemoveMoney(amount);
    }
    
    
}
