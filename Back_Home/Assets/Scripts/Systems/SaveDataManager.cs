using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveDataManager
{
    private static object dataObject = null;

    public static void SaveData(object saveData, string path)
    {
        FileStream fileStream = null;

        if (!Directory.Exists(Application.persistentDataPath + "/data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/data");
        }

        try
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            fileStream = File.Create(Application.persistentDataPath + path);

            binaryFormatter.Serialize(fileStream, saveData);

            Debug.Log("Save Data Path( " + Application.persistentDataPath + path + " ) achieved !");

        }
        catch (Exception exception)
        {
            if (exception != null)
            {
                Debug.LogError("#Important ! Save Data Path( " + Application.persistentDataPath + path + " ) failed - Exception Massage : " + exception.Message);
            }

        }
        finally
        {
            if (fileStream != null)
            {
                fileStream.Close();
            }

        }

    }

    /// <summary>
    /// Summary: Checks whether this collider is touching any colliders on the specified layerMask or not.
    /// </summary>
    private static void LoadData(string path)
    {
        FileStream fileStream = null;

        if (File.Exists(Application.persistentDataPath + path))
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                fileStream = File.Open(Application.persistentDataPath + path, FileMode.Open);

                dataObject = binaryFormatter.Deserialize(fileStream) as object;

                Debug.Log("Load Data Path( " + Application.persistentDataPath + path + " ) achieved !");

            }
            catch (Exception exception)
            {
                if(exception != null)
                {
                    Debug.LogError("#Important ! Load Data Path( " + Application.persistentDataPath + path + " ) failed - Exception Massage : " + exception.Message);
                }

            }
            finally
            {
                if(fileStream != null)
                {
                    fileStream.Close();
                }

            }

        }
        else
        {

            dataObject = null;
            Debug.LogWarning("#Warning ! Empty File Path( " + Application.persistentDataPath + path + " ) - Load data failed!");

        }

    }

    public static object LoadDataGetObject(string path)
    {
        LoadData(path);

        if (dataObject != null)
        {
            object tempObject = dataObject;
            dataObject = null;
            return tempObject;
        }
        else
        {
            return dataObject;
        }
        
    }

    public static void DeleteData(string path)
    {
        if (File.Exists(Application.persistentDataPath + path))
        {
            try
            {
                File.Delete(Application.persistentDataPath + path);
                Debug.Log("Delete Data Path( " + Application.persistentDataPath + path + " ) achieved !");
            }
            catch (Exception exception)
            {
                if (exception != null)
                {
                    Debug.LogError("#Important ! Delete Data Path( " + Application.persistentDataPath + path + " ) failed - Exception Massage : " + exception.Message);
                }
            }
        }
        else
        {
            Debug.LogWarning("#Warning ! Empty File Path( " + Application.persistentDataPath + path + " ) - Delete data failed!");

        }
    }

}