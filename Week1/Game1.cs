using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Week1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        VertexPositionColor[] colorVertices;
        BasicEffect colorEffect;

        Matrix worldTransform;
        Matrix view;
        Matrix projection;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_graphics.PreferredBackBufferHeight = 1080;
            //_graphics.PreferredBackBufferWidth = 1920;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            colorVertices = new VertexPositionColor[6];

            colorVertices[0] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Red);
            colorVertices[1] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Green);
            colorVertices[2] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Blue);

            colorVertices[3] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Red);
            colorVertices[4] = new VertexPositionColor(new Vector3(-1, 1, 0), Color.Green);
            colorVertices[5] = new VertexPositionColor(new Vector3(1, 1, 0), Color.Blue);

            colorEffect = new BasicEffect(GraphicsDevice);
            colorEffect.VertexColorEnabled = true;
            colorEffect.TextureEnabled = false;

            worldTransform = Matrix.Identity * Matrix.CreateTranslation(0,0,-2);

            view = Matrix.CreateLookAt(new Vector3(0,1.8f, 5), new Vector3(0, 0, -1), Vector3.Up);

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(80), GraphicsDevice.DisplayMode.AspectRatio, 0.01f, 1000f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            colorEffect.View = view;
            colorEffect.Projection = projection;
            colorEffect.World = worldTransform;

            // TODO: Add your drawing code here

            foreach (EffectPass pass in colorEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, colorVertices, 0,colorVertices.Length / 3);
            }

            base.Draw(gameTime);
        }
    }
}
