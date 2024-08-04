using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public string mainSceneAddress = "MainScene";
    public AssetLabelReference skillIconLabel;
    public TextMeshProUGUI loadingText;

    private List<AsyncOperationHandle<Sprite>> loadedIcons = new List<AsyncOperationHandle<Sprite>>();

    void Start()
    {
        StartLoadingProcess();
    }

    private void StartLoadingProcess()
    {
        loadingText.text = "Downloading resource...";
        LoadDependencies();
    }

    private void LoadDependencies()
    {
        var labels = new List<string>() { skillIconLabel.labelString };
        List<AsyncOperationHandle> handles = new List<AsyncOperationHandle>();

        foreach (var label in labels)
        {
            var handle = Addressables.LoadAssetsAsync<Sprite>(label, null);
            handles.Add(handle);
            handle.Completed += (AsyncOperationHandle<IList<Sprite>> loadHandle) =>
            {
                if (loadHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    foreach (var icon in loadHandle.Result)
                    {
                        string iconKey = $"Resources_moved/SkillIcon/{icon.name}";
                        var iconHandle = Addressables.LoadAssetAsync<Sprite>(iconKey);
                        iconHandle.Completed += (AsyncOperationHandle<Sprite> iconLoadHandle) =>
                        {
                            if (iconLoadHandle.Status == AsyncOperationStatus.Succeeded)
                            {
                                loadedIcons.Add(iconLoadHandle);
                                Debug.Log($"Successfully loaded icon: {iconKey}");
                            }
                            else
                            {
                                Debug.LogError($"Failed to load icon: {iconKey}");
                            }
                        };
                    }

                    Debug.Log($"Loaded assets for label: {label}");
                    if (AllLoadsCompleted(handles))
                    {
                        LoadMainScene();
                    }
                }
                else
                {
                    Debug.LogError($"Failed to load assets for label: {label}");
                }
            };
        }
    }
    bool AllLoadsCompleted(List<AsyncOperationHandle> handles)
    {
        foreach (var handle in handles)
        {
            if (!handle.IsDone || handle.Status != AsyncOperationStatus.Succeeded)
            {
                return false;
            }
        }
        return true;
    }

    void LoadMainScene()
    {
        loadingText.text = "Loading main scene...";
        Addressables.LoadSceneAsync(mainSceneAddress, LoadSceneMode.Single).Completed += OnSceneLoaded;
    }

    void OnSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Main scene loaded successfully.");
        }
        else
        {
            Debug.LogError($"Failed to load main scene: {obj.OperationException}");
        }
    }
}
