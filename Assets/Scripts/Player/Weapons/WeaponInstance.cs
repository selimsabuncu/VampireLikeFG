namespace Player.Weapons
{
    public class WeaponInstance
    {
        public Weapon Weapon { get; private set; }
        public int Level { get; private set; }
        public float Damage { get; private set; }
        public float Cooldown { get; private set; }
        public int ProjectileCount { get; private set; }
        public float ProjectileSpeed { get; private set; }
        public float Area { get; private set; }
        public float Duration { get; private set; }

        public WeaponInstance(Weapon weapon)
        {
            Weapon = weapon;
            Level = 1;

            InitializeStats();
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