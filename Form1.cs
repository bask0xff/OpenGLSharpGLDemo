using System;
using System.Windows.Forms;
using SharpGL;

namespace OpenGLSharpGLDemo;

public partial class Form1 : Form
{
    private float rotation = 0.0f;

    public Form1()
    {
        InitializeComponent();
    }

    private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
    {
        var gl = openGLControl.OpenGL;

        gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        gl.LoadIdentity();
        gl.Translate(0.0f, 0.0f, -6.0f);
        gl.Rotate(rotation, 1.0f, 1.0f, 0.0f);

        gl.Begin(OpenGL.GL_QUADS);

        // ????? ????
        gl.Color(1f, 0f, 0f); // ???????
        gl.Vertex(-1.0f, 1.0f, -1.0f);
        gl.Vertex(1.0f, 1.0f, -1.0f);
        gl.Vertex(1.0f, 1.0f, 1.0f);
        gl.Vertex(-1.0f, 1.0f, 1.0f);

        gl.Color(0f, 1f, 0f); // ???????
        gl.Vertex(-1.0f, -1.0f, 1.0f);
        gl.Vertex(1.0f, -1.0f, 1.0f);
        gl.Vertex(1.0f, -1.0f, -1.0f);
        gl.Vertex(-1.0f, -1.0f, -1.0f);

        gl.Color(0f, 0f, 1f); // ?????
        gl.Vertex(-1.0f, 1.0f, 1.0f);
        gl.Vertex(1.0f, 1.0f, 1.0f);
        gl.Vertex(1.0f, -1.0f, 1.0f);
        gl.Vertex(-1.0f, -1.0f, 1.0f);

        gl.Color(1f, 1f, 0f); // ??????
        gl.Vertex(-1.0f, -1.0f, -1.0f);
        gl.Vertex(1.0f, -1.0f, -1.0f);
        gl.Vertex(1.0f, 1.0f, -1.0f);
        gl.Vertex(-1.0f, 1.0f, -1.0f);

        gl.Color(1f, 0f, 1f); // ??????????
        gl.Vertex(1.0f, 1.0f, -1.0f);
        gl.Vertex(1.0f, 1.0f, 1.0f);
        gl.Vertex(1.0f, -1.0f, 1.0f);
        gl.Vertex(1.0f, -1.0f, -1.0f);

        gl.Color(0f, 1f, 1f); // ???????
        gl.Vertex(-1.0f, 1.0f, 1.0f);
        gl.Vertex(-1.0f, 1.0f, -1.0f);
        gl.Vertex(-1.0f, -1.0f, -1.0f);
        gl.Vertex(-1.0f, -1.0f, 1.0f);

        gl.End();
        gl.Flush();

        rotation += 1.0f;
    }

    private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
    {
        var gl = openGLControl.OpenGL;
        gl.ClearColor(0f, 0f, 0f, 1f);
        gl.Enable(OpenGL.GL_DEPTH_TEST);
    }

}
