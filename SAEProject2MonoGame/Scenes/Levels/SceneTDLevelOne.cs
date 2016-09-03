// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    class SceneTDLevelOne : Scene
    {
        private VictoryTrigger victoryTrigger;
        private TopDownWeightedCompanionCube cubeTheOneAndOnly;
        private TopDownHeavyDutySuperCollidingSuperButton respawnButton;

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheetTD");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheetTD.xml");

            TopDownMap chamberOne = new TopDownMap("ChamberOne");
            chamberOne.LoadMapFromImage(GameManager.LoadTexture2D("PortalChamberOneTiles"));
            chamberOne.LoadSpritesFromImage(GameManager.LoadTexture2D("PortalChamberOneSprites"));

            TopDownPlayer player = new TopDownPlayer(new Vector2(1, 3));

            victoryTrigger = (VictoryTrigger)FindGameObject("VictoryTrigger");
            victoryTrigger.OnActivation += OnVictory;

            AssignTriggers();

            cubeTheOneAndOnly = ((TopDownWeightedCompanionCube)FindGameObject("Cube"));
            respawnButton.OnActivation += respawnButton_OnActivation;

            GameManager.SetPreferredBackBufferSize(chamberOne.Width * chamberOne.TileWidth, chamberOne.Height * chamberOne.TileHeight);
        }

        public override void UnloadContent()
        {
            victoryTrigger.OnActivation -= OnVictory;
            respawnButton.OnActivation -= respawnButton_OnActivation;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (cubeTheOneAndOnly.IsActive == false)
                cubeTheOneAndOnly.Respawn();
        }

        private void AssignTriggers()
        {
            List<TopDownTriggerableObject> triggerableObj = new List<TopDownTriggerableObject>();
            List<TopDownTrigger> triggers = new List<TopDownTrigger>();
            foreach (var item in addedGameObjects)
            {
                if (item is TopDownTrigger)
                    triggers.Add((TopDownTrigger)item);
                if (item is TopDownTriggerableObject)
                    triggerableObj.Add((TopDownTriggerableObject)item);
            }
            triggerableObj.Find(c => c.Name.Contains("Grill") && c.ID == 1).AssignTrigger(triggers.Find(t => t.Name.Contains("Button") && t.ID == 2));
            respawnButton = (TopDownHeavyDutySuperCollidingSuperButton)triggers.Find(t => t.Name.Contains("Button") && t.ID == 1);
        }

        private void respawnButton_OnActivation(GameObject activator)
        {
            cubeTheOneAndOnly.Respawn();
        }

        private void OnVictory(GameObject activator)
        {
            SceneManager.LoadScene<SceneSideScroller>();
            victoryTrigger.OnActivation -= OnVictory;
        }
    }
}
