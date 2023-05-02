using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI_Elements
{


    public class GridOverlay
    {
        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPos, int fontSize,
            Color color, TextAnchor textAnchor, TextAlignment textAlignment = TextAlignment.Center, int sortingOrder = 5000)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPos;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }
    }
}
