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

        //cube
        VertexPositionColor[] cubeVerticies;
        int[] cubeIndices;
        VertexBuffer cubeVBuffer;
        IndexBuffer cubeIBuffer;


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

            colorVertices[0] = new VertexPositionColor(new Vector3(0, 0, 1), Color.Red);//middle
            colorVertices[1] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Green);//top
            colorVertices[2] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Blue);//right
            colorVertices[3] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Yellow);//left

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


            vbuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), colorVertices.Length, BufferUsage.WriteOnly);
            ibuffer = new IndexBuffer(GraphicsDevice, IndexElementSize.ThirtyTwoBits, indices.Length, BufferUsage.WriteOnly);

            vbuffer.SetData(colorVertices);
            ibuffer.SetData(indices);


            colorEffect = new BasicEffect(GraphicsDevice);
            colorEffect.VertexColorEnabled = true;
            colorEffect.TextureEnabled = false;
            colorEffect.Texture = texture;


            //worldTransform = Matrix.Identity * Matrix.CreateTranslation(0,0,-2);

            //worldTransform = Matrix.CreateRotationX(MathHelper.ToRadians(90)) * Matrix.CreateTranslation(0, 0, -2);

            worldTransform = Matrix.Identity;

            view = Matrix.CreateLookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.Up);

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(80), GraphicsDevice.Viewport.AspectRatio, 0.01f, 1000f);


            //cube

            cubeVerticies = new VertexPositionColor[24];

            //face1
            cubeVerticies[0] = new VertexPositionColor(new Vector3(-1, 1, 1), Color.Red);
            cubeVerticies[1] = new VertexPositionColor(new Vector3(1, 1, 1), Color.Red);
            cubeVerticies[2] = new VertexPositionColor(new Vector3(1, -1, 1), Color.Red);
            cubeVerticies[3] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.Red);

            //face2
            cubeVerticies[4] = new VertexPositionColor(new Vector3(1, 1, -1), Color.Green);
            cubeVerticies[5] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.Green);
            cubeVerticies[6] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.Green);
            cubeVerticies[7] = new VertexPositionColor(new Vector3(1, -1, -1), Color.Green);

            //face3
            cubeVerticies[8] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.Blue);
            cubeVerticies[9] = new VertexPositionColor(new Vector3(1, 1, -1), Color.Blue);
            cubeVerticies[10] = new VertexPositionColor(new Vector3(1, 1, 1), Color.Blue);
            cubeVerticies[11] = new VertexPositionColor(new Vector3(-1, 1, 1), Color.Blue);

            //face4
            cubeVerticies[12] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.Yellow);
            cubeVerticies[13] = new VertexPositionColor(new Vector3(1, -1, 1), Color.Yellow);
            cubeVerticies[14] = new VertexPositionColor(new Vector3(1, -1, -1), Color.Yellow);
            cubeVerticies[15] = new VertexPositionColor(new Vector3(-1, -1, -1), Color.Yellow);

            //face5
            cubeVerticies[16] = new VertexPositionColor(new Vector3(-1, 1, -1), Color.Cyan);
            cubeVerticies[17] = new VertexPositionColor(new Vector3(-1, 1, 1), Color.Cyan);
            cubeVerticies[18] = new VertexPositionColor(new Vector3(-1, -1, 1), Color.Cyan);
            cubeVerticies[19] = new VertexPositionColor(new Vector3(-1, -1,-1), Color.Cyan);

            //face6
            cubeVerticies[20] = new VertexPositionColor(new Vector3(1, 1, 1), Color.Magenta);
            cubeVerticies[21] = new VertexPositionColor(new Vector3(1, 1, -1), Color.Magenta);
            cubeVerticies[22] = new VertexPositionColor(new Vector3(1, -1, -1), Color.Magenta);
            cubeVerticies[23] = new VertexPositionColor(new Vector3(1, -1, 1), Color.Magenta);

            cubeIndices = new int[36];

            for (int i = 0; i < 6; i++)
            {
                int x = i * 6;
                int v = i * 4;

                cubeIndices[x] = v;
                cubeIndices[x + 1] = v + 1;
                cubeIndices[x + 2] = v + 2;
                cubeIndices[x + 3] = v;
                cubeIndices[x + 4] = v + 2;
                cubeIndices[x + 5] = v + 3;

            }

            cubeVBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), cubeVerticies.Length, BufferUsage.WriteOnly);
            cubeVBuffer.SetData(cubeVerticies);

            cubeIBuffer = new IndexBuffer(GraphicsDevice, IndexElementSize.ThirtyTwoBits, cubeIndices.Length, BufferUsage.WriteOnly);
            cubeIBuffer.SetData(cubeIndices);

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
            //colorEffect.World = worldTransform;


            // TODO: Add your drawing code here

            //GraphicsDevice.Indices = ibuffer;
            //GraphicsDevice.SetVertexBuffer(vbuffer);

            //foreach (EffectPass pass in colorEffect.CurrentTechnique.Passes)
            //{
            //    pass.Apply();

            //    GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, indices.Length / 3);
            //}

            float time = (float)gameTime.TotalGameTime.TotalSeconds;

            foreach (EffectPass pass in colorEffect.CurrentTechnique.Passes)
            {
                colorEffect.World = Matrix.CreateRotationY(time) * Matrix.CreateTranslation(-2, 0, 0);

                colorEffect.CurrentTechnique.Passes[0].Apply();

                GraphicsDevice.SetVertexBuffer(vbuffer);
                GraphicsDevice.Indices = ibuffer;
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, indices.Length / 3);


                colorEffect.World = Matrix.CreateRotationX(time) * Matrix.CreateRotationY(time) * Matrix.CreateTranslation(2, 0, 0);
                colorEffect.CurrentTechnique.Passes[0].Apply();

                GraphicsDevice.SetVertexBuffer(cubeVBuffer);
                GraphicsDevice.Indices = cubeIBuffer;

                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, cubeVerticies.Length, 0, cubeIndices.Length / 3);



            }

            base.Draw(gameTime);
        }
    }
}
