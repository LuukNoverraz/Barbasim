using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    public Texture2D[] cursors;
    public Texture2D cursor;
    private Vector2 cursorOffset;
    public GameController gameController;
    void Start()
    {
        Vector2 cursorOffset = new Vector2(128, 128);
        Cursor.SetCursor(cursor, cursorOffset, CursorMode.Auto);
    }
}
