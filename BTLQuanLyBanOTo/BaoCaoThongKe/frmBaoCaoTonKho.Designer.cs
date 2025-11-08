namespace BTLQuanLyBanOTo.BaoCaoThongKe
{
    partial class frmBaoCaoTonKho
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartTonKho = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.grbChiTiet = new System.Windows.Forms.GroupBox();
            this.btnBoLoc = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grbBieuDo = new System.Windows.Forms.GroupBox();
            this.cboDMC = new System.Windows.Forms.ComboBox();
            this.lblDMC = new System.Windows.Forms.Label();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.lblNV = new System.Windows.Forms.Label();
            this.cboNCC = new System.Windows.Forms.ComboBox();
            this.lblNCC = new System.Windows.Forms.Label();
            this.grbLoc = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cboTG = new System.Windows.Forms.ComboBox();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.lblTG = new System.Windows.Forms.Label();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnXuat = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.chartTonKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.grbChiTiet.SuspendLayout();
            this.panel3.SuspendLayout();
            this.grbBieuDo.SuspendLayout();
            this.grbLoc.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartTonKho
            // 
            chartArea6.Name = "ChartArea1";
            this.chartTonKho.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chartTonKho.Legends.Add(legend6);
            this.chartTonKho.Location = new System.Drawing.Point(3, 18);
            this.chartTonKho.Name = "chartTonKho";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chartTonKho.Series.Add(series6);
            this.chartTonKho.Size = new System.Drawing.Size(1178, 245);
            this.chartTonKho.TabIndex = 0;
            this.chartTonKho.Text = "chart1";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(3, 18);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.Size = new System.Drawing.Size(1178, 179);
            this.dgvChiTiet.TabIndex = 0;
            // 
            // grbChiTiet
            // 
            this.grbChiTiet.Controls.Add(this.dgvChiTiet);
            this.grbChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbChiTiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grbChiTiet.Location = new System.Drawing.Point(0, 235);
            this.grbChiTiet.Name = "grbChiTiet";
            this.grbChiTiet.Size = new System.Drawing.Size(1184, 200);
            this.grbChiTiet.TabIndex = 13;
            this.grbChiTiet.TabStop = false;
            this.grbChiTiet.Text = "Chi tiết";
            // 
            // btnBoLoc
            // 
            this.btnBoLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBoLoc.Location = new System.Drawing.Point(129, 93);
            this.btnBoLoc.Name = "btnBoLoc";
            this.btnBoLoc.Size = new System.Drawing.Size(112, 38);
            this.btnBoLoc.TabIndex = 16;
            this.btnBoLoc.Text = "Bỏ lọc";
            this.btnBoLoc.UseVisualStyleBackColor = true;
            this.btnBoLoc.Click += new System.EventHandler(this.btnBoLoc_Click);
            // 
            // btnLoc
            // 
            this.btnLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLoc.Location = new System.Drawing.Point(3, 93);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(112, 38);
            this.btnLoc.TabIndex = 15;
            this.btnLoc.Text = "Lọc dữ liệu";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnBoLoc);
            this.panel3.Controls.Add(this.btnLoc);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(926, 18);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(255, 134);
            this.panel3.TabIndex = 23;
            // 
            // grbBieuDo
            // 
            this.grbBieuDo.Controls.Add(this.chartTonKho);
            this.grbBieuDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbBieuDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grbBieuDo.Location = new System.Drawing.Point(0, 435);
            this.grbBieuDo.Name = "grbBieuDo";
            this.grbBieuDo.Size = new System.Drawing.Size(1184, 266);
            this.grbBieuDo.TabIndex = 14;
            this.grbBieuDo.TabStop = false;
            this.grbBieuDo.Text = "Biểu đồ";
            // 
            // cboDMC
            // 
            this.cboDMC.FormattingEnabled = true;
            this.cboDMC.Location = new System.Drawing.Point(628, 100);
            this.cboDMC.Name = "cboDMC";
            this.cboDMC.Size = new System.Drawing.Size(241, 24);
            this.cboDMC.TabIndex = 44;
            // 
            // lblDMC
            // 
            this.lblDMC.AutoSize = true;
            this.lblDMC.Location = new System.Drawing.Point(538, 103);
            this.lblDMC.Name = "lblDMC";
            this.lblDMC.Size = new System.Drawing.Size(67, 16);
            this.lblDMC.TabIndex = 43;
            this.lblDMC.Text = "Danh mục";
            // 
            // cboNV
            // 
            this.cboNV.FormattingEnabled = true;
            this.cboNV.Location = new System.Drawing.Point(172, 100);
            this.cboNV.Name = "cboNV";
            this.cboNV.Size = new System.Drawing.Size(241, 24);
            this.cboNV.TabIndex = 42;
            // 
            // lblNV
            // 
            this.lblNV.AutoSize = true;
            this.lblNV.Location = new System.Drawing.Point(54, 103);
            this.lblNV.Name = "lblNV";
            this.lblNV.Size = new System.Drawing.Size(67, 16);
            this.lblNV.TabIndex = 41;
            this.lblNV.Text = "Nhân viên";
            // 
            // cboNCC
            // 
            this.cboNCC.FormattingEnabled = true;
            this.cboNCC.Location = new System.Drawing.Point(628, 56);
            this.cboNCC.Name = "cboNCC";
            this.cboNCC.Size = new System.Drawing.Size(241, 24);
            this.cboNCC.TabIndex = 40;
            // 
            // lblNCC
            // 
            this.lblNCC.AutoSize = true;
            this.lblNCC.Location = new System.Drawing.Point(537, 59);
            this.lblNCC.Name = "lblNCC";
            this.lblNCC.Size = new System.Drawing.Size(90, 16);
            this.lblNCC.TabIndex = 39;
            this.lblNCC.Text = "Nhà cung cấp";
            // 
            // grbLoc
            // 
            this.grbLoc.Controls.Add(this.panel5);
            this.grbLoc.Controls.Add(this.panel3);
            this.grbLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grbLoc.Location = new System.Drawing.Point(0, 80);
            this.grbLoc.Name = "grbLoc";
            this.grbLoc.Size = new System.Drawing.Size(1184, 155);
            this.grbLoc.TabIndex = 12;
            this.grbLoc.TabStop = false;
            this.grbLoc.Text = "Bộ lọc";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cboDMC);
            this.panel5.Controls.Add(this.lblDMC);
            this.panel5.Controls.Add(this.cboNV);
            this.panel5.Controls.Add(this.lblNV);
            this.panel5.Controls.Add(this.cboNCC);
            this.panel5.Controls.Add(this.lblNCC);
            this.panel5.Controls.Add(this.cboTG);
            this.panel5.Controls.Add(this.dtpDenNgay);
            this.panel5.Controls.Add(this.dtpTuNgay);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.lblDenNgay);
            this.panel5.Controls.Add(this.lblTG);
            this.panel5.Controls.Add(this.lblTuNgay);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 18);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(923, 134);
            this.panel5.TabIndex = 24;
            // 
            // cboTG
            // 
            this.cboTG.FormattingEnabled = true;
            this.cboTG.Location = new System.Drawing.Point(172, 56);
            this.cboTG.Name = "cboTG";
            this.cboTG.Size = new System.Drawing.Size(241, 24);
            this.cboTG.TabIndex = 38;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(628, 10);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(241, 22);
            this.dtpDenNgay.TabIndex = 37;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(172, 10);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(241, 22);
            this.dtpTuNgay.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(472, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 16);
            this.label4.TabIndex = 35;
            this.label4.Text = ":";
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(537, 15);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(64, 16);
            this.lblDenNgay.TabIndex = 34;
            this.lblDenNgay.Text = "Đến ngày";
            // 
            // lblTG
            // 
            this.lblTG.AutoSize = true;
            this.lblTG.Location = new System.Drawing.Point(54, 59);
            this.lblTG.Name = "lblTG";
            this.lblTG.Size = new System.Drawing.Size(63, 16);
            this.lblTG.TabIndex = 33;
            this.lblTG.Text = "Thời gian";
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(54, 15);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(56, 16);
            this.lblTuNgay.TabIndex = 32;
            this.lblTuNgay.Text = "Từ ngày";
            // 
            // btnDong
            // 
            this.btnDong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDong.Location = new System.Drawing.Point(129, 11);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(112, 38);
            this.btnDong.TabIndex = 15;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnXuat
            // 
            this.btnXuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnXuat.Location = new System.Drawing.Point(3, 11);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(112, 38);
            this.btnXuat.TabIndex = 14;
            this.btnXuat.Text = "Xuất báo cáo";
            this.btnXuat.UseVisualStyleBackColor = true;
            this.btnXuat.Click += new System.EventHandler(this.btnXuat_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnDong);
            this.panel4.Controls.Add(this.btnXuat);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(926, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(258, 60);
            this.panel4.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 701);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 60);
            this.panel2.TabIndex = 11;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.Blue;
            this.lblTieuDe.Location = new System.Drawing.Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(1184, 80);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "BÁO CÁO TỒN KHO";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTieuDe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 80);
            this.panel1.TabIndex = 10;
            // 
            // frmBaoCaoTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.grbBieuDo);
            this.Controls.Add(this.grbChiTiet);
            this.Controls.Add(this.grbLoc);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "frmBaoCaoTonKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo tồn kho";
            this.Load += new System.EventHandler(this.frmBaoCaoTonKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartTonKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.grbChiTiet.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.grbBieuDo.ResumeLayout(false);
            this.grbLoc.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartTonKho;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.GroupBox grbChiTiet;
        private System.Windows.Forms.Button btnBoLoc;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox grbBieuDo;
        private System.Windows.Forms.ComboBox cboDMC;
        private System.Windows.Forms.Label lblDMC;
        private System.Windows.Forms.ComboBox cboNV;
        private System.Windows.Forms.Label lblNV;
        private System.Windows.Forms.ComboBox cboNCC;
        private System.Windows.Forms.Label lblNCC;
        private System.Windows.Forms.GroupBox grbLoc;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cboTG;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.Label lblTG;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnXuat;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel panel1;
    }
}