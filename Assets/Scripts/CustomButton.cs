using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    private SceneHandler sceneHandler;
    public SceneHandlerInstruction instruction;
    public string instructionText;
    void Awake()
    {
        sceneHandler = GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandler>();
        AttachOnClick();
    }

    void AttachOnClick()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(delegate {sceneHandler.UseInstruction(instruction, instructionText);});
    }
}
