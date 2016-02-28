using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;

namespace MonoGamePortal3Practise
{
    public enum States { Idle, Walk, Jump }

    class SpriteAnimation
    {
        private string currentAnimationName;
        private List<SpriteFrame> allFrames = new List<SpriteFrame>();
        private List<SpriteFrame> currentFrames = new List<SpriteFrame>();
        private int currentFrameCount;
        private float timer;

        public string Name { get; private set; }
        public int FrameDelay { get; set; }
        public Texture2D SpriteAnimationSheet { get; private set; }
        public SpriteFrame CurrentFrame { get; private set; }

        public SpriteAnimation(string name, Texture2D spriteAnimationSheet, string sheetDataPath, string firstAnimationName)
        {
            Name = name;
            SpriteAnimationSheet = spriteAnimationSheet;

            LoadFrames(sheetDataPath);

            SetAnimation(firstAnimationName);
        }

        public void Update(GameTime gameTime, string animationFrameName)
        {
            SetAnimation(animationFrameName);
            UpdateAnimationFrame(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            spriteBatch.Draw(SpriteAnimationSheet, position, CurrentFrame.SourceRect, color);
        }

        private void SetAnimation(string animName)
        {
            if (currentAnimationName != animName) // wenn diese anim gerade nicht abgespielt wird
            {
                currentFrames = allFrames.FindAll(animationFrame => animationFrame.Name.Contains(animName)); // alle frames mit diesem namen sind jetzt die aktuellen frames
                currentAnimationName = animName; // der name wird aktualisiert
                currentFrameCount = 0; // anim fängt vorne an
                CurrentFrame = currentFrames[0]; // erster frame = erster frame

                if (currentFrames.Count == 0) // wenn aktuell keine frames da sind, dann war der <name> falsch
                    throw new Exception(string.Format("No AnimationFrame found for {0}", animName));
            }
        }

        private void UpdateAnimationFrame(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;

            if (currentFrames.Count == 0)
                throw new Exception("No current animation set");

            if (timer > FrameDelay)
            {
                timer = 0;
                if (currentFrameCount < currentFrames.Count - 1) // wenn der aktulle frame nicht der letzte ist
                    currentFrameCount++; // dann einen frame weiter zählen
                else
                    currentFrameCount = 0; // sonst wieder vorne anfangen

                CurrentFrame = currentFrames[currentFrameCount];
            }
        }

        private void LoadFrames(string dataPath)
        {
            XmlReader xmlReader = XmlReader.Create(dataPath);

            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement("sprite"))
                {
                    string frameName = xmlReader.GetAttribute("name");
                    if (frameName.Contains(Name))
                    {
                        SpriteFrame animationFrame = new SpriteFrame();
                        animationFrame.Name = frameName;
                        animationFrame.SourceRect.X = Convert.ToInt32(xmlReader.GetAttribute("x"));
                        animationFrame.SourceRect.Y = Convert.ToInt32(xmlReader.GetAttribute("y"));
                        animationFrame.SourceRect.Width = Convert.ToInt32(xmlReader.GetAttribute("width"));
                        animationFrame.SourceRect.Height = Convert.ToInt32(xmlReader.GetAttribute("height"));

                        allFrames.Add(animationFrame);
                    }
                }
            }
        }
    }
}
