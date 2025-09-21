using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class dialoge : MonoBehaviour

{

[Serializable]
public class DialogeClusters
{
    public List<string> strings = new List<string>();
}
    
    public List<DialogeClusters> listOfLists = new List<DialogeClusters>();
    [Header("The dialoge system also comes with editable functions")]
    [Header("O to present options, E to end Dialoge")]
    [Header("F to use the function")]
    [Header("additonal oporation properties")]
    public GameObject player;
    public Animator animator;
    int DialogeNumber;
    public TextMeshProUGUI text;
    public UnityEvent function;

    public void interact()
    {
        player.SendMessage("BeStill");
        DialogeNumber = -1;
        text.enabled = true;
        next();
    }
    public void next()
    {
        DialogeNumber += 1;
        Update_Dialoge();
    }
    public void Update_Dialoge()
    {
        text.text = listOfLists[0].strings[DialogeNumber];
        if (listOfLists[0].strings[DialogeNumber] == "E")
        {
            end();
        }
        if (listOfLists[0].strings[DialogeNumber] == "F")
        {
            function.Invoke();
            next();
        }
    }
    public void new_dialoge()
    {
        DialogeNumber = -1;
        
    }
    public void end()
    {
        player.SendMessage("EnableMovement");
        text.enabled = false;
    }
}
