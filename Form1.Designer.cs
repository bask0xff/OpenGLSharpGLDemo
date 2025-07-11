namespace OpenGLSharpGLDemo;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;


    private SharpGL.OpenGLControl openGLControl;


    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.openGLControl = new SharpGL.OpenGLControl();
        ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
        this.SuspendLayout();

        // 
        // openGLControl
        // 
        this.openGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
        this.openGLControl.DrawFPS = false;
        this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
        this.openGLControl.RenderContextType = SharpGL.RenderContextType.NativeWindow;
        this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.Manual;
        this.openGLControl.Size = new System.Drawing.Size(800, 600);
        this.openGLControl.TabIndex = 0;
        this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
        this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
        this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);

        // 
        // Form1
        // 
        this.ClientSize = new System.Drawing.Size(800, 600);
        this.Controls.Add(this.openGLControl);
        this.Name = "Form1";
        this.Text = "OpenGL Demo";
        this.Load += new System.EventHandler(this.Form1_Load);
        ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
        this.ResumeLayout(false);
    }


    #endregion
}
