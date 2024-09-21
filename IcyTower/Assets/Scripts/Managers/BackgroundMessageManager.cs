using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackgroundMessageManager : MonoBehaviour
{
    [Serializable]
    public class BackgroundMessage
    {
        public string message;
        public int combo;
    }

    [SerializeField] private List<BackgroundMessage> messageList= new List<BackgroundMessage>();
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private Transform textTransform;
    [SerializeField] private IScoreManager scoreManager;
    [SerializeField] private Camera gameCamera;

    private LinkedList<BackgroundMessage> messagesLinkedList = new LinkedList<BackgroundMessage>();
    private LinkedListNode<BackgroundMessage> currentMessage;
    private bool isShowMessage = false;
    private int combo;

    private void Awake()
    {
        foreach (BackgroundMessage message in messageList)
        {
            messagesLinkedList.AddLast(message);
        }

        currentMessage = messagesLinkedList.First;
    }

    private void Start()
    {
        scoreManager.ComboChanged += ScoreManager_ComboChanged;
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
            currentMessage = messagesLinkedList.First;
        }
        else if (currentMessage?.Value?.combo <= combo)
        {   
            showMessage(currentMessage.Value);
            currentMessage = currentMessage.Next;
        }
    }

    private void showMessage(BackgroundMessage message)
    {
        Debug.Log($"message: {message.message}");

        if (!isShowMessage)
        {
            isShowMessage = true;
            float messageRelativeCamera = -2;

            textTransform.gameObject.SetActive(true);
            textMesh.text = message.message;
            textTransform.position = gameCamera.ScreenToWorldPoint(new Vector3(0, 0, 10)) + (Vector3.up * messageRelativeCamera);

            StartCoroutine(moveMessageUp(messageRelativeCamera));
        }
    }

    private IEnumerator moveMessageUp(float reletiveCamera)
    {
        float yCameraPos;
        do
        {
            yCameraPos = gameCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 10)).y;

            reletiveCamera += 0.05f;

            textTransform.position = gameCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 10)) + (Vector3.up * reletiveCamera);


            yield return new WaitForSeconds(0.01f);
        }
        while (textTransform.position.y < yCameraPos + 1f);

        textTransform.gameObject.SetActive(false);
        isShowMessage = false;
    }

    
}
