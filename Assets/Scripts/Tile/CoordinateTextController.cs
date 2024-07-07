using System;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateTextController : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TextMeshPro coordLable;
    Vector2Int coordinate = new Vector2Int();

    Tile tile;

    private void Awake()
    {
        tile = GetComponentInParent<Tile>();
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
        if (!tile.IsPlaceacle)
        {
            coordLable.color = blockedColor;
        }
        else
        {
            coordLable.color = defaultColor;
        }
    }

    private void DisplayCoordinate()
    {
        coordinate.x = (int)Math.Round(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinate.y = (int)Math.Round(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.y);
        coordLable.text = coordinate.x + "," + coordinate.y;
    }

    private void UpdateObjectName()
    {
        transform.parent.name = $"({coordinate.x},{coordinate.y})";
    }
}
