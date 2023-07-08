namespace Tower
{
    
    
    [System.Serializable]
    public class TowerAbility
    {
        private Tower parent;
        public float CooldownTime { get; private set; }
        private float lastCastTime;

        /*public TowerAbility( Tower parent, float cooldownTime)
        {
            this.parent = parent;
            CooldownTime = cooldownTime;
        }*/

        public void Update()
        {
            
        }
    }
}