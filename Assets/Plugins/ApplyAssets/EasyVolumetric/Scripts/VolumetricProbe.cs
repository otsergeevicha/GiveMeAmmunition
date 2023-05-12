using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Main class for the EasyVolumetric.
/// </summary>
[ExecuteInEditMode]
public class VolumetricProbe : MonoBehaviour
{

    /// <summary>
    /// Types of islands that showed in the editor.
    /// </summary>
    public enum IslandTypes { Empty, Duplicate, Rect, Circle, Mesh, MeshFilter };

    /// <summary>
    /// This struct contains positions of the vertices of the island.
    /// </summary>
    [System.Serializable]
    public struct Island
    {
        /// <summary>
        /// Position of the points.
        /// </summary>
        public Vector3[] vertices;

        /// <summary>
        /// Create island from the Vector3 array.
        /// </summary>
        public Island(Vector3[] vertices)
        {
            this.vertices = vertices;
        }

        /// <summary>
        /// Create new island identical to the another island.
        /// </summary>
        public Island(Island island)
        {
            vertices = new Vector3[island.vertices.Length];
            for (int i = 0; i < island.vertices.Length; i++)
            {
                vertices[i] = island.vertices[i];
            }
        }

        /// <summary>
        /// Create new island from the mesh vertices.
        /// </summary>
        public Island(Mesh mesh)
        {
            List<Vector3> vcs = new List<Vector3>();
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                if (!vcs.Contains(mesh.vertices[i]))
                    vcs.Add(mesh.vertices[i]);
            }
            vertices = vcs.ToArray();
        }

        /// <summary>
        /// Create new island from the mesh filter using its position.
        /// </summary>
        public Island(MeshFilter meshFilter)
        {
            List<Vector3> vcs = new List<Vector3>();
            for (int i = 0; i < meshFilter.sharedMesh.vertexCount; i++)
            {
                if (!vcs.Contains(meshFilter.sharedMesh.vertices[i]))
                    vcs.Add(meshFilter.sharedMesh.vertices[i]);
            }

            for (int i = 0; i < vcs.Count; i++)
            {
                vcs[i] = meshFilter.transform.TransformPoint(vcs[i]);
            }
            vertices = vcs.ToArray();
        }

        /// <summary>
        /// Swap vertices
        /// </summary>
        public void Swap(int a, int b)
        {
            Vector3 temp = vertices[a];
            vertices[a] = vertices[b];
            vertices[b] = temp;
        }

        /// <summary>
        /// Scale all vertices by scale value.
        /// </summary>
        public void Scale(float scale)
        {
            for (int i = 0; i < vertices.Length; i++)
                vertices[i] *= scale;
        }

        /// <summary>
        /// Shape definitions for the islands.
        /// </summary>
        public static readonly Island[] IslandDefinitions = new Island[4]
        {
            new Island(new Vector3[0]),
            new Island(new Vector3[0]),
            new Island(new Vector3[4]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(1, 0, 1),
                new Vector3(0, 0, 1)
            }),
            new Island(new Vector3[12]
            {
                new Vector3(0.5f, 0, 0),
                new Vector3(0.43f, 0, 0.25f),
                new Vector3(0.25f, 0, 0.43f),
                new Vector3(0, 0, 0.5f),
                new Vector3(-0.25f, 0, 0.43f),
                new Vector3(-0.43f, 0, 0.25f),
                new Vector3(-0.5f, 0, 0),
                new Vector3(-0.43f, 0, -0.25f),
                new Vector3(-0.25f, 0, -0.43f),
                new Vector3(0, 0, -0.5f),
                new Vector3(0.25f, 0, -0.43f),
                new Vector3(0.43f, 0, -0.25f)
            })
        };
    }

    [SerializeField]
    private Island[] islands = new Island[0];
    /// <summary>
    /// Islands of this probe.
    /// </summary>
    public Island[] Islands
    {
        get
        {
            return islands;
        }
        set
        {
            if (islands == value)
                return;

            islands = value;
            UpdateMesh();
        }
    }

    [SerializeField, Header("Options")]
    private Light lightSource = null;
    /// <summary>
    /// Light source used for the calculations.
    /// </summary>
    public Light LightSource
    {
        get
        {
            return lightSource;
        }
        set
        {
            if (lightSource == value)
                return;

            lightSource = value;
            UpdateMesh();
        }
    }

    [SerializeField, Tooltip("You can use transform instead of the LightSource. Remember LightSource has more priority!")]
    private Transform overrideTransform = null;
    /// <summary>
    /// You can use transform instead of the LightSource. Remember LightSource has more priority!
    /// </summary>
    public Transform OverrideTransform
    {
        get
        {
            return overrideTransform;
        }
        set
        {
            if (overrideTransform == value)
                return;

            overrideTransform = value;
            UpdateMesh();
        }
    }

    [SerializeField]
    private float overrideIntensity = 1;
    [SerializeField]
    private Color overrideColor = Color.white;

    /// <summary>
    /// Final intensity based off the LightSource or Point.
    /// </summary>
    public float Intensity => lightSource ? lightSource.intensity : (overrideTransform ? overrideIntensity : 0);
    /// <summary>
    /// Final color based off the LightSource or Point.
    /// </summary>
    public Color Color => lightSource ? lightSource.color : (overrideTransform ? overrideColor : Color.white);
    /// <summary>
    /// Final color based off the LightSource or Point.
    /// </summary>
    public LightType LightType => lightSource ? lightSource.type : LightType.Point;
    /// <summary>
    /// Final transform based off the LightSource or Point.
    /// </summary>
    public Transform FinalTransform => lightSource ? lightSource.transform : overrideTransform;

    [SerializeField, Tooltip("Volumetric probe will be calculated only once and on validation")]
    private bool isStatic = false;
    /// <summary>
    /// Volumetric probe will be calculated only once and on validation.
    /// </summary>
    public bool IsStatic
    {
        get
        {
            return isStatic;
        }
        set
        {
            if (isStatic == value)
                return;

            isStatic = value;
            UpdateMesh();
        }
    }

    [SerializeField, Header("Physics"), Tooltip("Use Raycast to calculate volume")]
    private bool usePhysics = false;
    /// <summary>
    /// Use Raycast to calculate volume.
    /// </summary>
    public bool UsePhysics
    {
        get
        {
            return usePhysics;
        }
        set
        {
            if (usePhysics == value)
                return;

            usePhysics = value;
            UpdateMesh();
        }
    }

    [SerializeField]
    private float rayDist = 3;
    /// <summary>
    /// Distance of the raycasts.
    /// </summary>
    public float RayDist
    {
        get
        {
            return rayDist;
        }
        set
        {
            if (rayDist == value)
                return;

            rayDist = value;
            UpdateMesh();
        }
    }

    [SerializeField]
    private LayerMask layerMask = new LayerMask();
    /// <summary>
    /// LayerMask of the raycasts.
    /// </summary>
    public LayerMask LayerMask
    {
        get
        {
            return layerMask;
        }
        set
        {
            if (layerMask == value)
                return;

            layerMask = value;
            UpdateMesh();
        }
    }

    [SerializeField, Header("Appearence")]
    private Material material = null;
    public Material Material
    {
        get
        {
            return material;
        }
        set
        {
            if (material == value)
                return;

            material = value;
            UpdateMesh();
        }
    }
    [SerializeField, Space]
    private bool useLightColor = true;
    /// <summary>
    /// Use light color while calculating volumetric.
    /// </summary>
    public bool UseLightColor
    {
        get
        {
            return useLightColor;
        }
        set
        {
            if (useLightColor == value)
                return;

            useLightColor = value;
            UpdateMesh();
        }
    }

    [SerializeField]
    private bool useLightIntensity = false;
    /// <summary>
    /// Use light intensity while calculating volumetric.
    /// </summary>
    public bool UseLightIntensity
    {
        get
        {
            return useLightIntensity;
        }
        set
        {
            if (useLightIntensity == value)
                return;

            useLightIntensity = value;
            UpdateMesh();
        }
    }

    [SerializeField]
    private float lightIntensityMultiplier = 1;
    public float LightIntensityMultiplier
    {
        get
        {
            return lightIntensityMultiplier;
        }
        set
        {
            if (lightIntensityMultiplier == value)
                return;

            lightIntensityMultiplier = value;
            UpdateMesh();
        }
    }

    [SerializeField, Tooltip("Clamp the light intensity when calculating the ray color.")]
    private float lightIntensityClamp = 1;
    /// <summary>
    /// Clamp the light intensity when calculating the ray color.
    /// </summary>
    public float LightIntensityClamp
    {
        get
        {
            return lightIntensityClamp;
        }
        set
        {
            if (lightIntensityClamp == value)
                return;

            lightIntensityClamp = value;
            UpdateMesh();
        }
    }

    [SerializeField, Space]
    private Color startColor = Color.white;
    /// <summary>
    /// Start color of the volumetric.
    /// </summary>
    public Color StartColor
    {
        get
        {
            return startColor;
        }
        set
        {
            if (startColor == value)
                return;

            startColor = value;
            UpdateMesh();
        }
    }

    [SerializeField]
    private Color endColor = new Color(1, 1, 1, 0);
    /// <summary>
    /// End color of the volumetric.
    /// </summary>
    public Color EndColor
    {
        get
        {
            return endColor;
        }
        set
        {
            if (endColor == value)
                return;

            endColor = value;
            UpdateMesh();
        }
    }

    [SerializeField, Space]
    private ParticleSystem particles = null;

    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Color oldLightColor;
    private float oldLightIntensity;
    private Vector3 oldLightPos;
    private Vector3 oldLightRot;
    private Vector3 oldPos;
    private Vector3 oldRot;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            //Calculate mesh
            UpdateMesh();

            //if in the play mode and particles is assigned then throw meshrenderer to it
            if (particles)
            {
                //if particles object is prefab we should instantiate it first
                if (!particles.gameObject.scene.IsValid())
                {
                    particles = Instantiate(particles, transform.position, Quaternion.identity);
                    particles.transform.SetParent(transform);
                }

                ParticleSystem.ShapeModule pShape = particles.shape;
                pShape.shapeType = ParticleSystemShapeType.MeshRenderer;
                pShape.meshRenderer = meshRenderer;
                particles.Clear();
                particles.Simulate(particles.main.duration);
                particles.Play();
            }
        }
    }

    //recalculate mesh when component values is changed
    private void OnValidate()
    {
        if (lightIntensityClamp < 0)
            lightIntensityClamp = 0;

        UpdateMesh();
    }

    private void Update()
    {
        //exit if the lightsource or point is not assigned
        if (!lightSource && !overrideTransform)
            return;

        //dont check for changes if isStatic
        if (isStatic)
            return;

        //if the position/rotation of the object or light has changed then recalculate mesh
        if (oldLightPos != FinalTransform.position || oldLightRot != FinalTransform.eulerAngles || oldPos != transform.position || oldRot != transform.eulerAngles || Color != oldLightColor || Intensity != oldLightIntensity)
            UpdateMesh();
    }

    private void CreateMeshComponents()
    {
        //Creating new MeshFilter and MeshRenderer to the object
        mesh = new Mesh();

        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        meshFilter.mesh = mesh;

        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }
    }

    /// <summary>
    /// Update volumetric probe mesh.
    /// </summary>
    public void UpdateMesh()
    {
        //stop if lightsource is not assigned
        if (!lightSource && !overrideTransform)
        {
            Debug.LogError("Light Source is not assigned!");
            return;
        }

        //create necessary components if they are not present in the object
        if (meshFilter == null)
            CreateMeshComponents();

        //assign material
        meshRenderer.material = material;

        //update position/rotation, light color and intensity
        oldLightColor = Color;
        oldLightIntensity = Intensity;
        oldLightPos = FinalTransform.position;
        oldLightRot = FinalTransform.eulerAngles;
        oldPos = transform.position;
        oldRot = transform.eulerAngles;

        //create lists for verices, tris, vertices colors and clear mesh
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Color> vertColor = new List<Color>();
        mesh.Clear();

        //main calculation algorithm
        int addIndex = 0;
        for (int s = 0; s < islands.Length; s++)
        {
            verts.AddRange(islands[s].vertices);
            for (int i = 0; i < islands[s].vertices.Length; i++)
            {
                Color col = useLightColor ? Color * startColor : startColor;
                if (useLightIntensity)
                    col *= Mathf.Clamp(Intensity, 0, lightIntensityClamp);
                vertColor.Add(col);
            }

            if (islands[s].vertices.Length < 2)
                continue;

            if (s > 0)
                addIndex += islands[s - 1].vertices.Length * 2;
            for (int i = 0; i < islands[s].vertices.Length; i++)
            {
                if (i == 0)
                {
                    tris.Add(i + addIndex);
                    tris.Add(islands[s].vertices.Length + 1 + addIndex);
                    tris.Add(islands[s].vertices.Length + addIndex);

                    tris.Add(i + addIndex);
                    tris.Add(i + 1 + addIndex);
                    tris.Add(islands[s].vertices.Length + 1 + addIndex);
                }
                else if (i == islands[s].vertices.Length - 1)
                {
                    tris.Add(i + addIndex);
                    tris.Add(islands[s].vertices.Length + addIndex);
                    tris.Add(islands[s].vertices.Length + i + addIndex);

                    tris.Add(i + addIndex);
                    tris.Add(0 + addIndex);
                    tris.Add(islands[s].vertices.Length + addIndex);
                }
                else
                {
                    tris.Add(i + addIndex);
                    tris.Add(islands[s].vertices.Length + i + 1 + addIndex);
                    tris.Add(islands[s].vertices.Length + i + addIndex);

                    tris.Add(i + addIndex);
                    tris.Add(i + 1 + addIndex);
                    tris.Add(islands[s].vertices.Length + i + 1 + addIndex);
                }

                Vector3 worldPos = transform.TransformPoint(islands[s].vertices[i]);
                Color color = useLightColor ? Color : Color.white;
                if (useLightIntensity)
                    color *= Mathf.Clamp(Intensity, 0, lightIntensityClamp);
                color.a = 0;
                RaycastHit hit;
                Vector3 dir;
                switch (LightType)
                {
                    case LightType.Directional:
                        if (usePhysics && Physics.Raycast(new Ray(worldPos, lightSource.transform.forward), out hit, rayDist, layerMask))
                        {
                            verts.Add(transform.InverseTransformPoint(hit.point));
                            color.a = 1f - hit.distance / rayDist;
                        }
                        else
                        {
                            verts.Add(islands[s].vertices[i] + lightSource.transform.forward * rayDist);
                        }
                        break;

                    case LightType.Spot:
                        float offset = lightSource.spotAngle * lightSource.range / 90f;
                        Vector3 origin = worldPos;
                        Vector3 pointDir = (origin - lightSource.transform.position).normalized;
                        Vector3 target = lightSource.transform.position + (lightSource.transform.forward) * lightSource.range + pointDir * offset;
                        dir = (target - origin).normalized;
                        if (usePhysics && Physics.Raycast(new Ray(worldPos, dir), out hit, rayDist, layerMask))
                        {
                            verts.Add(transform.InverseTransformPoint(hit.point));
                            color.a = 1f - hit.distance / rayDist;
                        }
                        else
                        {
                            verts.Add(islands[s].vertices[i] + dir * rayDist);
                        }
                        break;

                    default:
                        dir = (worldPos - FinalTransform.position).normalized;
                        if (usePhysics && Physics.Raycast(new Ray(worldPos, dir), out hit, rayDist, layerMask))
                        {
                            verts.Add(transform.InverseTransformPoint(hit.point));
                            color.a = 1f - hit.distance / rayDist;
                        }
                        else
                        {
                            verts.Add(transform.InverseTransformPoint(worldPos + dir * rayDist));
                        }
                        break;
                }
                vertColor.Add(color * endColor);
            }
        }

        //update mesh
        mesh.SetVertices(verts);
        mesh.SetTriangles(tris, 0);
        mesh.RecalculateNormals();

        mesh.SetColors(vertColor);
        mesh.UploadMeshData(false);
    }

    /// <summary>
    /// Set the individual vertex position in the island.
    /// </summary>
    /// <param name="islandIndex">Island index.</param>
    /// <param name="vertexIndex">Vertex index.</param>
    /// <param name="vertex">New position.</param>
    public void SetIslandVertex(int islandIndex, int vertexIndex, Vector3 vertex)
    {
        if (islandIndex < 0 || islandIndex >= islands.Length)
            return;

        if (vertexIndex < 0 || vertexIndex >= islands[islandIndex].vertices.Length)
            return;

        islands[islandIndex].vertices[vertexIndex] = vertex;
        UpdateMesh();
    }

    /// <summary>
    /// Set the new vertex positions for the island.
    /// </summary>
    /// <param name="islandIndex">Island index.</param>
    /// <param name="vertices">Array of new positions.</param>
    public void SetIslandVertices(int islandIndex, Vector3[] vertices)
    {
        if (islandIndex < 0 || islandIndex >= islands.Length)
            return;

        if (vertices.Length <= 0)
            return;

        for(int i = 0; i < vertices.Length; i++)
        {
            if (i >= islands[islandIndex].vertices.Length)
                break;

            islands[islandIndex].vertices[i] = vertices[i];
        }
        
        UpdateMesh();
    }

    /// <summary>
    /// Rotate the island.
    /// </summary>
    /// <param name="islandIndex">Island index.</param>
    /// <param name="eulerAngle">Vector3 angle rotation.</param>
    public void RotateIsland(int islandIndex, Vector3 eulerAngle)
    {
        if (islandIndex < 0 || islandIndex >= islands.Length)
            return;

        Quaternion rot = Quaternion.Euler(eulerAngle);

        for (int i = 0; i < Islands[islandIndex].vertices.Length; i++)
            Islands[islandIndex].vertices[i] = rot * Islands[islandIndex].vertices[i];

        UpdateMesh();
    }

#if UNITY_EDITOR
    [MenuItem("GameObject/Effects/Volumetric Probe")]
    public static void CreateVolumetricProbe()
    {
        SceneView view = SceneView.lastActiveSceneView;
        GameObject go = new GameObject("VolumetricProbe");
        go.transform.position = view.camera.transform.position + view.camera.transform.forward * 2;
        go.AddComponent<VolumetricProbe>();
        Selection.activeGameObject = go;
    }
#endif

}
