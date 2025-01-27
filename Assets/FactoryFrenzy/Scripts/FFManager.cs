using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class FFManager : MonoBehaviour
{
    [System.Serializable]
    public struct POIPrefabMapping
    {
        public string Type; // Type name as specified in the JSON file
        public GameObject Prefab; // Corresponding prefab for this type
    }

    public string ConfigFileName = "SceneData.json"; // Name of the JSON file
    public List<POIPrefabMapping> POIPrefabs; // List of prefab mappings for different POI types

    private Dictionary<string, GameObject> _prefabDictionary;
    private string _filePath;

    private void Start()
    {
        // Construct the file path
        _filePath = Path.Combine(Application.streamingAssetsPath, ConfigFileName);

        // Initialize the prefab dictionary for quick lookup
        _prefabDictionary = new Dictionary<string, GameObject>();
        foreach (var mapping in POIPrefabs)
        {
            if (!_prefabDictionary.ContainsKey(mapping.Type))
            {
                _prefabDictionary[mapping.Type] = mapping.Prefab;
            }
        }

        // Load POIs into the scene
        LoadPointsOfInterest();
    }

    private void LoadPointsOfInterest()
    {
        // Check if the file exists
        if (!File.Exists(_filePath))
        {
            Debug.LogError($"Configuration file not found at {_filePath}");
            return;
        }

        // Read and deserialize the JSON file
        using (StreamReader file = File.OpenText(_filePath))
        {
            JsonSerializer serializer = new JsonSerializer();
            List<PointOfInterestStruct> poiList = (List<PointOfInterestStruct>)serializer.Deserialize(file, typeof(List<PointOfInterestStruct>));

            if (poiList == null || poiList.Count == 0)
            {
                Debug.LogWarning("No Points of Interest found in the JSON file.");
                return;
            }

            // Instantiate each POI in the scene
            foreach (var poiData in poiList)
            {
                InstantiatePointOfInterest(poiData);
            }
        }
    }

    private void InstantiatePointOfInterest(PointOfInterestStruct poiData)
    {
        // Check if the prefab for this type exists
        if (_prefabDictionary.TryGetValue(poiData.Type, out GameObject prefab))
        {
            // Instantiate the prefab at the specified position
            Vector3 position = new Vector3(poiData.Position.x, 1.0f, poiData.Position.y); // Adjust height as needed
            var poiInstance = Instantiate(prefab, position, Quaternion.identity);

            // Optionally, assign the data to the POI script if needed
            var poiScript = poiInstance.GetComponent<PointOfInterest>();
            if (poiScript != null)
            {
                poiScript.PointOfInterestData = poiData;
            }

            Debug.Log($"Loaded POI: ID={poiData.Id}, Type={poiData.Type}, Title={poiData.Title}");
        }
        else
        {
            Debug.LogError($"No prefab found for POI type: {poiData.Type}");
        }
    }
}
