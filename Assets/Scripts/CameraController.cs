using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private UFO player;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Transform targetLine;
    private Vector2 targeLinePos;

    [SerializeField]
    private Camera playerCam;
    public Camera PlayerCam => playerCam;

    public Vector2 Size { get; private set; }
    public Vector2 LowerLeftEdge => (Vector2)transform.position - Size / 2;

    public bool Freeze;

    void Start()
    {
        Size = GetCameraSize();
        Freeze = false;
        player = GameController.Instance.UFO;
        targeLinePos = targetLine.position;
    }

    void Update()
    {
        if (Freeze) return;

        Vector3 forwardMovement = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        transform.Translate(forwardMovement);

        if (!IsCircleInView(player.transform.position, player.Radius))
        {
            player.transform.Translate(forwardMovement);
        }

        if (IsPointInView(targeLinePos))
        {
            Freeze = true;
        }
    }

    public bool IsCircleInView(Vector2 pos, float radius)
    {
        Vector2 relativPos = pos - LowerLeftEdge;
        return !(relativPos.x - radius < 0 || relativPos.x + radius > Size.x || relativPos.y - radius < 0 || relativPos.y + radius > Size.y);
    }

    public bool IsPointInView(Vector2 point)
    {
        Vector2 relativPos = point - LowerLeftEdge;
        return !(relativPos.x < 0 || relativPos.x > Size.x || relativPos.y < 0 || relativPos.y > Size.y);
    }

    public void Reset(Vector3 startPosition)
    {
        Vector3 startPos = new Vector3(startPosition.x, startPosition.y, -10);
        transform.position = startPos;
        Freeze = false;
    }

    private Vector2 GetCameraSize()
    {
        float height = 2 * playerCam.orthographicSize;
        float width = height * playerCam.aspect;
        return new Vector2(width, height);
    }
}
