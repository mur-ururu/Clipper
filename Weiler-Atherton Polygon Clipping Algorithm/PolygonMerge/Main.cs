using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace PolygonMerge
{
    public partial class fmMain : Form
    {
        ImageEditor ImgEditor;


        public fmMain()
        {
            InitializeComponent();

            Image image = Image.FromFile(Application.StartupPath +@"\bmp_init.bmp");
            int width = image.Width;
            int height = image.Height;
            Bitmap bmp = new Bitmap(Image.FromFile("bmp_init.bmp"), width, height);
            pbPolygon.Image = bmp;

            ImgEditor = new ImageEditor(pbPolygon,this); 
            ImgEditor.EditingOff();
            ImgEditor.mouseSensitivity = trackBar1.Maximum - trackBar1.Value;
       
        }
        public void SetbtInnerContour(bool Enabled)
        {
            btInnerContour.Enabled = Enabled;
        }

        private void pbPolygon_MouseDown(object sender, MouseEventArgs e)
        {
            ImgEditor.MouseDown(e);
        }

        private void pbPolygon_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = e.X + "," + e.Y;
        }

        private void печатьПолигонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument.OriginAtMargins = true;
            printDocument.DocumentName = "POLYGON PRINTING";

            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDocument.Print();
        }
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmpSave = (Bitmap)pbPolygon.Image;
            bmpSave.Save(@"polygToPrint.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            e.Graphics.DrawImage(bmpSave, 0, 0);
        }

        private void btSaveToDB_Click(object sender, EventArgs e)
        {
            if (ImgEditor.CurPolygon == null) {MessageBox.Show("Полигон не выбран"); return;}
            if (txtSaveName.Text != "")
            {
                DBConnector DBConnect = new DBConnector();

                string name = txtSaveName.Text;
                if (DBConnect.PolygonExists(name))
                {
                    DialogResult result = MessageBox.Show("Полигон с этим именем существует. Перезаписать?",
                            "Important Question",
                            MessageBoxButtons.YesNo);
                    if (result == System.Windows.Forms.DialogResult.No) return;
                    DBConnect.DeletePolygonByName(name);
                } 
                DBConnect.SavePolygon(ImgEditor.CurPolygon, name);
                txtSaveName.Text = "";
            }
            else
            {
                MessageBox.Show("Введите имя полигона");
                txtSaveName.Focus();
            }
        }

        private void btGetFromDB_Click(object sender, EventArgs e)
        {
            if (ImgEditor.Polygs.Count == 2) {  MessageBox.Show("Вначале объедините существующие полигоны"); return;} 
            
            
            if (cbFromBase.SelectedIndex!=-1)
            {
                DBConnector DBConnect = new DBConnector();
               
                string name = cbFromBase.SelectedItem.ToString();           

                Polygon poly = DBConnect.GetPolygonByName(name);
                
                //g.Clear(Color.White);
                Graphics g = ImgEditor.GetGraphics();
                poly.Paint(g);
                ImgEditor.ReDraw();

                ImgEditor.Polygs.Add(poly);
                ImgEditor.CurPolygon = poly;
            }
            else
            {
                MessageBox.Show("Выберите полигон");
                cbFromBase.Focus();
            }
        }
        
        private void txtSaveName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtSaveName.Clear();
        }
        private void btNew_Click(object sender, EventArgs e)
        {
            gbEditor.Enabled = true;
            btUnion.Enabled = false;
            btNew.Enabled = false;

            btOuterContour.Enabled = true;
            btInnerContour.Enabled = false;
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            ImgEditor.DeletePoint(e);
        }

        private void btAddPoint_Click(object sender, EventArgs e)
        {
            ImgEditor.AddPoint(e);
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            ImgEditor.ImageClean();
            gbEditor.Enabled = false;
            btUnion.Enabled = true;
            btNew.Enabled = true;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ImgEditor.mouseSensitivity = trackBar1.Maximum - trackBar1.Value;

        }

        private void btUnion_Click(object sender, EventArgs e)
        {
            gbEditor.Enabled = false;
            ImgEditor.Merge();            
        }

        private void btOuterContour_Click(object sender, EventArgs e)
        {
            btOuterContour.Enabled = false;
            ImgEditor.AddOuterContour();
            
        }

        private void btInnerContour_Click(object sender, EventArgs e)
        {
            ImgEditor.AddInnerContour();
        }

        private void btComplete_Click(object sender, EventArgs e)
        {
            ImgEditor.ValidateCur();
            if (ImgEditor.NotValidated)
            {
                btUnion.Enabled = false;
                btNew.Enabled = false;                
            }
            else
            {
                gbOp.Enabled = true;
                btUnion.Enabled = true;
                btNew.Enabled = true;
                btInnerContour.Enabled = false;
            }
        }

        private void cbFromBase_Click(object sender, EventArgs e)
        {
            DBConnector DBConnect = new DBConnector();
            List<string> names = DBConnect.loadPolygonsList();
            cbFromBase.Items.Clear();
            foreach (string s in names)
            {
                cbFromBase.Items.Add(s);
            }
        }

        private void fmMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            ImgEditor.KeyPress(e);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImgEditor.CurPolygon==null) {   MessageBox.Show ("Полигон не выбран"); return;}

            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить полигон ...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;           
            savedialog.Filter = "txt files (*.txt)|*.txt";
            savedialog.ShowHelp = true;            
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = savedialog.FileName;
                string[] lines = ImgEditor.CurPolygon.ToStringArray("polygon");
                ToFileFromFile.SaveToFile(lines, fileName); 
            }             

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImgEditor.Polygs.Count == 2) { MessageBox.Show("Вначале объедините существующие полигоны"); return; }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите файл";
            openFileDialog.Filter = "Текстовые файлы|*.txt";

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            string[] lines = ToFileFromFile.ReadFromFile(openFileDialog.FileName);

            Polygon poly = new Polygon(lines);
            Graphics g = ImgEditor.GetGraphics();
            poly.Paint(g);
            ImgEditor.ReDraw();

            ImgEditor.Polygs.Add(poly);
            ImgEditor.CurPolygon = poly;
        }
    }
}