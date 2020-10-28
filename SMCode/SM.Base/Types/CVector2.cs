﻿#region usings

using OpenTK;

#endregion

namespace SM.Base.Types
{
    /// <summary>
    ///     Represents a vector of two floats.
    /// </summary>
    public class CVector2 : CVector
    {
        /// <inheritdoc />
        public CVector2() : this(0)
        {
        }

        /// <inheritdoc />
        public CVector2(float uniform) : base(uniform)
        {
        }

        /// <inheritdoc />
        public CVector2(float x, float y) : base(x, y, default, default)
        {
        }

        /// <inheritdoc />
        protected CVector2(float x, float y, float z, float w) : base(x, y, z, w)
        {
        }

        /// <summary>
        ///     X-component
        /// </summary>
        public float X
        {
            get => _X;
            set => _X = value;
        }

        /// <summary>
        ///     Y-component
        /// </summary>
        public float Y
        {
            get => _Y;
            set => _Y = value;
        }

        /// <summary>
        ///     Sets the X and Y-component.
        /// </summary>
        public new void Set(float x, float y)
        {
            base.Set(x, y);
        }

        /// <summary>
        ///     Sets the X and Y-component from a <see cref="OpenTK.Vector2" />
        /// </summary>
        public new void Set(Vector2 vector)
        {
            base.Set(vector);
        }

        /// <summary>
        ///     Adds a <see cref="OpenTK.Vector2" /> to the X and Y-component.
        /// </summary>
        public new void Add(Vector2 vector)
        {
            base.Add(vector);
        }

        /// <summary>
        /// Converts a <see cref="OpenTK.Vector2"/> to <see cref="CVector2"/>
        /// </summary>
        public static implicit operator CVector2(Vector2 v) => new CVector2(v.X,v.Y);
    }
}