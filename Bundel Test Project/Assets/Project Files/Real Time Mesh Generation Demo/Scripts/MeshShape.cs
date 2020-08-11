using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assignment
{
    public class MeshShape
    {

        public MeshData meshData;

        public Vector3 LeftBottom;
        public Vector3 RightBottom;

        public Vector3 LeftTop;
        public Vector3 RightTop;

        public Vector3 Inner_LeftTop;
        public Vector3 Inner_RightTop;

        public Vector3 Inner_LeftBottom;
        public Vector3 Inner_RightBottom;

        public MeshShape() 
        {
            meshData = new MeshData();
        }
        public MeshShape(Vector3 RootPoint, Quaternion rotation, float scale = 1f)
        {
            LeftTop = RootPoint + rotation * new Vector3(-1f, 0.3f, 0f) * scale;
            LeftBottom = RootPoint + rotation * new Vector3(-1f, -0.3f, 0f) * scale;

            RightTop = RootPoint + rotation * new Vector3(1f, 0.3f, 0f) * scale;
            RightBottom = RootPoint + rotation * new Vector3(1f, -0.3f, 0f) * scale;

            Inner_LeftTop = LeftTop + rotation * (Vector3.right * 0.3f) * scale;
            Inner_LeftBottom = Inner_LeftTop + rotation * (Vector3.down * 0.3f) * scale;

            Inner_RightTop = RightTop + rotation * (Vector3.left * 0.3f) * scale;
            Inner_RightBottom = Inner_RightTop + rotation * (Vector3.down * 0.3f) * scale;
            meshData = new MeshData();
        }


        public static List<Vector3> AddShape(MeshShape A, MeshShape B)
        {
            List<Vector3> vertices = new List<Vector3>();

            //left side edge
            List<Vector3> subquad = GetRectVertex(A.LeftBottom, B.LeftBottom, B.LeftTop, A.LeftTop);
            vertices.AddRange(subquad);

            //right side adge
            subquad = GetRectVertex(A.RightTop, B.RightTop, B.RightBottom, A.RightBottom);
            vertices.AddRange(subquad);

            //road surfce
            subquad = GetRectVertex(A.Inner_LeftBottom, B.Inner_LeftBottom, B.Inner_RightBottom, A.Inner_RightBottom);
            vertices.AddRange(subquad);

            //left footpath
            subquad = GetRectVertex(A.LeftTop, B.LeftTop, B.Inner_LeftTop, A.Inner_LeftTop);
            vertices.AddRange(subquad);


            //right footpath
            subquad = GetRectVertex(A.Inner_RightTop, B.Inner_RightTop, B.RightTop, A.RightTop);
            vertices.AddRange(subquad);


            //left border
            subquad = GetRectVertex(A.Inner_LeftTop, B.Inner_LeftTop, B.Inner_LeftBottom, A.Inner_LeftBottom);
            vertices.AddRange(subquad);


            //right border
            subquad = GetRectVertex(A.Inner_RightBottom, B.Inner_RightBottom, B.Inner_RightTop, A.Inner_RightTop);
            vertices.AddRange(subquad);

            return vertices;
        }


        public static List<Vector3> GetRectVertex(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
        {
            List<Vector3> vertices = new List<Vector3>();

            vertices.Add(A);
            vertices.Add(B);
            vertices.Add(C);
            vertices.Add(D);
            return vertices;
        }


        #region Final Mesh
        public class MeshData
        {
            private List<Vector3> Vertices = new List<Vector3>();
            private int[] Triangles;


            public List<Vector3> SetVertices
            {
                set 
                {
                    this.Vertices = value;
                }
            }


            public void AddVertices(List<Vector3> Vertices)
            {
                if(Vertices!= null)
                    this.Vertices.AddRange(Vertices);
            }


            private void GenerateTriangles(int vertecesCount)
            {
                int triLength = (vertecesCount / 4) * 6;
                this.Triangles = new int[triLength];

                for (int i = 0; i < this.Triangles.Length; i += 6)
                {

                    int rootVert = (i / 6) * 4;

                    this.Triangles[i] = rootVert;
                    this.Triangles[i + 1] = rootVert + 1;
                    this.Triangles[i + 2] = rootVert + 2;
                    this.Triangles[i + 3] = rootVert;
                    this.Triangles[i + 4] = rootVert + 2;
                    this.Triangles[i + 5] = rootVert + 3;

                }
            }

            /// <summary>
            /// return medh object 
            /// </summary>
            public Mesh Mesh
            {
                get
                {
                    Mesh mesh = new Mesh();
                    GenerateTriangles(this.Vertices.Count);
                    mesh.SetVertices(this.Vertices);
                    mesh.SetTriangles(this.Triangles, 0);
                    mesh.RecalculateNormals();
                    mesh.RecalculateTangents();
                    return mesh;
                }
            }

        }

        #endregion  Final Mesh
    }
}