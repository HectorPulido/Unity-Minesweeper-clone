using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    Vector2Int id;
    Action<int, int> onClick;
    bool _mine;
    bool _showed;

    public bool showed { set { _showed = value; } get { return _showed; } }
    public bool mine { get { return _mine; } }
    public string text { set { tm.text = value; } }
    public Sprite sprite { set { sr.sprite = value; } }

    TextMesh tm;
    SpriteRenderer sr;

    public void Init(Vector2Int id, bool mine, 
        Action<int, int> onClick)
    {
        tm = GetComponentInChildren<TextMesh>();
        sr = GetComponent<SpriteRenderer>();

        this.id = id;
        this._mine = mine;
        this.onClick = onClick;
        this.showed = false;
        this.text = "";
    }
    
	void OnMouseDown ()
    {
        onClick(id.x, id.y);
    }
}
