using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreCollectable : MonoBehaviour
{

    [SerializeField]
    private int highScorePoints;
    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collected) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collected = true;
            GameController.Instance.IncreaseHighScore(highScorePoints);
            GetComponent<SpriteRenderer>().sprite = null;
            Destroy(this.gameObject, 1);
        }
    }
}
