namespace Player.Weapons
{
    public class WeaponInstance
    {
        public Weapon Weapon { get; private set; }
        public int Level { get; set; }
        public float Damage { get; set; }
        public float Cooldown { get; set; }
        public int ProjectileCount { get; set; }
        public float ProjectileSpeed { get; set; }
        public float Area { get; set; }
        public float Duration { get; set; }

        public WeaponInstance(Weapon weapon)
        {
            Weapon = weapon;
            Level = 0;

            InitializeStats();
        }

        public void LevelUp()
        {
            if (Level >= Weapon.levels.Count) return;
            
            WeaponLevelData levelData = Weapon.levels[Level];
            levelData.ApplyTo(this);
            Level++;
        }

        
        private void InitializeStats()
        {
            Damage = Weapon.baseData.damage;
            Cooldown = Weapon.baseData.cooldown;
            ProjectileCount = Weapon.baseData.projectileCount;
            ProjectileSpeed = Weapon.baseData.projectileSpeed;
            Area = Weapon.baseData.area;
            Duration = Weapon.baseData.duration;
        }
    }
}