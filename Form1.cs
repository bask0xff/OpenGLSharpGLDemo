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

    private void Form1_Load(object sender, EventArgs e)
    {
        var timer = new System.Windows.Forms.Timer();
        timer.Interval = 16; // ~60 FPS
        timer.Tick += (s, ev) => openGLControl.DoRender();
        timer.Start();
    }


    private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
    {
        var gl = openGLControl.OpenGL;

        gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        gl.LoadIdentity();
        gl.Translate(0.0f, 0.0f, -6.0f);
        gl.Rotate(rotation, 1.0f, 1.0f, 0.0f);

        gl.Begin(OpenGL.GL_QUADS);

        // Грани куба
        gl.Color(1f, 0f, 0f); // красная
        gl.Vertex(-1.0f, 1.0f, -1.0f);
        gl.Vertex(1.0f, 1.0f, -1.0f);
        gl.Vertex(1.0f, 1.0f, 1.0f);
        gl.Vertex(-1.0f, 1.0f, 1.0f);

        gl.Color(0f, 1f, 0f); // зелёная
        gl.Vertex(-1.0f, -1.0f, 1.0f);
        gl.Vertex(1.0f, -1.0f, 1.0f);
        gl.Vertex(1.0f, -1.0f, -1.0f);
        gl.Vertex(-1.0f, -1.0f, -1.0f);

        gl.Color(0f, 0f, 1f); // синяя
        gl.Vertex(-1.0f, 1.0f, 1.0f);
        gl.Vertex(1.0f, 1.0f, 1.0f);
        gl.Vertex(1.0f, -1.0f, 1.0f);
        gl.Vertex(-1.0f, -1.0f, 1.0f);

        gl.Color(1f, 1f, 0f); // жёлтая
        gl.Vertex(-1.0f, -1.0f, -1.0f);
        gl.Vertex(1.0f, -1.0f, -1.0f);
        gl.Vertex(1.0f, 1.0f, -1.0f);
        gl.Vertex(-1.0f, 1.0f, -1.0f);

        gl.Color(1f, 0f, 1f); // фиолетовая
        gl.Vertex(1.0f, 1.0f, -1.0f);
        gl.Vertex(1.0f, 1.0f, 1.0f);
        gl.Vertex(1.0f, -1.0f, 1.0f);
        gl.Vertex(1.0f, -1.0f, -1.0f);

        gl.Color(0f, 1f, 1f); // голубая
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

        // Настройка перспективной проекции
        gl.MatrixMode(OpenGL.GL_PROJECTION);
        gl.LoadIdentity();
        gl.Perspective(45.0f, (double)openGLControl.Width / openGLControl.Height, 1.0, 100.0);
        gl.MatrixMode(OpenGL.GL_MODELVIEW);
    }

    private void openGLControl_Resized(object sender, EventArgs e)
    {
        var gl = openGLControl.OpenGL;
        gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);

        gl.MatrixMode(OpenGL.GL_PROJECTION);
        gl.LoadIdentity();
        gl.Perspective(45.0f, (double)openGLControl.Width / openGLControl.Height, 1.0, 100.0);
        gl.MatrixMode(OpenGL.GL_MODELVIEW);
    }

}
