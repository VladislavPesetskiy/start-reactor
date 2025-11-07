using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Saves.Scripts
{
    public interface IPersistence
    {
        string SaveKey { get; }
        object Persistence { get; }
        UniTask Load(CancellationToken cancellationToken);
        UniTask Save(CancellationToken cancellationToken);
    }
}