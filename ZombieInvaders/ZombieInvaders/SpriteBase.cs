using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombieInvaders
{
    public class SpriteBase
    {
        public enum EdgeAction { Wrap, Reverse, Ricoshet, Stop, Die, Nothing };


        protected  Texture2D txtre;
        public Vector2 spd;
        protected Rectangle pstn;
        protected Color tnt;
        protected SpriteEffects fx;
        protected EdgeAction eg;
        protected Boolean alive;
        double lstfrme;
        double msprfrme;
        Rectangle cllsn;
        protected  Rectangle src = new Rectangle(0,0,0,0);
        Point crntfrme;
        Point colnrowsze;
        private Texture2D texture;
        private Vector2 position;
        private SpriteEffects effects;
        private double timeBetweenUpdates;

        public Boolean Alive
        {
            get { return alive; }
            set { alive = value; }
        }

       
        public SpriteBase()
        {
            txtre = null;
            pstn = new Rectangle(0, 0, 0, 0);
            cllsn = new Rectangle(0, 0, 0, 0);
            tnt = Color.White;
            fx = SpriteEffects.None;
            eg = EdgeAction.Stop;
            lstfrme = 20;
            msprfrme = 20;
            tnt = Color.White;
            spd = new Vector2(0, 0);  
            alive = true;



        }

         public SpriteBase(Texture2D txtre, Rectangle pstn, Vector2 spd, double msprfrme,  Color tnt, SpriteEffects fx)
        {
            this.txtre = txtre;
            this.pstn = pstn;
            this.spd = spd;
            this.msprfrme = msprfrme;
            //this.crntfrme = crntfrme;
            this.tnt = tnt;
            this.fx = fx;
            this.eg = EdgeAction.Stop;

            src = new Rectangle(0, 0, txtre.Width, txtre.Height);

            cllsn = new Rectangle(0,0,0,0);
            cllsn.X = pstn.X;
            cllsn.Y = pstn.Y;
            cllsn.Width = pstn.Width;
            cllsn.Height = pstn.Height;

            this.alive = true;

            }

         public Boolean Collides(SpriteBase other)
         {
             return cllsn.Intersects(other.cllsn);
         }
         
         public SpriteBase(Texture2D texture, Vector2 position, SpriteEffects effects, double timeBetweenUpdates)
         {
             // TODO: Complete member initialization
             this.texture = texture;
             this.position = position;
             this.effects = effects;
             this.timeBetweenUpdates = timeBetweenUpdates;
         }

         public virtual void Update(GameTime gmetme, Rectangle ClientBounds)
         {
             lstfrme -= gmetme.ElapsedGameTime.Milliseconds;
             if (lstfrme <= 0)
             {
                 lstfrme = msprfrme;
                 if (spd.X != 0 || spd.Y != 0) // moving?
                 {

                     pstn.X += (int)spd.X;  // yes change position
                     pstn.Y += (int)spd.Y;
                     cllsn.X += (int)spd.X;
                     cllsn.Y += (int)spd.Y;

                     switch (eg)
                     {
                         case EdgeAction.Wrap:

                            if (cllsn.X + cllsn.Width >= ClientBounds.Width)
                            {
                                cllsn.X = 0;  // wrap around
                                pstn.X = 0;
                            }
                            if (cllsn.Y + cllsn.Height >= ClientBounds.Height)
                            {
                                cllsn.Y = 0; // wrap around
                                pstn.Y = 0;
                            }
                            if (cllsn.Y < 0)
                            {
                                cllsn.Y = ClientBounds.Height - 1;
                                pstn.Y = ClientBounds.Height - 1;
                            }
                            if (cllsn.X < 0)
                            {
                                cllsn.X = ClientBounds.Width - 1;
                                pstn.X = ClientBounds.Width - 1;
                            }
                            break;

                         case EdgeAction.Stop:
                        case EdgeAction.Die:
                            if (cllsn.X + cllsn.Width >= ClientBounds.Width - 1 ||
                               cllsn.Y + cllsn.Height >= ClientBounds.Height - 1 ||
                               cllsn.Y <= 0 ||
                               cllsn.X <= 0)
                            {
                                pstn.X -= (int)spd.X; // backup
                                pstn.Y -= (int)spd.Y;
                                cllsn.X -= (int)spd.X; // backup
                                cllsn.Y -= (int)spd.Y; // backup
                                spd.X = 0;
                                spd.Y = 0;
                                if (eg == EdgeAction.Die)
                                    alive = false;
                            }

                            break;

                             case EdgeAction.Reverse:
                            if (cllsn.X + cllsn.Width >= ClientBounds.Width - 1 ||
                               cllsn.Y + cllsn.Height >= ClientBounds.Height - 1 ||
                               cllsn.Y <= 0 ||
                               cllsn.X <= 0)
                            {
                                position.X -= (int)spd.X; // backup
                                position.Y -= (int)spd.Y;
                                cllsn.X -= (int)spd.X; // backup
                                cllsn.Y -= (int)spd.Y;
                                spd.X = -spd.X;  // turn around
                                spd.Y = -spd.Y;
                                if (effects != SpriteEffects.None)
                                    effects = SpriteEffects.None;
                                else
                                    effects = SpriteEffects.None;
                            }
                            break;
                             case EdgeAction.Ricoshet:

                            if (cllsn.X + cllsn.Width >= ClientBounds.Width - 1 || cllsn.X <= 0)
                                spd.X = -spd.X; // reverse X direction
                            if (cllsn.Y + cllsn.Height >= ClientBounds.Height - 1 || cllsn.Y <= 0)
                                spd.Y = -spd.Y; // reverse Y direction
                            break;

                     }
                           

                     if (cllsn.X + cllsn.Width >= ClientBounds.Width - 1 ||
                         cllsn.Y + cllsn.Height >= ClientBounds.Height - 1 ||
                         cllsn.Y <= 0 ||
                         cllsn.X <= 0)



                         lstfrme += gmetme.ElapsedGameTime.Milliseconds;
                     if (lstfrme > msprfrme)
                     {
                         lstfrme = 0;
                         ++crntfrme.X;
                         if (crntfrme.X >= colnrowsze.X)
                         {
                             crntfrme.X = 0;
                             ++crntfrme.Y;
                             if (crntfrme.Y > +colnrowsze.Y)
                                 crntfrme.Y = 0;
                         }
                     }
                 }
             }
         }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(txtre, pstn, src, tnt, 0, Vector2.Zero, fx, 0);
            //spriteBatch.Draw(txtre, pstn, tnt);
        
        }


        }

    

    
}
