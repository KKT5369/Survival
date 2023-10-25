using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceLoadManager : SingleTon<ResourceLoadManager>
{
    // AssetReference 로 에셋 로딩
    public async UniTask LoadAssetasync<T>(AssetReference assetReference, Action<T> action)
    {
        if (assetReference == null)
            return;

        AsyncOperationHandle asset;
        asset = Addressables.LoadAssetAsync<T>(assetReference);


        await UniTask.WaitUntil(() => asset.IsDone);
        action?.Invoke((T)asset.Result);
    }

    // string 값으로 에셋 로딩
    public async UniTask LoadAssetasync<T>(string strAddress, Action<T> action)
    {
        if (strAddress.Equals(""))
            return;

        AsyncOperationHandle asset;
        asset = Addressables.LoadAssetAsync<T>(strAddress);

        await UniTask.WaitUntil(() => asset.IsDone);
        action?.Invoke((T)asset.Result);
    }

    public async UniTask DownloadDependenciesAsync(string strLabel)
    {
        var size = await Addressables.GetDownloadSizeAsync(strLabel);
        if (size != 0) return;

        var handle = Addressables.DownloadDependenciesAsync(strLabel, true);
        await UniTask.WaitUntil((() => handle.IsDone));
    }
}

