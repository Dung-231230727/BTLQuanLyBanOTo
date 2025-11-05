using BTLQuanLyBanOTo.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;

namespace BTLQuanLyBanOTo.BaoCaoThongKe
{
    public partial class frmBaoCaoTonKho : Form
    {
        DataProcesser dt;

        public frmBaoCaoTonKho()
        {
            InitializeComponent();
        }

        private void frmBaoCaoTonKho_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();

            LoadFLP();
            LoadDGV();
            LoadChartTonKho();
        }

        public void LoadFLP()
        {
            string[] danhMuc = { "Hãng SX", "Loại xe", "Màu sắc", "Đời xe", "Số chỗ ngồi", "Nước SX", "Tình trạng" };
            foreach (var item in danhMuc)
            {
                CheckBox cb = new CheckBox
                {
                    Text = item,
                    AutoSize = true,
                    Margin = new Padding(15, 5, 15, 5)
                };
                flpBoLoc.Controls.Add(cb);
            }
        }

        public void LoadDGV()
        {
            try
            {
                string sql = "SELECT MaHang, TenHang, SoLuong, DonGiaBan FROM DanhMucHang";
                dgvChiTiet.DataSource = dt.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string BuildSqlTheoBoLoc()
        {
            var checkedItems = flpBoLoc.Controls.OfType<CheckBox>().Where(c => c.Checked).ToList();

            if (checkedItems.Count == 0)
            {
                return "SELECT MaHang, TenHang, SoLuong, DonGiaBan FROM DanhMucHang";
            }

            List<string> joinParts = new List<string>();
            List<string> selectParts = new List<string>();
            List<string> groupParts = new List<string>();

            foreach (var cb in checkedItems)
            {
                switch (cb.Text)
                {
                    case "Hãng SX":
                        joinParts.Add("JOIN HangSX h ON sp.MaHangSX = h.MaHangSX");
                        selectParts.Add("h.TenHangSX AS [Hãng SX]");
                        groupParts.Add("h.TenHangSX");
                        break;

                    case "Loại xe":
                        joinParts.Add("JOIN TheLoai l ON sp.MaLoai = l.MaLoai");
                        selectParts.Add("l.TenLoai AS [Loại xe]");
                        groupParts.Add("l.TenLoai");
                        break;

                    case "Màu sắc":
                        joinParts.Add("JOIN MauSac m ON sp.MaMau = m.MaMau");
                        selectParts.Add("m.TenMau AS [Màu sắc]");
                        groupParts.Add("m.TenMau");
                        break;

                    case "Đời xe":
                        joinParts.Add("JOIN DoiXe d ON sp.MaDoi = d.MaDoi");
                        selectParts.Add("d.TenDoi AS [Đời xe]");
                        groupParts.Add("d.TenDoi");
                        break;

                    case "Số chỗ ngồi":
                        joinParts.Add("JOIN SoChoNgoi sc ON sp.MaSoCho = sc.MaSoCho");
                        selectParts.Add("sc.TenSoCho AS [Số chỗ]");
                        groupParts.Add("sc.TenSoCho");
                        break;

                    case "Nước SX":
                        joinParts.Add("JOIN NuocSX nsx ON sp.MaNuocSX = nsx.MaNuocSX");
                        selectParts.Add("nsx.TenNuocSX AS [Nước SX]");
                        groupParts.Add("nsx.TenNuocSX");
                        break;

                    case "Tình trạng":
                        joinParts.Add("JOIN TinhTrang tt ON sp.MaTinhTrang = tt.MaTinhTrang");
                        selectParts.Add("tt.TenTinhTrang AS [Tình trạng]");
                        groupParts.Add("tt.TenTinhTrang");
                        break;
                }
            }

            string sql = $@"
                SELECT {string.Join(", ", selectParts)}, SUM(sp.SoLuong) AS [Tổng tồn]
                FROM DanhMucHang sp
                {string.Join("\n", joinParts)}
                GROUP BY {string.Join(", ", groupParts)}";

            return sql;
        }

        public void LoadChartTonKho()
        {
            DataTable tbl = dgvChiTiet.DataSource as DataTable;
            if (tbl == null || tbl.Rows.Count == 0) return;

            chartTonKho.Series.Clear();
            chartTonKho.ChartAreas[0].AxisX.Interval = 1;
            chartTonKho.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTonKho.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            Series s = new Series("Tồn kho");
            s.ChartType = SeriesChartType.Column;
            s.XValueType = ChartValueType.String;

            // Xác định cột hiển thị
            string cotSoLuong = tbl.Columns.Contains("Tổng tồn") ? "Tổng tồn" : "SoLuong";

            // Lấy danh sách các cột danh mục (trừ cột tổng)
            var cotDanhMuc = tbl.Columns.Cast<DataColumn>()
                                .Where(c => c.ColumnName != cotSoLuong)
                                .Select(c => c.ColumnName)
                                .ToList();

            foreach (DataRow r in tbl.Rows)
            {
                // Ghép tên hiển thị từ các cột danh mục
                string tenHienThi = string.Join(" - ", cotDanhMuc.Select(c => r[c].ToString()));

                s.Points.AddXY(tenHienThi, Convert.ToDouble(r[cotSoLuong]));
            }

            chartTonKho.Series.Add(s);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "Bạn có chắc muốn thoát không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                );
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string sql = BuildSqlTheoBoLoc();
            dgvChiTiet.DataSource = dt.ExecuteQuery(sql);
            LoadChartTonKho();
        }

        private void btnBoLoc_Click(object sender, EventArgs e)
        {
            foreach (CheckBox cb in flpBoLoc.Controls.OfType<CheckBox>())
                cb.Checked = false;

            LoadDGV();
            LoadChartTonKho();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            Excel.Application app = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                if (dgvChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Khởi tạo SaveFileDialog
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook|*.xlsx",
                    Title = "Xuất báo cáo tồn kho ra Excel",
                    FileName = "BaoCaoTonKho_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                app = new Excel.Application();
                app.Visible = false;
                wb = app.Workbooks.Add(Type.Missing);
                ws = (Excel.Worksheet)wb.ActiveSheet;
                ws.Name = "BaoCaoTonKho";
                //
                Excel.Range title = ws.Range["A1", "E1"];
                title.Merge();
                title.Value = "BÁO CÁO TỒN KHO";
                title.Font.Size = 18;
                title.Font.Bold = true;
                title.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                for (int i = 1; i <= dgvChiTiet.Columns.Count; i++)
                {
                    ws.Cells[3, i] = dgvChiTiet.Columns[i - 1].HeaderText;
                    ((Excel.Range)ws.Cells[3, i]).Font.Bold = true;
                    ((Excel.Range)ws.Cells[3, i]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSkyBlue);
                }

                for (int i = 0; i < dgvChiTiet.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvChiTiet.Columns.Count; j++)
                    {
                        ws.Cells[i + 4, j + 1] = dgvChiTiet.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                Excel.Range usedRange = ws.UsedRange;
                usedRange.Columns.AutoFit();
                usedRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // Lưu và đóng ứng dụng
                wb.SaveAs(sfd.FileName);
                wb.Close(false);
                app.Quit();

                MessageBox.Show("Xuất báo cáo thành công!\n" + Path.GetFileName(sfd.FileName), "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ws != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                if (wb != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                if (app != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                GC.Collect();
            }
        }
    }
}
