﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLinePostRender : MonoBehaviour
{
    public delegate void LineDrawnHandler(Vector3 begin, Vector3 end, Vector3 depth);
    public event LineDrawnHandler OnLineDrawn;

    bool dragging;
    Vector3 start;
    Vector3 end;
    Camera cam;

    public Material lineMaterial;
    void Start()
    {
        cam = Camera.main;
        dragging = false;
    }

    private void OnEnable()
    {
        Camera.onPostRender += PostRenderDrawLine;
    }

    private void OnDisable()
    {
        Camera.onPostRender -= PostRenderDrawLine;
    }

     

    // Update is called once per frame
    void Update()
    {
        if (!dragging && Input.GetMouseButtonDown(0))
        {
            start = cam.ScreenToViewportPoint(Input.mousePosition);
            dragging = true;
        }

        if (dragging)
        {
            end = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (dragging && Input.GetMouseButtonUp(0))
        {
            // Finished dragging. We draw the line segment
            end = cam.ScreenToViewportPoint(Input.mousePosition);
            dragging = false;

            var startRay = cam.ViewportPointToRay(start);
            var endRay = cam.ViewportPointToRay(end);

            // here is the point to trigger
            // Raise OnLineDrawnEvent
            this.OnLineDrawn?.Invoke(
                startRay.GetPoint(cam.nearClipPlane),
                endRay.GetPoint(cam.nearClipPlane),
                endRay.direction.normalized);
        }   
    }

    void PostRenderDrawLine(Camera cam)
    {
        if (dragging && lineMaterial)
        {
            GL.PushMatrix();
            lineMaterial.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.LINE_STRIP);
            GL.Color(Color.red);
            GL.Vertex(start);
            GL.Vertex(end);
            GL.End();
            GL.PopMatrix();
        }
    }
}
