using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Space_Invaders
{
    class BlockFormation
    {
        List<Block> blocks = new List<Block>();
        public BlockFormation()
        {

        }

        public void LoadContent(ContentManager content, int blockPoint, int blockNumber, int height)
        {
            blocks = new List<Block>();

            blocks.Add(new Block("DefenseBlock", new Vector2(-100, -100), SpriteEffects.None));

            blocks.Add(new Block("DefenseInnerCornerRight", new Vector2(blockPoint + blockPoint * blockNumber - blocks[0].height / 2, height - 100), SpriteEffects.FlipHorizontally));
            blocks.Add(new Block("DefenseInnerCornerRight", new Vector2(blockPoint + blockPoint * blockNumber + blocks[0].height / 2, height - 100), SpriteEffects.None));
            blocks.Add(new Block("DefenseBlock", new Vector2(blockPoint + blockPoint * blockNumber - blocks[0].height - blocks[0].height / 2, height - 100), SpriteEffects.None));
            blocks.Add(new Block("DefenseBlock", new Vector2(blockPoint + blockPoint * blockNumber + blocks[0].height + blocks[0].height / 2, height - 100), SpriteEffects.None));
            blocks.Add(new Block("DefenseBlock", new Vector2(blockPoint + blockPoint * blockNumber - blocks[0].height / 2, height - 100 - blocks[0].height), SpriteEffects.None));
            blocks.Add(new Block("DefenseBlock", new Vector2(blockPoint + blockPoint * blockNumber + blocks[0].height / 2, height - 100 - blocks[0].height), SpriteEffects.None));
            blocks.Add(new Block("DefenseBlock", new Vector2(blockPoint + blockPoint * blockNumber - blocks[0].height - blocks[0].height / 2, height - 100 + blocks[0].height), SpriteEffects.None));
            blocks.Add(new Block("DefenseBlock", new Vector2(blockPoint + blockPoint * blockNumber + blocks[0].height + blocks[0].height / 2, height - 100 + blocks[0].height), SpriteEffects.None));
            blocks.Add(new Block("DefenceOuterCornerLeft", new Vector2(blockPoint + blockPoint * blockNumber - blocks[0].height - blocks[0].height / 2, height - 100 - blocks[0].height), SpriteEffects.None));
            blocks.Add(new Block("DefenceOuterCornerLeft", new Vector2(blockPoint + blockPoint * blockNumber + blocks[0].height + blocks[0].height / 2, height - 100 - blocks[0].height), SpriteEffects.FlipHorizontally));
            foreach (var block in blocks)
            {
                block.GetFrameWidth();
            }
        }

        public bool CollisionCheck(Laser laser)
        {
            for (int j = 0; j < blocks.Count; j++)
            {
                if (laser.Box().Intersects(blocks[j].Box()))
                {
                    blocks[j].GetLife--;
                    if (blocks[j].GetLife <= 0)
                    {
                        blocks.Remove(blocks[j]);
                        j--;
                    }
                    return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch sprite)
        {
            foreach (var block in blocks) 
            {
                block.Draw(sprite);
            }
        }
    }
}
