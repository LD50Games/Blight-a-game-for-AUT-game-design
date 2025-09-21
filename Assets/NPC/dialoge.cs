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
    [Header("F to use the function, E to end Dialoge")]
    [Header("O creates a option the first two after O determine text")]
    [Header("and the next two determine next dialouge cluster")]
    [Header("additonal oporation properties")]
    public GameObject player;
    public Animator animator;
    int DialogeNumber;
    int DialogeClusterNumber;
    public TextMeshProUGUI text;
    public UnityEvent function;
    bool InOptionMode = false;

    public void interact()
    {
        player.SendMessage("BeStill");
        DialogeNumber = -1;
        text.enabled = true;
        next();
    }
    public void next()
    {
        if (InOptionMode == false)
        {
            DialogeNumber += 1;
            Update_Dialoge();
        }
        else
        {
            DialogeClusterNumber = int.Parse(listOfLists[DialogeClusterNumber].strings[DialogeNumber + 4]);
            DialogeNumber = 0;
            Update_Dialoge();
            InOptionMode = false;
        }
    }
    public void Update_Dialoge()
    {
        text.text = listOfLists[DialogeClusterNumber].strings[DialogeNumber];
        if (listOfLists[DialogeClusterNumber].strings[DialogeNumber] == "E")
        {
            end();
        }
        if (listOfLists[DialogeClusterNumber].strings[DialogeNumber] == "X")
        {
            DialogeClusterNumber = int.Parse(listOfLists[DialogeClusterNumber].strings[DialogeNumber + 1]);
            end();
        }
        if (listOfLists[DialogeClusterNumber].strings[DialogeNumber] == "F")
        {
            function.Invoke();
            next();
        }
        if (listOfLists[DialogeClusterNumber].strings[DialogeNumber] == "O")
        {
            option();
        }
    }
    public void option()
    {
        InOptionMode = true;
        text.text = " Q: " + listOfLists[DialogeClusterNumber].strings[DialogeNumber + 1] + " E: " + listOfLists[DialogeClusterNumber].strings[DialogeNumber + 2];
    }
    public void end()
    {
        DialogeNumber = 0;
        player.SendMessage("EnableMovement");
        text.enabled = false;
    }
    public void qOption()
    {
        if (InOptionMode) 
        {
            DialogeClusterNumber = int.Parse(listOfLists[DialogeClusterNumber].strings[DialogeNumber + 3]);
            DialogeNumber = 0;
            Update_Dialoge();
            InOptionMode = false;
        }
    }
}
