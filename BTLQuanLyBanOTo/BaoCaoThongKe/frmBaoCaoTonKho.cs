using BTLQuanLyBanOTo.Classes;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
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

            LoadCBO();
            LoadDGV();
        }

        public void LoadCBO()
        {
            // --- Thời gian ---
            cboTG.Items.Clear();
            cboTG.Items.AddRange(new string[] { "Từ trước đến nay", "Theo ngày", "Theo tháng", "Theo năm" });
            cboTG.SelectedIndex = 0;

            // --- Nhân viên ---
            string sqlNV = "SELECT MaNV, TenNV FROM NhanVien where TrangThai = 1";
            DataTable dtNV = dt.ExecuteQuery(sqlNV);
            cboNV.DataSource = dtNV;
            cboNV.DisplayMember = "TenNV";
            cboNV.ValueMember = "MaNV";
            cboNV.SelectedIndex = -1;

            // --- Khách hàng ---
            string sqlNCC = "SELECT MaNCC, TenNCC FROM NhaCungCap where TrangThai = 1";
            DataTable dtNCC = dt.ExecuteQuery(sqlNCC);
            cboNCC.DataSource = dtNCC;
            cboNCC.DisplayMember = "TenNCC";
            cboNCC.ValueMember = "MaNCC";
            cboNCC.SelectedIndex = -1;

            // --- Danh mục chung ---
            cboDMC.Items.Clear();
            cboDMC.Items.AddRange(new string[]
            {
                "Tất cả",
                "Hãng sản xuất",
                "Loại xe",
                "Màu sắc",
                "Đời xe",
                "Số chỗ ngồi",
                "Nước sản xuất",
                "Tình trạng"
            });
            cboDMC.SelectedIndex = 0;
        }

        public void LoadDGV()
        {
            // --- Phần SELECT/GROUP BY cơ bản (linh hoạt) ---
            StringBuilder selectParts = new StringBuilder();
            StringBuilder groupParts = new StringBuilder();
            StringBuilder fromParts = new StringBuilder();

            // Khởi tạo các thành phần FROM và WHERE cơ bản
            fromParts.AppendLine("FROM ChiTietHoaDonNhap ct");
            fromParts.AppendLine("JOIN HoaDonNhap hdn ON ct.SoHDN = hdn.SoHDN");
            fromParts.AppendLine("JOIN DanhMucHang h ON ct.MaHang = h.MaHang");
            string where = "WHERE 1=1";

            // Luôn bao gồm Mã/Tên hàng vào SELECT và GROUP BY (Báo cáo Sản phẩm)
            selectParts.Append("h.MaHang AS [Mã hàng], h.TenHang AS [Tên hàng]");
            groupParts.Append("h.MaHang, h.TenHang");

            // --- Bộ lọc Khách hàng (Thêm cột Khách hàng vào báo cáo nếu có lọc) ---
            if (cboNCC.SelectedIndex != -1 && cboNCC.SelectedValue != null)
            {
                selectParts.Append(", ncc.TenNCC AS [Tên nhà cung cấp]");
                groupParts.Append(", ncc.TenNCC");
                fromParts.AppendLine("JOIN NhaCungCap ncc ON hdn.MaNCC = ncc.MaNCC");
                where += $" AND hdn.MaNCC = '{cboNCC.SelectedValue}'"; // Lọc thêm vào WHERE
            }

            // --- Bộ lọc Nhân viên (Thêm cột Nhân viên vào báo cáo nếu có lọc) ---
            if (cboNV.SelectedIndex != -1 && cboNV.SelectedValue != null)
            {
                selectParts.Append(", nv.TenNV AS [Tên Nhân viên]");
                groupParts.Append(", nv.TenNV");
                fromParts.AppendLine("JOIN NhanVien nv ON hdn.MaNV = nv.MaNV");
                where += $" AND hdn.MaNV = '{cboNV.SelectedValue}'"; // Lọc thêm vào WHERE
            }

            // --- Bộ lọc Thời gian ---
            if (cboTG.SelectedItem != null)
            {
                string loaiTG = cboTG.SelectedItem.ToString();
                if (loaiTG == "Theo ngày")
                {
                    where += $" AND CONVERT(date, hdn.NgayNhap) BETWEEN '{dtpTuNgay.Value:yyyy-MM-dd}' AND '{dtpDenNgay.Value:yyyy-MM-dd}'";
                }
                else if (loaiTG == "Theo tháng")
                {
                    where += $" AND MONTH(hdn.NgayNhap) = {dtpTuNgay.Value.Month} AND YEAR(hdn.NgayNhap) = {dtpTuNgay.Value.Year}";
                }
                else if (loaiTG == "Theo năm")
                {
                    where += $" AND YEAR(hdn.NgayNhap) = {dtpTuNgay.Value.Year}";
                }
            }

            // --- Bộ lọc Danh mục chung (Thêm cột Danh mục vào báo cáo nếu được chọn) ---
            string cboDM = cboDMC.SelectedItem?.ToString() ?? "Tất cả";
            if (cboDM != "Tất cả")
            {
                switch (cboDM)
                {
                    case "Hãng sản xuất":
                        selectParts.Append(", hsx.TenHangSX AS [Hãng sản xuất]");
                        fromParts.AppendLine(" JOIN HangSX hsx ON h.MaHangSX = hsx.MaHangSX");
                        groupParts.Append(", hsx.TenHangSX");
                        break;
                    case "Loại xe":
                        selectParts.Append(", lx.TenLoai AS [Loại xe]");
                        fromParts.AppendLine(" JOIN TheLoai lx ON h.MaLoai = lx.MaLoai");
                        groupParts.Append(", lx.TenLoai");
                        break;
                    case "Màu sắc":
                        selectParts.Append(", ms.TenMau AS [Màu sắc]");
                        fromParts.AppendLine(" JOIN MauSac ms ON h.MaMau = ms.MaMau");
                        groupParts.Append(", ms.TenMau");
                        break;
                    case "Đời xe":
                        selectParts.Append(", dx.TenDoi AS [Đời xe]");
                        fromParts.AppendLine(" JOIN DoiXe dx ON h.MaDoi = dx.MaDoi");
                        groupParts.Append(", dx.TenDoi");
                        break;
                    case "Số chỗ ngồi":
                        selectParts.Append(", scn.TenSoCho AS [Số chỗ]");
                        fromParts.AppendLine(" JOIN SoChoNgoi scn ON h.MaSoCho = scn.MaSoCho");
                        groupParts.Append(", scn.TenSoCho");
                        break;
                    case "Nước sản xuất":
                        selectParts.Append(", nsx.TenNuocSX AS [Nước SX]");
                        fromParts.AppendLine(" JOIN NuocSX nsx ON h.MaNuocSX = nsx.MaNuocSX");
                        groupParts.Append(", nsx.TenNuocSX");
                        break;
                    case "Tình trạng":
                        selectParts.Append(", tt.TenTinhTrang AS [Tình trạng]");
                        fromParts.AppendLine(" JOIN TinhTrang tt ON h.MaTinhTrang = tt.MaTinhTrang");
                        groupParts.Append(", tt.TenTinhTrang");
                        break;
                }
            }

            // --- Ghép toàn bộ lại ---
            string select = $"SELECT {selectParts.ToString()}, SUM(ct.SoLuong) AS [Số lượng nhập], sum(h.SoLuong) AS [Số lượng tồn]";
            string group = $"GROUP BY {groupParts.ToString()}";
            string order = "ORDER BY [Số lượng tồn] DESC";

            string sql = $"{select} {fromParts.ToString()} {where} {group} {order}";

            DataTable data = dt.ExecuteQuery(sql);
            dgvChiTiet.DataSource = data;

            LoadChartTonKho(data);
        }

        public void LoadChartTonKho(DataTable data)
        {
            chartTonKho.Series.Clear();
            chartTonKho.ChartAreas[0].AxisX.Title = "Sản phẩm";
            chartTonKho.ChartAreas[0].AxisY.Title = "Số lượng tồn";
            chartTonKho.ChartAreas[0].AxisX.Interval = 1;

            if (data == null || data.Rows.Count == 0)
                return;

            // Tạo series
            var series = new Series("Tồn kho");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;

            // Xác định tên cột hiển thị
            string nameCol = "Tên hàng";
            string valueCol = "Số lượng tồn";

            // Thêm điểm dữ liệu vào biểu đồ
            foreach (DataRow row in data.Rows)
            {
                string label = row[nameCol].ToString();
                double value = 0;
                double.TryParse(row[valueCol].ToString(), out value);
                series.Points.AddXY(label, value);
            }

            chartTonKho.Series.Add(series);
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
            LoadDGV();
        }

        private void btnBoLoc_Click(object sender, EventArgs e)
        {
            cboTG.SelectedIndex = 0;
            cboNV.SelectedIndex = -1;
            cboNV.Text = "";
            cboNCC.SelectedIndex = -1;
            cboNCC.Text = "";
            cboDMC.SelectedIndex = 0;

            LoadDGV();
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
                    ((Excel.Range)ws.Cells[3, i]).Interior.Color = ColorTranslator.ToOle(Color.LightSkyBlue);
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
