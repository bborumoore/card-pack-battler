using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListContainer : ObjectContainerList<Sprite> {
    [SerializeField] List<Sprite> startItems = new List<Sprite>();
    [SerializeField] bool disposeEmpty = false;
    
    private void Start() {
        CreateSlots(startItems);
    }
    
    public override bool IsReadOnly() => !disposeEmpty;

    public override bool CanDrop(Draggable draggable, Slot slot) {
        return slot != null && slot.Unlocked;
    }
    
}
