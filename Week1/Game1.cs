using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Week1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        //private SpriteBatch _spriteBatch;

        VertexPositionColor[] colorVertices;
        BasicEffect colorEffect;

        Matrix worldTransform;
        Matrix view;
        Matrix projection;

        Texture2D texture;
        int[] indices;

        VertexBuffer vbuffer;
        IndexBuffer ibuffer;


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

            texture = Content.Load<Texture2D>("test_texture");

            //colorVertices = new VertexPositionColorTexture[4];

            //colorVertices[0] = new VertexPositionColorTexture(new Vector3(1, -1, 0), Color.White, new Vector2(1, 1));
            //colorVertices[1] = new VertexPositionColorTexture(new Vector3(-1, -1, 0), Color.White, new Vector2(0, 1));
            //colorVertices[2] = new VertexPositionColorTexture(new Vector3(-1, 1, 0), Color.White, new Vector2(0, 0));
            //colorVertices[3] = new VertexPositionColorTexture(new Vector3(1, 1, 0), Color.White, new Vector2(1, 0));

            //indices = new int[6];

            //indices[0] = 0;
            //indices[1] = 1;
            //indices[2] = 2;


            //indices[3] = 0;
            //indices[4] = 2;
            //indices[5] = 3;

            colorVertices = new VertexPositionColor[4];

            colorVertices[0] = new VertexPositionColor(new Vector3(0, 0, 0), Color.White);//middle
            colorVertices[1] = new VertexPositionColor(new Vector3(0, 1, 0), Color.White);//top
            colorVertices[2] = new VertexPositionColor(new Vector3(1, -1, 0), Color.White);//right
            colorVertices[3] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.White);//left

            indices = new int[9];

            indices[0] = 0;
            indices[1] = 1; // red
            indices[2] = 2;

            indices[3] = 2;
            indices[4] = 3; // yellow
            indices[5] = 0;

            indices[6] = 0;
            indices[7] = 3; // green
            indices[8] = 1;


            vbuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColorTexture), colorVertices.Length, BufferUsage.WriteOnly);
            ibuffer = new IndexBuffer(GraphicsDevice, IndexElementSize.ThirtyTwoBits, indices.Length, BufferUsage.WriteOnly);

            vbuffer.SetData(colorVertices);
            ibuffer.SetData(indices);


            colorEffect = new BasicEffect(GraphicsDevice);
            colorEffect.VertexColorEnabled = true;
            colorEffect.TextureEnabled = true;
            colorEffect.Texture = texture;


            worldTransform = Matrix.Identity * Matrix.CreateTranslation(0,0,-2);

            view = Matrix.CreateLookAt(new Vector3(0,1.8f, 5), new Vector3(0, 0, -1), Vector3.Up);

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(80), GraphicsDevice.DisplayMode.AspectRatio, 0.01f, 1000f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //_spriteBatch = new SpriteBatch(GraphicsDevice);

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

            GraphicsDevice.Indices = ibuffer;
            GraphicsDevice.SetVertexBuffer(vbuffer);

            foreach (EffectPass pass in colorEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, colorVertices, 0, colorVertices.Length, indices, 0, indices.Length / 9);
            }

            base.Draw(gameTime);
        }
    }
}
