using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAnimCoroutines : MonoBehaviour
{
    public IEnumerator animPos(Vector3 destination, float lerpDuration)
    {
        float timeElapsed = 0;
        var initPos = transform.localPosition;

        while (timeElapsed < lerpDuration)
        {
            initPos = Vector3.Lerp(initPos, destination, timeElapsed / lerpDuration);
            transform.localPosition = initPos;

            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator animScaleAndPos(Vector3 finalScale, Vector3 finalPos, float lerpDuration)
    {
        float timeElapsed = 0;
        var initScale = transform.localScale;
        var initPos = transform.localPosition;

        while (timeElapsed < lerpDuration)
        {
            initScale = Vector3.Lerp(initScale, finalScale, timeElapsed / lerpDuration);
            transform.localScale = initScale;

            initPos = Vector3.Lerp(initPos, finalPos, timeElapsed / lerpDuration);
            transform.localPosition = initPos;

            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator MoveSlideDelay(Vector3 finalScale, Vector3 finalPos, float lerpDuration, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(animScaleAndPos(finalScale, finalPos, lerpDuration));
    }
}
