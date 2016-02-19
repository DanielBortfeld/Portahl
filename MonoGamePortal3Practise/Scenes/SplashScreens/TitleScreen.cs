using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGamePortal3Practise
{
    class TitleScreen : Scene
    {
        private Texture2D splashScreen;
        private UIButton button;
        private UILabel title;

        public override void LoadContent()
        {
            splashScreen = GameManager.LoadTexture2D("background");
            button = new UIButton(splashScreen);
            button.OnLeftClick += OnClick;
            button.OnRightClick += OnClick;

            title = new UILabel(Fonts.MonkirtaPursuitNC, "PORTAHL", new Vector2(100, 100), Color.White, 1);

            GameManager.SetPreferredBackBufferSize(1920, 1080);
            GameManager.ToggleFullScreen();
        }

        void OnClick()
        {
            button.OnLeftClick -= OnClick;
            button.OnRightClick -= OnClick;
            SceneManager.LoadScene<SceneTDLevelOne>();
        }
    }
}
