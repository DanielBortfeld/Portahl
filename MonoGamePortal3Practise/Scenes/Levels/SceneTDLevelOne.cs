using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    class SceneTDLevelOne : Scene
    {
        private TopDownVictoryTrigger victoryTrigger;
        private TopDownWeightedCompanionCube cubeTheOneAndOnly;

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheetTD");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheetTD.xml");

            TopDownMap chamberOne = new TopDownMap("ChamberOne");
            chamberOne.LoadMapFromImage(GameManager.LoadTexture2D("PortalChamberOneTilesDEBUG"));
            chamberOne.LoadSpritesFromImage(GameManager.LoadTexture2D("PortalChamberOneSprites"));

            TopDownPlayer player = new TopDownPlayer(new Vector2(1, 3));

            victoryTrigger = (TopDownVictoryTrigger)FindGameObject("VictoryTrigger");
            victoryTrigger.OnActivation += OnVictory;

            AssignTriggers();

            cubeTheOneAndOnly = ((TopDownWeightedCompanionCube)FindGameObject("Cube"));

            GameManager.SetPreferredBackBufferSize(chamberOne.Width * chamberOne.TileWidth, chamberOne.Height * chamberOne.TileHeight);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (cubeTheOneAndOnly.IsActive == false)
                cubeTheOneAndOnly.Respawn();
        }

        private void AssignTriggers()
        {
            List<TopDownTriggerableObject> conds = new List<TopDownTriggerableObject>();
            List<TopDownTrigger> triggers = new List<TopDownTrigger>();
            foreach (var item in addedGameObjects)
            {
                if (item is TopDownTrigger)
                    triggers.Add((TopDownTrigger)item);
                if (item is TopDownTriggerableObject)
                    conds.Add((TopDownTriggerableObject)item);
            }
            conds.Find(c => c.Name.Contains("Grill") && c.ID == 1).AssignTrigger(triggers.Find(t => t.Name.Contains("Button") && t.ID == 1));
        }

        private void OnVictory()
        {
            SceneManager.LoadScene<SceneSideScroller>();
            victoryTrigger.OnActivation -= OnVictory;
        }
    }
}
