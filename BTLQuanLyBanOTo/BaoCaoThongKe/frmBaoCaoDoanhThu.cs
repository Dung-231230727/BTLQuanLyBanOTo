using BTLQuanLyBanOTo.Classes;
using System;
using System.Data;
using System.IO;
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
            // --- Phần SELECT/GROUP BY cơ bản (linh hoạt) ---
            System.Text.StringBuilder selectParts = new System.Text.StringBuilder();
            System.Text.StringBuilder groupParts = new System.Text.StringBuilder();
            System.Text.StringBuilder fromParts = new System.Text.StringBuilder();

            // Khởi tạo các thành phần FROM và WHERE cơ bản
            fromParts.AppendLine("FROM ChiTietDonDatHang ct");
            fromParts.AppendLine("JOIN DonDatHang dh ON ct.SoDDH = dh.SoDDH");
            fromParts.AppendLine("JOIN DanhMucHang h ON ct.MaHang = h.MaHang");
            string where = "WHERE 1=1";

            // Luôn bao gồm Mã/Tên hàng vào SELECT và GROUP BY (Báo cáo Sản phẩm)
            selectParts.Append("h.MaHang AS [Mã hàng], h.TenHang AS [Tên hàng]");
            groupParts.Append("h.MaHang, h.TenHang");

            // --- Bộ lọc Khách hàng (Thêm cột Khách hàng vào báo cáo nếu có lọc) ---
            if (cboKH.SelectedIndex != -1 && cboKH.SelectedValue != null)
            {
                selectParts.Append(", kh.TenKhach AS [Tên Khách hàng]");
                groupParts.Append(", kh.TenKhach");
                fromParts.AppendLine("JOIN KhachHang kh ON dh.MaKhach = kh.MaKhach");
                where += $" AND dh.MaKhach = '{cboKH.SelectedValue}'"; // Lọc thêm vào WHERE
            }

            // --- Bộ lọc Nhân viên (Thêm cột Nhân viên vào báo cáo nếu có lọc) ---
            if (cboNV.SelectedIndex != -1 && cboNV.SelectedValue != null)
            {
                selectParts.Append(", nv.TenNV AS [Tên Nhân viên]");
                groupParts.Append(", nv.TenNV");
                fromParts.AppendLine("JOIN NhanVien nv ON dh.MaNV = nv.MaNV");
                where += $" AND dh.MaNV = '{cboNV.SelectedValue}'"; // Lọc thêm vào WHERE
            }

            // --- Bộ lọc Thời gian ---
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
            // THÊM CỘT [Số lượng bán] vào SELECT
            string select = $"SELECT {selectParts.ToString()}, SUM(ct.SoLuong) AS [Số lượng bán], SUM(ct.ThanhTien) AS [Doanh thu (VNĐ)]";
            string group = $"GROUP BY {groupParts.ToString()}";
            string order = "ORDER BY [Doanh thu (VNĐ)] DESC";

            string sql = $"{select} {fromParts.ToString()} {where} {group} {order}";

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
            Excel.Application app = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                if (dgvChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Khởi tạo SaveFileDialog
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook|*.xlsx",
                    Title = "Xuất báo cáo doanh thu ra Excel",
                    FileName = "BaoCaoDoanhThu_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                //Khởi tạo Excel ẩn
                app = new Excel.Application();
                app.Visible = false;
                wb = app.Workbooks.Add(Type.Missing);
                ws = (Excel.Worksheet)wb.ActiveSheet;
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

                ws.Columns.AutoFit();

                //Lưu và đóng ứng dụng
                wb.SaveAs(sfd.FileName);
                wb.Close(false);
                app.Quit();

                MessageBox.Show("Xuất báo cáo thành công!\n" + Path.GetFileName(sfd.FileName), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
