using UnityEngine;

namespace Enemy
{
    public class OrbitingEnemy : Enemy
    {
        [SerializeField] private float orbitSpeed = 90f;
        [SerializeField] private float orbitRadius = 5f;
        [SerializeField] private float approachSpeed = 1f;

        private float _currentRadius;
        private float _orbitAngle;

        private void Start()
        {
            Transform player = Player.PlayerController.Instance.transform;

            Vector3 offset = transform.position - player.position;
            offset.y = 0f;

            _currentRadius = offset.magnitude;
            _orbitAngle = Mathf.Atan2(offset.z, offset.x) * Mathf.Rad2Deg;

            if (_currentRadius < 0.1f)
            {
                _currentRadius = orbitRadius;
                _orbitAngle = Random.Range(0f, 360f);
            }
        }

        protected override void Move()
        {
            Transform player = Player.PlayerController.Instance.transform;

            // Rotate around the player
            _orbitAngle += orbitSpeed * Time.deltaTime;

            // Gradually reduce the orbit radius
            _currentRadius = Mathf.MoveTowards(
                _currentRadius,
                0f,
                approachSpeed * Time.deltaTime
            );

            // Calculate a circular position around the player
            float angleRadians = _orbitAngle * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(
                Mathf.Cos(angleRadians) * _currentRadius,
                0f,
                Mathf.Sin(angleRadians) * _currentRadius
            );

            transform.position = player.position + offset;
        }
    }
}