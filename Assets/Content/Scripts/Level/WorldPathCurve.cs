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
    private float nextCurvetUpdateTime;

    private void Start()
    {
        Camera = Camera.main;
        player = FindAnyObjectByType<PlayerController>().transform;

        foreach (var m in assetReference.worldCurveMaterial)
        {
            m.SetVector("_curve_Amount", curveVec);
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
        float CoTime = 0;

        float x = Random.Range(0.0f,1.0f) > 0.5f?-1:1;
        float y = Random.Range(0.0f,1.0f) > 0.5f?-1:1;

        Vector2 initialPos = curveVec;
        Vector2 targetPos = new Vector2(x * 0.0015f, y * 0.00075f);
        while (CoTime < 1)
        {
            CoTime += Time.deltaTime * .75f;

            curveVec = Vector2.Lerp(initialPos,targetPos,CoTime);

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

    private void OnDestroy()
    {
        foreach (var m in assetReference.worldCurveMaterial)
        {
            m.SetVector("_curve_Amount", new Vector2(0,0));
        }
    }
}
