using System;
using Player;
using UnityEngine;

public class XPDrop : MonoBehaviour
{
    public float xpValue;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(xpValue);
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.CollectedXP(xpValue);
            ObjectPooling.ObjectPooling.Instance.ReturnToPool("xpDrop", gameObject);
        }
    }
}
