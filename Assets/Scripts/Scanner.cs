using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Scanner : MonoBehaviour
{
    private SpriteMask scanField;

    private SpriteRenderer spriteRenderer;

    private Light2D scanLight;

    private void Awake()
    {
        scanField = GetComponent<SpriteMask>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        scanLight = GetComponent<Light2D>();
    }

    private FieldInfo _LightCookieSprite = typeof(Light2D).GetField("m_LightCookieSprite", BindingFlags.NonPublic | BindingFlags.Instance);

    void UpdateCookieSprite(Sprite sprite)
    {
        _LightCookieSprite.SetValue(scanLight, sprite);
    }

    public void SetScanProperty(ScanProperty scanProperty, Vector3 parentPos)
    {
        scanField.sprite = scanProperty.Sprite;
        spriteRenderer.sprite = scanProperty.Sprite;
        UpdateCookieSprite(scanProperty.Sprite);
        scanField.transform.position = parentPos + new Vector3(scanProperty.pos.x, scanProperty.pos.y, 0);
        scanField.transform.localScale = new Vector3(scanProperty.size.x, scanProperty.size.y, 0);
    }

    [System.Serializable]
    public class ScanProperty
    {
        public Sprite Sprite;
        public float ScanCooldown;
        public float ScanDuration;
        public Vector2 pos;
        public Vector2 size;
    }
}
