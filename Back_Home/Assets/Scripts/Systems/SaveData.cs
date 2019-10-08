using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public Transform shipTransform;
    [SerializeField] ShipPosition shipPosition;

    public void SavePosition()
    {
        SetPosition();
        SaveDataManager.SaveData(shipPosition, "Testing");
    }

    public void LoadPosition()
    {
        shipPosition = (ShipPosition)SaveDataManager.LoadDataGetObject("Testing");
        shipTransform.position = GetPosition();
    }

    private void SetPosition()
    {
        shipPosition.SetVector3(shipTransform.position);
    }

    private Vector3 GetPosition()
    {
        return new Vector3(shipPosition.X, shipPosition.Y, shipPosition.Z);
    }

}

[System.Serializable] class ShipPosition
{
    private float x = 0.0f;
    private float y = 0.0f;
    private float z = 0.0f;

    public float X { get { return x; } }
    public float Y { get { return y; } }
    public float Z { get { return z; } }

    public void SetVector3(Vector3 position)
    {
        x = position.x;
        y = position.y;
        z = position.z;
    }
}