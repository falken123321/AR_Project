using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{
    // Reference to AR tracked image manager component
    private ARTrackedImageManager _trackedImagesManager;

    // List of prefabs to instantiate - these should be named the same
    // as their corresponding 2D images in the reference image library 
    //public GameObject[] ArPrefabs;
    public TextMeshProUGUI  recognitionText;
    private int Counter;

    // Keep dictionary array of created prefabs
    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        // Cache a reference to the Tracked Image Manager component
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        // Attach event handler when tracked images change
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        // Remove event handler
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // Event Handler
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        Debug.Log("OnTrackedImagesChanged called");

        foreach (var trackedImage in eventArgs.added)
        {
            Counter++;
            Debug.Log("Image added: " + trackedImage.referenceImage.name + "Counter = " + Counter);
            UpdateRecognitionText(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            Debug.Log("Image updated: " + trackedImage.referenceImage.name);
            UpdateRecognitionText(trackedImage);
        }
    }

    void UpdateRecognitionText(ARTrackedImage trackedImage)
    {
        
        recognitionText.text = "Recognized: " + trackedImage.referenceImage.name;
        Debug.Log("Text updated to: " + recognitionText.text);
        
    }
}