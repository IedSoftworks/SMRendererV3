﻿using System.Collections.Generic;
using System.Windows.Controls;
using OpenTK;
using SM.Base.Controls;
using SM.Base.Drawing;
using SM.Base.Scene;
using SM.Utility;
using SM2D.Scene;
using SM2D.Types;

namespace SM2D.Controls
{
    public class Mouse2D
    {
        public static Vector2 InWorld(Vector2 worldScale)
        {
            var res = worldScale;
            res.Y *= -1;
            return Mouse.InScreenNormalized * res - res / 2;
        }

        public static Vector2 InWorld(Camera cam)
        {
            return InWorld(cam.WorldScale) + cam.Position;
        }

        public static Vector2 InWorld(Vector2 worldScale, Vector2 position)
        {
            return InWorld(worldScale) + position;
        }

        public static bool MouseOver<TObject>(Vector2 mousePos, params TObject[] checkingObjects)
            where TObject : IModelItem, ITransformItem<Transformation>
            => MouseOver(mousePos, out _, checkingObjects);

        public static bool MouseOver<TObject>(Vector2 mousePos, ICollection<TObject> checkingObjects)
            where TObject : IModelItem, ITransformItem<Transformation>
            => MouseOver<TObject>(mousePos, out _, checkingObjects);

        public static bool MouseOver<TObject>(Vector2 mousePos, out TObject clicked, params TObject[] checkingObjects) 
            where TObject : IModelItem, ITransformItem<Transformation>
            => MouseOver<TObject>(mousePos, out clicked, (ICollection<TObject>)checkingObjects);

        public static bool MouseOver<TObject>(Vector2 mousePos, out TObject clickedObj, ICollection<TObject> checkingObjects)
            where TObject : IModelItem, ITransformItem<Transformation>
        {
            clickedObj = default;
            bool success = false;

            float distance = -10;

            foreach (TObject item in checkingObjects)
            {
                Matrix4 worldPos = item.Transform.InWorldSpace;
                item.Mesh.BoundingBox.GetBounds(worldPos, out Vector3 min, out Vector3 max);

                if (mousePos.X > min.X && mousePos.X < max.X &&
                    mousePos.Y > min.Y && mousePos.Y < max.Y)
                {
                    // if z is greater than distance
                    if (worldPos[3, 2] > distance)
                    {
                        clickedObj = item;
                        distance = worldPos[3, 2];
                    }

                    success = true;
                }
            }

            return success;
        }
    }
}