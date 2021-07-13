using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NeiraEngine.Render.Objects.OGL;

namespace NeiraEngine.World.Model
{
    public class MaterialManager
    {

        private UniformBuffer _ubo_bindless;
        public UniformBuffer ubo_bindless
        {
            get { return _ubo_bindless; }
        }

        private int _material_count = 0;

        public MaterialManager()
        {
            // Create UBO for bindless material textures
            _ubo_bindless = new UniformBuffer(BufferStorageFlags.DynamicStorageBit, 2, EngineHelper.size.i64, 256 * 2);
        }

        public int addImage(long handle)
        {
            _ubo_bindless.update(_material_count * 2, handle);

            _material_count++;
            return _material_count - 1;
        }

    }
}
