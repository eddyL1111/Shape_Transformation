using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace Transformations
{
    /// <summary>
    /// Summary description for Transformer.
    /// </summary>
    public class Transformer : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;
        //private bool GetNewData();

        // basic data for Transformer

        int numpts = 0;
        int numlines = 0;
        bool gooddata = false;
        double[,] vertices;
        double[,] scrnpts;
        double[,] ctrans = new double[4, 4];  //your main transformation matrix
        int _Row = 4;
        int _Col = 4;
        public static int rotType = 0;
        private BackgroundWorker bw = new BackgroundWorker();
        private System.Windows.Forms.ImageList tbimages;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ToolBarButton transleftbtn;
        private System.Windows.Forms.ToolBarButton transrightbtn;
        private System.Windows.Forms.ToolBarButton transupbtn;
        private System.Windows.Forms.ToolBarButton transdownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton1;
        private System.Windows.Forms.ToolBarButton scaleupbtn;
        private System.Windows.Forms.ToolBarButton scaledownbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton2;
        private System.Windows.Forms.ToolBarButton rotxby1btn;
        private System.Windows.Forms.ToolBarButton rotyby1btn;
        private System.Windows.Forms.ToolBarButton rotzby1btn;
        private System.Windows.Forms.ToolBarButton toolBarButton3;
        private System.Windows.Forms.ToolBarButton rotxbtn;
        private System.Windows.Forms.ToolBarButton rotybtn;
        private System.Windows.Forms.ToolBarButton rotzbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton4;
        private System.Windows.Forms.ToolBarButton shearrightbtn;
        private System.Windows.Forms.ToolBarButton shearleftbtn;
        private System.Windows.Forms.ToolBarButton toolBarButton5;
        private System.Windows.Forms.ToolBarButton resetbtn;
        private System.Windows.Forms.ToolBarButton exitbtn;
        int[,] lines;

        public Transformer()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            Text = "Shape Transformations";
            ResizeRedraw = true;
            BackColor = Color.Black;
            MenuItem miNewDat = new MenuItem("New &Data...",
                new EventHandler(MenuNewDataOnClick));
            MenuItem miExit = new MenuItem("E&xit",
                new EventHandler(MenuFileExitOnClick));
            MenuItem miDash = new MenuItem("-");
            MenuItem miFile = new MenuItem("&File",
                new MenuItem[] { miNewDat, miDash, miExit });
            MenuItem miAbout = new MenuItem("&About",
                new EventHandler(MenuAboutOnClick));
            Menu = new MainMenu(new MenuItem[] { miFile, miAbout });

            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transformer));
            this.tbimages = new System.Windows.Forms.ImageList(this.components);
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.transleftbtn = new System.Windows.Forms.ToolBarButton();
            this.transrightbtn = new System.Windows.Forms.ToolBarButton();
            this.transupbtn = new System.Windows.Forms.ToolBarButton();
            this.transdownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.scaleupbtn = new System.Windows.Forms.ToolBarButton();
            this.scaledownbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.rotxby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotyby1btn = new System.Windows.Forms.ToolBarButton();
            this.rotzby1btn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.rotxbtn = new System.Windows.Forms.ToolBarButton();
            this.rotybtn = new System.Windows.Forms.ToolBarButton();
            this.rotzbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.shearrightbtn = new System.Windows.Forms.ToolBarButton();
            this.shearleftbtn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resetbtn = new System.Windows.Forms.ToolBarButton();
            this.exitbtn = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // tbimages
            // 
            this.tbimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tbimages.ImageStream")));
            this.tbimages.TransparentColor = System.Drawing.Color.Transparent;
            this.tbimages.Images.SetKeyName(0, "");
            this.tbimages.Images.SetKeyName(1, "");
            this.tbimages.Images.SetKeyName(2, "");
            this.tbimages.Images.SetKeyName(3, "");
            this.tbimages.Images.SetKeyName(4, "");
            this.tbimages.Images.SetKeyName(5, "");
            this.tbimages.Images.SetKeyName(6, "");
            this.tbimages.Images.SetKeyName(7, "");
            this.tbimages.Images.SetKeyName(8, "");
            this.tbimages.Images.SetKeyName(9, "");
            this.tbimages.Images.SetKeyName(10, "");
            this.tbimages.Images.SetKeyName(11, "");
            this.tbimages.Images.SetKeyName(12, "");
            this.tbimages.Images.SetKeyName(13, "");
            this.tbimages.Images.SetKeyName(14, "");
            this.tbimages.Images.SetKeyName(15, "");
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.transleftbtn,
            this.transrightbtn,
            this.transupbtn,
            this.transdownbtn,
            this.toolBarButton1,
            this.scaleupbtn,
            this.scaledownbtn,
            this.toolBarButton2,
            this.rotxby1btn,
            this.rotyby1btn,
            this.rotzby1btn,
            this.toolBarButton3,
            this.rotxbtn,
            this.rotybtn,
            this.rotzbtn,
            this.toolBarButton4,
            this.shearrightbtn,
            this.shearleftbtn,
            this.toolBarButton5,
            this.resetbtn,
            this.exitbtn});
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.tbimages;
            this.toolBar1.Location = new System.Drawing.Point(484, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(24, 306);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // transleftbtn
            // 
            this.transleftbtn.ImageIndex = 1;
            this.transleftbtn.Name = "transleftbtn";
            this.transleftbtn.ToolTipText = "translate left";
            // 
            // transrightbtn
            // 
            this.transrightbtn.ImageIndex = 0;
            this.transrightbtn.Name = "transrightbtn";
            this.transrightbtn.ToolTipText = "translate right";
            // 
            // transupbtn
            // 
            this.transupbtn.ImageIndex = 2;
            this.transupbtn.Name = "transupbtn";
            this.transupbtn.ToolTipText = "translate up";
            // 
            // transdownbtn
            // 
            this.transdownbtn.ImageIndex = 3;
            this.transdownbtn.Name = "transdownbtn";
            this.transdownbtn.ToolTipText = "translate down";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // scaleupbtn
            // 
            this.scaleupbtn.ImageIndex = 4;
            this.scaleupbtn.Name = "scaleupbtn";
            this.scaleupbtn.ToolTipText = "scale up";
            // 
            // scaledownbtn
            // 
            this.scaledownbtn.ImageIndex = 5;
            this.scaledownbtn.Name = "scaledownbtn";
            this.scaledownbtn.ToolTipText = "scale down";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxby1btn
            // 
            this.rotxby1btn.ImageIndex = 6;
            this.rotxby1btn.Name = "rotxby1btn";
            this.rotxby1btn.ToolTipText = "rotate about x by 1";
            // 
            // rotyby1btn
            // 
            this.rotyby1btn.ImageIndex = 7;
            this.rotyby1btn.Name = "rotyby1btn";
            this.rotyby1btn.ToolTipText = "rotate about y by 1";
            // 
            // rotzby1btn
            // 
            this.rotzby1btn.ImageIndex = 8;
            this.rotzby1btn.Name = "rotzby1btn";
            this.rotzby1btn.ToolTipText = "rotate about z by 1";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // rotxbtn
            // 
            this.rotxbtn.ImageIndex = 9;
            this.rotxbtn.Name = "rotxbtn";
            this.rotxbtn.ToolTipText = "rotate about x continuously";
            // 
            // rotybtn
            // 
            this.rotybtn.ImageIndex = 10;
            this.rotybtn.Name = "rotybtn";
            this.rotybtn.ToolTipText = "rotate about y continuously";
            // 
            // rotzbtn
            // 
            this.rotzbtn.ImageIndex = 11;
            this.rotzbtn.Name = "rotzbtn";
            this.rotzbtn.ToolTipText = "rotate about z continuously";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // shearrightbtn
            // 
            this.shearrightbtn.ImageIndex = 12;
            this.shearrightbtn.Name = "shearrightbtn";
            this.shearrightbtn.ToolTipText = "shear right";
            // 
            // shearleftbtn
            // 
            this.shearleftbtn.ImageIndex = 13;
            this.shearleftbtn.Name = "shearleftbtn";
            this.shearleftbtn.ToolTipText = "shear left";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resetbtn
            // 
            this.resetbtn.ImageIndex = 14;
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.ToolTipText = "restore the initial image";
            // 
            // exitbtn
            // 
            this.exitbtn.ImageIndex = 15;
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.ToolTipText = "exit the program";
            // 
            // Transformer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(508, 306);
            this.Controls.Add(this.toolBar1);
            this.Name = "Transformer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Transformer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Transformer());
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.White, 3);
            double temp;
            int k;

            if (gooddata)
            {
                //create the screen coordinates:
                // scrnpts = vertices*ctrans

                for (int i = 0; i < numpts; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        temp = 0.0d;
                        for (k = 0; k < 4; k++)
                            temp += vertices[i, k] * ctrans[k, j];
                        scrnpts[i, j] = temp;
                    }
                }

                //now draw the lines

                for (int i = 0; i < numlines; i++)
                {
                    grfx.DrawLine(pen, (int)scrnpts[lines[i, 0], 0], (int)scrnpts[lines[i, 0], 1],
                        (int)scrnpts[lines[i, 1], 0], (int)scrnpts[lines[i, 1], 1]);
                }


            } // end of gooddata block	
        } // end of OnPaint

        void MenuNewDataOnClick(object obj, EventArgs ea)
        {
            //MessageBox.Show("New Data item clicked.");
            gooddata = GetNewData();
            RestoreInitialImage();
        }

        void MenuFileExitOnClick(object obj, EventArgs ea)
        {
            Close();
        }

        void MenuAboutOnClick(object obj, EventArgs ea)
        {
            AboutDialogBox dlg = new AboutDialogBox();
            dlg.ShowDialog();
        }

        void RestoreInitialImage()
        {
            setIdentity(ctrans, _Row, _Col);
            Invalidate();
        } // end of RestoreInitialImage

        bool GetNewData()
        {
            string strinputfile, text;
            ArrayList coorddata = new ArrayList();
            ArrayList linesdata = new ArrayList();
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Title = "Choose File with Coordinates of Vertices";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo coordfile = new FileInfo(strinputfile);
                StreamReader reader = coordfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) coorddata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeCoords(coorddata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Coordinates File***");
                return false;
            }

            opendlg.Title = "Choose File with Data Specifying Lines";
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                strinputfile = opendlg.FileName;
                FileInfo linesfile = new FileInfo(strinputfile);
                StreamReader reader = linesfile.OpenText();
                do
                {
                    text = reader.ReadLine();
                    if (text != null) linesdata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeLines(linesdata);
            }
            else
            {
                MessageBox.Show("***Failed to Open Line Data File***");
                return false;
            }
            scrnpts = new double[numpts, 4];
            ctrans = new double[4, 4]; // create new array whenever new data is loaded
            setIdentity(ctrans, 4, 4);  //initialize transformation matrix to identity
            return true;
        } // end of GetNewData

        void DecodeCoords(ArrayList coorddata)
        {
            //this may allocate slightly more rows that necessary
            vertices = new double[coorddata.Count, 4];
            numpts = 0;
            string[] text = null;
            for (int i = 0; i < coorddata.Count; i++)
            {
                text = coorddata[i].ToString().Split(' ', ',');
                vertices[numpts, 0] = double.Parse(text[0]);
                if (vertices[numpts, 0] < 0.0d) break;
                vertices[numpts, 1] = double.Parse(text[1]); // y
                vertices[numpts, 2] = double.Parse(text[2]);
                vertices[numpts, 3] = 1.0d;
                numpts++;
            }

        }// end of DecodeCoords

        void DecodeLines(ArrayList linesdata)
        {
            //this may allocate slightly more rows that necessary
            lines = new int[linesdata.Count, 2];
            numlines = 0;
            string[] text = null;
            for (int i = 0; i < linesdata.Count; i++)
            {
                text = linesdata[i].ToString().Split(' ', ',');
                lines[numlines, 0] = int.Parse(text[0]);
                if (lines[numlines, 0] < 0) break;
                lines[numlines, 1] = int.Parse(text[1]);
                numlines++;
            }
        } // end of DecodeLines

        void setIdentity(double[,] A, int nrow, int ncol)
        {
            /// vi) Three main arrays:
            ///     -vertices[,] Initial coordinates of the points
            ///     -scrnpts[,] Transformed points for rendering
            ///     -ctrans[,]  Cumulative transformation matrix
            ///  Only work with the cumulative transformation matrix

            /// Initializing the A[,] matrix
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++)
                {
                    A[i, j] = 0.0d;
                }
                A[i, i] = 1.0d;
            } // end of printing


            // Assuming all centre points are the first element in the data
            double tX = vertices[0, 0] * -1; // Translate tX units to the origin X=0
            double tY = vertices[0, 1] * -1; // Translate tY units to the origin Y=0
            double tZ = vertices[0, 2]; // For 3D shapes

            // Translating all points from shape from the shape's centre to the screen origin
            translate(A, nrow, ncol, tX, tY, tZ);

            // Scaling: Shape height equal half of the screen/window height
            //double xLen = vertices[0, 0] * 2;
            double yLen = vertices[0, 1] * 2; // Original vertical length of the shape
            double halfScreen = (Screen.PrimaryScreen.Bounds.Height / 2);
            double sY = (halfScreen / yLen); // scale factor of y
            double sX = sY;
            double sZ = sY;
            scale(A, nrow, ncol, sX, sY, sZ);

            // Reflection on the y axis from the centre of the shape
            double refX = 1.0d;
            double refY = -1.0d;
            double refZ = 1.0d;
            reflect(A, nrow, ncol, refX, refY, refZ);

            // Translating back to the centre of the screen/window
            tX = Screen.PrimaryScreen.Bounds.Width / 2;
            tY = Screen.PrimaryScreen.Bounds.Height / 2;
            //tZ = 0.0d;
            translate(A, nrow, ncol, tX, tY, tZ);

        }// end of setIdentity

        private void translate(double[,] A, int nrow, int ncol, double x, double y, double z)
        {
            double[,] initTranslationMx = {
                    { 1, 0, 0, 0},
                    { 0, 1, 0, 0},
                    { 0, 0, 1, 0},
                    { x, y, z, 1}
                };
            multiplyMatrix(A, initTranslationMx, nrow, ncol);
        }

        private void scale(double[,] A, int nrow, int ncol, double x, double y, double z)
        {
            double[,] scalingMx = {
                    {x,   0,  0, 0},
                    { 0,  y,  0, 0},
                    { 0,   0, z, 0},
                    { 0,   0,  0, 1}
                };
            multiplyMatrix(A, scalingMx, nrow, ncol);
        }

        private void reflect(double[,] A, int nrow, int ncol, double x, double y, double z)
        {
            double[,] reflectionMx = {
                    { x, 0, 0, 0},
                    { 0, y, 0, 0},
                    { 0, 0, z, 0},
                    { 0, 0, 0, 1}
                };

            multiplyMatrix(A, reflectionMx, nrow, ncol);
        }

        private void rotateX(double[,] A, int nrow, int ncol, double x)
        {
            double cos1 = Math.Cos(x);
            double sin1 = Math.Sin(x);
            double cos2 = Math.Cos(x);
            double sin2 = Math.Sin(x);

            double[,] xRotation = {
                    { 1,    0,     0, 0},
                    { 0,  cos1, -sin1, 0},
                    { 0,  sin2,  cos2, 0},
                    { 0,    0,     0, 1}
                };

            multiplyMatrix(A, xRotation, nrow, ncol);
        }

        private void rotateY(double[,] A, int nrow, int ncol, double y)
        {
            double cos1 = Math.Cos(y);
            double sin1 = Math.Sin(y);
            double cos2 = Math.Cos(y);
            double sin2 = Math.Sin(y);

            double[,] yRotation = {
                    { cos1,  0, -sin1, 0},
                    {    0,  1,     0, 0},
                    { sin2,  0,  cos2, 0},
                    {    0,  0,     0, 1}
                };

            multiplyMatrix(A, yRotation, nrow, ncol);
        }

        private void rotateZ(double[,] A, int nrow, int ncol, double z)
        {
            double cos1 = Math.Cos(z);
            double sin1 = Math.Sin(z);
            double cos2 = Math.Cos(z);
            double sin2 = Math.Sin(z);

            double[,] zRotation = {
                    {  cos1, sin1, 0, 0},
                    { -sin2, cos2, 0, 0},
                    {     0,    0, 1, 0},
                    {     0,    0, 0, 1}
                };

            multiplyMatrix(A, zRotation, nrow, ncol);
        }

        private void shearX(double[,] A, int nrow, int ncol, double x)
        {
            double[,] xShear = {
                    {  1, 0, 0, 0},
                    {  x, 1, 0, 0},
                    {  0, 0, 1, 0},
                    {  0, 0, 0, 1}
                };

            multiplyMatrix(A, xShear, nrow, ncol);

        }

        private void multiplyMatrix(double[,] A, double[,] matrix, int nrow, int ncol)
        {
            double[,] net = new double[4, 4];

            for (int y = 0; y < nrow; y++)
            {
                for (int x = 0; x < ncol; x++)
                {
                    for (int i = 0; i < ncol; i++)
                    {
                        net[y, x] += A[y, i] * matrix[i, x];
                    }// for i
                }// for x
            }// for y

            /// Assigning the new value of the cumulative transform matrix -- A[,]
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++)
                {
                    A[i, j] = net[i, j];
                }
            }

        }

        private void Transformer_Load(object sender, System.EventArgs e)
        {

        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
            double x, y, z;
			if (e.Button == transleftbtn) // Translate  left
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                x = -75.0d;  y = 0.0d;    z = 0.0d; 
                translate(ctrans, _Row, _Col, x, y, z);   
				Refresh();
			}
			if (e.Button == transrightbtn) // Translate right
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                x = 75.0d; y = 0.0d; z = 0.0d;
                translate(ctrans, _Row, _Col, x, y, z);
                Refresh();
			}
			if (e.Button == transupbtn) // Translate up
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                x = 0.0d; y = 35.0d; z = 0.0d;
                y *= -1;  // y axis is on top (screen coordinates)
                translate(ctrans, _Row, _Col, x, y, z);
                Refresh();
			}
			
			if(e.Button == transdownbtn) // Translate down
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                x = 0.0d; y = -35.0d; z = 0.0d;
                y *= -1; // y axis is on top (screen coordinates)
                translate(ctrans, _Row, _Col, x, y, z);
                Refresh();
			}
			if (e.Button == scaleupbtn) // Scale up
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                x = scrnpts[0, 0] * -1; // Translate tX units to the origin X=0
                y = scrnpts[0, 1] * -1; // Translate tY units to the origin Y=0
                z = scrnpts[0, 2]; // For 3D shapes
                translate(ctrans, _Row, _Col, x, y, z);

                x = 1.1d; y = 1.1; z = 1.1d;
                scale(ctrans, _Row, _Col, x, y, z); // Scale up by 10%

                x = scrnpts[0, 0]; // Translate back
                y = scrnpts[0, 1]; // Translate back
                z = scrnpts[0, 2]; // For 3D shapes
                translate(ctrans, _Row, _Col, x, y, z); 
                
                Refresh();
            }
			if (e.Button == scaledownbtn) // Scale down
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                x = scrnpts[0, 0] * -1; // Translate tX units to the origin X=0
                y = scrnpts[0, 1] * -1; // Translate tY units to the origin Y=0
                z = scrnpts[0, 2]; // For 3D shapes
                translate(ctrans, _Row, _Col, x, y, z);

                x = 0.9d; y = 0.9; z = 0.9d;
                scale(ctrans, _Row, _Col, x, y, z); // Scale down by 10%
                
                x = scrnpts[0, 0]; // Translate back
                y = scrnpts[0, 1]; // Translate back
                z = scrnpts[0, 2]; // For 3D shapes
                translate(ctrans, _Row, _Col, x, y, z);

                Refresh();
            }
			if (e.Button == rotxby1btn)  // rotate x in the yz plane
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                x = scrnpts[0, 0] * -1; // Translate tX units to the origin X=0
                y = scrnpts[0, 1] * -1; // Translate tY units to the origin Y=0
                z = scrnpts[0, 2] * -1; // For 3D shapes
                translate(ctrans, _Row, _Col, x, y, z);

                double xRad = 0.05d; 
                rotateX(ctrans, _Row, _Col, xRad);
                
                x = scrnpts[0, 0]; // Translate back
                y = scrnpts[0, 1]; // Translate back
                z = scrnpts[0, 2]; // For 3D shapes
                translate(ctrans, _Row, _Col, x, y, z);
                
                Refresh();
            }
			if (e.Button == rotyby1btn)  // rotate y
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                x = scrnpts[0, 0] * -1; // Translate tX units to the origin X=0
                y = scrnpts[0, 1] * -1; // Translate tY units to the origin Y=0
                z = scrnpts[0, 2] * -1; // For 3D shapes
                translate(ctrans, _Row, _Col, x, y, z);

                double xRad = 0.05d;
                rotateY(ctrans, _Row, _Col, xRad);

                x = scrnpts[0, 0]; // Translate back
                y = scrnpts[0, 1]; // Translate back
                z = scrnpts[0, 2]; // For 3D shapes
                translate(ctrans, _Row, _Col, x, y, z);

                Refresh();
            }
			if (e.Button == rotzby1btn) // Rotate on the xy plane
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                x = scrnpts[0, 0] * -1; // Translate tX units to the origin X=0
                y = scrnpts[0, 1] * -1; // Translate tY units to the origin Y=0
                z = scrnpts[0, 2] * -1; // For 3D shapes
                translate(ctrans, _Row, _Col, x, y, z);

                double xRad = 0.05d;
                rotateZ(ctrans, _Row, _Col, xRad);

                x = scrnpts[0, 0]; // Translate back
                y = scrnpts[0, 1]; // Translate back
                z = scrnpts[0, 2]; // For 3D shapes
                translate(ctrans, _Row, _Col, x, y, z);

                Refresh();
            }

			if (e.Button == rotxbtn) 
			{
                rotType = 1;
                if (bw.IsBusy != true)
                {
                    bw.RunWorkerAsync();
                }
            }
			if (e.Button == rotybtn) 
			{
                rotType = 2;
                if (bw.IsBusy != true)
                {
                    bw.RunWorkerAsync();
                }
            }
			
			if (e.Button == rotzbtn) 
			{
                rotType = 3;
                if (bw.IsBusy != true)
                {
                    bw.RunWorkerAsync();
                }
            }

			if(e.Button == shearleftbtn)
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                double[,] temp_ctrans = (double[,])ctrans.Clone();
                // Reflection on the y axis from the centre of the shape
                double refX = 1.0d;
                double refY = -1.0d;
                double refZ = 1.0d;
                reflect(ctrans, _Row, _Col, refX, refY, refZ);
                // Shear 
                double xFactor = -0.1d;
                shearX(ctrans, _Row, _Col, xFactor);
                // reflect back
                reflect(ctrans, _Row, _Col, refX, refY, refZ);
                ctrans[3, 0] = temp_ctrans[3, 0];
                ctrans[3, 1] = temp_ctrans[3, 1];

                Refresh();
			}

			if (e.Button == shearrightbtn) 
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                double[,] temp_ctrans = (double[,])ctrans.Clone();
                // Reflection on the y axis from the centre of the shape
                double refX = 1.0d;
                double refY = -1.0d;
                double refZ = 1.0d;
                reflect(ctrans, _Row, _Col, refX, refY, refZ);
                // Shear 
                double xFactor = 0.1d;
                shearX(ctrans, _Row, _Col, xFactor);
                // reflect back
                reflect(ctrans, _Row, _Col, refX, refY, refZ);
                ctrans[3, 0] = temp_ctrans[3, 0];
                ctrans[3, 1] = temp_ctrans[3, 1];

                Refresh();
            }

			if (e.Button == resetbtn)
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                RestoreInitialImage();
			}

			if(e.Button == exitbtn) 
			{
                if (bw.WorkerSupportsCancellation)
                {
                    bw.CancelAsync();
                }
                Close();
            }

		}

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            double x, y, z;
            double xRad = 0.05d;
            for (int i = 1; ; i++)

            {
                if ((worker.CancellationPending))
                {
                    e.Cancel = true;
                    break;
                }
                else if (rotType == 1)
                {
                    x = scrnpts[0, 0] * -1; // Translate tX units to the origin X=0
                    y = scrnpts[0, 1] * -1; // Translate tY units to the origin Y=0
                    z = scrnpts[0, 2] * -1; // For 3D shapes
                    translate(ctrans, _Row, _Col, x, y, z);

                    
                    rotateX(ctrans, _Row, _Col, xRad);

                    x = scrnpts[0, 0]; // Translate back
                    y = scrnpts[0, 1]; // Translate back
                    z = scrnpts[0, 2]; // For 3D shapes
                    translate(ctrans, _Row, _Col, x, y, z);

                    //Refresh();
                    System.Threading.Thread.Sleep(50);
                    worker.ReportProgress((i));
                } else if (rotType == 2)
                {
                    x = scrnpts[0, 0] * -1; // Translate tX units to the origin X=0
                    y = scrnpts[0, 1] * -1; // Translate tY units to the origin Y=0
                    z = scrnpts[0, 2] * -1; // For 3D shapes
                    translate(ctrans, _Row, _Col, x, y, z);

                    rotateY(ctrans, _Row, _Col, xRad);

                    x = scrnpts[0, 0]; // Translate back
                    y = scrnpts[0, 1]; // Translate back
                    z = scrnpts[0, 2]; // For 3D shapes
                    translate(ctrans, _Row, _Col, x, y, z);

                    //Refresh();
                    System.Threading.Thread.Sleep(50);
                    worker.ReportProgress((i));
                } else
                {
                    x = scrnpts[0, 0] * -1; // Translate tX units to the origin X=0
                    y = scrnpts[0, 1] * -1; // Translate tY units to the origin Y=0
                    z = scrnpts[0, 2] * -1; // For 3D shapes
                    translate(ctrans, _Row, _Col, x, y, z);

                    rotateZ(ctrans, _Row, _Col, xRad);

                    x = scrnpts[0, 0]; // Translate back
                    y = scrnpts[0, 1]; // Translate back
                    z = scrnpts[0, 2]; // For 3D shapes
                    translate(ctrans, _Row, _Col, x, y, z);

                    //Refresh();
                    System.Threading.Thread.Sleep(50);
                    worker.ReportProgress((i));
                }
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Refresh();
        }

    }

	
}
