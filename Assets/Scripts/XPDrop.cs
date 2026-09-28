using System;
using Player;
using UnityEngine;

public class XPDrop : MonoBehaviour
{
    public int xpValue;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(xpValue);
        if (other.CompareTag("Player"))
        {
            Debug.Log("HEYO");
            PlayerController.Instance.CollectedXP(xpValue);
        }
    }
}
