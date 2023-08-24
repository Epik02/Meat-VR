using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    // This is so you can create multiple lists of points the guideline should follow
    [System.Serializable] 
    public class GameObjectList
    {
        public List<GameObject> points = new List<GameObject>();
    }

    public Guidelines guide;
    public List<GameObjectList> points = new List<GameObjectList>();

    private LineRenderer line;
    private int pointIndex = 0;

    // Checks if there is a next guideline to cut and if so increase the index
    public bool NextGuideline() 
    {
        pointIndex++;
        if (pointIndex >= points.Count) 
        {
            pointIndex = points.Count - 1;
            return false;
        }
        return true;
    }

    // Generates new invisible hitboxes
    public void GenerateHitboxes()
    {
        GameObjectList pts = points[pointIndex];
        line.positionCount = pts.points.Count;
        for (int i = 0; i < pts.points.Count; i++)
        {
            line.SetPosition(i, pts.points[i].transform.position);
        }

        for (int i = 0; i < pts.points.Count - 1; i++)
        {
            // Cut path checker hitbox
            GameObject pathChecker = new GameObject();
            pathChecker.name = "PathCheckerObject";
            pathChecker.AddComponent<CuttingPath>();
            pathChecker.AddComponent<BoxCollider>();
            pathChecker.GetComponent<BoxCollider>().isTrigger = true;
            pathChecker.GetComponent<BoxCollider>().size = new Vector3(line.startWidth / 4f, line.startWidth / 4f, (pts.points[i].transform.position - pts.points[i + 1].transform.position).magnitude);
            pathChecker.transform.position = (pts.points[i].transform.position + pts.points[i + 1].transform.position) / 2f;
            pathChecker.transform.LookAt(pts.points[i].transform.position);

            // Low hitbox
            GameObject startHitbox = new GameObject();
            startHitbox.name = "LowHitboxObject";
            startHitbox.AddComponent<CuttingSurface>();
            startHitbox.AddComponent<BoxCollider>();
            startHitbox.GetComponent<CuttingSurface>().accuracy = CuttingSurface.KnifeAccuracy.BAD;
            startHitbox.GetComponent<CuttingSurface>().guide = this.guide;
            startHitbox.GetComponent<BoxCollider>().isTrigger = true;
            startHitbox.GetComponent<BoxCollider>().size = new Vector3(line.startWidth, line.startWidth, (pts.points[i].transform.position - pts.points[i + 1].transform.position).magnitude);
            startHitbox.transform.position = (pts.points[i].transform.position + pts.points[i + 1].transform.position) / 2f;
            startHitbox.transform.LookAt(pts.points[i].transform.position);

            // High hitbox
            GameObject highHitbox = Instantiate(startHitbox);
            highHitbox.name = "HighHitboxObject";
            highHitbox.GetComponent<BoxCollider>().size = new Vector3(line.startWidth / 2f, line.startWidth / 2f, (pts.points[i].transform.position - pts.points[i + 1].transform.position).magnitude);
            highHitbox.GetComponent<CuttingSurface>().accuracy = CuttingSurface.KnifeAccuracy.GOOD;

            pathChecker.transform.parent = pts.points[i].transform;
            startHitbox.transform.parent = pts.points[i].transform;
            highHitbox.transform.parent = pts.points[i].transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        GenerateHitboxes();
    }

    // Update is called once per frame
    void Update() 
    {
        line.positionCount = points[pointIndex].points.Count;
        for (int i = 0; i < points[pointIndex].points.Count; i++)
        {
            line.SetPosition(i, points[pointIndex].points[i].transform.position);
        }
    }
}
