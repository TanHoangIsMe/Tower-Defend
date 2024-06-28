using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateTextController : MonoBehaviour
{
    TextMeshPro coordLable;
    Vector2Int coordinate = new Vector2Int();

    private void Awake()
    {
        coordLable = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinate();
            UpdateObjectName();
        }    
    }

    private void DisplayCoordinate()
    {
        coordinate.x = (int)Math.Round(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinate.y = (int)Math.Round(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        coordLable.text = coordinate.x + "," + coordinate.y;
    }

    private void UpdateObjectName()
    {
        transform.parent.name = $"({coordinate.x},{coordinate.y})";
    }
}
