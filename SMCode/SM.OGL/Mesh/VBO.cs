﻿#region usings

using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

#endregion

namespace SM.OGL.Mesh
{
    /// <summary>
    ///     Represents a Vertex Buffer Object used for meshes.
    /// </summary>
    public class VBO : List<float>
    {
        /// <summary>
        ///     Specifies the expected usage pattern of the data store.
        /// </summary>
        public BufferUsageHint BufferUsageHint;

        /// <summary>
        ///     Normalise floats?
        /// </summary>
        public bool Normalised;

        /// <summary>
        ///     Specifies a offset of the first component of the first generic vertex attribute in the array in the data store of
        ///     the buffer currently bound to the GL_ARRAY_BUFFER target.
        /// </summary>
        public int PointerOffset;

        /// <summary>
        ///     Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, 4.
        /// </summary>
        public int PointerSize;

        /// <summary>
        ///     Specifies the byte offset between consecutive generic vertex attributes.
        /// </summary>
        public int PointerStride;

        public bool Active = true;

        /// <summary>
        ///     Specifies the data type of each component in the array.
        /// </summary>
        public VertexAttribPointerType PointerType;

        /// <summary>
        ///     Generates a VBO for inserting mesh data.
        /// </summary>
        /// <param name="bufferUsageHint">
        ///     Specifies the expected usage pattern of the data store.
        ///     <para>Default: StaticDraw</para>
        /// </param>
        /// <param name="pointerType">
        ///     Specifies the data type of each component in the array.
        ///     <para>Default: Float</para>
        /// </param>
        /// <param name="pointerSize">
        ///     Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, 4.
        ///     <para>Default: 3</para>
        /// </param>
        /// <param name="pointerStride">
        ///     Specifies the byte offset between consecutive generic vertex attributes.
        ///     <para>Default: 0</para>
        /// </param>
        /// <param name="pointerOffset">
        ///     Specifies a offset of the first component of the first generic vertex attribute in the
        ///     array in the data store of the buffer currently bound to the GL_ARRAY_BUFFER target.
        ///     <para>Default: 0</para>
        /// </param>
        /// <param name="normalised">
        ///     Normalise floats?
        ///     <para>Default: false</para>
        /// </param>
        public VBO(BufferUsageHint bufferUsageHint = BufferUsageHint.StaticDraw,
            VertexAttribPointerType pointerType = VertexAttribPointerType.Float, int pointerSize = 3,
            int pointerStride = 0, int pointerOffset = 0, bool normalised = false)
        {
            BufferUsageHint = bufferUsageHint;
            PointerType = pointerType;
            PointerSize = pointerSize;
            PointerStride = pointerStride;
            PointerOffset = pointerOffset;
            Normalised = normalised;
        }

        /// <summary>
        ///     Adds two values to the VBO.
        /// </summary>
        public void Add(float x, float y)
        {
            AddRange(new[] {x, y});
        }

        /// <summary>
        ///     Adds three values to the VBO.
        /// </summary>
        public void Add(float x, float y, float z)
        {
            AddRange(new[] {x, y, z});
        }

        /// <summary>
        ///     Adds four values to the VBO.
        /// </summary>
        public void Add(float x, float y, float z, float w)
        {
            AddRange(new[] {x, y, z, w});
        }

        /// <summary>
        ///     Adds a Vector2.
        /// </summary>
        public void Add(Vector2 vector)
        {
            Add(vector.X, vector.Y);
        }

        /// <summary>
        ///     Adds a Vector2 and a value.
        /// </summary>
        public void Add(Vector2 vector, float z)
        {
            Add(vector.X, vector.Y, z);
        }

        /// <summary>
        ///     Adds a Vector2 and two values.
        /// </summary>
        public void Add(Vector2 vector, float z, float w)
        {
            Add(vector.X, vector.Y, z, w);
        }

        public void Add(params Vector2[] vectors)
        {
            foreach (Vector2 vector in vectors)
            {
                Add(vector);
            }
        }

        /// <summary>
        ///     Adds a Vector3.
        /// </summary>
        public void Add(Vector3 vector)
        {
            Add(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        ///     Adds a Vector3 and a value.
        /// </summary>
        public void Add(Vector3 vector, float w)
        {
            Add(vector.X, vector.Y, vector.Z, w);
        }

        public void Add(params Vector3[] vectors)
        {
            foreach (Vector3 vector in vectors)
            {
                Add(vector);
            }
        }
        /// <summary>
        ///     Adds a vector4.
        /// </summary>
        /// <param name="vector"></param>
        public void Add(Vector4 vector)
        {
            Add(vector.X, vector.Y, vector.Z, vector.W);
        }

        /// <summary>
        ///     Adds a color.
        /// </summary>
        public void Add(Color4 color)
        {
            Add(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        ///     Binds the buffer to the active VAO.
        /// </summary>
        /// <param name="attribID">The id for the attribute.</param>
        internal void BindBuffer(int attribID)
        {
            if (!Active) return;

            var data = ToArray();

            var buffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint);

            GL.VertexAttribPointer(attribID, PointerSize, PointerType, Normalised, PointerStride, PointerOffset);
            GL.EnableVertexAttribArray(attribID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}