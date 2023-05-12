using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Custom editor class for the EasyVolumetric.
/// </summary>
[CustomEditor(typeof(VolumetricProbe))]
public class VolumetricProbeEditor : Editor, IRotationReceiver
{

    private static Color[] islandColors = {Color.green, Color.yellow, Color.red, Color.blue, Color.magenta, Color.cyan, Color.white};

    private SerializedProperty islands;

    private SerializedProperty lightSource;
    private SerializedProperty overrideTransform;
    private SerializedProperty overrideIntensity;
    private SerializedProperty overrideColor;
    private SerializedProperty isStatic;

    private SerializedProperty usePhysics;
    private SerializedProperty rayDist;
    private SerializedProperty layerMask;

    private SerializedProperty material;
    private SerializedProperty useLightColor;
    private SerializedProperty useLightIntensity;
    private SerializedProperty lightIntensityMultiplier;
    private SerializedProperty lightIntensityClamp;
    private SerializedProperty startColor;
    private SerializedProperty endColor;
    private SerializedProperty particles;

    private VolumetricProbe script;
    private VolumetricProbe.IslandTypes islandType = VolumetricProbe.IslandTypes.Duplicate;
    private int islandToRotate = -1;
    private float islandScale = 1;
    private Mesh islandMesh;
    private MeshFilter islandMeshFilter;
    private bool editMode;
    private bool islandsFoldOut;
    private List<bool> islandFoldOut;

    private void OnEnable()
    {
        islands = serializedObject.FindProperty("islands");

        lightSource = serializedObject.FindProperty("lightSource");
        overrideTransform = serializedObject.FindProperty("overrideTransform");
        overrideIntensity = serializedObject.FindProperty("overrideIntensity");
        overrideColor = serializedObject.FindProperty("overrideColor");
        isStatic = serializedObject.FindProperty("isStatic");

        usePhysics = serializedObject.FindProperty("usePhysics");
        rayDist = serializedObject.FindProperty("rayDist");
        layerMask = serializedObject.FindProperty("layerMask");

        material = serializedObject.FindProperty("material");
        useLightColor = serializedObject.FindProperty("useLightColor");
        useLightIntensity = serializedObject.FindProperty("useLightIntensity");
        lightIntensityMultiplier = serializedObject.FindProperty("lightIntensityMultiplier");
        lightIntensityClamp = serializedObject.FindProperty("lightIntensityClamp");
        startColor = serializedObject.FindProperty("startColor");
        endColor = serializedObject.FindProperty("endColor");
        particles = serializedObject.FindProperty("particles");

        islandFoldOut = new List<bool>();
        for (int i = 0; i < islands.arraySize; i++)
            islandFoldOut.Add(true);

        script = (VolumetricProbe)target;
    }

    private void OnDisable()
    {
        Tools.hidden = false;
    }

    private Color GetIslandColor(int index)
    {
        while (index >= islandColors.Length)
        {
            index -= islandColors.Length;
        }

        return islandColors[index];
    }

    /// <inheritdoc/>
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //foldoutstyle
        GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldout);
        foldoutStyle.fontStyle = FontStyle.Bold;

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUI.indentLevel++;
        islandsFoldOut = EditorGUILayout.Foldout(islandsFoldOut, "Islands", foldoutStyle);
        EditorGUI.indentLevel--;

        if (islandsFoldOut)
        {
            float oldWidth;
            for (int i = 0; i < islands.arraySize; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.BeginHorizontal();
                EditorGUI.indentLevel++;
                islandFoldOut[i] = EditorGUILayout.Foldout(islandFoldOut[i], "Island " + i, foldoutStyle);
                EditorGUI.DrawRect(EditorGUILayout.GetControlRect(GUILayout.MaxWidth(18)), GetIslandColor(i));
                if (GUILayout.Button("Delete island"))
                {
                    islands.DeleteArrayElementAtIndex(i);
                    break;
                }
                if (GUILayout.Button("Rotate"))
                {
                    islandToRotate = i;
                    RotationDialogue.Init(this);
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.EndHorizontal();

                oldWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 80;
                if (islandFoldOut[i])
                {
                    EditorGUILayout.Space();
                    EditorGUI.indentLevel++;
                    int arraySize = islands.GetArrayElementAtIndex(i).FindPropertyRelative("vertices").arraySize;
                    for (int j = 0; j < arraySize; j++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PropertyField(islands.GetArrayElementAtIndex(i).FindPropertyRelative("vertices").GetArrayElementAtIndex(j), new GUIContent("Vertex " + j));
                        GUI.enabled = j > 0;
                        if (GUILayout.Button("▲", GUILayout.MaxWidth(20)))
                        {
                            script.Islands[i].Swap(j, j - 1);
                        }
                        GUI.enabled = j < arraySize - 1;
                        if (GUILayout.Button("▼", GUILayout.MaxWidth(20)))
                        {
                            script.Islands[i].Swap(j, j + 1);
                        }
                        GUI.enabled = true;
                        if (GUILayout.Button("X", GUILayout.MaxWidth(32)))
                        {
                            islands.GetArrayElementAtIndex(i).FindPropertyRelative("vertices").DeleteArrayElementAtIndex(j);
                            break;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    if (GUILayout.Button("Add vertex"))
                    {
                        islands.GetArrayElementAtIndex(i).FindPropertyRelative("vertices").InsertArrayElementAtIndex(islands.GetArrayElementAtIndex(i).FindPropertyRelative("vertices").arraySize);
                    }
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndVertical();
                EditorGUIUtility.labelWidth = oldWidth;
            }

            oldWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 80;
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add island", GUILayout.MaxWidth(180)))
            {
                AddIsland();
            }
            islandType = (VolumetricProbe.IslandTypes)EditorGUILayout.EnumPopup("", islandType, GUILayout.MaxWidth(80));
            int type = (int)islandType;
            if (type > 1)
            {
                if (type < 4)
                {
                    islandScale = EditorGUILayout.FloatField("Scale:", islandScale);
                }
                else if (type == 4)
                {
                    islandMesh = (Mesh)EditorGUILayout.ObjectField("Mesh:", islandMesh, typeof(Mesh), false);
                }
                else
                {
                    islandMeshFilter = (MeshFilter)EditorGUILayout.ObjectField("MeshFilter:", islandMeshFilter, typeof(MeshFilter), true);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = oldWidth;
        }
        EditorGUILayout.EndVertical();

        editMode = EditorGUILayout.Toggle("Edit mode", editMode);
        if (!islandsFoldOut && editMode)
            EditorGUILayout.HelpBox("Expand Islands to edit vertices", MessageType.Warning);
        if (editMode)
            EditorGUILayout.HelpBox("Hold shift to move islands", MessageType.Info);

        EditorGUILayout.PropertyField(lightSource);
        EditorGUILayout.PropertyField(overrideTransform);
        if (overrideTransform.objectReferenceValue != null)
        {
            if (lightSource.objectReferenceValue != null)
                EditorGUILayout.HelpBox("The lightSource have more priority over the point!", MessageType.Info);

            EditorGUILayout.PropertyField(overrideIntensity);
            EditorGUILayout.PropertyField(overrideColor);
        }
        EditorGUILayout.PropertyField(isStatic);

        EditorGUILayout.PropertyField(usePhysics);
        EditorGUILayout.PropertyField(rayDist);
        EditorGUILayout.PropertyField(layerMask);

        EditorGUILayout.PropertyField(material);
        EditorGUILayout.PropertyField(useLightColor);
        EditorGUILayout.PropertyField(useLightIntensity);
        if (useLightIntensity.boolValue)
        {
            EditorGUILayout.PropertyField(lightIntensityMultiplier);
            EditorGUILayout.PropertyField(lightIntensityClamp);
        }
        EditorGUILayout.PropertyField(startColor);
        EditorGUILayout.PropertyField(endColor);

        EditorGUILayout.PropertyField(particles);
        if (particles.objectReferenceValue)
        {
            Component p_component = particles.objectReferenceValue as Component;
            if (p_component && !p_component.gameObject.scene.IsValid())
            {
                EditorGUILayout.HelpBox("Selected particles object is not present in the scene. So it's prefab and will be instantiated on start.", MessageType.Info);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        Tools.hidden = editMode;

        if (Application.isPlaying)
            return;

        if (!islandsFoldOut)
            return;

        SceneView.RepaintAll();

        for (int i = 0; i < script.Islands.Length; i++)
        {
            if (!islandFoldOut[i])
                continue;

            VolumetricProbe.Island island = script.Islands[i];
            Handles.color = GetIslandColor(i);
            for (int j = 0; j < island.vertices.Length; j++)
            {
                Vector3 worldPos = script.transform.TransformPoint(island.vertices[j]);
                float size = HandleUtility.GetHandleSize(worldPos) * 0.05f;
                Handles.DrawWireDisc(worldPos, Vector3.up, size);
                Handles.DrawWireDisc(worldPos, Vector3.right, size);
                Handles.DrawWireDisc(worldPos, Vector3.forward, size);

                if (j < island.vertices.Length - 1)
                    Handles.DrawLine(worldPos, script.transform.TransformPoint(island.vertices[j + 1]));
                else Handles.DrawLine(worldPos, script.transform.TransformPoint(island.vertices[0]));
            }
        }

        if (!editMode)
            return;

        EditorGUI.BeginChangeCheck();

        List<Vector3> deltas = new List<Vector3>();

        GUIStyle style = new GUIStyle();
        if (Event.current.shift)
        {
            for (int i = 0; i < script.Islands.Length; i++)
            {
                if (!islandFoldOut[i])
                    continue;

                VolumetricProbe.Island island = script.Islands[i];
                Vector3 delta = Vector3.zero;
                delta = island.vertices[0] - script.transform.InverseTransformPoint(Handles.PositionHandle(script.transform.TransformPoint(island.vertices[0]), Quaternion.identity));
                for (int j = 0; j < island.vertices.Length; j++)
                {
                    deltas.Add(delta);
                }
            }
        }
        else
        {
            for (int i = 0; i < script.Islands.Length; i++)
            {
                if (!islandFoldOut[i])
                    continue;

                VolumetricProbe.Island island = script.Islands[i];
                style.normal.textColor = GetIslandColor(i);
                for (int j = 0; j < island.vertices.Length; j++)
                {
                    Vector3 wPos = script.transform.TransformPoint(island.vertices[j]);
                    Handles.Label(wPos + Camera.current.transform.right * 0.1f, j.ToString(), style);

                    deltas.Add(island.vertices[j] - script.transform.InverseTransformPoint(Handles.PositionHandle(script.transform.TransformPoint(island.vertices[j]), Quaternion.identity)));
                }
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            int v = 0;
            for (int i = 0; i < script.Islands.Length; i++)
            {
                VolumetricProbe.Island island = script.Islands[i];
                Undo.RecordObject(script, "Changed vertex pos");
                for (int j = 0; j < island.vertices.Length; j++)
                {
                    island.vertices[j] = island.vertices[j] - deltas[v];
                    v++;
                }
            }
            script.UpdateMesh();
        }
    }

    private void AddIsland()
    {
        List<VolumetricProbe.Island> list = new List<VolumetricProbe.Island>(script.Islands);
        VolumetricProbe.Island isl;
        switch (islandType)
        {
            case VolumetricProbe.IslandTypes.Duplicate:
                islands.InsertArrayElementAtIndex(islands.arraySize);
                break;

            case VolumetricProbe.IslandTypes.Mesh:
                if (!islandMesh)
                    return;

                Undo.RecordObject(script, "Added new island");
                isl = new VolumetricProbe.Island(islandMesh);
                list.Add(isl);
                script.Islands = list.ToArray();
                break;

            case VolumetricProbe.IslandTypes.MeshFilter:
                if (!islandMeshFilter)
                    return;

                Undo.RecordObject(script, "Added new island");
                isl = new VolumetricProbe.Island(islandMeshFilter);

                for(int i = 0; i < isl.vertices.Length; i++)
                {
                    isl.vertices[i] = script.transform.InverseTransformPoint(isl.vertices[i]);
                }

                list.Add(isl);
                script.Islands = list.ToArray();
                break;

            default:
                Undo.RecordObject(script, "Added new island");
                isl = new VolumetricProbe.Island(VolumetricProbe.Island.IslandDefinitions[(int)islandType]);
                isl.Scale(islandScale);
                list.Add(isl);
                script.Islands = list.ToArray();
                break;
        }
        islandFoldOut.Add(true);

        script.UpdateMesh();
    }

    /// <summary>
    /// Rotate island by Vector3 angles. Not recommended to use this because it solely used with RotationDialogue.
    /// </summary>
    public void ReceiveRotation(Vector3 rotation)
    {
        Undo.RecordObject(script, "Rotate island");
        script.RotateIsland(islandToRotate, rotation);
    }

}