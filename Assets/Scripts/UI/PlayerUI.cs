using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public DialogUI dialog;
    public DialogUI modal;
    private Session session;
    public Conversation[] conversations;
    public int conversationIndex;
    public int sentenceIndex;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(Button);
        session = GetComponent<Session>();
    }

    public IEnumerator ShowMessage(string message, Sprite face)
    {
        modal.gameObject.SetActive(true);
        modal.text.text = message;
        modal.image.sprite = face;
        yield return new WaitForSeconds(2);
        modal.gameObject.SetActive(false);        
    }

    public void ShowConversation() {
        dialog.gameObject.SetActive(true);
        var sentences = conversations[conversationIndex].sentences;
        var sentence = sentences[sentenceIndex];
        SetDialogue(sentence.character, sentence.text);
    }

    public void ShowNextConversation()
    {
        conversationIndex += 1;
        sentenceIndex = 0;
        ShowConversation();
    }

    public void SetDialogue(CharacterData chr, string text)
    {
        dialog.image.sprite = chr.dialogImage;
        dialog.text.text = text;
    }
    
    public void Button()
    {     
        sentenceIndex++;
        
        if (sentenceIndex >= conversations[conversationIndex].sentences.Count)
        {
            sentenceIndex = 0;
            dialog.gameObject.SetActive(false);
            return;
        }
        
        ShowConversation();
    }
}
