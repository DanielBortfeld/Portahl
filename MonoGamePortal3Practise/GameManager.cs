using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGamePortal3Practise
{
    static class GameManager
    {
        public delegate void MoveEvent();
        public static event MoveEvent OnMove;

        public delegate void GameEvent();
        public static event GameEvent OnToggleMouseVisibilitiy, OnSetMouseVisibility, OnGameCompletion;

        public static ContentManager Content;
        public static GraphicsDeviceManager Graphics;

        public static bool IsMouseVisible { get; private set; }

        public static Texture2D LoadTexture2D(string name)
        {
            return Content.Load<Texture2D>(name);
        }

        public static void SetPreferredBackBufferSize(int width, int height)
        {
            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.ApplyChanges();
        }

        public static void ReportMove()
        {
            if (OnMove != null)
                OnMove();
        }

        public static void ToggleFullScreen()
        {
            Graphics.ToggleFullScreen();
        }

        public static void SetMouseVisibility(bool isVisible)
        {
            IsMouseVisible = isVisible;
            if (OnSetMouseVisibility != null)
                OnSetMouseVisibility();
        }

        public static void ToggleMouseVisibility()
        {
            if (OnToggleMouseVisibilitiy != null)
                OnToggleMouseVisibilitiy();
        }

        public static void Exit()
        {
            if (OnGameCompletion != null)
                OnGameCompletion();
        }

        #region Old GameManager

        //public static Texture2D SpriteSheet;

        //private static List<Sprite> sprites = new List<Sprite>();
        //private static List<GameObject> gameObjects = new List<GameObject>();
        //private static List<GameObject> addedGameObjects = new List<GameObject>();
        //private static List<GameObject> removedGameObjects = new List<GameObject>();

        //public static List<GameObject> GameObjects
        //{
        //    get { return gameObjects; }
        //}

        //public static void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Begin();

        //    gameObjects.ForEach(e => e.Draw(spriteBatch));

        //    spriteBatch.End();
        //}

        //public static void Update(GameTime gameTime)
        //{
        //    gameObjects.AddRange(addedGameObjects);
        //    addedGameObjects.ForEach(e => e.LoadContent());
        //    addedGameObjects.Clear();

        //    gameObjects.ForEach(e => e.Update(gameTime));

        //    removedGameObjects.ForEach(e => gameObjects.Remove(e));
        //    removedGameObjects.Clear();
        //}

        //public static Rectangle GetSpriteRect(string name)
        //{
        //    return ((Sprite)sprites.Find(s => s.Name.Contains(name))).SourceRect;
        //}

        //public static void LoadSprites(string dataPath)
        //{
        //    XmlReader xmlReader = XmlReader.Create(dataPath);

        //    while (xmlReader.Read())
        //    {
        //        if (xmlReader.IsStartElement("SubTexture"))
        //        {
        //            Sprite sprite = new Sprite();

        //            sprite.Name = xmlReader.GetAttribute("name"); ;
        //            sprite.SourceRect.X = Convert.ToInt32(xmlReader.GetAttribute("x"));
        //            sprite.SourceRect.Y = Convert.ToInt32(xmlReader.GetAttribute("y"));
        //            sprite.SourceRect.Width = Convert.ToInt32(xmlReader.GetAttribute("width"));
        //            sprite.SourceRect.Height = Convert.ToInt32(xmlReader.GetAttribute("height"));
        //            sprites.Add(sprite);
        //        }
        //    }
        //}

        //public static GameObject FindGameObject(string name)
        //{
        //    return gameObjects.Find(g => g.Name.Contains(name));
        //}

        //public static void AddGameObject(GameObject gameObject)
        //{
        //    addedGameObjects.Add(gameObject);
        //}

        //public static void RemoveGameObject(GameObject gameObject)
        //{
        //    removedGameObjects.Add(gameObject);
        //}

        #endregion Old GameManager
    }
}
