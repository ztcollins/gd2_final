using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    

    #region References
        [SerializeField] TextMeshProUGUI dialogueBox;
    #endregion
    

    public void SetDialogue(string dialogue)
    {
        dialogueBox.text = dialogue;
    }


}
