using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// TODO: handle multiple scene and multiple save.

/// <summary>
/// Consider only game objects with "Editable" tag.
/// Save name, position and rotation for each game objects
/// those data are store in a JSON File
/// </summary>
public class SavingManager : MonoBehaviour {

    private GameObject[] editableGameObjects;

    private static readonly string EDITABLE_TAG = "Editable";
    private static string SAVING_DIRECTORY = "/Saves";
    private static string SAVING_FILE = "test.save";

    void Awake()
    {
        SAVING_DIRECTORY = Application.dataPath + SAVING_DIRECTORY;
        editableGameObjects = GameObject.FindGameObjectsWithTag(EDITABLE_TAG);
    }

    /// <summary>
    /// load games objects data and place them on the scene.
    /// </summary>
    /// <returns>boolean : true if savingID exist false elsewhere</returns>
    public bool LoadGameObjects(string savingID)
    {
        string savingPath = SAVING_DIRECTORY + '/' + savingID;

        if (File.Exists(savingPath))
        {
            string data = File.ReadAllText(savingPath);
            SavedScene savedScene = JsonUtility.FromJson(data, typeof(SavedScene)) as SavedScene;

            PlaceGameObjects(savedScene);
            return true;
        }

        return false;
    }
    private void PlaceGameObjects(SavedScene savedScene)
    {
        foreach (SavedGameObject savedGameObject in savedScene.savedGameObjects)
        {
            GameObject gameObject = GameObject.Find(savedGameObject.title);
            gameObject.transform.position = savedGameObject.position;
            gameObject.transform.rotation = savedGameObject.rotation;
        }
    }

    /// <summary>
    /// Save each editable game objects informations.
    /// </summary>
    public void SaveGameObjects()
    {
        SavedScene savedScene = new SavedScene(SceneManager.GetActiveScene().name, editableGameObjects);
        CreateSavingDirectoryIfNotExist(savedScene.name);

        String saveID = GenerateFreeSaveID();
        File.WriteAllText(SAVING_DIRECTORY + "/" + saveID + ".save", JsonUtility.ToJson(savedScene));

        String[] directoriesName = GetDirectoriesName(SAVING_DIRECTORY);
        foreach (string directoryName in directoriesName)
        {
            String[] filesName = GetFilesName(SAVING_DIRECTORY + "/" + directoryName);
            foreach(string fileName in filesName)
            {
                Debug.Log(directoryName + " " + fileName);
            }
        }
    }

    /// <summary>
    /// Create current scene saving directory if not exist
    /// </summary>
    /// <param name="directory">Saving directory for current scene</param>
    private void CreateSavingDirectoryIfNotExist(string directory)
    {
        if (!Directory.Exists(SAVING_DIRECTORY))
            Directory.CreateDirectory(SAVING_DIRECTORY);
        if (!Directory.Exists(SAVING_DIRECTORY + '/' + directory))
            Directory.CreateDirectory(SAVING_DIRECTORY + '/' + directory);
    }
    
    private String GenerateFreeSaveID()
    {
        List<String> usedSaveIdentifiers = GetUsedSaveIDs();
        String saveIdentifier;

        do
        {
            saveIdentifier = GenerateRandomSaveID();
        }
        while (usedSaveIdentifiers.Contains(saveIdentifier));

        return saveIdentifier;

    }
    private List<String> GetUsedSaveIDs()
    {
        List<String> usedSaveIdentifiers = new List<string>();

        String[] directoriesName = GetDirectoriesName(SAVING_DIRECTORY);
        foreach (string directoryName in directoriesName)
        {
            String[] filesName = GetFilesName(SAVING_DIRECTORY + "/" + directoryName);
            foreach (string fileName in filesName)
            {
                usedSaveIdentifiers.Add(fileName);
            }
        }

        return usedSaveIdentifiers;
    }
    private String GenerateRandomSaveID()
    {
        String saveIdentifier = "";

        for (int i = 0; i < 3; i++)
            saveIdentifier += (char) UnityEngine.Random.Range('A', 'Z');

        return saveIdentifier;
    }

    private String[] GetDirectoriesName(string path)
    {
        String[] directories = Directory.GetDirectories(path);

        for (int i = 0; i < directories.Length; i++)
            directories[i] = directories[i].Split('\\')[1];

        return directories;
    }
    private String[] GetFilesName(string path)
    {
        String[] files = Directory.GetFiles(path, "*.save");

        for (int i = 0; i < files.Length; i++)
            files[i] = files[i].Split('\\')[1].Split('.')[0];

        return files;
    }
}


[Serializable]
public class SavedScene
{
    public string name;
    public List<SavedGameObject> savedGameObjects;

    public SavedScene(string name, GameObject[] gameObjectToSave)
    {
        this.name = name;

        savedGameObjects = new List<SavedGameObject>();
        foreach (GameObject editableGameObject in gameObjectToSave)
        {
            savedGameObjects.Add(new SavedGameObject(editableGameObject.name, editableGameObject.transform));
        }
    }
}

[Serializable]
public class SavedGameObject
{
    public string title;
    public Vector3 position;
    public Quaternion rotation;

    public SavedGameObject(string title, Transform transform)
    {
        this.title = title;
        this.rotation = transform.rotation;
        this.position = transform.position;
    }
}

