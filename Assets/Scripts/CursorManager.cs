using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static CursorManager instance;
    [SerializeField] private Texture2D aimCursor;
    [SerializeField] private Texture2D reloadCursor;
    // Start is called before the first frame update
    private Vector2 cursorHotspot;
    void Start()
    {
        instance = this;
        AimCursor();
    }

    public static CursorManager GetInstance()
    {
        return instance;
    }
    // Update is called once per frame
    public void ReloadCursor()
    {
        cursorHotspot = new Vector2(reloadCursor.width/2, reloadCursor.height/2);
        Cursor.SetCursor(reloadCursor, cursorHotspot, CursorMode.Auto);
    }

    public void AimCursor()
    {
        cursorHotspot = new Vector2(aimCursor.width/2, aimCursor.height/2);
        Cursor.SetCursor(aimCursor, cursorHotspot, CursorMode.Auto);
    }
}
