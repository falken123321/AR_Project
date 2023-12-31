using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using CardEums; // Add this line to include the CardEums namespace

public class ImageRecognitionUi : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public TextMeshProUGUI recognitionText;
    private int Counter;
    //Reffrence to boardScript
    private Board boardScript;
    public ARSession arSession;

    private void Start()
    {
        boardScript = GameObject.FindGameObjectWithTag("GOD").GetComponent<Board>();
    }

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
            //UpdateRecognitionText(trackedImage); //TODO: Fjern inden push - Skal regognizion text ikk bare fjernes
            
            //Adskil reffrence name og lav carded ud af det
            var imageName = trackedImage.referenceImage.name;
            var imageNameCpy = imageName.Split('_');

            

            Suits suit = (Suits)Enum.Parse(typeof(Suits), imageNameCpy[1], ignoreCase: true);
            
            CardType type = (CardType)Enum.Parse(typeof(CardType), imageNameCpy[0], ignoreCase: true);
            // Register the new card in the codebase
            Card c = new Card(type, suit);
            boardScript.player.RegisterCard(c);
            boardScript.updateInstructions();
        }
        
        foreach (var trackedImage in eventArgs.updated)
        {
            Debug.Log("Image updated: " + trackedImage.referenceImage.name);
        }
        
        
    }

    void updateRecognitionText(ARTrackedImage trackedImage)
    {
            recognitionText.text = "Recognized: " + trackedImage.referenceImage.name;
            Debug.Log("Text updated to: " + recognitionText.text);
        
    }

    public void Reset()
    {
       arSession.Reset();
    }

}