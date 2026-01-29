using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace Week_2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        BasicModelObject customModel;
        Matrix view;
        Matrix projection;
        Vector3 cameraPosition = new Vector3(0, 30, 0);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + Vector3.Forward, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90),
                GraphicsDevice.Adapter.CurrentDisplayMode.AspectRatio, 0.25f, 1000f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            customModel = new BasicModelObject("WindTurbine", Matrix.Identity * Matrix.CreateTranslation(0, 0, -40));

            customModel.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            customModel.Update();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            customModel.Draw(view, projection);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
