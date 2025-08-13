using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteDraggable : Draggable {
    [SerializeField] private Image icon;

    public override void UpdateObject() {
        var s = obj as Sprite;
        if (icon) {
            icon.sprite = s;
            icon.enabled = s != null;
        }
    }
}
