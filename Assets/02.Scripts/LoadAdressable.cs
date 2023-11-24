using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.ComponentModel;
using System;

public class LoadAdressable : MonoBehaviour
{

    AsyncOperationHandle DownloadHandle;

    public Slider progressbar;
    public Text progressText;

    void Start()
    {
        progressText.text = "0 %";
        progressbar.value = 0;
        DownloadBundle();
    }

    private void DownloadBundle()
    {
        DownloadHandle = Addressables.DownloadDependenciesAsync("Download");
        StartCoroutine(CoDownloadProgress());
        DownloadHandle.Completed += (Handle) =>
        {
            progressText.text = string.Concat(Math.Truncate(DownloadHandle.PercentComplete * 1000f) / 10f, "%");
            progressbar.value = DownloadHandle.PercentComplete;
        };
    }

    IEnumerator CoDownloadProgress()
    {
        while (true)
        {
            progressText.text = string.Concat(Math.Truncate(DownloadHandle.PercentComplete * 1000f) / 10f, "%");
            progressbar.value = DownloadHandle.PercentComplete;
            yield return null;
        }
    }
}
