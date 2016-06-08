using UnityEngine;
using System.Collections;

public class LifeTime : MonoBehaviour
{
    public float lifeTime;
    public MeshRenderer meshRenderer;

    float blinkRate = 0.2f;
    float destroyTime;
    float blinkTime = 2;

    void Start()
    {
        destroyTime = Time.time + lifeTime;
        StartCoroutine("Blink");
    }

    void Update()
    {
        if (Time.time >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(lifeTime - blinkTime);

        while (true)
        {
            meshRenderer.enabled = false;
            yield return new WaitForSeconds(blinkRate);
            meshRenderer.enabled = true;
            yield return new WaitForSeconds(blinkRate);
        }
    }
}
