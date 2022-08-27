using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GameUI.Logic
{
    public class GameWindowsHandler : MonoBehaviour
    {
        [SerializeField] private GameWindow[] _gameWindowPrefabs;

        private Dictionary<string, GameWindow> _windows = new Dictionary<string, GameWindow>();

        public DiContainer Container { get; set; }

        private void Start()
        {
            OpenWindow("MainWindow");
        }

        public void OpenWindow(string windowName, bool isAdditive = true)
        {
            GameWindow window;

            if (!_windows.ContainsKey(windowName))
            {
                window = CreateWindow(windowName);
            }
            else
            {
                window = _windows[windowName];
            }

            if (window)
            {
                window.SetActive(true);
            }

            if (!isAdditive)
            {
                foreach(var openedWindow in _windows)
                {
                    openedWindow.Value.SetActive(false);
                }
            }
        }

        public void CloseWindow(string windowName)
        {
            if (!_windows.ContainsKey(windowName)) return;

            _windows[windowName].SetActive(false);
        }

        private GameWindow CreateWindow(string windowName)
        {
            GameWindow result = null;
            var windowPrefab = _gameWindowPrefabs.Where(window => window.WindowName.Equals(windowName)).FirstOrDefault();

            if (windowPrefab)
            {
                result = Container.InstantiatePrefabForComponent<GameWindow>(windowPrefab);

                if (result)
                {
                    result.transform.parent = transform;
                    _windows.Add(result.WindowName, result);
                }
            }

            return result;
        }
    }
}

