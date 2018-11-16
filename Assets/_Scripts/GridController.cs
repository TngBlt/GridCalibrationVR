﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{

    private Camera sceneCamera;
    private TextMesh positionText;
    private LineRenderer heading;
    private Vector3 standardViewportPoint = new Vector3(0.5f, 0.5f, 10);
    private Vector2 gazePointLeft;
    private Vector2 gazePointRight;
    private Vector2 gazePointCenter;
    public Material shaderMaterial;
    private RaycastHit hit;

    void Start()
    {
        PupilData.calculateMovingAverage = false;
        positionText = gameObject.GetComponent<TextMesh>();
        sceneCamera = gameObject.GetComponent<Camera>();
        heading = gameObject.GetComponent<LineRenderer>();
    }

    void OnEnable()
    {
        if (PupilTools.IsConnected)
        {
            PupilTools.IsGazing = true;
            PupilTools.SubscribeTo("gaze");
        }
    }

    void Update()
    {
        Vector3 viewportPoint = standardViewportPoint;

        if (PupilTools.IsConnected && PupilTools.IsGazing)
        {
            gazePointLeft = PupilData._2D.GetEyePosition(sceneCamera, PupilData.leftEyeID);
            gazePointRight = PupilData._2D.GetEyePosition(sceneCamera, PupilData.rightEyeID);
            gazePointCenter = PupilData._2D.GazePosition;
            viewportPoint = new Vector3(gazePointCenter.x, gazePointCenter.y, 1f);
        }

        if (heading.enabled)
        {
            heading.SetPosition(0, sceneCamera.transform.position - sceneCamera.transform.up);

            Ray ray = sceneCamera.ViewportPointToRay(viewportPoint);
            if (Physics.Raycast(ray, out hit))
            {
                heading.SetPosition(1, hit.point);

                positionText.text = sceneCamera.transform.InverseTransformDirection(hit.point).ToString();
            }
            else
            {
                heading.SetPosition(1, ray.origin + ray.direction * 50f);
            }
        }
    }
    public RaycastHit GetCurrentCollider()
    {
        return hit;
    }

    public Vector2 getCurrentColliderPosition(RaycastHit collider)
    {
        collider.transform.GetComponent<CapsuleCollider>().enabled = false;
        Renderer rend = collider.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = collider.collider as MeshCollider;
        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = collider.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        return pixelUV;
    }
}
