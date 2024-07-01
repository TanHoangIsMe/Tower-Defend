using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class CoordinateTextController : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TextMeshPro coordLable;
    Vector2Int coordinate = new Vector2Int();
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponentInParent<Waypoint>();
        coordLable = GetComponent<TextMeshPro>();
        coordLable.enabled = false;
        DisplayCoordinate();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinate();
            UpdateObjectName();
        }
       UpdateColor();
       ControlLable();
    }

    private void ControlLable()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            coordLable.enabled = !coordLable.IsActive();
        }
    }

    private void UpdateColor()
    {
        if (waypoint.IsPlaceable == true)
        { 
            coordLable.color = defaultColor;
        }
        else
        {
            coordLable.color = blockedColor;
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
