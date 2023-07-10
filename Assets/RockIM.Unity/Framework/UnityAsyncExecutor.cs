using System;
using System.Collections.Concurrent;
using System.Threading;
using RockIM.Sdk.Framework;
using UnityEngine;

namespace RockIM.Unity.Framework
{
    public class UnityAsyncExecutor : MonoBehaviour, IAsyncExecutor
    {
        public static UnityAsyncExecutor Instance { get; private set; }

        private static volatile bool _initialized;

        private static int _currentThreadId;

        private static readonly ConcurrentQueue<Action> Actions = new ConcurrentQueue<Action>();

        static UnityAsyncExecutor()
        {
            if (_initialized && _currentThreadId == Thread.CurrentThread.ManagedThreadId)
            {
                return;
            }

            initOnce();
        }


        public void Execute(Action action)
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
            var target = new GameObject(nameof(UnityAsyncExecutor));
            Instance = target.AddComponent<UnityAsyncExecutor>();
            DontDestroyOnLoad(target);
            DontDestroyOnLoad(Instance);
            _currentThreadId = Thread.CurrentThread.ManagedThreadId;
            _initialized = true;
        }
    }
}