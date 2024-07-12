using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PropertyHandler : MonoBehaviour
{
    public Boolean caldronActionActive = false;
    public Boolean pathCompleted = false;
    public string directionSelected = "NONE";
    public Boolean directionSelectorActive = false;
    public Boolean selectionWasActive = true;
    public Vector3[] path;
    public string madePotion = "NONE";
}
