using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////
// Copyrights: https://github.com/Ideefixze/Softbodies/tree/master
//////////////////////////////////////////////////////////////////

public class Softbody : MonoBehaviour
{
    public float bounciness = 1f;
    public float stiffness = 1f;
    public float force = 1f;

    private Mesh originalMesh;
    private Vector3[] originalVertices;
    private Vector3[] deformedVertices;
    private SoftVertex[] softVertices;
    private int verticesCount;

    // Start is called before the first frame update
    void Start()
    {
        originalMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = originalMesh.vertices;
        verticesCount = originalVertices.Length;
        softVertices = new SoftVertex[verticesCount];
        deformedVertices = new Vector3[verticesCount];

        for (int i = 0; i < verticesCount; i++)
        {
            softVertices[i] = new SoftVertex(i, originalVertices[i]);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateSoftbody();
    }

    private void UpdateSoftbody()
    {
        for (int i = 0; i < verticesCount; i++)
        {
            softVertices[i].UpdateVertex();
            softVertices[i].UpdateVelocity(bounciness);
            softVertices[i].Settle(stiffness);
            deformedVertices[i] = softVertices[i].GetVertexPosition();
        }

        originalMesh.vertices = deformedVertices;
        originalMesh.RecalculateBounds();
        originalMesh.RecalculateNormals();
        originalMesh.RecalculateTangents();
    }

    public void ApplyPressure(Vector3 position, float pressure)
    {
        for (int i = 0; i < verticesCount; i++)
        {
            softVertices[i].ApplyPressure(transform, position, pressure);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        ContactPoint[] contactPoints = new ContactPoint[other.contactCount];
        other.GetContacts(contactPoints);
        foreach (var cp in contactPoints)
        {
            ApplyPressure(cp.point, other.relativeVelocity.magnitude * force);
        }
    }
}

