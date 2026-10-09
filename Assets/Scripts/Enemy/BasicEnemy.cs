using Extensions;
using UnityEngine;

namespace Enemy
{
    public class BasicEnemy : Enemy
    {
        protected override void Die()
        {
            base.Die();
            
            ParticleHelper.Instance.SpawnParticleAtLocation("blood", transform.position);
        }
    }
}