﻿using UnityEngine;

namespace NetWare;

public static class Render
{
    public static void DrawLine(Color color, Vector3 origin, Vector3 destination)
    {
        // initialize material
        var material = CreateMaterial();

        // draw line
        GL.PushMatrix();
        material.SetPass(0);
        GL.LoadPixelMatrix(0, Screen.width, Screen.height, 0);
        GL.Begin(1);
        GL.Color(color);

        GL.Vertex(new Vector3(origin.x, origin.y));
        GL.Vertex(new Vector3(destination.x, destination.y));

        GL.End();
        GL.PopMatrix();
    }

    public static void DrawCircle(Color color, Vector2 position, float radius, int sides = 16)
    {
        float angleIncrement = (6.28318548f / sides);

        // draw circle
        for (float angle = 0; angle < 6.28318548f; angle += angleIncrement)
            DrawLine(
                color,
                new Vector3(
                    ((Mathf.Cos(angle) * radius) + position.x),
                    ((Mathf.Sin(angle) * radius) + position.y)
                ),
                new Vector3(
                    ((Mathf.Cos(angle + angleIncrement) * radius) + position.x),
                    ((Mathf.Sin(angle + angleIncrement) * radius) + position.y)
                )
            );
    }

    public static void DrawBox(Color color, Vector2 position, float width, float height)
    {
        // initialize material
        var material = CreateMaterial();

        // fixed values
        width /= 2;
        height /= 2;

        // draw line
        GL.PushMatrix();
        material.SetPass(0);
        GL.LoadPixelMatrix(0, Screen.width, Screen.height, 0);
        GL.Begin(7);
        GL.Color(color);

        GL.TexCoord2(position.x + width, position.y + height);
        GL.Vertex(new Vector3(position.x + width, position.y + height));

        GL.TexCoord2(position.x + width, position.y - height);
        GL.Vertex(new Vector3(position.x + width, position.y - height));

        GL.TexCoord2(position.x - width, position.y - height);
        GL.Vertex(new Vector3(position.x - width, position.y - height));

        GL.TexCoord2(position.x - width, position.y + height);
        GL.Vertex(new Vector3(position.x - width, position.y + height));

        GL.End();
        GL.PopMatrix();
    }

    // internal methods and variables
    public static Vector3 screenCenter = new Vector3((Screen.width / 2), (Screen.height / 2));
    public static Vector3 screenCenterBottom = new Vector3((Screen.width / 2), Screen.height);

    private static Material CreateMaterial()
    {
        var material = new Material(Shader.Find("Hidden/Internal-Colored"))
        {
            hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy
        };
        material.SetInt("_SrcBlend", 5);
        material.SetInt("_DstBlend", 10);
        material.SetInt("_Cull", 0);
        material.SetInt("_ZTest", 8);
        material.SetInt("_ZWrite", 0);
        material.SetColor("_Color", Color.white);

        return material;
    }
}
