using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    

    #region References
        [SerializeField] TextMeshProUGUI dialogueBox;
    #endregion
    

    public void SetDialogue(string dialogue)
    {
        gameObject.SetActive(true);
        dialogueBox.text = dialogue;
    }


}
