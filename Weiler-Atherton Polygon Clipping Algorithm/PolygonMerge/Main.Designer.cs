namespace PolygonMerge
{
    partial class fmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.печатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.печатьПолигонаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.tbOptions = new System.Windows.Forms.TabControl();
            this.tbEditor = new System.Windows.Forms.TabPage();
            this.gbEditor = new System.Windows.Forms.GroupBox();
            this.btComplete = new System.Windows.Forms.Button();
            this.btInnerContour = new System.Windows.Forms.Button();
            this.btOuterContour = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btDelete = new System.Windows.Forms.Button();
            this.btAddPoint = new System.Windows.Forms.Button();
            this.gbOp = new System.Windows.Forms.GroupBox();
            this.btClear = new System.Windows.Forms.Button();
            this.btUnion = new System.Windows.Forms.Button();
            this.btNew = new System.Windows.Forms.Button();
            this.tbDatabase = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbFromBase = new System.Windows.Forms.ComboBox();
            this.btGetFromDB = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btSaveToDB = new System.Windows.Forms.Button();
            this.txtSaveName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.pbPolygon = new System.Windows.Forms.PictureBox();
            this.pMenu = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.tbOptions.SuspendLayout();
            this.tbEditor.SuspendLayout();
            this.gbEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.gbOp.SuspendLayout();
            this.tbDatabase.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPolygon)).BeginInit();
            this.pMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.печатьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(929, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.открытьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.файлToolStripMenuItem.Text = "Полигон";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить в файл";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.открытьToolStripMenuItem.Text = "Загрузить из файла";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // печатьToolStripMenuItem
            // 
            this.печатьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.печатьПолигонаToolStripMenuItem});
            this.печатьToolStripMenuItem.Name = "печатьToolStripMenuItem";
            this.печатьToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.печатьToolStripMenuItem.Text = "Печать";
            // 
            // печатьПолигонаToolStripMenuItem
            // 
            this.печатьПолигонаToolStripMenuItem.Name = "печатьПолигонаToolStripMenuItem";
            this.печатьПолигонаToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.печатьПолигонаToolStripMenuItem.Text = "Печать полигона";
            this.печатьПолигонаToolStripMenuItem.Click += new System.EventHandler(this.печатьПолигонаToolStripMenuItem_Click);
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // tbOptions
            // 
            this.tbOptions.Controls.Add(this.tbEditor);
            this.tbOptions.Controls.Add(this.tbDatabase);
            this.tbOptions.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tbOptions.ItemSize = new System.Drawing.Size(60, 18);
            this.tbOptions.Location = new System.Drawing.Point(2, 27);
            this.tbOptions.Name = "tbOptions";
            this.tbOptions.SelectedIndex = 0;
            this.tbOptions.Size = new System.Drawing.Size(665, 185);
            this.tbOptions.TabIndex = 2;
            // 
            // tbEditor
            // 
            this.tbEditor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbEditor.Controls.Add(this.gbEditor);
            this.tbEditor.Controls.Add(this.gbOp);
            this.tbEditor.Location = new System.Drawing.Point(4, 22);
            this.tbEditor.Name = "tbEditor";
            this.tbEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tbEditor.Size = new System.Drawing.Size(657, 159);
            this.tbEditor.TabIndex = 0;
            this.tbEditor.Text = "Редактор                              ";
            // 
            // gbEditor
            // 
            this.gbEditor.Controls.Add(this.btComplete);
            this.gbEditor.Controls.Add(this.btInnerContour);
            this.gbEditor.Controls.Add(this.btOuterContour);
            this.gbEditor.Controls.Add(this.trackBar1);
            this.gbEditor.Controls.Add(this.label1);
            this.gbEditor.Controls.Add(this.btDelete);
            this.gbEditor.Controls.Add(this.btAddPoint);
            this.gbEditor.Enabled = false;
            this.gbEditor.Location = new System.Drawing.Point(202, 6);
            this.gbEditor.Name = "gbEditor";
            this.gbEditor.Size = new System.Drawing.Size(449, 147);
            this.gbEditor.TabIndex = 1;
            this.gbEditor.TabStop = false;
            this.gbEditor.Text = "Редактирование контуров";
            // 
            // btComplete
            // 
            this.btComplete.Location = new System.Drawing.Point(34, 96);
            this.btComplete.Name = "btComplete";
            this.btComplete.Size = new System.Drawing.Size(108, 28);
            this.btComplete.TabIndex = 9;
            this.btComplete.Text = "Готово";
            this.toolTip1.SetToolTip(this.btComplete, "Завершить редактирование полигона");
            this.btComplete.UseVisualStyleBackColor = true;
            this.btComplete.Click += new System.EventHandler(this.btComplete_Click);
            // 
            // btInnerContour
            // 
            this.btInnerContour.Location = new System.Drawing.Point(6, 50);
            this.btInnerContour.Name = "btInnerContour";
            this.btInnerContour.Size = new System.Drawing.Size(167, 28);
            this.btInnerContour.TabIndex = 7;
            this.btInnerContour.Text = "Добавить внутренний контур";
            this.toolTip1.SetToolTip(this.btInnerContour, "Введите точки контура левым щелчком мыши. Не соединяйте последнюю точку с начальн" +
        "ой. Нажмите пробел для завершения ввода.");
            this.btInnerContour.UseVisualStyleBackColor = true;
            this.btInnerContour.Click += new System.EventHandler(this.btInnerContour_Click);
            // 
            // btOuterContour
            // 
            this.btOuterContour.Location = new System.Drawing.Point(6, 16);
            this.btOuterContour.Name = "btOuterContour";
            this.btOuterContour.Size = new System.Drawing.Size(167, 28);
            this.btOuterContour.TabIndex = 3;
            this.btOuterContour.Text = "Добавить внешний контур";
            this.toolTip1.SetToolTip(this.btOuterContour, "Введите точки контура левым щелчком мыши. Не соединяйте последнюю точку с начальн" +
        "ой. Нажмите пробел для завершения ввода.");
            this.btOuterContour.UseVisualStyleBackColor = true;
            this.btOuterContour.Click += new System.EventHandler(this.btOuterContour_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(190, 96);
            this.trackBar1.Maximum = 30;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.RightToLeftLayout = true;
            this.trackBar1.Size = new System.Drawing.Size(100, 45);
            this.trackBar1.SmallChange = 5;
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Value = 20;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(296, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Чувствительность курсора";
            // 
            // btDelete
            // 
            this.btDelete.Location = new System.Drawing.Point(190, 50);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(167, 28);
            this.btDelete.TabIndex = 4;
            this.btDelete.Text = "Удалить точку";
            this.toolTip1.SetToolTip(this.btDelete, "Выберите точку контура левым щелчком мыши");
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btAddPoint
            // 
            this.btAddPoint.Location = new System.Drawing.Point(190, 16);
            this.btAddPoint.Name = "btAddPoint";
            this.btAddPoint.Size = new System.Drawing.Size(167, 28);
            this.btAddPoint.TabIndex = 3;
            this.btAddPoint.Text = "Добавить точку";
            this.toolTip1.SetToolTip(this.btAddPoint, "Выделите 2 смежных точки одного контура. Затем добавьте новую точку левым щелчком" +
        " мыши.");
            this.btAddPoint.UseVisualStyleBackColor = true;
            this.btAddPoint.Click += new System.EventHandler(this.btAddPoint_Click);
            // 
            // gbOp
            // 
            this.gbOp.Controls.Add(this.btClear);
            this.gbOp.Controls.Add(this.btUnion);
            this.gbOp.Controls.Add(this.btNew);
            this.gbOp.Location = new System.Drawing.Point(6, 3);
            this.gbOp.Name = "gbOp";
            this.gbOp.Size = new System.Drawing.Size(190, 153);
            this.gbOp.TabIndex = 0;
            this.gbOp.TabStop = false;
            this.gbOp.Text = "Операции";
            // 
            // btClear
            // 
            this.btClear.Location = new System.Drawing.Point(6, 106);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(167, 28);
            this.btClear.TabIndex = 2;
            this.btClear.Text = "Очистить экран";
            this.toolTip1.SetToolTip(this.btClear, "Введите точки контура левым щелчком мыши. Не соединяйте последнюю точку с начальн" +
        "ой. Нажмите пробел для завершения ввода.");
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btUnion
            // 
            this.btUnion.Location = new System.Drawing.Point(6, 53);
            this.btUnion.Name = "btUnion";
            this.btUnion.Size = new System.Drawing.Size(167, 28);
            this.btUnion.TabIndex = 1;
            this.btUnion.Text = "Объединить полигоны";
            this.btUnion.UseVisualStyleBackColor = true;
            this.btUnion.Click += new System.EventHandler(this.btUnion_Click);
            // 
            // btNew
            // 
            this.btNew.Location = new System.Drawing.Point(6, 19);
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(167, 28);
            this.btNew.TabIndex = 0;
            this.btNew.Text = "Создать полигон";
            this.btNew.UseVisualStyleBackColor = true;
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // tbDatabase
            // 
            this.tbDatabase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbDatabase.Controls.Add(this.groupBox2);
            this.tbDatabase.Controls.Add(this.groupBox1);
            this.tbDatabase.Location = new System.Drawing.Point(4, 22);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Padding = new System.Windows.Forms.Padding(3);
            this.tbDatabase.Size = new System.Drawing.Size(657, 159);
            this.tbDatabase.TabIndex = 1;
            this.tbDatabase.Text = "Архив                                   ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbFromBase);
            this.groupBox2.Controls.Add(this.btGetFromDB);
            this.groupBox2.Location = new System.Drawing.Point(297, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Загрузить из архива";
            // 
            // cbFromBase
            // 
            this.cbFromBase.FormattingEnabled = true;
            this.cbFromBase.Location = new System.Drawing.Point(40, 19);
            this.cbFromBase.Name = "cbFromBase";
            this.cbFromBase.Size = new System.Drawing.Size(218, 21);
            this.cbFromBase.TabIndex = 8;
            this.cbFromBase.Text = "Выберите имя полигона";
            this.cbFromBase.Click += new System.EventHandler(this.cbFromBase_Click);
            // 
            // btGetFromDB
            // 
            this.btGetFromDB.Location = new System.Drawing.Point(113, 46);
            this.btGetFromDB.Name = "btGetFromDB";
            this.btGetFromDB.Size = new System.Drawing.Size(75, 23);
            this.btGetFromDB.TabIndex = 7;
            this.btGetFromDB.Text = "Загрузить";
            this.btGetFromDB.UseVisualStyleBackColor = true;
            this.btGetFromDB.Click += new System.EventHandler(this.btGetFromDB_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btSaveToDB);
            this.groupBox1.Controls.Add(this.txtSaveName);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сохранить в архив";
            // 
            // btSaveToDB
            // 
            this.btSaveToDB.Location = new System.Drawing.Point(97, 45);
            this.btSaveToDB.Name = "btSaveToDB";
            this.btSaveToDB.Size = new System.Drawing.Size(75, 23);
            this.btSaveToDB.TabIndex = 6;
            this.btSaveToDB.Text = "Сохранить";
            this.btSaveToDB.UseVisualStyleBackColor = true;
            this.btSaveToDB.Click += new System.EventHandler(this.btSaveToDB_Click);
            // 
            // txtSaveName
            // 
            this.txtSaveName.BackColor = System.Drawing.SystemColors.Window;
            this.txtSaveName.Location = new System.Drawing.Point(15, 19);
            this.txtSaveName.Name = "txtSaveName";
            this.txtSaveName.Size = new System.Drawing.Size(210, 20);
            this.txtSaveName.TabIndex = 5;
            this.txtSaveName.Text = "Введите имя сохраняемого полигона";
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // pbPolygon
            // 
            this.pbPolygon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPolygon.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pbPolygon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPolygon.ErrorImage = null;
            this.pbPolygon.Location = new System.Drawing.Point(2, 218);
            this.pbPolygon.Name = "pbPolygon";
            this.pbPolygon.Size = new System.Drawing.Size(665, 453);
            this.pbPolygon.TabIndex = 0;
            this.pbPolygon.TabStop = false;
            this.pbPolygon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbPolygon_MouseDown);
            this.pbPolygon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbPolygon_MouseMove);
            // 
            // pMenu
            // 
            this.pMenu.BackgroundImage = global::PolygonMerge.Properties.Resources.bmp_help;
            this.pMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pMenu.Controls.Add(this.statusStrip1);
            this.pMenu.Location = new System.Drawing.Point(673, 49);
            this.pMenu.Name = "pMenu";
            this.pMenu.Size = new System.Drawing.Size(252, 622);
            this.pMenu.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 600);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(252, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 683);
            this.Controls.Add(this.tbOptions);
            this.Controls.Add(this.pbPolygon);
            this.Controls.Add(this.pMenu);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(945, 721);
            this.MinimumSize = new System.Drawing.Size(945, 721);
            this.Name = "fmMain";
            this.Text = "Объединение полигонов";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.fmMain_KeyPress);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tbOptions.ResumeLayout(false);
            this.tbEditor.ResumeLayout(false);
            this.gbEditor.ResumeLayout(false);
            this.gbEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.gbOp.ResumeLayout(false);
            this.tbDatabase.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPolygon)).EndInit();
            this.pMenu.ResumeLayout(false);
            this.pMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pMenu;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.PictureBox pbPolygon;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem печатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem печатьПолигонаToolStripMenuItem;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.TabControl tbOptions;
        private System.Windows.Forms.TabPage tbEditor;
        private System.Windows.Forms.TabPage tbDatabase;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSaveName;
        private System.Windows.Forms.Button btSaveToDB;
        private System.Windows.Forms.Button btGetFromDB;
        private System.Windows.Forms.ComboBox cbFromBase;
        private System.Windows.Forms.GroupBox gbEditor;
        private System.Windows.Forms.GroupBox gbOp;
        private System.Windows.Forms.Button btUnion;
        private System.Windows.Forms.Button btNew;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btAddPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btOuterContour;
        private System.Windows.Forms.Button btInnerContour;
        private System.Windows.Forms.Button btComplete;
        private System.Windows.Forms.PrintDialog printDialog;
    }
}

