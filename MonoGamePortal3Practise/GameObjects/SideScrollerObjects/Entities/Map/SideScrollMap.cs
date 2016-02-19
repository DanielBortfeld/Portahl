using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;

namespace MonoGamePortal3Practise
{
    class SideScrollMap : GameObject
    {
        private Floor floor;
        private Wall leftWall;
        private Wall rightWall;
        private List<Entity> mapObjects = new List<Entity>();
        public Texture2D Background { get; private set; }

        public SideScrollMap(string name)
        {
            Name = name;
            Tag = "SSMap";
            Background = GameManager.LoadTexture2D("backgroundForest");
        }

        public override void LoadContent()
        {
            floor = new Floor(0, Background.Height, Background.Width);
            leftWall = new Wall(-10, 0, Background.Height);
            rightWall = new Wall(Background.Width, 0, Background.Height);

            LoadMapObjects(GameManager.Content.RootDirectory + "/PortalChamberTwoMapSprites.xml");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, Background.Bounds, White);
            mapObjects.ForEach(obj => obj.Draw(spriteBatch));
        }

        public override void Update(GameTime gameTime)
        {
        }

        public void LoadMapObjects(string dataPath)
        {
            XmlReader xmlReader = XmlReader.Create(dataPath);

            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement("Object"))
                {
                    string typeName = xmlReader.GetAttribute("type");
                    int x = Convert.ToInt32(xmlReader.GetAttribute("x"));
                    int y = Convert.ToInt32(xmlReader.GetAttribute("y"));
                    switch (typeName)
                    {
                        case "WhiteWall":
                            mapObjects.Add(new WhiteWall(x, y));
                            break;
                        case "Cube":
                            mapObjects.Add(new WeightedCompanionCube(x, y));
                            break;
                        case "Platform_Left":
                        case "Platform_Down":
                            mapObjects.Add(new Platform(x, y, typeName));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
