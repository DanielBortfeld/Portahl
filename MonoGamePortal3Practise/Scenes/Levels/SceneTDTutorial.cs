using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    class SceneTDTutorial : Scene
    {
        private TopDownVictoryTrigger victoryTrigger;
        private TopDownHeavyDutySuperCollidingSuperButton buttonID3;
        private TopDownWeightedCompanionCube cubeID1;
        private TopDownWeightedCompanionCube cubeID2;

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheetOne");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheetOne.xml");

            TopDownMap tutorialMap = new TopDownMap("Tutorial");
            tutorialMap.LoadMapFromImage(GameManager.LoadTexture2D("PortalChamberTutorialTiles"));
            tutorialMap.LoadSpritesFromImage(GameManager.LoadTexture2D("PortalChamberTutorialSprites"));

            TopDownPlayer player = new TopDownPlayer(new Vector2(1, 6));

            AssignTriggers();
            SetCubeRespawnability();

            victoryTrigger.OnActivation += OnVictory;
            buttonID3.OnActivation += buttonID3_OnActivation;

            GameManager.SetPreferredBackBufferSize(tutorialMap.Width * tutorialMap.TileWidth, tutorialMap.Height * tutorialMap.TileHeight);
        }

        private void buttonID3_OnActivation()
        {
            cubeID2.Respawn();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(!(GameObjects[GameObjects.Count-1] is TopDownPlayer))
            {
                GameObject temp = FindGameObject("Chell");
                temp.Destroy();
                AddGameObject(temp);
            }

            if (cubeID1.IsActive == false && cubeID1.IsRespawnable == true)
                cubeID1.Respawn();
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
            conds.Find(c => c.Name.Contains("Grill") && c.ID == 2).AssignTrigger(triggers.Find(t => t.Name.Contains("Button") && t.ID == 2));
            conds.Find(c => c.Name.Contains("Grill") && c.ID == 3).AssignTrigger(triggers.Find(t => t.Name.Contains("Button") && t.ID == 1));
            conds.Find(c => c.Name.Contains("Grill") && c.ID == 5).AssignTrigger(triggers.Find(t => t.Name.Contains("Button") && t.ID == 4));
            conds.Find(c => c.Name.Contains("Grill") && c.ID == 8).AssignTrigger(triggers.Find(t => t.Name.Contains("Button") && t.ID == 4));

            buttonID3 = (TopDownHeavyDutySuperCollidingSuperButton)triggers.Find(t => t.Name.Contains("Button") && t.ID == 3);
            victoryTrigger = (TopDownVictoryTrigger)triggers.Find(t => t.Name.Contains("VictoryTrigger"));
        }

        private void SetCubeRespawnability()
        {
            List<TopDownWeightedCompanionCube> cubes = new List<TopDownWeightedCompanionCube>();
            foreach (var item in addedGameObjects)
                if (item is TopDownWeightedCompanionCube)
                    cubes.Add((TopDownWeightedCompanionCube)item);

            cubes.Find(c => c.ID == 3).IsRespawnable = false;
            cubes.Find(c => c.ID == 4).IsRespawnable = false;
            cubeID1 = cubes.Find(c => c.ID == 1);
            cubeID2 = cubes.Find(c => c.ID == 2);
        }

        private void OnVictory()
        {
            SceneManager.LoadScene<SceneTDLevelOne>();
            victoryTrigger.OnActivation -= OnVictory;
            buttonID3.OnActivation -= buttonID3_OnActivation;
        }
    }
}
