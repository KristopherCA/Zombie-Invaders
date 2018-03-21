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
    public class zombies : SpriteBase
    {

        double xshift = 4000;
        double shiftEvery = 2000;
        int shiftdir = 4;
        
       // internal SoundEffect zombieSnd;

        public zombies()
            {
            }
        public zombies(Texture2D txtre, Rectangle pstn, Vector2 spd, double msprfrme, Color tnt)
            : base(txtre, pstn, spd, msprfrme, tnt, SpriteEffects.None)
        {
            
            //zombieSnd = Game.Content.Load<SoundEffect>(@"Audio\zombieSnd");
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
      

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime, Rectangle ClientsBounds)
        {
            xshift -= gameTime.ElapsedGameTime.Milliseconds;
            if (xshift <= 0)
            {
                xshift = shiftEvery;
                pstn.X += 10 * shiftdir;
                shiftdir = shiftdir * -1;
            }



            base.Update(gameTime, ClientsBounds);


        }
            public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(txtre, pstn, src, tnt, 0, Vector2.Zero, fx, 0);
                //spriteBatch.Draw(txtre, pstn, tnt);
        
            }

            internal void Update(EdgeAction edgeAction)
            {
                throw new NotImplementedException();
            }
    }
}
