using Microsoft.Xna.Framework;
using System;

namespace MonoGamePortal3Practise
{
    class SceneLevelOneTD : Scene
    {
        private TopDownVictoryTrigger victoryTrigger;

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheetOne");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheetOne.xml");

            TopDownMap chamberOne = new TopDownMap("ChamberOne");
            chamberOne.LoadMapFromImage(GameManager.LoadTexture2D("PortalChamberOneTilesDEBUG"));
            chamberOne.LoadSpritesFromImage(GameManager.LoadTexture2D("PortalChamberOneSprites"));

            TopDownPlayer player = new TopDownPlayer(new Vector2(1, 3));

            victoryTrigger = (TopDownVictoryTrigger)addedGameObjects.Find(g => g.Name.Contains("VictoryTrigger"));
            victoryTrigger.OnVictory += OnVictory;

            GameManager.SetPreferredBackBufferSize(chamberOne.Width * chamberOne.TileWidth, chamberOne.Height * chamberOne.TileHeight);
        }

        private void OnVictory()
        {
            SceneManager.LoadScene<SceneSideScroller>();
            victoryTrigger.OnVictory -= OnVictory;
        }
    }
}
