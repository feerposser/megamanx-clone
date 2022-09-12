using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField] private Material blink;
    private Material originalMaterial;
    [SerializeField] private float blinkTime = 0.1f;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalMaterial = sprite.material;
    }

    public void PlayBlink()
    {
        StartCoroutine("ExecuteBlink");
    }

    private IEnumerator ExecuteBlink()
    {
        sprite.material = blink;
        yield return new WaitForSeconds(0.1f);
        sprite.material = originalMaterial;
    }
}
