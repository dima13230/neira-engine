using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cgen.Audio;

using OpenTK;

using NeiraEngine.World;
using NeiraEngine.Output;

namespace NeiraEngine.Components
{
    public class AudioComponent : Component
    {
        public string initialFilename = "";

        public Sound sound;

        public bool playOnStart = false;
        public bool twoD = false;
        public bool loop = false;

        public float minDistance = 1;
        public float pitch = 1;
        public float volume = 100;

        public float pan = 0;

        public AudioComponent(WorldObject relatedTo) : base(relatedTo)
        {
        }

        public override void Start()
        {
            if (initialFilename != "")
                if (System.IO.File.Exists(EngineHelper.path_resources_audio + initialFilename))
                    sound = new Sound(EngineHelper.path_resources_audio + initialFilename);

            if (sound != null)
            {

                Vector3 pos = worldObject.spatial.position;

                if (!twoD)
                {
                    sound.Position3D = new float[] { pos.X, pos.Y, pos.Z };
                }
                else
                    sound.Pan = pan;

                sound.MinimumDistance = minDistance;
                sound.Pitch = pitch;
                sound.Volume = volume;

                sound.IsLooping = loop;

                if(playOnStart)
                {
                    sound.Play();
                }
            }
        }

        public override void Update()
        {
            if (sound != null)
            {
                Vector3 pos = worldObject.globalPosition;
                float[] fpos = new float[] { pos.X, pos.Y, pos.Z };
                if (!twoD && (pos.X == fpos[0] | pos.Y == fpos[1] | pos.Z == fpos[2]))
                    sound.Position3D = fpos;
                else if (sound.Pan != pan)
                    sound.Pan = pan;

                if (sound.IsLooping != loop)
                    sound.IsLooping = loop;
                if (sound.MinimumDistance != minDistance)
                    sound.MinimumDistance = minDistance;
                if (sound.Pitch != pitch)
                    sound.Pitch = pitch;
                if (sound.Volume != volume)
                    sound.Volume = volume;
            }
        }

        public override void Remove()
        {
            sound.Dispose();
        }

        public void Play()
        {
            if (sound != null)
            {
                sound.Play();
            }
        }

        public void PlayAtPosition(Vector3 position)
        {
            if (sound != null)
            {
                float[] pos = new float[] { position.X, position.Y, position.Z };
                sound.Position3D = pos;
                sound.Play();
            }
        }
    }
}
