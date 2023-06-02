using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////////////////////
// Copyrights: https://github.com/Ideefixze/Softbodies/tree/master
//////////////////////////////////////////////////////////////////

public class SoftVertex
{
    public int index;
    private Vector3 initialVertexPosition;
    private Vector3 currentVertexPosition;
    private Vector3 velocity;

    public SoftVertex(int id, Vector3 v)
    {
        index = id;
        initialVertexPosition = v;
        currentVertexPosition = v;
        velocity = new Vector3(0, 0, 0);
    }

    public Vector3 GetVertexPosition()
    {
        return currentVertexPosition;
    }

    public Vector3 CurrentDisplacement()
    {
        return currentVertexPosition - initialVertexPosition;
    }

    public void Settle(float stiffness)
    {
        velocity *= Mathf.Max(1f - stiffness * Time.deltaTime, 0f);
    }

    public void ApplyPressure(Transform transform, Vector3 position, float pressure)
    {
        Vector3 point = currentVertexPosition - transform.InverseTransformPoint(position);
        pressure = pressure / (1f + point.sqrMagnitude * 4f);
        velocity += point.normalized * pressure * Time.deltaTime;
    }

    public void UpdateVelocity(float bounciness)
    {
        velocity = velocity - CurrentDisplacement() * bounciness * Time.deltaTime;
    }

    public void UpdateVertex()
    {
        currentVertexPosition += velocity * Time.deltaTime;
    }
}

