using System;
using System.Windows.Forms;
using SharpGL;

namespace OpenGLSharpGLDemo;

public partial class Form1 : Form
{
    private float rotation = 0.0f;
    private float rotationX = 20f;
    private float rotationY = 30f;
    private float zoom = -6.0f;

    private Point lastMousePos;
    private bool isDragging = false;

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
        gl.Translate(0.0f, 0.0f, zoom);
        gl.Rotate(rotationX, 1.0f, 0.0f, 0.0f);
        gl.Rotate(rotationY, 0.0f, 1.0f, 0.0f);

        DrawCube(gl); // Вынеси отрисовку куба в отдельный метод
    }

    private void DrawCube(OpenGL gl)
    {
        gl.Begin(OpenGL.GL_QUADS);

        // Верх
        gl.Normal(0, 1, 0);
        gl.Color(1f, 0f, 0f);
        gl.Vertex(-1, 1, -1); gl.Vertex(1, 1, -1); gl.Vertex(1, 1, 1); gl.Vertex(-1, 1, 1);

        // Низ
        gl.Normal(0, -1, 0);
        gl.Color(0f, 1f, 0f);
        gl.Vertex(-1, -1, 1); gl.Vertex(1, -1, 1); gl.Vertex(1, -1, -1); gl.Vertex(-1, -1, -1);

        // Перед
        gl.Normal(0, 0, 1);
        gl.Color(0f, 0f, 1f);
        gl.Vertex(-1, 1, 1); gl.Vertex(1, 1, 1); gl.Vertex(1, -1, 1); gl.Vertex(-1, -1, 1);

        // Зад
        gl.Normal(0, 0, -1);
        gl.Color(1f, 1f, 0f);
        gl.Vertex(-1, -1, -1); gl.Vertex(1, -1, -1); gl.Vertex(1, 1, -1); gl.Vertex(-1, 1, -1);

        // Правая
        gl.Normal(1, 0, 0);
        gl.Color(1f, 0f, 1f);
        gl.Vertex(1, 1, -1); gl.Vertex(1, 1, 1); gl.Vertex(1, -1, 1); gl.Vertex(1, -1, -1);

        // Левая
        gl.Normal(-1, 0, 0);
        gl.Color(0f, 1f, 1f);
        gl.Vertex(-1, 1, 1); gl.Vertex(-1, 1, -1); gl.Vertex(-1, -1, -1); gl.Vertex(-1, -1, 1);

        gl.End();
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

        // Включаем освещение и один источник света
        gl.Enable(OpenGL.GL_LIGHTING);
        gl.Enable(OpenGL.GL_LIGHT0);

        // Параметры света
        float[] lightPosition = { 2.0f, 2.0f, 2.0f, 1.0f };
        float[] lightAmbient = { 0.2f, 0.2f, 0.2f, 1.0f };
        float[] lightDiffuse = { 0.8f, 0.8f, 0.8f, 1.0f };
        float[] lightSpecular = { 1.0f, 1.0f, 1.0f, 1.0f };

        gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lightPosition);
        gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, lightAmbient);
        gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, lightDiffuse);
        gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, lightSpecular);

        // Материал (по умолчанию — можно не указывать)
        gl.Enable(OpenGL.GL_COLOR_MATERIAL);
        gl.ShadeModel(OpenGL.GL_SMOOTH);

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

    private void openGLControl_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDragging = true;
            lastMousePos = e.Location;
        }
    }

    private void openGLControl_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            isDragging = false;
    }

    private void openGLControl_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging)
        {
            var dx = e.X - lastMousePos.X;
            var dy = e.Y - lastMousePos.Y;

            rotationY += dx * 0.5f;
            rotationX += dy * 0.5f;

            lastMousePos = e.Location;
        }
    }

    private void openGLControl_MouseWheel(object sender, MouseEventArgs e)
    {
        zoom += e.Delta > 0 ? 0.5f : -0.5f;
    }

}
