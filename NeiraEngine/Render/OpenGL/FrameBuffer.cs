using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace NeiraEngine.Render.OpenGL
{

    public class FrameBuffer
    {

        private int _id;
        public int id
        {
            get { return _id; }
        }

        public string name { get; }

        public Dictionary<FramebufferAttachment, Texture> attachements { get; private set; }


        //------------------------------------------------------
        // Constructor
        //------------------------------------------------------

        public FrameBuffer(string name)
        {
            // Create Frame Buffer Object
            _id = 0;
            GL.GenFramebuffers(1, out _id);

            this.name = name;
        }


        //------------------------------------------------------
        // Main Methods
        //------------------------------------------------------

        public void load(Dictionary<FramebufferAttachment, Texture> attachements)
        {
            this.attachements = attachements;

            OGL.BindFramebuffer(FramebufferTarget.Framebuffer, _id);

            // Loop through and attach each FBO item
            foreach (var a in attachements)
            {               
                GL.FramebufferTexture(OpenTK.Graphics.OpenGL.FramebufferTarget.Framebuffer, (OpenTK.Graphics.OpenGL.FramebufferAttachment)a.Key, a.Value.id, 0);
            }

            // Check for FBO errors
            if (GL.CheckFramebufferStatus(OpenTK.Graphics.OpenGL.FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                Debug.logError("[ ERROR ] FrameBuffer (" + name + ")", GL.CheckFramebufferStatus(OpenTK.Graphics.OpenGL.FramebufferTarget.Framebuffer).ToString());
            }
            else
            {
                Debug.logInfo(2, "[ INFO ] FrameBuffer (" + name + ")", "SUCCESS");
            }
            
            OGL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void unload()
        {

        }

        // Bind Draw Attachements Only
        public void bindAttachements(DrawBuffersEnum draw_attachment)
        {
            OpenTK.Graphics.OpenGL.DrawBuffersEnum[] temp_attchements = new OpenTK.Graphics.OpenGL.DrawBuffersEnum[] { (OpenTK.Graphics.OpenGL.DrawBuffersEnum)draw_attachment };

            int buffer_count = temp_attchements.Length;
            GL.DrawBuffers(buffer_count, temp_attchements);
        }
        public void bindAttachements(DrawBuffersEnum[] draw_attachements)
        {
            int buffer_count = draw_attachements.Length;
            OpenTK.Graphics.OpenGL.DrawBuffersEnum[] draw_buffers = new OpenTK.Graphics.OpenGL.DrawBuffersEnum[draw_attachements.Length];
            for(int i = 0; i < draw_buffers.Length; i++)
            {
                draw_buffers[i] = (OpenTK.Graphics.OpenGL.DrawBuffersEnum)draw_attachements[i];
            }
            GL.DrawBuffers(buffer_count, draw_buffers);
        }

        // Bind Read Attachements Only
        public void bindAttachements(ReadBufferMode read_attachement)
        {
            GL.ReadBuffer((OpenTK.Graphics.OpenGL.ReadBufferMode)read_attachement);
        }

        // Bind to Draw
        public void bind(DrawBuffersEnum draw_attachment)
        {
            bind(new DrawBuffersEnum[] { draw_attachment });
        }
        public void bind(DrawBuffersEnum[] draw_attachements)
        {
            OGL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, _id);

            bindAttachements(draw_attachements);
        }

        // Bind to Read
        public void bind(ReadBufferMode read_attachement)
        {
            OGL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, _id);

            bindAttachements(read_attachement);
        }


        public void bindTexture(FramebufferAttachment attachement, int texture_id)
        {
            GL.FramebufferTexture(OpenTK.Graphics.OpenGL.FramebufferTarget.Framebuffer, (OpenTK.Graphics.OpenGL.FramebufferAttachment)attachement, texture_id, 0);

            // Check for FBO errors
            if (GL.CheckFramebufferStatus(OpenTK.Graphics.OpenGL.FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                Debug.logError("[ ERROR ] FrameBuffer (" + name + ")", GL.CheckFramebufferStatus(OpenTK.Graphics.OpenGL.FramebufferTarget.Framebuffer).ToString());
            }
        }
    }
}
