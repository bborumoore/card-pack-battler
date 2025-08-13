using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour {
    public Image img;
    public Color normal = Color.white;
    public Color can = Color.white;
    public Color cannot = new Color(1,0,0,0.5f);
    void Reset(){ img = GetComponent<Image>();}
    void Awake(){ if(!img) img = GetComponent<Image>(); if(img) img.color = normal; }
    public void EnterCan() { if(img) img.color = can; }
    public void EnterCannot() { if(img) img.color = cannot; }
    public void Exit() { if(img) img.color = normal; }
    public void Slotted() { /* play sound */ }
}
