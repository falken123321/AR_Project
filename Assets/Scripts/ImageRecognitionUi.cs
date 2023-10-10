using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageRecognitionUi : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public TMP_Text  recognitionText;
    private int Counter;

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

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