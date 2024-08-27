using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public string mainSceneAddress = "MainScene";
    public AssetLabelReference skillIconLabel;
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;

    private List<AsyncOperationHandle<Sprite>> loadedIcons = new List<AsyncOperationHandle<Sprite>>();
    private List<AsyncOperationHandle> handles = new List<AsyncOperationHandle>();
    private int totalAssetsToLoad = 0;
    private int assetsLoaded = 0;
    private float targetFillAmount = 0f;
    private float fillSpeed = 2f;
    private bool allAssetsLoaded = false;
    private Coroutine loadingTextCoroutine;

    void Start()
    {
        StartLoadingProcess();
        loadingTextCoroutine = StartCoroutine(CoUpdateLoadingText());
    }

    private void Update()
    {
        loadingBar.value = Mathf.Lerp(loadingBar.value, targetFillAmount, Time.deltaTime * fillSpeed);

        if (allAssetsLoaded && Mathf.Abs(loadingBar.value - targetFillAmount) < 0.01f)
        {
            allAssetsLoaded = false;
            StopCoroutine(loadingTextCoroutine);
            LoadMainScene();
        }
    }
    private IEnumerator CoUpdateLoadingText()
    {
        string[] loadingStates = { "Loading.", "Loading..", "Loading..." };
        int index = 0;

        while (true)
        {
            loadingText.text = loadingStates[index];
            index = (index + 1) % loadingStates.Length;
            yield return new WaitForSeconds(0.4f);
        }
    }

    private void StartLoadingProcess()
    {
        LoadDependencies();
    }

    private void LoadDependencies()
    {
        var labels = new List<string>() { skillIconLabel.labelString };

        foreach (var label in labels)
        {
            var handle = Addressables.LoadAssetsAsync<Sprite>(label, null);
            handles.Add(handle);

            handle.Completed += (AsyncOperationHandle<IList<Sprite>> loadHandle) =>
            {
                if (loadHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    totalAssetsToLoad += loadHandle.Result.Count;

                    foreach (var icon in loadHandle.Result)
                    {
                        string iconKey = $"SkillIcon/{icon.name}";
                        var iconHandle = Addressables.LoadAssetAsync<Sprite>(iconKey);
                        handles.Add(iconHandle);

                        iconHandle.Completed += (AsyncOperationHandle<Sprite> iconLoadHandle) =>
                        {
                            if (iconLoadHandle.Status == AsyncOperationStatus.Succeeded)
                            {
                                loadedIcons.Add(iconLoadHandle);
                                assetsLoaded++;
                                UpdateTargetFillAmount();
                            }
                            else
                            {
                                Debug.LogError($"Failed to load icon: {iconKey}");
                            }

                            if (AllLoadsCompleted())
                            {
                                allAssetsLoaded = true;
                            }
                        };
                    }
                }
                else
                {
                    Debug.LogError($"Failed to load assets for label: {label}");
                }
            };
        }
    }

    private void UpdateTargetFillAmount()
    {
        if (totalAssetsToLoad > 0)
        {
            targetFillAmount = (float)assetsLoaded / totalAssetsToLoad;
        }
    }

    private bool AllLoadsCompleted()
    {
        return assetsLoaded == totalAssetsToLoad;
    }

    private void LoadMainScene()
    {
        Addressables.LoadSceneAsync(mainSceneAddress, LoadSceneMode.Single);
    }
}
