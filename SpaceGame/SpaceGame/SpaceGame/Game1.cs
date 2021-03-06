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

        Texture2D deathSplash;
        Texture2D pauseSplash;

        SpriteFont mainFont;

        SoundEffect death;

        Boolean paused = false;
        Boolean defeat = false;
        Boolean win = false;

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

            mainFont = Content.Load<SpriteFont>("mainfont");

            List<Room> rooms = new List<Room>();

            rooms.Add(Content.Load<Room>("Levels/Start"));  
            rooms.Add(Content.Load<Room>("Levels/LD1"));
            rooms.Add(Content.Load<Room>("Levels/LR1"));
            rooms.Add(Content.Load<Room>("Levels/LU1"));
            rooms.Add(Content.Load<Room>("Levels/LUR1")); 
            rooms.Add(Content.Load<Room>("Levels/RD1"));   
            rooms.Add(Content.Load<Room>("Levels/UR1"));
            rooms.Add(Content.Load<Room>("Levels/UD1"));

            /* Added by ship gen
            rooms.Add(Content.Load<Room>("Levels/R1"));
            rooms.Add(Content.Load<Room>("Levels/L1"));
            rooms.Add(Content.Load<Room>("Levels/D1"));
            rooms.Add(Content.Load<Room>("Levels/U1"));
             * */

            deathSplash = Content.Load<Texture2D>(FileNames.deathSplash);
            pauseSplash = Content.Load<Texture2D>(FileNames.pauseSplash);

            Song track1 = Content.Load<Song>(FileNames.phase1);
            Song track2 = Content.Load<Song>(FileNames.phase2);
            Song track3 = Content.Load<Song>(FileNames.phase3);

            //death = Content.Load<SoundEffect>("Sounds/death");

            Song[] songs = new Song[] { track1, track2, track3 };

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
        /// 
        private int inputDelay = 0;
        private int defeatDelay = 0;
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.Space))
            {
                if (defeat)
                {
                    currentShip = null;
                    Content.Unload();
                    this.LoadContent();
                    defeat = false;
                }
            }
            else if (key.IsKeyDown(Keys.P))
            {
                if (inputDelay > 20)
                {
                    if (paused) { paused = false; }
                    else { paused = true; }
                    inputDelay = 0;
                }
            }

            if (currentShip.defeat)
            {
                //MediaPlayer.Pause();
                
               // death.Play();

                defeatDelay++;
                if (defeatDelay > 100)
                {
                    this.defeat = true;
                    defeatDelay = 0;
                }
            }

            if (!paused)
            {
                currentShip.Update(gameTime);

                // TODO: Add your update logic here
                base.Update(gameTime);
            }

            inputDelay++;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        private int redLevel = 255;
        private int dir = -1;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(105, 105, 105));
            Color toUse = Color.White;

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (paused)
            {
                spriteBatch.Draw(pauseSplash, new Rectangle(0,0,1024,768), Color.White);
            }
            else if (defeat)
            {
                spriteBatch.Draw(deathSplash, new Rectangle(0,0,1024,768), Color.White);
                spriteBatch.DrawString(mainFont, "Press Space to Continue", new Vector2(300, 700), Color.White);
            }
            else if (currentShip.alarm)
            {
                
                redLevel += dir;
                if (redLevel >= 254)
                {
                    dir = -2;
                }
                if (redLevel <= 100)
                {
                    dir = 2;
                }
                toUse = new Color(255, redLevel, redLevel);
                currentShip.Render(spriteBatch, new Vector2(), toUse);
            }
            else
            {
                currentShip.Render(spriteBatch, new Vector2(), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
