﻿using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Contexts;
using SM.Base.StaticObjects;
using SM.OGL.Shaders;

namespace SM.Base.Shader
{
    public class InstanceShader : GenericShader
    {
        protected override bool AutoCompile { get; } = true;

        public Action<UniformCollection, DrawContext> SetUniform;

        public InstanceShader(string vertex, string fragment, Action<UniformCollection, DrawContext> setUniform) : base(
            new ShaderFileCollection(vertex, fragment))
        {
            SetUniform = setUniform;
        }
        public void Draw(DrawContext context)
        {
            GL.UseProgram(this);

            SetUniform.Invoke(Uniforms, context);

            DrawObject(context.Mesh, true);

            GL.UseProgram(0);
        }
    }
}