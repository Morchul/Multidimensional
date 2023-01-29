using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLine : MonoBehaviour
{
    private Color targetLineColor = new Color(233 / 255f, 168 / 255f, 32 / 255f, 0.5f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.Instance.Win();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = targetLineColor;
        Gizmos.DrawCube(transform.position, new Vector3(1, 10, 1));
        Gizmos.color = Color.white;
    }
}
