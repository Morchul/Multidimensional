using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDimension : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Dead");
            GameController.Instance.GameOver();
        }
    }
}
