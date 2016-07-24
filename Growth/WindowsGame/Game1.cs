using System.Linq;
using GameWorld;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoShims;
using VectorMath;
using Vector2 = VectorMath.Vector2;

namespace WindowsGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Renderer Renderer;
        private World World;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            World = new World(42);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Renderer = new Renderer(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Lets draw a tree
            spriteBatch.Begin();
            var origin = new Vector2(300, -300);
            DrawBranch(origin, World.Tree.Stem);

            origin += World.Tree.Stem.Vector;
            foreach (var mainBranch in World.Tree.Stem.Branches)
            {
                DrawBranchSegments(origin, mainBranch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBranchSegments(Vector2 origin, ProceduralTreeBranch mainBranch)
        {
            var position = origin;
            DrawBranch(position, mainBranch);
            position += mainBranch.Vector;

            var branch = mainBranch;
            while((branch = branch.Branches.FirstOrDefault()) != null)
            {
                DrawBranch(position, branch);
                position += branch.Vector;
            }
        }

        private void DrawBranch(Vector2 origin, ProceduralTreeBranch branch)
        {
            Renderer.DrawVector(spriteBatch, origin, branch.Vector, Color.Black, branch.Thickness);
        }
    }
}
