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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnXuat = new System.Windows.Forms.Button();
            this.grbLoc = new System.Windows.Forms.GroupBox();
            this.flpBoLoc = new System.Windows.Forms.FlowLayoutPanel();
            this.grbChiTiet = new System.Windows.Forms.GroupBox();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.grbBieuDo = new System.Windows.Forms.GroupBox();
            this.chartTonKho = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnBoLoc = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.grbLoc.SuspendLayout();
            this.grbChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.grbBieuDo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTonKho)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTieuDe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 80);
            this.panel1.TabIndex = 0;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 701);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 60);
            this.panel2.TabIndex = 1;
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
            // grbLoc
            // 
            this.grbLoc.Controls.Add(this.flpBoLoc);
            this.grbLoc.Controls.Add(this.panel3);
            this.grbLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grbLoc.Location = new System.Drawing.Point(0, 80);
            this.grbLoc.Name = "grbLoc";
            this.grbLoc.Size = new System.Drawing.Size(1184, 100);
            this.grbLoc.TabIndex = 2;
            this.grbLoc.TabStop = false;
            this.grbLoc.Text = "Bộ lọc";
            // 
            // flpBoLoc
            // 
            this.flpBoLoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBoLoc.Location = new System.Drawing.Point(3, 18);
            this.flpBoLoc.Name = "flpBoLoc";
            this.flpBoLoc.Size = new System.Drawing.Size(923, 79);
            this.flpBoLoc.TabIndex = 0;
            // 
            // grbChiTiet
            // 
            this.grbChiTiet.Controls.Add(this.dgvChiTiet);
            this.grbChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbChiTiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grbChiTiet.Location = new System.Drawing.Point(0, 180);
            this.grbChiTiet.Name = "grbChiTiet";
            this.grbChiTiet.Size = new System.Drawing.Size(1184, 200);
            this.grbChiTiet.TabIndex = 3;
            this.grbChiTiet.TabStop = false;
            this.grbChiTiet.Text = "Chi tiết";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(3, 18);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.Size = new System.Drawing.Size(1178, 179);
            this.dgvChiTiet.TabIndex = 0;
            // 
            // grbBieuDo
            // 
            this.grbBieuDo.Controls.Add(this.chartTonKho);
            this.grbBieuDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbBieuDo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.grbBieuDo.Location = new System.Drawing.Point(0, 380);
            this.grbBieuDo.Name = "grbBieuDo";
            this.grbBieuDo.Size = new System.Drawing.Size(1184, 321);
            this.grbBieuDo.TabIndex = 4;
            this.grbBieuDo.TabStop = false;
            this.grbBieuDo.Text = "Biểu đồ";
            // 
            // chartTonKho
            // 
            chartArea7.Name = "ChartArea1";
            this.chartTonKho.ChartAreas.Add(chartArea7);
            this.chartTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            legend7.Name = "Legend1";
            this.chartTonKho.Legends.Add(legend7);
            this.chartTonKho.Location = new System.Drawing.Point(3, 18);
            this.chartTonKho.Name = "chartTonKho";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            this.chartTonKho.Series.Add(series7);
            this.chartTonKho.Size = new System.Drawing.Size(1178, 300);
            this.chartTonKho.TabIndex = 0;
            this.chartTonKho.Text = "chart1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnBoLoc);
            this.panel3.Controls.Add(this.btnLoc);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(926, 18);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(255, 79);
            this.panel3.TabIndex = 1;
            // 
            // btnBoLoc
            // 
            this.btnBoLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBoLoc.Location = new System.Drawing.Point(129, 38);
            this.btnBoLoc.Name = "btnBoLoc";
            this.btnBoLoc.Size = new System.Drawing.Size(112, 38);
            this.btnBoLoc.TabIndex = 18;
            this.btnBoLoc.Text = "Bỏ lọc";
            this.btnBoLoc.UseVisualStyleBackColor = true;
            this.btnBoLoc.Click += new System.EventHandler(this.btnBoLoc_Click);
            // 
            // btnLoc
            // 
            this.btnLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLoc.Location = new System.Drawing.Point(3, 38);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(112, 38);
            this.btnLoc.TabIndex = 17;
            this.btnLoc.Text = "Lọc dữ liệu";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
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
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.grbLoc.ResumeLayout(false);
            this.grbChiTiet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.grbBieuDo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTonKho)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnXuat;
        private System.Windows.Forms.GroupBox grbLoc;
        private System.Windows.Forms.GroupBox grbChiTiet;
        private System.Windows.Forms.GroupBox grbBieuDo;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTonKho;
        private System.Windows.Forms.FlowLayoutPanel flpBoLoc;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnBoLoc;
        private System.Windows.Forms.Button btnLoc;
    }
}