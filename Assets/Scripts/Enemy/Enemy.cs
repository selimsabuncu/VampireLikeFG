using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected float movementSpeed = 5f;
        [SerializeField] protected float health = 100f;

        public virtual float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public virtual float Health { get => health; set => health = value; }

        public virtual void Start()
        {
            Debug.Log(gameObject.name);
        }

        public virtual void Update()
        {
            Move();
        }

        public virtual void Move()
        {
            Vector3 direction = PlayerController.Instance.transform.position - transform.position;
            transform.Translate(direction.normalized * (MovementSpeed * Time.deltaTime), Space.World);
            Debug.Log("Tried movement");
        }
    }
}
