using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using Zenject;

namespace GameUI.Logic
{
    public class GameWindowsHandler : MonoBehaviour
    {
        private const string MAIN_WINDOW_NAME = "MainWindow";

        [SerializeField] private GameWindow[] _gameWindowPrefabs;

        private Dictionary<string, GameWindow> _windows = new Dictionary<string, GameWindow>();

        public DiContainer Container { get; set; }

        public event Action<GameWindow, bool> OnWindowStateChanged = delegate { };

        private void Start()
        {
            OpenMainWindow();
        }

        public void OpenWindow(string windowName)
        {
            var window = GetWindow(windowName);

            if (window)
            {
                window.SetActive(true);

                WindowStateChanged(window);
            }
        }

        public void CloseWindow(string windowName)
        {
            if (!_windows.ContainsKey(windowName)) return;

            _windows[windowName].SetActive(false);
        }

        public void SwitchWindowState(string windowName)
        {
            if (_windows.ContainsKey(windowName))
            {
                var window = _windows[windowName];
                window.SwitchState();

                WindowStateChanged(window);
            }
            else
            {
                OpenWindow(windowName);
            }
        }

        private void WindowStateChanged(GameWindow window)
        {
            bool isOpened = window.IsOpened;

            if (isOpened)
            {
                CloseOtherWindows(window);
            }
            else
            {
                //if closed non-additive window then need to open Main Window

                if (!window.IsAdditive)
                {
                    if (!window.WindowName.Equals(MAIN_WINDOW_NAME))
                    {
                        OpenMainWindow();
                    }
                }
            }

            OnWindowStateChanged(window, isOpened);
        }

        private void CloseOtherWindows(GameWindow window)
        {
            if (!window.IsOpened) return;

            if (!window.IsAdditive)
            {
                foreach (var openedWindow in _windows)
                {
                    if (openedWindow.Value.WindowName.Equals(window.WindowName)) continue;

                    openedWindow.Value.SetActive(false);
                }
            }
        }

        private void OpenMainWindow()
        {
            OpenWindow(MAIN_WINDOW_NAME);
        }

        private GameWindow GetWindow(string windowName)
        {
            if (_windows.ContainsKey(windowName))
            {
                return _windows[windowName];
            }
            else
            {
                return CreateWindow(windowName);
            }
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
                    result.transform.SetParent(transform, false);
                    _windows.Add(result.WindowName, result);
                }
            }

            return result;
        }
    }
}

