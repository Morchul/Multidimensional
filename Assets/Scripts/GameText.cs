using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameText : MonoBehaviour
{
    private TextMeshPro text;

    [SerializeField]
    private float xOffset;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.enabled = false;
        gameController = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameController.GetCameraXPosition() > transform.position.x + xOffset)
        {
            text.enabled = true;
        }
    }
}
