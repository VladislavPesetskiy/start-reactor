using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Game.Extensions
{
    public static class DOTweenExtensions
    {
        public static UniTask ToUniTask(this Tween tween, CancellationToken token)
        {
            var tcs = new UniTaskCompletionSource();
            if (tween == null || tween.active == false)
            {
                tcs.TrySetResult();
                return tcs.Task;
            }

            tween.OnComplete(() =>
            {
                if (tcs.Task.Status.IsCompleted() == false)
                {
                    tcs.TrySetResult();
                }
            });

            tween.OnKill(() =>
            {
                if (tcs.Task.Status.IsCompleted() == false)
                {
                    tcs.TrySetCanceled();
                }
            });

            token.Register(() =>
            {
                if (tween.IsActive())
                {
                    tween.Kill();
                }

                if (tcs.Task.Status.IsCompleted() == false)
                {
                    tcs.TrySetCanceled();
                }
            });

            return tcs.Task;
        }
    }
}