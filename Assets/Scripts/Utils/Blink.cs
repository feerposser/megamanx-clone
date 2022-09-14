using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField] private Material blink;
    private Material originalMaterial;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalMaterial = sprite.material;
    }

    public void PlayBlink(float blinkTime=0.03f, int blinkSequences=1)
    {
        StartCoroutine("ExecuteBlink", new object[2] { blinkTime, blinkSequences });
    }

    private IEnumerator ExecuteBlink(object[] blinkParams)
    {
        float blinkTime = (float) blinkParams[0];
        int blinkSequences = (int) blinkParams[1];

        for (int i=0; i<blinkSequences; i++)
        {
            sprite.material = blink;
            yield return new WaitForSeconds(blinkTime);
            sprite.material = originalMaterial;
            yield return new WaitForSeconds(blinkTime);
        }
    }
}
