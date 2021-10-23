using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HexGrid
{
    [CustomPropertyDrawer(typeof(HexCoordinates))]
    public class HexCoordinatesDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            HexCoordinates coord = new HexCoordinates(
                property.FindPropertyRelative("_x").intValue,
                property.FindPropertyRelative("_z").intValue);
            EditorGUI.LabelField(position, label.text, coord.ToString());
        }
    }
}
