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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        List<SpriteBase> spriteList = new List<SpriteBase>();

        double newrowtimer = 5000;
        double newrowReset = 15000;
        double newrowchange = 5000;
        double lastnewrow = 0;

        ZmbeInvdrs game;
        Texture2D shotgun;
        SoundEffect zombieSnd;
        SoundEffect shotSnd;
        SoundEffect deadZSnd;
        Texture2D bullet; 
        Texture2D zombie;
        Texture2D zombie2;
        Texture2D zombie3;
        Texture2D zombie4;
        Texture2D zombie5;
        Texture2D zombie6;
        
        UserControlledSprite plyble; // the shotgun
        Boolean wasSpaceDown = false;
        
        //score
        Vector2 scoreVect = new Vector2(640,10);
        SpriteFont scoreFont = null;
        String timeString = "0:00";
        int collisionCount = 0;
        
        public SpriteManager(ZmbeInvdrs game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }


       

        protected override void LoadContent()
        {
          

            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            shotgun = Game.Content.Load<Texture2D>(@"Images\shotgun");
            shotSnd = Game.Content.Load<SoundEffect>(@"Audio\shotSnd");
            zombieSnd = Game.Content.Load<SoundEffect>(@"Audio\zombieSnd");
            deadZSnd = Game.Content.Load<SoundEffect>(@"Audio\DeadZ");
            scoreFont = Game.Content.Load<SpriteFont>(@"Fonts\scoreFont");
            zombie = Game.Content.Load<Texture2D>(@"Images\zombieA");
            zombie2 = Game.Content.Load<Texture2D>(@"Images\zombieB");
            zombie3 = Game.Content.Load<Texture2D>(@"Images\zombieC");
            zombie4 = Game.Content.Load<Texture2D>(@"Images\zombieD");
            zombie5 = Game.Content.Load<Texture2D>(@"Images\zombieE");
            zombie6 = Game.Content.Load<Texture2D>(@"Images\zombieF");
            bullet = Game.Content.Load<Texture2D>(@"Images\bulletA");
           
            plyble = new UserControlledSprite(shotgun, new Rectangle((900 / 2 - shotgun.Width / 2), 674 - shotgun.Height, shotgun.Width, shotgun.Height), 50, Color.White);
            spriteList.Add(plyble);

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        protected void changerowtime(GameTime gameTime)
        {
            if (newrowReset > 10)
        {
            lastnewrow += gameTime.ElapsedGameTime.Milliseconds;
            if (newrowReset > newrowchange)
            {
                lastnewrow -= newrowchange;
                if (newrowReset > 10)
                {
                    newrowReset -= 100;
                    newrowtimer -= 100;
                }
                else
                {
                    newrowReset -= 10;
                    newrowtimer -= 10;
                }
            }
        }
    }


        public override void Update(GameTime gameTime)
        {


            timeString = gameTime.TotalGameTime.Minutes.ToString() + ":" + gameTime.TotalGameTime.Seconds.ToString("00");
            
            for (int i = 0; i < spriteList.Count; i++)
                spriteList[i].Update(gameTime, Game.Window.ClientBounds);


            ///
            //  add for loop to check for collisions
            for(int i = 0; i < spriteList.Count; i++)
                for (int j = i + 1; j < spriteList.Count; j++)
                {
                    if (spriteList[i].Collides(spriteList[j]) && spriteList[i].Alive && spriteList[j].Alive)
                    {
                        if (spriteList[i] is bullet)
                        {
                            ((zombies)spriteList[j]).Alive = false;
                            collisionCount++;
                        }
                        else
                            if (spriteList[j] is bullet)
                            {
                                ((zombies)spriteList[i]).Alive = false;
                                collisionCount++;

                            }
                        deadZSnd.Play();



                        
                    }
                }

            for (int i = 0; i < spriteList.Count; i++)
            {

                if (spriteList[i].spd.Y == 0 && spriteList[i] is zombies)
                {
                    --((ZmbeInvdrs)Game).NumLives;
                    spriteList.Clear();
                    spriteList.Add(plyble);
                    timeString = ("00");
                    collisionCount = 0;
        


                }

            }
                
            //Remove zombies 
            for (int i = spriteList.Count - 1; i >= 0; i--)
            {
                if (spriteList[i].Alive == false)
                    spriteList.Remove(spriteList[i]);
                

            }


           //Game OVer
            



            //Create Zombies
            newrowtimer -= gameTime.ElapsedGameTime.Milliseconds;
            if (newrowtimer <= 0)
            {
                newrowtimer = newrowReset;
                zombies z1 = new zombies(zombie, new Rectangle(Game.Window.ClientBounds.Width - 180, 10, zombie.Width, zombie.Height), new Vector2(0, 1), 50, Color.White);
                spriteList.Add(z1);
                zombieSnd.Play(1F, 0F, -1F);
                zombies z2 = new zombies(zombie, new Rectangle(Game.Window.ClientBounds.Width - 260, 10, zombie.Width, zombie.Height), new Vector2(0, 1), 50, Color.White);
                spriteList.Add(z2);
                zombies z3 = new zombies(zombie2, new Rectangle(Game.Window.ClientBounds.Width - 340, 10, zombie.Width, zombie.Height), new Vector2(0, 1), 50, Color.White);
                spriteList.Add(z3);
                zombies z4 = new zombies(zombie3, new Rectangle(Game.Window.ClientBounds.Width - 420, 10, zombie.Width, zombie.Height), new Vector2(0, 1), 50, Color.White);
                spriteList.Add(z4);
                zombies z5 = new zombies(zombie4, new Rectangle(Game.Window.ClientBounds.Width - 500, 10, zombie.Width, zombie.Height), new Vector2(0, 1), 50, Color.White);
                spriteList.Add(z5);
                zombies z6 = new zombies(zombie5, new Rectangle(Game.Window.ClientBounds.Width - 580, 10, zombie.Width, zombie.Height), new Vector2(0, 1), 50, Color.White);
                spriteList.Add(z6);
                zombies z7 = new zombies(zombie6, new Rectangle(Game.Window.ClientBounds.Width - 660, 10, zombie.Width, zombie.Height), new Vector2(0, 1), 50, Color.White);
                spriteList.Add(z7);
                zombies z8 = new zombies(zombie, new Rectangle(Game.Window.ClientBounds.Width - 740, 10, zombie.Width, zombie.Height), new Vector2(0, 1), 50, Color.White);
                spriteList.Add(z8);
                zombies z9 = new zombies(zombie, new Rectangle(Game.Window.ClientBounds.Width - 820, 10, zombie.Width, zombie.Height), new Vector2(0, 1), 50, Color.White);
                spriteList.Add(z9);
            }


            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Space))
                wasSpaceDown = true;
            else
            {
                if (wasSpaceDown)
                {
                    bullet blt = new bullet(bullet, new Rectangle(plyble.getXPostion() +58, 674 - shotgun.Height - bullet.Height, bullet.Width, bullet.Height), new Vector2(0, -10), 50, Color.White);
                    spriteList.Add(blt);
                    shotSnd.Play();
                    wasSpaceDown = false;
                }

                
            }
            changerowtime(gameTime);
                base.Update(gameTime);
            }
        
      

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            String score = "Score =  " + collisionCount;
            spriteBatch.DrawString(scoreFont, score, new Vector2(30, 10), Color.Gold);
            spriteBatch.DrawString(scoreFont,"Elapsed Time= " + timeString, scoreVect, Color.Gold);
            for (int i = 0; i < spriteList.Count; i++)
                spriteList[i].Draw(gameTime, spriteBatch);
            // TODO: Add your update code here

            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
