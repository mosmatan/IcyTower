using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages and displays background messages based on player combos.
/// </summary>
public class BackgroundMessageManager : MonoBehaviour
{
    [Serializable]
    public class BackgroundMessage
    {
        public string message; // The message to display.
        public int combo; // Combo threshold for the message.
    }

    [SerializeField] private List<BackgroundMessage> messageList = new List<BackgroundMessage>(); // List of messages.
    [SerializeField] private TextMeshPro textMesh; 
    [SerializeField] private Transform textTransform; 
    [SerializeField] private IScoreManager scoreManager; // Reference to the score manager.
    [SerializeField] private Camera gameCamera; // Reference to the game camera.

    private LinkedList<BackgroundMessage> messagesLinkedList = new LinkedList<BackgroundMessage>(); 
    private LinkedListNode<BackgroundMessage> currentMessage; 
    private bool isShowMessage = false; // Is a message currently being shown?
    private int combo; // Current combo value.

    private void Awake()
    {
        foreach (BackgroundMessage message in messageList)
        {
            messagesLinkedList.AddLast(message); // Populate the linked list with messages.
        }

        currentMessage = messagesLinkedList.First; // Set the first message as current.
    }

    private void Start()
    {
        scoreManager.ComboChanged += ScoreManager_ComboChanged; // Subscribe to combo changes.
    }

    private void ScoreManager_ComboChanged(int comb)
    {
        combo = comb; 
        showMessages(); 
    }

    private void showMessages()
    {
        if (combo == 0)
        {
            currentMessage = messagesLinkedList.First; // Reset to first message if combo is zero.
        }
        else if (currentMessage?.Value?.combo <= combo)
        {
            showMessage(currentMessage.Value); // Show current message if combo threshold is met.
            currentMessage = currentMessage.Next; 
        }
    }

    private void showMessage(BackgroundMessage message)
    {
        if (!isShowMessage)
        {
            isShowMessage = true; 
            float messageRelativeCamera = -2; // Initial vertical offset.

            textTransform.gameObject.SetActive(true); 
            textMesh.text = message.message; 
            textTransform.position = gameCamera.ScreenToWorldPoint(new Vector3(0, 0, 10)) + (Vector3.up * messageRelativeCamera); // Position the message.

            StartCoroutine(moveMessageUp(messageRelativeCamera)); // Start moving the message up.
        }
    }

    private IEnumerator moveMessageUp(float relativeCamera)
    {
        float yCameraPos;
        do
        {
            yCameraPos = gameCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 10)).y; 

            relativeCamera += 0.05f; // Increment the vertical offset.

            textTransform.position = gameCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 10)) + (Vector3.up * relativeCamera); // Update position.

            yield return new WaitForSeconds(0.01f);
        }
        while (textTransform.position.y < yCameraPos + 1f); // Continue until the message is above the camera view.

        textTransform.gameObject.SetActive(false); // Deactivate the message text.
        isShowMessage = false; 
    }
}
