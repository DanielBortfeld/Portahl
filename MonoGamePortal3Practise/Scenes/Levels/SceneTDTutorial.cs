// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonoGamePortal3Practise
{
    class SceneTDTutorial : Scene
    {
        private TopDownPlayer player;

        private List<TopDownTriggerableObject> triggeredObjs = new List<TopDownTriggerableObject>();
        private List<TopDownTrigger> triggers = new List<TopDownTrigger>();

        private VictoryTrigger victoryTrigger;
        private TopDownHeavyDutySuperCollidingSuperButton buttonID2;
        private TopDownHeavyDutySuperCollidingSuperButton buttonID3;
        private TopDownWeightedCompanionCube cubeID1;
        private TopDownWeightedCompanionCube cubeID2;
        private bool cube3isVaporised = false;

        private Texture2D textboxBackground;
        private Texture2D textboxButton;
        private Vector2 textboxPosition = new Vector2(135, 165);
        private float maxLineWidth = 575f;

        public override void LoadContent()
        {
            SpriteSheet = GameManager.LoadTexture2D("SpriteSheetTD");
            LoadSprites(GameManager.Content.RootDirectory + "/spritesheetTD.xml");

            TopDownMap tutorialMap = new TopDownMap("Tutorial");
            tutorialMap.LoadMapFromImage(GameManager.LoadTexture2D("PortalChamberTutorialTiles"));
            tutorialMap.LoadSpritesFromImage(GameManager.LoadTexture2D("PortalChamberTutorialSprites"));

            textboxBackground = GameManager.LoadTexture2D("TEXTBOX3");
            textboxButton = GameManager.LoadTexture2D("BUTTON");

            player = new TopDownPlayer(new Vector2(1, 5));
            player.OnStepOnToxicGoo += Player_OnDeathThroughToxicGoo;
            player.OnTraversingEmancipationGrill += Player_OnTraversingEmancipationGrill;

            AssignTriggers();
            SetCubeRespawnability();
            SetWelcomeText();

            victoryTrigger.OnActivation += OnVictory;
            buttonID2.OnActivation += ButtonID2_OnActivation;
            buttonID3.OnActivation += ButtonID3_OnActivation;

            GameManager.SetPreferredBackBufferSize(tutorialMap.Width * tutorialMap.TileWidth, tutorialMap.Height * tutorialMap.TileHeight);
        }

        public override void UnloadContent()
        {
            player.OnStepOnToxicGoo -= Player_OnDeathThroughToxicGoo;
            player.OnTraversingEmancipationGrill -= Player_OnTraversingEmancipationGrill;
            victoryTrigger.OnActivation -= OnVictory;
            buttonID2.OnActivation -= ButtonID2_OnActivation;
            buttonID3.OnActivation -= ButtonID3_OnActivation;
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 1).OnTraversingEmancipationGrill -= OnTraversingGrillID1;
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 2).OnTraversingEmancipationGrill -= OnTraversingGrillID2;
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 7).OnTraversingEmancipationGrill -= OnTraversingGrillID7;
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 9).OnTraversingEmancipationGrill -= OnTraversingGrillID9;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!(GameObjects[GameObjects.Count - 1] is TopDownPlayer))
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
            foreach (var item in addedGameObjects)
            {
                if (item is TopDownTrigger)
                    triggers.Add((TopDownTrigger)item);
                if (item is TopDownTriggerableObject)
                    triggeredObjs.Add((TopDownTriggerableObject)item);
            }
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 1).OnTraversingEmancipationGrill += OnTraversingGrillID1;
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 2).OnTraversingEmancipationGrill += OnTraversingGrillID2;
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 7).OnTraversingEmancipationGrill += OnTraversingGrillID7;
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 9).OnTraversingEmancipationGrill += OnTraversingGrillID9;

            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 2).AssignTrigger(triggers.Find(t => t.Name.Contains("Button") && t.ID == 2));
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 3).AssignTrigger(triggers.Find(t => t.Name.Contains("Button") && t.ID == 1));
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 5).AssignTrigger(triggers.Find(t => t.Name.Contains("Button") && t.ID == 4));
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 8).AssignTrigger(triggers.Find(t => t.Name.Contains("Button") && t.ID == 4));

            buttonID2 = (TopDownHeavyDutySuperCollidingSuperButton)triggers.Find(t => t.Name.Contains("Button") && t.ID == 2);
            buttonID3 = (TopDownHeavyDutySuperCollidingSuperButton)triggers.Find(t => t.Name.Contains("Button") && t.ID == 3);
            victoryTrigger = (VictoryTrigger)triggers.Find(t => t.Name.Contains("VictoryTrigger"));
        }

        /// <summary>
        /// Determines if a cube can respawn after its destruction.
        /// </summary>
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

        private void SetWelcomeText()
        {
            string title = "Welcome to the Test-Facility! ";
            string welcomeText = "Use WASD to move and the Left and Right Mouse Button to shoot portals. You always shoot in the direction you last moved into. Portals can only appear on the White Walls.";
            UILabel UITitle = new UILabel(Fonts.Verdana, title, new Vector2(36, 12 * 32 + 4), 15 * 32, Color.White, 0.225f);
            UILabel UIWelcomeText = new UILabel(Fonts.Verdana, welcomeText, new Vector2(36, 13 * 32), 15 * 32, Color.White, 0.225f);

            UITitle.AddShadow(Color.Black, new Vector2(1, 1));
            UIWelcomeText.AddShadow(Color.Black, new Vector2(1, 1));

            UITextBox textBox = new UITextBox(textboxPosition, title, welcomeText, maxLineWidth, textboxBackground, textboxButton);
            textBox.Show();
        }

        private void Player_OnTraversingEmancipationGrill()
        {
            string grillText = "Everything traversing an Vaporization Grill will get vaporised (except you). If You pass through it, your portals will be closed.";
            UILabel UIGrillText = new UILabel(Fonts.Verdana, grillText, new Vector2(36, 19.5f * 32), 15 * 32, Color.Cyan, 0.23f);

            UIGrillText.AddShadow(Color.Black, new Vector2(1, 1));

            UITextBox textBox = new UITextBox(textboxPosition, "The Vaporization Grill", grillText, maxLineWidth, textboxBackground, textboxButton);
            textBox.Show();

            player.OnTraversingEmancipationGrill -= Player_OnTraversingEmancipationGrill;
        }

        private void Player_OnDeathThroughToxicGoo()
        {
            string gooText = "Ooops, you stepped into the Toxic Goo. I don't know why it is in this Chamber, but keep away from it! You can't swim! ...And it's toxic.";
            UILabel UIdeathText = new UILabel(Fonts.Verdana, gooText, new Vector2(36, 16 * 32 + 8), 15 * 32, Color.Green, 0.225f);

            UIdeathText.AddShadow(Color.Black, new Vector2(1, 1));

            UITextBox textBox = new UITextBox(textboxPosition, "Toxic Goo", gooText, maxLineWidth, textboxBackground, textboxButton);
            textBox.Show();

            player.OnStepOnToxicGoo -= Player_OnDeathThroughToxicGoo;
        }

        private void OnTraversingGrillID2()
        {
            string buttonText = "Hey, do you see this Box with the Heart on it? Try pushing it onto that big Red Button to deactivate the Grill below.";
            UILabel UIButtonText = new UILabel(Fonts.Verdana, buttonText, new Vector2(23 * 32 + 4, 36), 16 * 32, Color.Violet, 0.23f);

            UIButtonText.AddShadow(Color.Black, new Vector2(1, 1));

            UITextBox textBox = new UITextBox(textboxPosition, "Buttons and Cubes", buttonText, maxLineWidth, textboxBackground, textboxButton);
            textBox.Show();

            player.StandartPosition = player.Position;
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 2).OnTraversingEmancipationGrill -= OnTraversingGrillID2;
        }

        private void OnTraversingGrillID7()
        {
            string puzzleText = "To finish your first Test, just solve that simple Cube Sliding Puzzle in front of you. (The Red Button in this room resets the cube in front of you.)";
            UITextBox textBox = new UITextBox(textboxPosition, "Sliding Puzzles", puzzleText, maxLineWidth, textboxBackground, textboxButton);

            if (cube3isVaporised)
            {
                player.StandartPosition = player.Position;

                UILabel UISlidingPuzzleText = new UILabel(Fonts.Verdana, puzzleText, new Vector2(23 * 32 + 4, 8 * 32 - 4), 16 * 32, Color.Red, 0.21f);
                UISlidingPuzzleText.AddShadow(Color.Black, new Vector2(1, 1));
                textBox.Show();

                triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 7).OnTraversingEmancipationGrill -= OnTraversingGrillID7;
            }
            cube3isVaporised = true;
        }

        private void OnTraversingGrillID9()
        {
            string cubeText = "Cubes can't swim, too. If you push them into the Toxic Goo, they're gone. If you push them into the Vaporization Grill they'll get vaporised and are gone as well.       Try it!";
            UILabel UICubeText = new UILabel(Fonts.Verdana, cubeText, new Vector2(26 * 32 + 4, 4 * 32 - 4), 13 * 32, Color.LightPink, 0.21f);

            UICubeText.AddShadow(Color.Black, new Vector2(1, 1));

            UITextBox textBox = new UITextBox(textboxPosition, "The Comrade Cube", cubeText, maxLineWidth, textboxBackground, textboxButton);
            textBox.Show();

            player.StandartPosition = player.Position;
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 9).OnTraversingEmancipationGrill -= OnTraversingGrillID9;
        }

        private void ButtonID2_OnActivation()
        {
            cubeID1.Respawn();
        }

        private void ButtonID3_OnActivation()
        {
            cubeID2.Respawn();
        }

        private void OnTraversingGrillID1()
        {
            player.StandartPosition = player.Position;
            triggeredObjs.Find(c => c.Name.Contains("Grill") && c.ID == 1).OnTraversingEmancipationGrill -= OnTraversingGrillID1;
        }

        private void OnVictory()
        {
            SceneManager.LoadScene<SceneTDLevelOne>();
            victoryTrigger.OnActivation -= OnVictory;
            buttonID2.OnActivation -= ButtonID2_OnActivation;
            buttonID3.OnActivation -= ButtonID3_OnActivation;
            player.OnStepOnToxicGoo -= Player_OnDeathThroughToxicGoo;
        }
    }
}
