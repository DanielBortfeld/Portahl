using Microsoft.Xna.Framework;
using System;

namespace MonoGamePortal3Practise
{
    class SceneTutorialTD : Scene
    {
        private TopDownVictoryTrigger victoryTrigger;

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheetOne");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheetOne.xml");

            TopDownMap tutorialMap = new TopDownMap("Tutorial");
            tutorialMap.LoadMapFromImage(GameManager.LoadTexture2D("PortalChamberTutorialTiles"));
            tutorialMap.LoadSpritesFromImage(GameManager.LoadTexture2D("PortalChamberTutorialSprites"));

            TopDownPlayer player = new TopDownPlayer(new Vector2(1, 6));

            victoryTrigger = (TopDownVictoryTrigger)addedGameObjects.Find(g => g.Name.Contains("VictoryTrigger"));
            victoryTrigger.OnVictory += OnVictory;

            GameManager.SetPreferredBackBufferSize(tutorialMap.Width * tutorialMap.TileWidth, tutorialMap.Height * tutorialMap.TileHeight);
        }

        private void OnVictory()
        {
            SceneManager.LoadScene<SceneLevelOneTD>();
            victoryTrigger.OnVictory -= OnVictory;
        }
    }
}
