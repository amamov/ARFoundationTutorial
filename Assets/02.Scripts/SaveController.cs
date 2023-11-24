using System;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{
    ARPointCloud m_PointCloud;
    public GameObject obj;
    public Text txt;
    Dictionary<ulong, Vector3> m_Points = new Dictionary<ulong, Vector3>();

    void Awake()
    {
        m_PointCloud = GetComponent<ARPointCloud>();
        txt = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
    }

    void OnPointCloudChanged(ARPointCloudUpdatedEventArgs eventArgs)
    {
        if (!m_PointCloud.positions.HasValue)
            return;

        var positions = m_PointCloud.positions.Value;

        if (m_PointCloud.identifiers.HasValue)
        {
            var identifiers = m_PointCloud.identifiers.Value;
            for (int i = 0; i < positions.Length; ++i)
            {
                m_Points[identifiers[i]] = positions[i];
            }
        }
    }

    void SavePoints()
    {
        string filePath = Application.persistentDataPath + "/points.txt";
        Debug.Log("file save at " + filePath);

        using (StreamWriter sr = File.CreateText(filePath))
        {
            foreach (var kvp in m_Points)
            {
                Vector3 p = kvp.Value;
                sr.WriteLine(p.x.ToString("F3") + "," + p.y.ToString("F3") + "," + p.z.ToString("F3"));
            }
        }

    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Instantiate(obj);
            txt.text = "Saved";
            SavePoints();
        }
    }

    void OnEnable()
    {
        m_PointCloud.updated += OnPointCloudChanged;
    }

    void OnDisable()
    {
        m_PointCloud.updated -= OnPointCloudChanged;
    }
}