using BTLQuanLyBanOTo.Classes;
using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;

namespace BTLQuanLyBanOTo.BaoCaoThongKe
{
    public partial class frmBaoCaoDoanhThu : Form
    {
        DataProcesser dt;

        public frmBaoCaoDoanhThu()
        {
            InitializeComponent();
        }

        private void frmBaoCaoDoanhThu_Load(object sender, EventArgs e)
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
            string sqlNV = "SELECT MaNV, TenNV FROM NhanVien";
            DataTable dtNV = dt.ExecuteQuery(sqlNV);
            cboNV.DataSource = dtNV;
            cboNV.DisplayMember = "TenNV";
            cboNV.ValueMember = "MaNV";
            cboNV.SelectedIndex = -1;

            // --- Khách hàng ---
            string sqlKH = "SELECT MaKhach, TenKhach FROM KhachHang";
            DataTable dtKH = dt.ExecuteQuery(sqlKH);
            cboKH.DataSource = dtKH;
            cboKH.DisplayMember = "TenKhach";
            cboKH.ValueMember = "MaKhach";
            cboKH.SelectedIndex = -1;

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
            // --- Phần SELECT cơ bản ---
            string select = @"
                                SELECT 
                                    h.MaHang AS [Mã hàng],
                                    h.TenHang AS [Tên hàng],
                                    SUM(ct.ThanhTien) AS [Doanh thu (VNĐ)]
                            ";

            string from = @"
                                FROM ChiTietDonDatHang ct
                                JOIN DonDatHang dh ON ct.SoDDH = dh.SoDDH
                                JOIN DanhMucHang h ON ct.MaHang = h.MaHang
                            ";

            string where = "WHERE 1=1";
            string group = "GROUP BY h.MaHang, h.TenHang";
            string order = "ORDER BY [Doanh thu (VNĐ)] DESC";

            // --- Bộ lọc thời gian ---
            if (cboTG.SelectedItem != null)
            {
                string loaiTG = cboTG.SelectedItem.ToString();
                if (loaiTG == "Theo ngày")
                {
                    where += $" AND CONVERT(date, dh.NgayDat) BETWEEN '{dtpTuNgay.Value:yyyy-MM-dd}' AND '{dtpDenNgay.Value:yyyy-MM-dd}'";
                }
                else if (loaiTG == "Theo tháng")
                {
                    where += $" AND MONTH(dh.NgayDat) = {dtpTuNgay.Value.Month} AND YEAR(dh.NgayDat) = {dtpTuNgay.Value.Year}";
                }
                else if (loaiTG == "Theo năm")
                {
                    where += $" AND YEAR(dh.NgayDat) = {dtpTuNgay.Value.Year}";
                }
            }

            // --- Bộ lọc nhân viên ---
            if (cboNV.SelectedIndex > 0)
                where += $" AND dh.MaNV = '{cboNV.SelectedValue}'";

            // --- Bộ lọc khách hàng ---
            if (cboKH.SelectedIndex > 0)
                where += $" AND dh.MaKhach = '{cboKH.SelectedValue}'";

            // --- Lọc theo danh mục ---
            string cboDM = cboDMC.SelectedItem?.ToString() ?? "";
            switch (cboDM)
            {
                case "Hãng sản xuất":
                    select += ", hsx.TenHangSX AS [Hãng sản xuất]";
                    from += " JOIN HangSX hsx ON h.MaHangSX = hsx.MaHangSX";
                    group += ", hsx.TenHangSX";
                    break;
                case "Loại xe":
                    select += ", lx.TenLoai AS [Loại xe]";
                    from += " JOIN TheLoai lx ON h.MaLoai = lx.MaLoai";
                    group += ", lx.TenLoai";
                    break;
                case "Màu sắc":
                    select += ", ms.TenMau AS [Màu sắc]";
                    from += " JOIN MauSac ms ON h.MaMau = ms.MaMau";
                    group += ", ms.TenMau";
                    break;
                case "Đời xe":
                    select += ", dx.TenDoi AS [Đời xe]";
                    from += " JOIN DoiXe dx ON h.MaDoi = dx.MaDoi";
                    group += ", dx.TenDoi";
                    break;
                case "Số chỗ ngồi":
                    select += ", scn.TenSoCho AS [Tên số chỗ]";
                    from += " JOIN SoChoNgoi scn ON h.MaSoCho = scn.MaSoCho";
                    group += ", scn.TenSoCho";
                    break;
                case "Nước sản xuất":
                    select += ", nsx.TenNuocSX AS [Nước SX]";
                    from += " JOIN NuocSX nsx ON h.MaNuocSX = nsx.MaNuocSX";
                    group += ", nsx.TenNuocSX";
                    break;
                case "Tình trạng":
                    select += ", tt.TenTinhTrang AS [Tình trạng]";
                    from += " JOIN TinhTrang tt ON h.MaTinhTrang = tt.MaTinhTrang";
                    group += ", tt.TenTinhTrang";
                    break;
            }

            // --- Ghép toàn bộ lại ---
            string sql = $"{select} {from} {where} {group} {order}";

            DataTable data = dt.ExecuteQuery(sql);
            dgvChiTiet.DataSource = data;
            LoadChartDoanhThu(data);
        }


        public void LoadChartDoanhThu(DataTable data)
        {
            chartDoanhThu.Series.Clear();
            chartDoanhThu.ChartAreas[0].AxisX.Title = "Sản phẩm";
            chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";
            chartDoanhThu.ChartAreas[0].AxisX.Interval = 1;
            chartDoanhThu.ChartAreas[0].AxisX.LabelStyle.Angle = -15;

            if (data == null || data.Rows.Count == 0)
                return;

            // Tạo series
            var series = new Series("Doanh thu");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "N0";

            // Xác định tên cột hiển thị
            string nameCol = "Tên hàng";
            string valueCol = "Doanh thu (VNĐ)";

            // Nếu có thêm cột danh mục (ví dụ: “Tình trạng”, “Hãng sản xuất”) thì hiển thị song song
            if (data.Columns.Contains("Nhóm"))
                nameCol = "Nhóm";
            else if (data.Columns.Contains("Tình trạng"))
                nameCol = "Tình trạng";

            // Thêm điểm dữ liệu vào biểu đồ
            foreach (DataRow row in data.Rows)
            {
                string label = row[nameCol].ToString();
                double value = 0;
                double.TryParse(row[valueCol].ToString(), out value);
                series.Points.AddXY(label, value);
            }

            chartDoanhThu.Series.Add(series);
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
            cboKH.SelectedIndex = -1;
            cboKH.Text = "";
            cboDMC.SelectedIndex = 0;

            LoadDGV();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
                Excel.Worksheet ws = (Excel.Worksheet)wb.ActiveSheet;
                ws.Name = "BaoCaoDoanhThu";

                // Ghi tiêu đề
                ws.Cells[1, 1] = "BÁO CÁO DOANH THU";
                Excel.Range titleRange = ws.Range["A1", "E1"];
                titleRange.Merge();
                titleRange.Font.Size = 16;
                titleRange.Font.Bold = true;
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Ghi thông tin ngày giờ
                ws.Cells[2, 1] = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

                // Ghi tiêu đề cột từ DataGridView
                for (int i = 0; i < dgvChiTiet.Columns.Count; i++)
                {
                    ws.Cells[4, i + 1] = dgvChiTiet.Columns[i].HeaderText;
                    ws.Cells[4, i + 1].Font.Bold = true;
                    ws.Cells[4, i + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                }

                // Ghi dữ liệu
                for (int i = 0; i < dgvChiTiet.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvChiTiet.Columns.Count; j++)
                    {
                        ws.Cells[i + 5, j + 1] = dgvChiTiet.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                // Auto-fit cột
                ws.Columns.AutoFit();

                // Hiển thị Excel
                app.Visible = true;

                MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
