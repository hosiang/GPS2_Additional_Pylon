using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public Transform shipTransform;
    [SerializeField] Position shipPosition;
    private HighScore highScore;

    private void Awake()
    {
        LoadHighScore();
    }

    public void SavePosition()
    {
        SetPosition();
        SaveDataManager.SaveData(shipPosition, "Testing");
    }

    public void LoadPosition()
    {
        shipPosition = (Position)SaveDataManager.LoadDataGetObject("Testing");
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

    #region ! HightScore !
    public void CompareAndSaveHighScore(float fastestTime)
    {
        if(fastestTime > highScore.FastestTime)
        {
            highScore.SetNewFastestTime(fastestTime);
            SaveDataManager.SaveData(highScore, Global.saveFile_HighScore);
        }
    }
    private void LoadHighScore()
    {
        highScore = (HighScore)SaveDataManager.LoadDataGetObject(Global.saveFile_HighScore);
    }
    #endregion

}

[System.Serializable] class Position
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

[System.Serializable] class Rotation
{
    private float x = 0.0f;
    private float y = 0.0f;
    private float z = 0.0f;
    private float w = 0.0f;

    public float X { get { return x; } }
    public float Y { get { return y; } }
    public float Z { get { return z; } }
    public float W { get { return w; } }

    public void SetQuaternion(Quaternion rotation)
    {
        
        x = rotation.x;
        y = rotation.y;
        z = rotation.z;
        w = rotation.w;
    }
}

[System.Serializable] class Scale
{
    private float x = 0.0f;
    private float y = 0.0f;
    private float z = 0.0f;

    public float X { get { return x; } }
    public float Y { get { return y; } }
    public float Z { get { return z; } }

    public void SetVector3(Vector3 Scale)
    {

        x = Scale.x;
        y = Scale.y;
        z = Scale.z;
    }
}

[System.Serializable] class HighScore
{
    private float fastestTime = 0.0f;

    public float FastestTime { get { return fastestTime; } }

    public void SetNewFastestTime(float fasterTime)
    {
        fastestTime = fasterTime;
    }
}
