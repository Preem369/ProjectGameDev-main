using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material hitFlashMat;
    [SerializeField] private float timeRestoreDefault = 0.2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;

    }

    public float GetTimeRestoreDefault()
    {
        return timeRestoreDefault;
    }
    public IEnumerator FlashRoutine ()
    {
        spriteRenderer.material = hitFlashMat;
        yield return new WaitForSeconds(timeRestoreDefault);
        spriteRenderer.material = defaultMat;
    }
}
