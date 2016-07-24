using System.Collections.Generic;
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
        private World World1;
        private World World2;
        private World World3;

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
            World1 = new World(42);
            World2 = new World(100);
            World3 = new World(1982);
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
            Renderer.ResetTransformation();
            Renderer.Translate(new Vector2(200, -300));
            RenderTree(World1.Tree);
            Renderer.Translate(new Vector2(150, 0));
            RenderTree(World2.Tree);
            Renderer.Translate(new Vector2(150, 0));
            RenderTree(World3.Tree);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void RenderTree(ProceduralTree tree)
        {
            DrawBranch(tree.Stem);

            foreach (var mainBranch in tree.Stem.Branches)
            {
                DrawBranchSegments(mainBranch);
            }
        }

        private void DrawBranchSegments(ProceduralTreeBranch mainBranch)
        {
            Stack<ProceduralTreeBranch> frontier = new Stack<ProceduralTreeBranch>();
            frontier.Push(mainBranch);

            while (frontier.Any())
            {
                var nextBranch = frontier.Pop();
                DrawBranch(nextBranch);

                foreach (var branch in nextBranch.Branches)
                {
                    frontier.Push(branch);
                }
            }
        }

        private void DrawBranch(ProceduralTreeBranch branch)
        {
            Renderer.DrawVector(spriteBatch, branch.Origin, branch.Vector, Color.Black, branch.Thickness);
        }
    }
}
