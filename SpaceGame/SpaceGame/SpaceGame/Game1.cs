using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaceGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Room testRoom;
        Ship currentShip;

        public static WorldObject hacks = null;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;

            //Console.WriteLine(testRoom.Map.Width);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
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

            List<Room> rooms = new List<Room>();
            rooms.Add(Content.Load<Room>("Levels/room6"));
            rooms.Add(Content.Load<Room>("Levels/doortest1"));
            rooms.Add(Content.Load<Room>("Levels/doortest2"));
            rooms.Add(Content.Load<Room>("Levels/doortest3"));
            rooms.Add(Content.Load<Room>("Levels/doortest4"));
            rooms.Add(Content.Load<Room>("Levels/doortest5"));
            rooms.Add(Content.Load<Room>("Levels/doortest6"));
            rooms.Add(Content.Load<Room>("Levels/doortest7"));

            Song track1 = Content.Load<Song>("Sounds/phase1");
            Song track2 = Content.Load<Song>("Sounds/phase2");
            Song track3 = Content.Load<Song>("Sounds/phase3");

            Song[] songs = new Song[]{track1,track2,track3};

            MediaPlayer.Play(songs[1]);


            //Need to create the first ship here
            currentShip = new Ship(rooms);

            //OLD for debug below
            
            currentShip.Init(Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

          currentShip.Update(gameTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(105,105,105));

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            currentShip.Render(spriteBatch, new Vector2(), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
