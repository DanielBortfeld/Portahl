// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    public static class InputManager
    {
        /// <summary>
        /// OnKeyDown -> called as long as it's down
        /// OnKeyPressed -> called once per press
        /// OnKeyUp -> called once per release
        /// </summary>
        /// the <param name="eventArgs"> returns <see cref="Keys"/> and <seealso cref="MouseButtons"/>></param>
        public delegate void InputEventHandler(InputEventArgs eventArgs);
        public static event InputEventHandler OnKeyPressed, OnKeyDown, OnKeyUp;

        private static Keys[] lastKeys = new Keys[0];
        private static Keys[] currentKeys = new Keys[0];
        private static Dictionary<MouseButtons, ButtonState> lastButtonStates = new Dictionary<MouseButtons, ButtonState>();
        private static Dictionary<MouseButtons, ButtonState> currentButtonStates = new Dictionary<MouseButtons, ButtonState>();

        private static MouseState mouseStatePrevious;
        public static MouseState MouseStateCurrent { get; private set; }

        public static void Update()
        {
            CheckKeyStates();
            CheckButtonStates();
        }

        private static void CheckKeyStates()
        {
            lastKeys = currentKeys;
            KeyboardState keyState = Keyboard.GetState();
            currentKeys = keyState.GetPressedKeys();

            foreach (var key in currentKeys)
            {
                if (OnKeyDown != null)
                    OnKeyDown(new InputEventArgs(key));

                bool keyPressed = false;

                foreach (var lastKey in lastKeys)
                    if (key == lastKey)
                        keyPressed = true;

                if (!keyPressed && OnKeyPressed != null)
                    OnKeyPressed(new InputEventArgs(key));
            }

            foreach (var key in lastKeys)
            {
                bool released = keyState.IsKeyUp(key);

                if (released && OnKeyUp != null)
                    OnKeyUp(new InputEventArgs(key));
            }
        }

        private static void CheckButtonStates()
        {
            MouseStateCurrent = Mouse.GetState();

            MapButtons(ref currentButtonStates, MouseStateCurrent);
            if (mouseStatePrevious != null)
                MapButtons(ref lastButtonStates, mouseStatePrevious);

            foreach (var state in currentButtonStates)
            {
                if (OnKeyDown != null && state.Value == ButtonState.Pressed)
                    OnKeyDown(new InputEventArgs(state.Key));

                bool released = false;
                bool buttonPressed = false;

                foreach (var lastState in lastButtonStates)
                    if (state.Key == lastState.Key)
                        if (state.Value == ButtonState.Pressed && lastState.Value == ButtonState.Released)
                            buttonPressed = true;
                        else if (state.Value == ButtonState.Released && lastState.Value == ButtonState.Pressed)
                            released = true;

                if (buttonPressed && OnKeyPressed != null)
                    OnKeyPressed(new InputEventArgs(state.Key));

                if (released && OnKeyUp != null)
                    OnKeyUp(new InputEventArgs(state.Key));
            }

            mouseStatePrevious = MouseStateCurrent;
        }

        private static void MapButtons(ref Dictionary<MouseButtons, ButtonState> buttonStates, MouseState mouseState)
        {
            buttonStates.Clear();
            buttonStates.Add(MouseButtons.None, mouseState.XButton1);
            buttonStates.Add(MouseButtons.LeftButton, mouseState.LeftButton);
            buttonStates.Add(MouseButtons.RightButton, mouseState.RightButton);
            buttonStates.Add(MouseButtons.MiddleButton, mouseState.MiddleButton);
        }
    }
}
