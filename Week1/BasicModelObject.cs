using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    public class BasicModelObject
    {
        Model modelAsset;
        string assetName;
        Matrix world = Matrix.Identity;
        Matrix[] boneTransforms;
        public int VertexCount { get; private set; }

        public BasicModelObject(string name, Matrix transform)
        {
            assetName = name;
            world = transform;
        }
        public void LoadContent(ContentManager content)
        {
            modelAsset = content.Load<Model>(assetName);

            boneTransforms = new Matrix[modelAsset.Bones.Count];

            modelAsset.CopyAbsoluteBoneTransformsTo(boneTransforms);

            foreach (ModelMesh mesh in modelAsset.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                    VertexCount += part.NumVertices;

            foreach (var mesh in modelAsset.Meshes)
            {
                foreach (var part in mesh.MeshParts)
                {
                    part.Effect = part.Effect.Clone();
                }
            }
        }
        public void Draw(Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in modelAsset.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.View = view;
                    effect.Projection = projection;

                    effect.World = boneTransforms[mesh.ParentBone.Index] * world;

                    effect.EnableDefaultLighting();
                }
            }
        }
    }
}
