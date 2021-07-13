using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using NeiraEngine.Render;
using NeiraEngine.Render.FX;
using NeiraEngine.World;
using NeiraEngine.World.Model;
using OpenTK.Graphics.OpenGL;

using OpenTK;

namespace NeiraEngine.Components
{
    public class SpriteComponent : Component
    {
        public Dictionary<string, SpriteBank> spriteBanks = new Dictionary<string, SpriteBank>();
        string currentBank;

        public string GetCurrentBankName() => currentBank;
        public SpriteBank GetCurrentBank() => spriteBanks[currentBank];
        public Image GetCurrentFrame() => GetCurrentBank().Images[currentFrame];

        public Vector3 color = Vector3.One;

        public void SetCurrentBank(string name)
        {
            if (spriteBanks.ContainsKey(name))
            {
                currentFrame = 0;
                currentBank = name;
            }
        }

        int currentFrame = 0;

        public string startupBank;

        public SpriteComponent(WorldObject relatedTo) : base(relatedTo)
        {
        }

        public override void Start()
        {
            SpriteBank bank = AddBank(startupBank);
            if(bank != null)
                SetCurrentBank(Path.GetFileNameWithoutExtension(startupBank));

            scene.sprites.Add(this);
        }

        public override void Update()
        {
            if(spriteBanks.Count > 0)
            {
                if (currentBank != null)
                {
                    List<Image> images = spriteBanks[currentBank].Images;
                    if (images.Count > 1)
                    {
                        if (currentFrame >= images.Count - 1)
                        {
                            currentFrame = 0;
                        }
                        else
                        {
                            currentFrame++;
                        }
                    }
                    else
                    {
                        if (currentFrame > 0) currentFrame = 0;
                    }
                }
            }
        }

        public SpriteBank AddBank(string source_name)
        {
            string filename = EngineHelper.path_resources_textures_sprites + source_name;
            if (File.Exists(filename))
            {
                SpriteBank bank = new SpriteBank(filename);
                string key = Path.GetFileNameWithoutExtension(filename);
                spriteBanks.Add(key, bank);
                return spriteBanks[key];
            }
            return null;
        }
    }
}