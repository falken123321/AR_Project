using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageRecognitionUi : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public TextMeshProUGUI  recognitionText;

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
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateRecognitionText(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateRecognitionText(trackedImage);
        }
    }

    void UpdateRecognitionText(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            recognitionText.text = "Recognized: " + trackedImage.referenceImage.name;
        }
    }
}