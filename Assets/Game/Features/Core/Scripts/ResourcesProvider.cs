using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Core
{
    public class ResourcesProvider
    {
        public async UniTask<T> LoadAsync<T>(string resourceId, CancellationToken cancellationToken) where T : Object
        {
            Object result = await Resources.LoadAsync<T>(resourceId).WithCancellation(cancellationToken);
            return result as T;
        }
    }
}