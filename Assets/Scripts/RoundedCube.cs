using System.Collections.Generic;
using Plugins.MonoCache;
using UnityEngine;

[DisallowMultipleComponent]
public class RoundedCube : MonoCache
{
    private Vector3 _cubeSize;
    [Min(0)] [SerializeField] private float _radius = 0.1f;
    [Min(1)] [SerializeField] private int _radialSegments = 1;

    private Vector3 _oldScale;

    protected override void UpdateCached()
    {
        if (transform.localScale != _oldScale)
        {
            _cubeSize = transform.localScale;
            CreateCubeStructure();
            _oldScale = transform.localScale;
        }
    }

    private void CreateCubeStructure()
    {
        List<Mesh> meshes = new List<Mesh>();
        Vector3 halfSize = _cubeSize / 2;
        
        Vector3[] cornerPositions =
        {
            new (1, 1, 1),
            new (-1, 1, 1),
            new (-1, 1, -1),
            new (1, 1, -1),
            new (1, -1, 1),
            new (-1, -1, 1),
            new (-1, -1, -1),
            new (1, -1, -1),
        };

        Vector3[] cornerEulers =
        {
            new (0, 0, 0),
            new (0, -90, 0),
            new (0, 180, 0),
            new (0, 90, 0),
            new (90, 0, 0),
            new (90, -90, 0),
            new (90, 180, 0),
            new (90, 90, 0),
        };

        for (int i = 0; i < 8; i++)
        {
            Vector3 half = new Vector3(halfSize.x - _radius, halfSize.y - _radius, halfSize.z - _radius);
            Vector3 position = Vector3.Scale(half, cornerPositions[i]);
            Vector3 euler = cornerEulers[i];
            Mesh sectorMesh = GenerateSphereSector(position, euler, _radialSegments, _radius);
            meshes.Add(sectorMesh);
        }


        Mesh mesh = CreateCuboidMesh(_cubeSize);
        meshes.Add(mesh);

        Vector3[] edgePositions =
        {
            new (halfSize.x - _radius, 0, halfSize.z - _radius),
            new (halfSize.x - _radius, 0, -halfSize.z + _radius),
            new (-halfSize.x + _radius, 0, halfSize.z - _radius),
            new (-halfSize.x + _radius, 0, -halfSize.z + _radius),
            new (0, halfSize.y - _radius, halfSize.z - _radius),
            new (0, halfSize.y - _radius, -halfSize.z + _radius),
            new (0, -halfSize.y + _radius, halfSize.z - _radius),
            new (0, -halfSize.y + _radius, -halfSize.z + _radius),
            new (halfSize.x - _radius, halfSize.y - _radius, 0),
            new (halfSize.x - _radius, -halfSize.y + _radius, 0),
            new (-halfSize.x + _radius, halfSize.y - _radius, 0),
            new (-halfSize.x + _radius, -halfSize.y + _radius, 0)
        };
        Vector3[] edgeRotations =
        {
            new (0, 0, 0),
            new (0, 90, 0),
            new (0, -90, 0),
            new (0, 180, 0),
            new (0, 0, 90),
            new (-90, 0, 90),
            new (90, 0, 90),
            new (180, 0, 90),
            new (-90, -90, 90),
            new (90, 0, 0),
            new (180, 90, -90),
            new (90, 90, -90)
        };
        float[] edgeScaleY =
        {
            _cubeSize.y,
            _cubeSize.y,
            _cubeSize.y,
            _cubeSize.y,
            _cubeSize.x,
            _cubeSize.x,
            _cubeSize.x,
            _cubeSize.x,
            _cubeSize.z,
            _cubeSize.z,
            _cubeSize.z,
            _cubeSize.z
        };


        for (int i = 0; i < edgePositions.Length; i++)
        {
            Mesh edgeMesh = CreateQuarterCylinderSurfaceMesh(edgeScaleY[i] - _radius * 2f, _radius, edgePositions[i],
                edgeRotations[i], _radialSegments);
            meshes.Add(edgeMesh);
        }

        Mesh resultMesh = CombineMeshes(meshes.ToArray());
        GetComponent<MeshFilter>().sharedMesh = resultMesh;
    }

    private Mesh CreateCuboidMesh(Vector3 cubeSize)
    {
        Mesh mesh = new Mesh();

        Vector3 halfCubeSize = cubeSize / 2;
        Vector3 reducedSize = cubeSize - Vector3.one * _radius * 2f;
        Vector3 halfReducedSize = reducedSize / 2;

        Vector3[] vertices =
        {
            new (-halfReducedSize.x, -halfReducedSize.y, halfCubeSize.z),
            new (halfReducedSize.x, -halfReducedSize.y, halfCubeSize.z),
            new (halfReducedSize.x, halfReducedSize.y, halfCubeSize.z),
            new (-halfReducedSize.x, halfReducedSize.y, halfCubeSize.z),

            new (-halfReducedSize.x, -halfReducedSize.y, -halfCubeSize.z),
            new (halfReducedSize.x, -halfReducedSize.y, -halfCubeSize.z),
            new (halfReducedSize.x, halfReducedSize.y, -halfCubeSize.z),
            new (-halfReducedSize.x, halfReducedSize.y, -halfCubeSize.z),

            new (-halfReducedSize.x, halfCubeSize.y, -halfReducedSize.z),
            new (halfReducedSize.x, halfCubeSize.y, -halfReducedSize.z),
            new (halfReducedSize.x, halfCubeSize.y, halfReducedSize.z),
            new (-halfReducedSize.x, halfCubeSize.y, halfReducedSize.z),

            new (-halfReducedSize.x, -halfCubeSize.y, -halfReducedSize.z),
            new (halfReducedSize.x, -halfCubeSize.y, -halfReducedSize.z),
            new (halfReducedSize.x, -halfCubeSize.y, halfReducedSize.z),
            new (-halfReducedSize.x, -halfCubeSize.y, halfReducedSize.z),

            new (-halfCubeSize.x, -halfReducedSize.y, -halfReducedSize.z),
            new (-halfCubeSize.x, -halfReducedSize.y, halfReducedSize.z),
            new (-halfCubeSize.x, halfReducedSize.y, halfReducedSize.z),
            new (-halfCubeSize.x, halfReducedSize.y, -halfReducedSize.z),

            new (halfCubeSize.x, -halfReducedSize.y, -halfReducedSize.z),
            new (halfCubeSize.x, -halfReducedSize.y, halfReducedSize.z),
            new (halfCubeSize.x, halfReducedSize.y, halfReducedSize.z),
            new (halfCubeSize.x, halfReducedSize.y, -halfReducedSize.z)
        };

        mesh.vertices = vertices;

        int[] triangles =
        {
            0, 1, 2, 0, 2, 3,
            4, 6, 5, 4, 7, 6,
            10, 9, 8, 11, 10, 8,
            14, 15, 12, 13, 14, 12,
            16, 17, 18, 16, 18, 19,
            20, 22, 21, 20, 23, 22
        };

        mesh.triangles = triangles;

        Vector3[] normals = new Vector3[24];

        for (int i = 0; i < 4; i++)
        {
            normals[i] = Vector3.forward;
            normals[i + 4] = Vector3.back;
            normals[i + 8] = Vector3.up;
            normals[i + 12] = Vector3.down;
            normals[i + 16] = Vector3.left;
            normals[i + 20] = Vector3.right;
        }

        mesh.normals = normals;

        return mesh;
    }

    private Mesh CombineMeshes(Mesh[] meshes)
    {
        Mesh combinedMesh = new Mesh();

        int totalVerticesCount = 0;
        int totalTrianglesCount = 0;

        foreach (Mesh mesh in meshes)
        {
            totalVerticesCount += mesh.vertexCount;
            totalTrianglesCount += mesh.triangles.Length;
        }

        Vector3[] combinedVertices = new Vector3[totalVerticesCount];
        Vector3[] combinedNormals = new Vector3[totalVerticesCount];
        Vector2[] combinedUVs = new Vector2[totalVerticesCount];
        int[] combinedTriangles = new int[totalTrianglesCount];

        int currentVertexIndex = 0;
        int currentTriangleIndex = 0;

        foreach (Mesh mesh in meshes)
        {
            int meshVerticesCount = mesh.vertexCount;

            mesh.vertices.CopyTo(combinedVertices, currentVertexIndex);
            mesh.uv.CopyTo(combinedUVs, currentVertexIndex);
            mesh.normals.CopyTo(combinedNormals, currentVertexIndex);

            int[] meshTriangles = mesh.triangles;
            for (int i = 0; i < meshTriangles.Length; i++)
            {
                combinedTriangles[currentTriangleIndex + i] = meshTriangles[i] + currentVertexIndex;
            }

            currentVertexIndex += meshVerticesCount;
            currentTriangleIndex += meshTriangles.Length;
        }

        for (int i = 0; i < combinedVertices.Length; i++)
        {
            combinedVertices[i] = new Vector3(
                combinedVertices[i].x / transform.localScale.x,
                combinedVertices[i].y / transform.localScale.y,
                combinedVertices[i].z / transform.localScale.z
            );
            combinedNormals[i] = new Vector3(
                combinedNormals[i].x * transform.localScale.x,
                combinedNormals[i].y * transform.localScale.y,
                combinedNormals[i].z * transform.localScale.z
            );
        }

        combinedMesh.vertices = combinedVertices;
        combinedMesh.normals = combinedNormals;

        combinedMesh.uv = combinedUVs;
        combinedMesh.triangles = combinedTriangles;

        combinedMesh.RecalculateBounds();


        return combinedMesh;
    }

    private Mesh CreateQuarterCylinderSurfaceMesh(float height, float radius, Vector3 position, Vector3 euler,
        int radialSegments = 16)
    {
        Mesh mesh = new Mesh();
        int verticesCount = (radialSegments + 1) * 2;
        int trianglesCount = radialSegments * 6;

        Vector3[] vertices = new Vector3[verticesCount];
        Vector3[] normals = new Vector3[verticesCount];
        Vector2[] uvs = new Vector2[verticesCount];
        int[] triangles = new int[trianglesCount];

        float radialStep = Mathf.PI / 2.0f / radialSegments;
        float halfHeight = height / 2.0f;

        for (int i = 0; i <= radialSegments; i++)
        {
            for (int j = 0; j <= 1; j++)
            {
                int index = i * 2 + j;
                float angle = i * radialStep;
                float xPos = radius * Mathf.Cos(angle);
                float yPos = j * height - halfHeight;
                float zPos = radius * Mathf.Sin(angle);

                vertices[index] = new Vector3(xPos, yPos, zPos);

                vertices[index] = Quaternion.Euler(euler) * vertices[index];
                vertices[index] += position; //

                normals[index] = new Vector3(xPos, 0, zPos);
                normals[index] = Quaternion.Euler(euler) * normals[index];

                uvs[index] = new Vector2((float)i / radialSegments, j);
            }
        }

        for (int i = 0; i < radialSegments; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                int index = i * 2 + j;
                int t = i * 6;

                triangles[t] = index;
                triangles[t + 1] = index + 1;
                triangles[t + 2] = index + 2;
                triangles[t + 3] = index + 1;
                triangles[t + 4] = index + 3;
                triangles[t + 5] = index + 2;
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        return mesh;
    }

    private Mesh GenerateSphereSector(Vector3 position, Vector3 euler, int resolution, float radius)
    {
        Mesh mesh = new Mesh();

        int vertexCount = (resolution + 1) * (resolution + 1);
        Vector3[] vertices = new Vector3[vertexCount];
        Vector3[] normals = new Vector3[vertexCount];

        float step = 1f / resolution;
        for (int y = 0; y <= resolution; y++)
        {
            for (int x = 0; x <= resolution; x++)
            {
                int index = x + y * (resolution + 1);
                float latitude = y * step * Mathf.PI / 2f;
                float longitude = x * step * Mathf.PI / 2f;
                Vector3 pointOnUnitSphere = new Vector3(Mathf.Sin(latitude) * Mathf.Cos(longitude),
                    Mathf.Sin(latitude) * Mathf.Sin(longitude), Mathf.Cos(latitude));
                Vector3 point = pointOnUnitSphere * radius;
                Vector3 pointRotated = Quaternion.Euler(euler) * point;
                vertices[index] = pointRotated; //
                vertices[index] += position; //

                normals[index] = pointRotated;
            }
        }

        mesh.vertices = vertices;
        mesh.normals = normals;

        int[] triangles = new int[resolution * resolution * 6];
        for (int y = 0, t = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++, t += 6)
            {
                int index = x + y * (resolution + 1);
                triangles[t] = index;
                triangles[t + 1] = index + resolution + 1;
                triangles[t + 2] = index + 1;
                triangles[t + 3] = index + 1;
                triangles[t + 4] = index + resolution + 1;
                triangles[t + 5] = index + resolution + 2;
            }
        }

        mesh.triangles = triangles;
        return mesh;
    }
}