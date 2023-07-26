using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HandDemo : MonoBehaviour
{
    public GameObject player;
    public List<Transform> lerpPositions;
    public Transform startPosition;
    public Transform endPosition;
    public float totalDistance = 5.0f;
    public float speed = 1.0f;
    public float restTotalTime = 1.0f;

    private bool distanceOut;
    private bool fadeOut;
    private float fadeOutSpeed = 2.0f;
    private float originalAlpha = 0.0f;
    private float timer = 0.0f;
    private int positionIndex = 0;
    private float travelTime = 1.0f;
    private float restTimer = 0.0f;

    SkinnedMeshRenderer[] skinned;
    MeshRenderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        var euler = startPosition.parent.rotation.eulerAngles;
        var rotate = Quaternion.Euler(euler.x, -euler.y, euler.z);
        transform.rotation = rotate;

        skinned = GetComponentsInChildren<SkinnedMeshRenderer>();
        renderers = GetComponentsInChildren<MeshRenderer>();
        originalAlpha = skinned[0].material.color.a;
        fadeOut = false;
        distanceOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speed;
        float t = timer / travelTime;

        if (totalDistance >= Vector3.Distance(transform.position, player.transform.position))
        {
            distanceOut = true;
            foreach (var item in skinned)
            {
                if (Vector3.Distance(transform.position, player.transform.position) > 0.0f)
                {
                    item.material.color = ChangeAlpha(item.material.color, item.material.color.a - (Time.deltaTime * fadeOutSpeed * 2));
                }
            }
            foreach (var item in renderers)
            {
                if (Vector3.Distance(transform.position, player.transform.position) > 0.0f)
                {
                    item.material.color = ChangeAlpha(item.material.color, item.material.color.a - (Time.deltaTime * fadeOutSpeed * 2));
                }
            }
        }
        else
        {
            distanceOut = false;
            foreach (var item in skinned)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < originalAlpha)
                {
                    item.material.color = ChangeAlpha(item.material.color, item.material.color.a + (Time.deltaTime * fadeOutSpeed * 2));
                }
            }
            foreach (var item in renderers)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < originalAlpha)
                {
                    item.material.color = ChangeAlpha(item.material.color, item.material.color.a + (Time.deltaTime * fadeOutSpeed * 2));
                }
            }
        }

        if (lerpPositions.Count <= 2 && timer < 1)
        {
            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, t);
        }
        if (lerpPositions.Count > 2)
        {
            DoLerp();
        }
        if (lerpPositions.Count <= 2 && timer > 1) 
        {
            restTimer += Time.deltaTime;

            fadeOut = true;

            if (restTimer >= restTotalTime)
            {
                foreach (var item in skinned)
                {
                    if (distanceOut)
                    {
                        item.material.color = ChangeAlpha(item.material.color, item.material.color.a);
                    }
                    else
                    {
                        item.material.color = ChangeAlpha(item.material.color, originalAlpha);
                    }
                }
                foreach (var item in renderers)
                {
                    if (distanceOut)
                    {
                        item.material.color = ChangeAlpha(item.material.color, item.material.color.a);
                    }
                    else
                    {
                        item.material.color = ChangeAlpha(item.material.color, originalAlpha);
                    }
                }

                fadeOut = false;
                restTimer = 0.0f;
                timer = 0.0f;
            }
        }

        if (fadeOut)
        {
            foreach (var item in skinned)
            {
                item.material.color = ChangeAlpha(item.material.color, item.material.color.a - (Time.deltaTime * fadeOutSpeed));
            }
            foreach (var item in renderers)
            {
                item.material.color = ChangeAlpha(item.material.color, item.material.color.a - (Time.deltaTime * fadeOutSpeed));
            }
        }
    }

    public Color ChangeAlpha(Color currentColor, float alpha)
    {
        Color tempColor;
        tempColor = currentColor;
        tempColor.a = alpha;
        currentColor = tempColor;
        return currentColor;
    }

    public void DoLerp()
    {
        if (timer > travelTime)
        {
            timer = 0.0f;
            positionIndex += 1;

            if (positionIndex >= lerpPositions.Count)
            {
                positionIndex = 0;
            }
        }

        float t = timer / travelTime;

        Vector3 p0, p1;
        int p0_index, p1_index;

        p1_index = positionIndex;
        p0_index = (p1_index == 0) ? lerpPositions.Count - 1 : p1_index - 1;

        p0 = lerpPositions[p0_index].position;
        p1 = lerpPositions[p1_index].position;

        transform.position = Vector3.Lerp(p0, p1, t);
    }
}
