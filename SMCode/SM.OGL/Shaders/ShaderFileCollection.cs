﻿using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Shaders
{
    public struct ShaderFileCollection
    {
        public ShaderFile Vertex;
        public ShaderFile Geometry;
        public ShaderFile Fragment;

        public Action<UniformCollection> SetUniforms;

        public ShaderFileCollection(string vertex, string fragment) : this(new ShaderFile(vertex), new ShaderFile(fragment)) {}

        public ShaderFileCollection(ShaderFile vertex, ShaderFile fragment, ShaderFile geometry = default)
        {
            Vertex = vertex;
            Geometry = geometry;
            Fragment = fragment;

            SetUniforms = u => { };
        }

        internal void Append(GenericShader shader)
        {
            Vertex.Compile(shader, ShaderType.VertexShader);
            Geometry?.Compile(shader, ShaderType.GeometryShader);
            Fragment.Compile(shader, ShaderType.FragmentShader);
        }

        internal void Detach(GenericShader shader)
        {
            GL.DetachShader(Vertex, shader);
            if (Geometry != null) GL.DetachShader(Geometry, shader);
            GL.DetachShader(Fragment, shader);
        }
    }
}