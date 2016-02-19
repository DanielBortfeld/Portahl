using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    class FinalScreen : Scene
    {
        private Texture2D splashScreen;
        private UIButton button;
        private UILabel endText;

        public override void LoadContent()
        {
            splashScreen = GameManager.LoadTexture2D("background");
            button = new UIButton(splashScreen);
            button.OnLeftClick += OnClick;
            button.OnRightClick += OnClick;

            endText = new UILabel(Fonts.MonkirtaPursuitNC, "That was a triumph!\nI'm making a note here:\nHuge success!\n\nIt's hard to overstate\nmy satisfaction.", new Vector2(100, 75), Color.DarkGoldenrod, 0.5f);

            GameManager.SetPreferredBackBufferSize(1920, 1080);
        }

        void OnClick()
        {
            button.OnLeftClick -= OnClick;
            button.OnRightClick -= OnClick;
            SceneManager.LoadScene<TitleScreen>();
        }
    }
}
