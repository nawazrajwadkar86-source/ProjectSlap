using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WorldPathCurve : MonoBehaviour
{
    private static int curverAmountID = Shader.PropertyToID("_curve_Amount");
    private static int seePosID = Shader.PropertyToID("_Position");
    private static int seeSizeID = Shader.PropertyToID("_size");
    private static int seeBoolID = Shader.PropertyToID("_Circle_Seethrough");
    [SerializeField] private Vector2 curveVec;
    private Camera Camera;
    private Transform player;
    public LayerMask SeeThroughLayer;

    public AssetReference assetReference;
    public List<Material> seeTrhoughMaterialList = new List<Material>();
    private MeshRenderer hitMeshRenderer;
    private bool rightSide = true;
    private float currentCurvedPath = 0.0015f;
    private float maxPathCurve = 0.0015f;
    public float CurveChangeSpeed = 0.0001f;

    float _time;
    float CoTime = 0;
    private float nextCurvetUpdateTime;

    private void Start()
    {
        Camera = Camera.main;
        player = FindAnyObjectByType<PlayerController>().transform;

        foreach (var m in assetReference.worldCurveMaterial)
        {
            m.SetVector("_curve_Amount", Vector2.zero);
        }

        nextCurvetUpdateTime = _time + 4;
    }
    private void Update()
    {
        _time += Time.deltaTime;
        PathCurveGenration();
        SeeTroughtObjects();
    }
    private void ChangePath()
    {
        StartCoroutine(ChangeInPath());
    }
    IEnumerator ChangeInPath()
    {
        float endTime = CoTime + 4;
        while (CoTime < endTime)
        {
            CoTime += Time.deltaTime;
            float pTIme = CoTime * 0.085f;
            float x = Mathf.Lerp(-1, 1, Mathf.PerlinNoise(pTIme + 25, pTIme + 50));
            float y = Mathf.Lerp(-1, 1, Mathf.PerlinNoise(pTIme + 75, pTIme));
            
            
            curveVec = new Vector2(x * 0.0025f, y * 0.00075f);

            foreach (var m in assetReference.worldCurveMaterial)
            {
                m.SetVector(curverAmountID, curveVec);
            }
            yield return null;
        }
    }
    private void PathCurveGenration()
    {
        if (_time > nextCurvetUpdateTime)
        {
            ChangePath();
            nextCurvetUpdateTime = _time + Random.Range(12, 20);
        }
    }
    private void SeeTroughtObjects()
    {
        Vector3 dir = ((player.position + Vector3.up * 0.5f) - Camera.transform.position).normalized;
        Ray ray = new Ray(Camera.transform.position, dir);


        if (Physics.Raycast(ray, out RaycastHit hit, 3000, SeeThroughLayer))
        {
            // Debug.Log("Hit: " + hit.transform.name);
            hitMeshRenderer = hit.transform.GetComponent<MeshRenderer>();
            if (hitMeshRenderer != null)
            {
                Material[] mats = hitMeshRenderer.materials;

                if (mats != seeTrhoughMaterialList.ToArray() && seeTrhoughMaterialList.Count > 0)
                {
                    foreach (var m in seeTrhoughMaterialList)
                    {
                        if (m.GetInt(seeBoolID) == 0)
                        {
                            return;
                        }
                        else
                        {
                            m.SetFloat(seeSizeID, 0);
                        }
                    }
                    seeTrhoughMaterialList.Clear();
                }

                foreach (var m in mats)
                {
                    seeTrhoughMaterialList.Add(m);
                    if (m.GetInt(seeBoolID) == 0)
                    {
                        return;
                    }
                    else
                    {
                        Vector3 view = Camera.WorldToViewportPoint(hit.point);
                        m.SetFloat(seeSizeID, .75f);
                        m.SetVector(seePosID, view);
                    }
                }
            }

        }
        else
        {
            // foreach (var m in seeTrhoughMaterialList)
            // {
            //     if (m.GetInt(seeBoolID) == 0)
            //     {
            //         return;
            //     }
            //     else
            //     {
            //         m.SetFloat(seeSizeID, 0);
            //     }
            // }
            seeTrhoughMaterialList.Clear();
        }
    }
}
