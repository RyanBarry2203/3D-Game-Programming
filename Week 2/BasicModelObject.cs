using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_2
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
        public void Update()
        {
            // Add any per-frame update logic here if needed
            //Vector3 translation = boneTransforms[2].Translation;

            //boneTransforms[2].Translation = Vector3.Zero;

            //boneTransforms[2] *= Matrix.CreateRotationY(MathHelper.ToRadians(1));

            //boneTransforms[2].Translation = translation;

            for (int i = 0; i < boneTransforms.Length; i++)
            {
                //if (modelAsset.Bones[i].Parent != null)
                //{
                //    boneTransforms[i] = boneTransforms[modelAsset.Bones[i].Parent.Index] * boneTransforms[i];
                //}
                //while (boneTransforms[i] != boneTransforms[1])
                //{
                    Vector3 translation = boneTransforms[i].Translation;

                    boneTransforms[i].Translation = Vector3.Zero;

                    boneTransforms[i] *= Matrix.CreateRotationY(MathHelper.ToRadians(1));

                    boneTransforms[i].Translation = translation;
                //}
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
                mesh.Draw();
            }

        }
    }
}