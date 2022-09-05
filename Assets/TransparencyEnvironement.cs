using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TransparencyEnvironement : MonoBehaviour
{
    private Renderer rend;
    public static float transparency = 0.2f;

    private void Awake()
    {
        rend = gameObject.GetComponent<Renderer>();
    }
    /*

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Transparent"))
        {
            MakeTransparent();
        }

        if (GUI.Button(new Rect(200, 10, 150, 100), "Opaque"))
        {
            Restore();
        }
    }*/
    public void MakeTransparent()
    {
        foreach(Material mat in rend.materials)
        {
            BlendMode blendMode = (BlendMode)mat.GetFloat("_Mode");
            if(blendMode == BlendMode.Opaque)
            {
                MaterialExtensions.ToFadeMode(mat);
                Color transparentAlbedo = mat.color;
                transparentAlbedo.a = transparency;
                mat.SetColor("_Color", transparentAlbedo);
            }
        }
    }
    public void Restore()
    {
        foreach (Material mat in rend.materials)
        {
            BlendMode blendMode = (BlendMode)mat.GetFloat("_Mode");
            if (blendMode == BlendMode.Opaque)
            {
                MaterialExtensions.ToOpaqueMode(mat);
                Color normalAlbedo = mat.color;
                normalAlbedo.a = 1;
                mat.SetColor("_Color", normalAlbedo);
            }
        }
    }
}
