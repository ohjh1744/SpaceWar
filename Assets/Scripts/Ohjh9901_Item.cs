using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ohjh9901_Item : MonoBehaviour
{
    private int spawnitem;
    private bool onetime;
    private bool term;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onetime && ! term)
        {
            StartCoroutine(SpawnItem());
        }
    }

    IEnumerator SpawnItem()
    {
        term = true;
        spawnitem = Random.Range(0, 100);

        if(spawnitem <= 10)
        {
            spriteRenderer.enabled = true;
            collider.enabled = true;
            onetime = true;
        }

        yield return new WaitForSeconds(10f);

        term = false;
    }
}
