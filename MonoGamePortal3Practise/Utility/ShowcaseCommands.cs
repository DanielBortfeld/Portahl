using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGamePortal3Practise
{
    public static class ShowcaseCommands
    {
        private static bool isEnabled = false;

        public static void Toggle()
        {
            if (isEnabled)
                Disable();
            else
                Enable();
        }

        public static void Enable()
        {
            InputManager.OnKeyPressed += OnKeyPressed;
            isEnabled = true;
        }

        public static void Disable()
        {
            InputManager.OnKeyPressed -= OnKeyPressed;
            isEnabled = false;
        }

        static void OnKeyPressed(InputEventArgs eventArgs)
        {
            switch (eventArgs.Key)
            {
                case Keys.F1:
                    SceneManager.LoadScene<SceneTDTutorial>();
                    break;
                case Keys.F2:
                    SceneManager.LoadScene<SceneTDLevelOne>();
                    break;
                case Keys.F3:
                    SceneManager.LoadScene<SceneSideScroller>();
                    break;
                case Keys.F4:
                    SceneManager.LoadScene<FinalScreen>();
                    break;
                case Keys.F10:
                    GameManager.ToggleFullScreen();
                    break;
                default:
                    break;
            }
        }
    }
}
