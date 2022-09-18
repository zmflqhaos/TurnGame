using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatRadarChart : MonoBehaviour
{
    private GameData gameData;
    private CanvasRenderer _radarMeshCanvasRenderer;
    [SerializeField] private Material _radarMat;

    private void Awake()
    {
        _radarMeshCanvasRenderer = transform.Find("RadarMesh").GetComponent<CanvasRenderer>();
    }

    public void Start()
    {
        gameData = GameManager.Instance.CurrentGameData;
    }

    public void SetStats()
    {
        UpdateStatsVisual();

    }

    private void UpdateStatsVisual()
    {
        Mesh mesh = new Mesh();

        float radarChartSize = 385f;
        float angleInc = 360 / 6f;

        Vector3 hpVertex = Quaternion.Euler(0, 0, -angleInc * 0) * Vector3.up * radarChartSize * gameData._playersData[0]._playerStat.GetStatAmountNormalized(Stat.Type.VIT);

        Vector3 mpVertex = Quaternion.Euler(0, 0, -angleInc * 1) * Vector3.up * radarChartSize * gameData._playersData[0]._playerStat.GetStatAmountNormalized(Stat.Type.MGI);

        Vector3 strVertex = Quaternion.Euler(0, 0, -angleInc * 2) * Vector3.up * radarChartSize * gameData._playersData[0]._playerStat.GetStatAmountNormalized(Stat.Type.STR);

        Vector3 dexVertex = Quaternion.Euler(0, 0, -angleInc * 3) * Vector3.up * radarChartSize * gameData._playersData[0]._playerStat.GetStatAmountNormalized(Stat.Type.DEX);

        Vector3 inteVertex = Quaternion.Euler(0, 0, -angleInc * 4) * Vector3.up * radarChartSize * gameData._playersData[0]._playerStat.GetStatAmountNormalized(Stat.Type.INT);

        Vector3 chaVertex = Quaternion.Euler(0, 0, -angleInc * 5) * Vector3.up * radarChartSize * gameData._playersData[0]._playerStat.GetStatAmountNormalized(Stat.Type.CHA);


        Vector3[] vertices = new Vector3[7];
        Vector2[] uv = new Vector2[7];
        int[] triangles = new int[18];

        vertices[0] = Vector3.zero;
        vertices[1] = hpVertex;
        vertices[2] = mpVertex;
        vertices[3] = strVertex;
        vertices[4] = dexVertex;
        vertices[5] = inteVertex;
        vertices[6] = chaVertex;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;
        triangles[6] = 0;
        triangles[7] = 3;
        triangles[8] = 4;
        triangles[9] = 0;
        triangles[10] = 4;
        triangles[11] = 5;
        triangles[12] = 0;
        triangles[13] = 5;
        triangles[14] = 6;
        triangles[15] = 0;
        triangles[16] = 1;
        triangles[17] = 6;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        _radarMeshCanvasRenderer.SetMesh(mesh);
        _radarMeshCanvasRenderer.SetMaterial(_radarMat, null);

    }
}
