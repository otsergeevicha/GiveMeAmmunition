using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor window that let's user to enter rotation Vector3.
/// </summary>
public class RotationDialogue : EditorWindow
{

    private IRotationReceiver receiver;
    private Vector3 rotation;

    public static void Init(IRotationReceiver receiver)
    {
        if (receiver == null)
            throw new System.Exception("Receiver is null");

        RotationDialogue window = new RotationDialogue();
        window.receiver = receiver;
        window.ShowUtility();
        window.maxSize = new Vector2(350, 120);
    }

    private void OnGUI()
    {
        rotation = EditorGUILayout.Vector3Field("Rotation: ", rotation);
        EditorGUILayout.HelpBox("In world space", MessageType.Info);
        if (GUILayout.Button("Rotate"))
        {
            receiver.ReceiveRotation(rotation);
            Close();
        }
    }

}