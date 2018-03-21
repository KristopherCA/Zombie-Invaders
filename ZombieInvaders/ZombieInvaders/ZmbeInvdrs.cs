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

namespace ZombieInvaders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ZmbeInvdrs : Microsoft.Xna.Framework.Game

    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteManager spriteManager;
        

        

    enum ZombieInvaders { Strt, Gme, Wn, GmeOvr };

        ZombieInvaders state = ZombieInvaders.Strt;//set initial state


        int numlives = 1;
        public int NumLives
        {
            get { return numlives; }
            set
            {
                numlives = value;

                if (numlives == 0)
                {
                    state = ZombieInvaders.GmeOvr;
                    spriteManager.Enabled = false;
                    spriteManager.Visible = false;
                    
                }
            }
        }




        Texture2D strtScrn;
        Texture2D psdScrn;
        SoundEffect theme = null;

        Texture2D gmeScrn;
        Texture2D wnScrn;
        Texture2D gmeovrScrn;
        SpriteFont overF;
        Vector2 overVect;
        Boolean wasPDown = false;

        Texture2D bckgrnd;  // draw the background

        Rectangle scrnSze = new Rectangle(0, 0, 900, 686); //set screen size

        const int ANYKEYCOUNTDOWN = 800;
        int anykeytimer = 0;
       

        public ZmbeInvdrs()
        {
            gfx = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            gfx.PreferredBackBufferWidth = 900; //set preferred width
            gfx.PreferredBackBufferHeight = 686; //set preferred height

            
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
            this.IsMouseVisible = true;

            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);

            spriteManager.Enabled = false;
            spriteManager.Visible = false;
         

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

            this.IsMouseVisible = true;//make mouse visible

            psdScrn = Content.Load<Texture2D>(@"Images\psdScrn");
            strtScrn = Content.Load<Texture2D>(@"Images\strtScrn");
            gmeovrScrn = Content.Load<Texture2D>(@"Images\gmeovrScrn");
            theme = Content.Load<SoundEffect>(@"Audio\theme");
            //theme.Play();
            SoundEffectInstance themeInstance = theme.CreateInstance();
            themeInstance.IsLooped = true; 
            themeInstance.Play(); 
            if (themeInstance.State == SoundState.Playing)
                themeInstance.Resume();
            else
                themeInstance.Stop();
            gmeScrn = Content.Load<Texture2D>(@"Images\gmeScrn");
            overF = Content.Load<SpriteFont>(@"Fonts\gameoverFont");
            Vector2 size = overF.MeasureString("Game Over!");
            overVect = new Vector2((Window.ClientBounds.Width - size.X) / 2, (Window.ClientBounds.Height - size.Y) / 2);
            bckgrnd = strtScrn;

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
            KeyboardState kySte = Keyboard.GetState();

            if (kySte.IsKeyDown(Keys.Escape))
                this.Exit();

            MouseState msSte = Mouse.GetState();
            this.Window.Title = "Zombie Invaders (" + msSte.X + ", " + msSte.Y + ")";
            var msePstn = new Point(msSte.X, msSte.Y);

            Rectangle strtBtn = new Rectangle(243, 464, (243 - -384), (464 - -510));
            Rectangle extBtn = new Rectangle(475, 462, (475 - -623), (462 - -509));

            if (strtBtn.Contains(msePstn) && msSte.LeftButton == ButtonState.Pressed && bckgrnd == strtScrn)
            {
                state = ZombieInvaders.Gme; //set state to game srreen and enable sprite manager
                spriteManager.Enabled = true;
                spriteManager.Visible = true;
                
            }
            if (extBtn.Contains(msePstn) && msSte.LeftButton == ButtonState.Pressed && bckgrnd == strtScrn)
                this.Exit();

            if (strtBtn.Contains(msePstn) && msSte.LeftButton == ButtonState.Pressed && bckgrnd == gmeovrScrn)
            {
                state = ZombieInvaders.Gme; //set state to game srreen and enable sprite manager
                spriteManager.Enabled = true;
                spriteManager.Visible = true;    
            }

            if (extBtn.Contains(msePstn) && msSte.LeftButton == ButtonState.Pressed && bckgrnd == gmeovrScrn)
                this.Exit();

           
         // TODO: Add your update logic here
           

            switch (state) //switch statement for various screens
            {
                case ZombieInvaders.Strt:
                anykeytimer -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (anykeytimer <= 0)
                    {
                        anykeytimer = ANYKEYCOUNTDOWN;
                       // anykey.Play();
                    }
                    if (Keyboard.GetState().GetPressedKeys().Length > 0)
                        state = ZombieInvaders.Gme;
                    bckgrnd = strtScrn;
                    break;
                    
                    
                case ZombieInvaders.Gme:
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                        wasPDown = true;
                    else
                        if (wasPDown)
                        {
                            wasPDown = false;
                            state = ZombieInvaders.Strt;
                        }
                    base.Update(gameTime);
                    bckgrnd = gmeScrn;
                    break;
                case ZombieInvaders.Wn:
                    bckgrnd = wnScrn;
                    break;
                case ZombieInvaders.GmeOvr:
                    bckgrnd = gmeovrScrn;

                    base.Update(gameTime);  // update the components
                    break;
            }
         
         

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(bckgrnd, scrnSze, Color.White);
            
     
                

            if (state == ZombieInvaders.GmeOvr)
            {
                 spriteBatch.DrawString(overF, "GAME OVER!", overVect, Color.Red);
                 numlives = 1;
        
              
            }

          
         
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public GraphicsDeviceManager gfx { get; set; }
    }
}
