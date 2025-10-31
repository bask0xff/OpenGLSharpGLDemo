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

        gl.Translate(0, 0, zoom);
        gl.Rotate(rotationX, 1, 0, 0);
        gl.Rotate(rotationY, 0, 1, 0);

        float[] lightPos = { 4f, 4f, 4f, 1f };
        gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lightPos);

        float[] ambient = { 0.7f, 0.7f, 0.7f, 1.0f };
        gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, ambient);

        float[] diffuse = { 0.6f, 0.6f, 0.6f, 1.0f };
        gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT_AND_DIFFUSE, diffuse);

        // === Glow pass ===
        DrawGlowCube(gl);

        // === Main cube pass ===
        DrawCube(gl);

        gl.Flush();
    }

    private void DrawGlowCube(OpenGL gl)
    {
        gl.PushAttrib(OpenGL.GL_ENABLE_BIT | OpenGL.GL_COLOR_BUFFER_BIT);

        gl.Disable(OpenGL.GL_LIGHTING);
        gl.Enable(OpenGL.GL_BLEND);
        gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE);
        gl.Disable(OpenGL.GL_DEPTH_TEST);

        float baseR = 0.8f, baseG = 1.0f, baseB = 0.5f;

        int glowLayers = 32;
        float maxOffsetX = 0.04f;
        float maxOffsetY = 0.025f;
        float maxOffsetZ = 0.03f;

        float time = (float)DateTime.Now.TimeOfDay.TotalSeconds;
        float pulsate = 1.0f + 0.03f * (float)Math.Sin(time * 2.0f); // мягче дыхание

        for (int i = 0; i < glowLayers; i++)
        {
            float t = (float)i / (glowLayers - 1);
            float spread = (float)Math.Sin(t * Math.PI / 2);

            float sx = (1.0f + maxOffsetX * spread) * pulsate;
            float sy = (1.0f + maxOffsetY * spread) * pulsate;
            float sz = (1.0f + maxOffsetZ * spread) * pulsate;

            float g = baseG + 0.05f * (float)Math.Sin(time * 1.5f + t * 6.0f); // чуть меньше перелива

            // Альфа теперь убывает квадратично: тише, мягче
            float alpha = 0.01f * (1.0f - t * t);

            gl.PushMatrix();
            gl.Scale(sx, sy, sz);
            gl.Color(baseR, g, baseB, alpha);
            DrawCubeGeometry(gl);
            gl.PopMatrix();
        }

        gl.PopAttrib();
    }

    private void DrawCubeGeometry(OpenGL gl)
    {
        gl.Begin(OpenGL.GL_QUADS);

        // Верх
        gl.Vertex(-1, 1, -1); gl.Vertex(1, 1, -1); gl.Vertex(1, 1, 1); gl.Vertex(-1, 1, 1);
        // Низ
        gl.Vertex(-1, -1, 1); gl.Vertex(1, -1, 1); gl.Vertex(1, -1, -1); gl.Vertex(-1, -1, -1);
        // Перед
        gl.Vertex(-1, 1, 1); gl.Vertex(1, 1, 1); gl.Vertex(1, -1, 1); gl.Vertex(-1, -1, 1);
        // Зад
        gl.Vertex(-1, -1, -1); gl.Vertex(1, -1, -1); gl.Vertex(1, 1, -1); gl.Vertex(-1, 1, -1);
        // Правая
        gl.Vertex(1, 1, -1); gl.Vertex(1, 1, 1); gl.Vertex(1, -1, 1); gl.Vertex(1, -1, -1);
        // Левая
        gl.Vertex(-1, 1, 1); gl.Vertex(-1, 1, -1); gl.Vertex(-1, -1, -1); gl.Vertex(-1, -1, 1);

        gl.End();
    }


    private void DrawCube(OpenGL gl)
    {
        float[] specular = { 1.0f, 1.0f, 1.0f, 1.0f };
        gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SPECULAR, specular);
        gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SHININESS, 128.0f); // максимум блеска

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

        // Освещение
        gl.Enable(OpenGL.GL_LIGHTING);
        gl.Enable(OpenGL.GL_LIGHT0);

        gl.Enable(OpenGL.GL_BLEND);
        gl.Hint(OpenGL.GL_LINE_SMOOTH_HINT, OpenGL.GL_NICEST);
        gl.Enable(OpenGL.GL_LINE_SMOOTH);
        gl.Enable(OpenGL.GL_POLYGON_SMOOTH);
        gl.Hint(OpenGL.GL_POLYGON_SMOOTH_HINT, OpenGL.GL_NICEST);

        float[] lightAmbient = { 0.1f, 0.1f, 0.1f, 1.0f };
        float[] lightDiffuse = { 0.9f, 0.9f, 0.9f, 1.0f };
        float[] lightSpecular = { 1.0f, 1.0f, 1.0f, 1.0f };
        float[] lightPosition = { 4.0f, 4.0f, 4.0f, 1.0f }; // Точка в 3D-пространстве

        gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, lightAmbient);
        gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, lightDiffuse);
        gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, lightSpecular);
        gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, lightPosition);

        // Материал
        gl.Enable(OpenGL.GL_COLOR_MATERIAL);
        gl.ColorMaterial(OpenGL.GL_FRONT, OpenGL.GL_AMBIENT_AND_DIFFUSE);
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
