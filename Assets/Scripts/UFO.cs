using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;

    private GameController gameController;

    private UFOSound sound;

    private Animator animator;

    [SerializeField]
    private UFOExplosion explosionPrefab;

    [Header("Scan")]
    private float scanDuration;
    private float scanTimer;

    private bool scanActive;

    private float scanCooldownTimer;
    public float ScanCooldownTimer => scanCooldownTimer;
    public float ScanCooldown { get; private set; }

    [SerializeField]
    private Scanner scanner;

    [SerializeField]
    private Scanner.ScanProperty[] scanProperties;

    [Header("Shoot")]
    [SerializeField]
    private float shootCooldown;
    private float shootCooldownTimer;

    [SerializeField]
    private Laser projectilePrefab;

    private SpriteRenderer spriteRenderer;

    private bool freeze;
    public bool Freeze
    {
        get => freeze;
        set
        {
            freeze = value;
            if (freeze)
            {
                sound.StopAllSounds();
            }
        }
    }

    public float Radius { get; private set; }

    void Start()
    {
        sound = GetComponent<UFOSound>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Radius = transform.localScale.x / 2;
        gameController = GameController.Instance;
        ResetPlayer();
    }

    void Update()
    {
        if (Freeze) return;

        Movement();
        Scan();
        Shoot();
    }

    public void ResetPlayer()
    {
        Freeze = false;
        transform.position = gameController.StartPos;
        StopScan();
        scanCooldownTimer = 0;
        shootCooldownTimer = 0;
    }

    public void GameOver()
    {
        animator.SetBool("Destroyed", true);
        animator.enabled = false;
        spriteRenderer.sprite = null;
        Freeze = true;
        sound.PlayDestroyedSound();
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    private void Movement()
    {
        Vector2 translation = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            translation += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            translation += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            translation += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            translation += Vector2.right;
        }

        animator.SetFloat("HorizontalMovement", translation.x);
        animator.SetFloat("VerticalMovement", translation.y);

        if(translation != Vector2.zero)
        {
            sound.StartPlayMoveSound();
        }
        else
        {
            sound.StopPlayMoveSound();
        }

        Vector2 finalPosition = (Vector2)transform.position + translation * Time.deltaTime * moveSpeed;
        if(gameController.IsCircleInView(finalPosition, Radius))
        {
            transform.position = finalPosition;
        }
    }

    private void Scan()
    {
        if (scanCooldownTimer > 0)
            scanCooldownTimer -= Time.deltaTime;

        else if (Input.GetKeyDown(KeyCode.Q))
            StartScan(scanProperties[0]);
        else if (Input.GetKeyDown(KeyCode.E))
            StartScan(scanProperties[1]);

        if (scanActive)
        {
            if (scanTimer < scanDuration)
                scanTimer += Time.deltaTime;
            else
                StopScan();
        }
    }

    private void Shoot()
    {
        if (shootCooldownTimer > 0)
            shootCooldownTimer -= Time.deltaTime;
        else if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = gameController.GetMouseWorldPos();

            Vector2 dirNorm = (mousePos - (Vector2)transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(dirNorm.x, dirNorm.y, 0));
            Vector2 spawnPos = (Vector2)transform.position + dirNorm * 0.5f;

            Instantiate(projectilePrefab, new Vector3(spawnPos.x, spawnPos.y, 0), rotation);

            shootCooldownTimer = shootCooldown;
            sound.PlayShootSound();
        }
    }

    private void StartScan(Scanner.ScanProperty scanProperty)
    {
        scanner.gameObject.SetActive(true);
        scanner.SetScanProperty(scanProperty, transform.position);

        scanActive = true;
        scanTimer = 0;
        scanDuration = scanProperty.ScanDuration;

        scanCooldownTimer = scanProperty.ScanCooldown;
        ScanCooldown = scanProperty.ScanCooldown;
        sound.StartPlayScanSound();
    }

    private void StopScan()
    {
        scanner.gameObject.SetActive(false);

        scanActive = false;
        scanTimer = 0;

        sound.StopPlayScanSound();
    }
}
