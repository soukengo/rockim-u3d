using System;
using System.Collections.Concurrent;
using System.Threading;
using RockIM.Sdk.Framework;
using UnityEngine;

namespace RockIM.Unity.Framework
{
    public class UnityAsyncHandler : MonoBehaviour, IAsyncHandler
    {
        public static UnityAsyncHandler Instance { get; private set; }

        private static volatile bool _initialized;

        private static int _currentThreadId;

        private static readonly ConcurrentQueue<Action> Actions = new ConcurrentQueue<Action>();

        static UnityAsyncHandler()
        {
            if (_initialized && _currentThreadId == Thread.CurrentThread.ManagedThreadId)
            {
                return;
            }

            initOnce();
        }


        public void Callback(Action action)
        {
            Actions.Enqueue(action);
        }


        private void Update()
        {
            if (Actions.TryDequeue(out var action))
            {
                action?.Invoke();
            }
        }

        private static void initOnce()
        {
            var target = new GameObject(nameof(UnityAsyncHandler));
            Instance = target.AddComponent<UnityAsyncHandler>();
            DontDestroyOnLoad(target);
            DontDestroyOnLoad(Instance);
            _currentThreadId = Thread.CurrentThread.ManagedThreadId;
            _initialized = true;
        }
    }
}