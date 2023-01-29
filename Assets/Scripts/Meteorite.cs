using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{

    private HighScoreCollectable resource;

    [SerializeField]
    private int health;

    [SerializeField]
    private bool moveOnStart;

    [SerializeField]
    private bool moving;
    private bool isMoving;

    [SerializeField]
    private Vector2 direction;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private SecondDimensionInterference sDInterferencePrefab;

    [SerializeField]
    private AnimationClip destroyAnimationClip;

    private Animator destroyAnimation;

    private bool destroyed;

    private GameController gameController;

    private void Start()
    {
        destroyAnimation = GetComponent<Animator>();

        resource = GetComponentInParent<HighScoreCollectable>();
        direction = direction.normalized;

        if (moving)
            gameController = GameController.Instance;
    }

    public void StartMoving()
    {
        if (sDInterferencePrefab != null)
        {
            SecondDimensionInterference sCI = Instantiate(sDInterferencePrefab, transform.position, Quaternion.identity, this.transform);
            sCI.Loop();
        }

        isMoving = true;
    }

    private void Update()
    {
        if (destroyed) return;

        if (isMoving)
        {
            Vector3 translation = direction * Time.deltaTime * moveSpeed;
            if(resource != null)
            {
                resource.transform.Translate(translation, Space.World);
            }
            else
            {
                transform.Translate(translation, Space.World);
            }
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            if (moveOnStart)
            {
                if (gameController.GetCameraXPosition() > transform.position.x + 20)
                {
                    SetDestroyed(true);
                }
            }
            else
            {
                if (!gameController.IsPointInView(new Vector2(transform.position.x, 0)))
                {
                    SetDestroyed(true);
                }
            }
        }
        else if (moving)
        {
            if (moveOnStart || gameController.IsPointInView(new Vector2(transform.position.x, 0)))
            {
                StartMoving();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with: " + collision.name);

        if (destroyed) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Dead");
            GameController.Instance.GameOver();
        }
        else if (collision.gameObject.CompareTag("Laser"))
        {
            Destroy(collision.gameObject);
            if(--health == 0)
            {
                SetDestroyed();
            }
        }
        else if (isMoving && collision.gameObject.CompareTag("Wall"))
        {
            SetDestroyed();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(direction.x, direction.y, 0));
    }

    private void SetDestroyed(bool destroyResourceTo = false)
    {
        Debug.Log("Set Destroyed: ");
        destroyAnimation.Play(destroyAnimationClip.name);
        Destroy(this.gameObject, 1.2f);
        destroyed = true;
        //disable collider

        if (sDInterferencePrefab != null)
        {
            Instantiate(sDInterferencePrefab, transform.position, Quaternion.identity);
        }

        if (resource != null)
        {
            if (destroyResourceTo)
                Destroy(resource.gameObject);
            else
                resource.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        }
    }
}
