// Copyright (c) 2016 Daniel Bortfeld
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MonoGamePortal3Practise
{
    class FinalScreen : Scene
    {
        private Texture2D splashScreen;
        private UIButton button;

        public override void LoadContent()
        {
            splashScreen = GameManager.LoadTexture2D("finalscreen");
            button = new UIButton(splashScreen);
            button.OnLeftClick += OnClick;
            button.OnRightClick += OnClick;

            string firstVerse = "That was a triumph!\nI'm making a note here:\nHuge success!\n\nIt's hard to overstate\nmy satisfaction.";

            UILabel endText = new UILabel(Fonts.MonkirtaPursuitNC, firstVerse, new Vector2(250, 250), Color.DarkGoldenrod, 0.5f);
            endText.AddShadow(Color.Black, new Vector2(2, 2));

            UILabel creditsProg = new UILabel(Fonts.MonkirtaPursuitNC, "Programming:\n  Daniel Bortfeld", new Vector2(1080, 420), Color.DarkGoldenrod, 0.5f);
            creditsProg.AddShadow(Color.Black, new Vector2(2, 2));

            UILabel creditsArt = new UILabel(Fonts.MonkirtaPursuitNC, "Art:\n  Neele Luckmann", new Vector2(1250, 666), Color.DarkGoldenrod, 0.5f);
            creditsArt.AddShadow(Color.Black, new Vector2(2, 2));

            GameManager.SetPreferredBackBufferSize(1920, 1080);
        }

        public override void UnloadContent()
        {
            button.OnLeftClick -= OnClick;
            button.OnRightClick -= OnClick;
        }

        private void OnClick()
        {
            button.OnLeftClick -= OnClick;
            button.OnRightClick -= OnClick;
            //SceneManager.LoadScene<TitleScreen>();
            Process.Start("https://youtu.be/Y6ljFaKRTrI?t=7s");
            GameManager.Exit();
        }
    }
}
