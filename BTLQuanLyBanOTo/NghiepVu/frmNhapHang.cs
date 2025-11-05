using BTLQuanLyBanOTo.Classes;
using BTLQuanLyBanOTo.DanhMuc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BTLQuanLyBanOTo.NghiepVu
{
    public partial class frmNhapHang : Form
    {
        public frmNhapHang()
        {
            InitializeComponent();
        }

        DataProcesser dt;
        DataTable tblChiTietDonHang;

        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();

            LoadAllCBO();
            LoadDGV();
            reset();
        }

        public void LoadDGV()
        {
            //khỏi tạo bảng tạm
            tblChiTietDonHang = new DataTable();

            //định nghĩa các cột
            tblChiTietDonHang.Columns.Add("MaHang", typeof(string));
            tblChiTietDonHang.Columns.Add("TenHang", typeof(string));
            tblChiTietDonHang.Columns.Add("SoLuong", typeof(int));
            tblChiTietDonHang.Columns.Add("DonGia", typeof(decimal));
            tblChiTietDonHang.Columns.Add("GiamGia", typeof(decimal));
            tblChiTietDonHang.Columns.Add("ThanhTien", typeof(decimal));

            //gán cho dgv
            dgvGioHang.DataSource = tblChiTietDonHang;

            //định dạng cột
            dgvGioHang.Columns["MaHang"].HeaderText = "Mã hàng";
            dgvGioHang.Columns["TenHang"].HeaderText = "Tên hàng";
            dgvGioHang.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvGioHang.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvGioHang.Columns["GiamGia"].HeaderText = "Giảm giá";
            dgvGioHang.Columns["ThanhTien"].HeaderText = "Thành tiền";
            dgvGioHang.Columns["TenHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        public void LoadCBO(ComboBox cbo, string sql, string displayMember, string valueMember)
        {
            DataTable tbl = dt.ExecuteQuery(sql);
            cbo.DataSource = tbl;
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
            cbo.SelectedIndex = -1;
        }

        public void LoadCBOTimKiem()
        {
            string sqlTim = "SELECT SoHDN FROM HoaDonNhap order by SoHDN desc";
            DataTable tblTim = dt.ExecuteQuery(sqlTim);
            cboTimKiem.DataSource = tblTim;
            cboTimKiem.DisplayMember = "SoHDN";
            cboTimKiem.ValueMember = "SoHDN";
            cboTimKiem.SelectedIndex = -1;
        }

        public void LoadAllCBO()
        {
            //
            //LoadCBO(cboMaNV, "SELECT MaNV, TenNV FROM NhanVien", "MaNV", "MaNV");
            //
            LoadCBO(cboMaNCC, "SELECT MaNCC, TenNCC FROM NhaCungCap", "MaNCC", "MaNCC");
            //
            LoadCBO(cboMaSP, "SELECT MaHang, TenHang FROM DanhMucHang WHERE SoLuong > 0", "MaHang", "MaHang");
            //
            LoadCBOTimKiem();
        }

        public void resetGrbTTC()
        {
            txtMa.Enabled = true;
            txtMa.Text = "";
            dtpNgayNhap.Enabled = true;
            dtpNgayNhap.Value = DateTime.Now;

            cboMaNV.DataSource = null;
            cboMaNV.Items.Clear();
            cboMaNV.Items.Add(frmMain.MaNV_DangNhap);
            cboMaNV.SelectedIndex = 0;
            cboMaNV.Enabled = false;

            txtTenNV.Text = frmMain.TenNV_DangNhap;
            txtTenNV.Enabled = false;

            cboMaNCC.Enabled = true;
            cboMaNCC.Text = "";
            cboMaNCC.SelectedIndex = -1;
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";

            cboTimKiem.Text = "";
            cboTimKiem.SelectedIndex = -1;
        }

        public void resetGrbCTMH()
        {
            cboMaSP.Text = "";
            cboMaSP.SelectedIndex = -1;
            txtTenSP.Text = "";
            txtDGN.Text = "0";
            txtSLT.Text = "0";
            numSLN.Value = 0;
            txtGG.Text = "0";
            txtTT.Text = "0";
        }

        public void resetDGV()
        {
            if (tblChiTietDonHang != null)
            {
                tblChiTietDonHang.Clear();
            }
            txtTongTien.Text = "0";
        }

        public void resetBtn()
        {
            //ẩn
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnIn.Enabled = false;
            btnTimKiem.Enabled = false;
            //btnHuy.Enabled = false;
            btnThemSP.Enabled = false;
            btnXoaSP.Enabled = false;

            //hiện
            btnThem.Enabled = true;
        }

        public void reset()
        {
            resetGrbTTC();
            resetGrbCTMH();
            resetBtn();
            resetDGV();
            action = "";
        }

        private string action = "";

        private void btnThem_Click(object sender, EventArgs e)
        {
            reset();
            txtMa.Text = TaoMaDonHang();

            //ẩn
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            //hiện
            btnThemSP.Enabled = true;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnIn.Enabled = true;

            action = "add";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            action = "edit";
            //ẩn
            txtMa.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
            btnIn.Enabled = false;
            //hiện
            btnThemSP.Enabled = true;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            action = "delete";
            //ẩn
            txtMa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = false;
            btnIn.Enabled = false;
            //hiện
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private string TaoMaDonHang()
        {
            string ngayHienTai = DateTime.Now.ToString("ddMMyyyy");
            string prefix = "HĐN_" + ngayHienTai; // Ví dụ: HĐB_26102025

            string sql = "SELECT MAX(SoHDN) FROM HoaDonNhap WHERE SoHDN LIKE @prefix";
            SqlParameter[] prms = new SqlParameter[] {
                new SqlParameter("@prefix", prefix + "%")
            };

            object result = dt.ExecuteScalar(sql, prms);

            int soThuTuMoi = 1;

            if (result != null && result != DBNull.Value)
            {
                string maLonNhat = result.ToString();
                string soCuoi = maLonNhat.Substring(prefix.Length);

                soThuTuMoi = int.Parse(soCuoi) + 1;
            }

            string maMoi = prefix + soThuTuMoi.ToString("D4");

            return maMoi;
        }

        private void cboMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNCC.SelectedIndex == -1) return;

            string sql = "select * from NhaCungCap where MaNCC=@ma";
            SqlParameter[] prms = new SqlParameter[] {
                new SqlParameter("@ma", cboMaNCC.SelectedValue.ToString())
            };
            DataTable tblKH = dt.ExecuteQuery(sql, prms);

            if (tblKH.Rows.Count > 0)
            {
                txtTenNCC.Text = tblKH.Rows[0]["TenNCC"].ToString();
                txtDiaChi.Text = tblKH.Rows[0]["DiaChi"].ToString();
                txtDienThoai.Text = tblKH.Rows[0]["DienThoai"].ToString();
            }
        }

        private void cboMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaSP.SelectedIndex == -1)
            {
                return;
            }

            string sql = "SELECT * FROM DanhMucHang WHERE MaHang = @ma";
            SqlParameter[] prms = new SqlParameter[] {
                new SqlParameter("@ma", cboMaSP.SelectedValue.ToString())
            };
            DataTable tblSP = dt.ExecuteQuery(sql, prms);

            if (tblSP.Rows.Count > 0)
            {
                txtTenSP.Text = tblSP.Rows[0]["TenHang"].ToString();
                object donGiaNhapValue = tblSP.Rows[0]["DonGiaNhap"];
                if (donGiaNhapValue != DBNull.Value)
                {
                    decimal donGiaNhap = Convert.ToDecimal(donGiaNhapValue);
                    txtDGN.Text = donGiaNhap.ToString("N2");
                }
                txtSLT.Text = tblSP.Rows[0]["SoLuong"].ToString();
            }
        }

        public void TinhThanhTien()
        {
            decimal donGia = 0;
            decimal giamGia = 0;
            int soLuongNhap = (int)numSLN.Value;

            decimal.TryParse(txtDGN.Text, out donGia);
            decimal.TryParse(txtGG.Text, out giamGia);

            decimal thanhTien = (soLuongNhap * donGia) - giamGia;
            txtTT.Text = string.Format("{0:N2} VNĐ", thanhTien);
        }

        public void TinhTongTien()
        {
            decimal tongTienHang = 0;

            foreach (DataRow row in tblChiTietDonHang.Rows)
            {
                tongTienHang += (decimal)row["ThanhTien"];
            }

            decimal tongCong = tongTienHang;

            txtTongTien.Text = string.Format("{0:N2} VNĐ", tongCong);
        }

        private void txtDGN_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void numSLN_ValueChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void txtGG_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (cboMaSP.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numSLN.Value <= 0)
            {
                MessageBox.Show("Số lượng bán phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSLN.Focus();
                return;
            }

            string maHang = cboMaSP.SelectedValue.ToString();
            foreach (DataRow row in tblChiTietDonHang.Rows)
            {
                if (row["MaHang"].ToString() == maHang)
                {
                    MessageBox.Show("Sản phẩm này đã có trong giỏ hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            string tenHang = txtTenSP.Text;
            int soLuongNhap = (int)numSLN.Value;

            decimal donGia, giamGia, thanhTien;
            decimal.TryParse(txtDGN.Text, out donGia);
            decimal.TryParse(txtGG.Text, out giamGia);
            decimal.TryParse(txtTT.Text, out thanhTien);

            tblChiTietDonHang.Rows.Add(maHang, tenHang, soLuongNhap, donGia, giamGia, thanhTien);
            MessageBox.Show("Đã thêm sản phẩm vào giỏ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TinhTongTien();
            resetGrbCTMH();
        }

        private void dgvGioHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvGioHang.Rows[e.RowIndex];
            if (row.Cells["MaHang"].Value == null || string.IsNullOrWhiteSpace(row.Cells["MaHang"].Value.ToString()))
            {
                return;
            }

            if (e.RowIndex >= 0 && (action != "search" && action != "delete"))
            {
                btnXoaSP.Enabled = true;
            }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            string tenSP_dangChon = dgvGioHang.CurrentRow.Cells["TenHang"].Value.ToString();

            DialogResult r = MessageBox.Show(
                "Bạn có chắc muốn xóa sản phẩm '" + tenSP_dangChon + "' khỏi giỏ hàng không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );

            if (r == DialogResult.Yes)
            {
                try
                {
                    int rowIndex = dgvGioHang.CurrentRow.Index;

                    tblChiTietDonHang.Rows.RemoveAt(rowIndex);

                    MessageBox.Show("Đã xóa sản phẩm khỏi giỏ hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TinhTongTien();
                    resetGrbCTMH();
                    btnXoaSP.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa khỏi giỏ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string connStr = "Server=Dung;Database=QuanLyCuaHangOto;Integrated Security=True";

            decimal tongTien = 0;
            string tongTienStr = txtTongTien.Text.Trim();
            decimal.TryParse(tongTienStr, NumberStyles.Currency, CultureInfo.CurrentCulture, out tongTien);
            DateTime ngayNhap = dtpNgayNhap.Value;

            //
            if (cboMaNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //
            if (ngayNhap.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Thời gian nhập phải bằng hoặc ở sau thời gian hiện tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //
            if (tblChiTietDonHang.Rows.Count == 0 && action != "delete")
            {
                MessageBox.Show("Hóa đơn phải có ít nhất một sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    if (action == "add" || action == "edit")
                    {
                        if (action == "edit")
                        {
                            HoanTraNCCVaXoaChiTietCu(conn, transaction);
                        }

                        string sqlDonHang = (action == "add") ?
                            @"INSERT INTO HoaDonBan(SoHDN, MaNV, MaNCC, NgayNhap, TongTien, TrangThai)
                            VALUES(@soHDN, @maNV, @maNCC, @ngayNhap, @tongTien, @trangThai)"
                            :
                            @"UPDATE HoaDonNhap SET MaNCC = @maNCC, NgayNhap = @ngayNhap,
                            TongTien = @tongTien, TrangThai = @trangThai WHERE SoHDN = @soHDN";

                        SqlCommand cmdDonHang = new SqlCommand(sqlDonHang, conn, transaction);
                        cmdDonHang.Parameters.AddWithValue("@soHDN", txtMa.Text);
                        cmdDonHang.Parameters.AddWithValue("@maNV", frmMain.MaNV_DangNhap);
                        cmdDonHang.Parameters.AddWithValue("@maNCC", cboMaNCC.SelectedValue);
                        cmdDonHang.Parameters.AddWithValue("@ngayNhap", ngayNhap);
                        cmdDonHang.Parameters.AddWithValue("@tongTien", tongTien);
                        cmdDonHang.Parameters.AddWithValue("@trangThai", 0);
                        cmdDonHang.ExecuteNonQuery();

                        //
                        LuuChiTietVaCapNhatKho(conn, transaction);

                        MessageBox.Show((action == "add" ? "Thêm" : "Sửa") + " hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else if (action == "delete")
                    {
                        //
                        HoanTraNCCVaXoaChiTietCu(conn, transaction);

                        //
                        string sqlDeleteDonHang = "DELETE FROM HoaDonNhap WHERE SoHDN = @soHDN";
                        SqlCommand cmdDeleteDonHang = new SqlCommand(sqlDeleteDonHang, conn, transaction);
                        cmdDeleteDonHang.Parameters.AddWithValue("@soHDN", txtMa.Text);
                        cmdDeleteDonHang.ExecuteNonQuery();

                        MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset();
                    }

                    //
                    transaction.Commit();

                    //
                    LoadCBO(cboMaSP, "SELECT MaHang, TenHang FROM DanhMucHang WHERE SoLuong > 0", "TenHang", "MaHang");
                    LoadCBOTimKiem();

                    //reset();

                    if (action == "add" || action == "edit")
                    {
                        btnIn.Enabled = true;
                        btnLuu.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lưu hóa đơn thất bại! Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            resetGrbCTMH();
        }

        //
        private void LuuChiTietVaCapNhatKho(SqlConnection conn, SqlTransaction transaction)
        {
            foreach (DataRow row in tblChiTietDonHang.Rows)
            {
                // INSERT VÀO BẢNG ChiTietHoaDonNhap
                string sqlChiTiet = @"INSERT INTO ChiTietHoaDonNhap(SoHDN, MaHang, SoLuong, DonGia, GiamGia, ThanhTien)
                             VALUES(@soHDN, @maHang, @soLuong, @donGia,@giamGia, @thanhTien)";

                SqlCommand cmdChiTiet = new SqlCommand(sqlChiTiet, conn, transaction);
                cmdChiTiet.Parameters.AddWithValue("@soHDN", txtMa.Text);
                cmdChiTiet.Parameters.AddWithValue("@maHang", row["MaHang"]);
                cmdChiTiet.Parameters.AddWithValue("@soLuong", row["SoLuong"]);
                cmdChiTiet.Parameters.AddWithValue("@donGia", row["DonGia"]);
                cmdChiTiet.Parameters.AddWithValue("@giamGia", row["GiamGia"]);
                cmdChiTiet.Parameters.AddWithValue("@thanhTien", row["ThanhTien"]);
                cmdChiTiet.ExecuteNonQuery();

                // CẬP NHẬT TỒN KHO (cộng vào)
                string sqlCapNhatKho = @"UPDATE DanhMucHang SET SoLuong = SoLuong + @soLuongNhap WHERE MaHang = @maHang";

                SqlCommand cmdCapNhatKho = new SqlCommand(sqlCapNhatKho, conn, transaction);
                cmdCapNhatKho.Parameters.AddWithValue("@soLuongNhap", row["SoLuong"]);
                cmdCapNhatKho.Parameters.AddWithValue("@maHang", row["MaHang"]);
                cmdCapNhatKho.ExecuteNonQuery();
            }
        }

        // 
        private void HoanTraNCCVaXoaChiTietCu(SqlConnection conn, SqlTransaction transaction)
        {
            // Lấy Chi tiết Đơn hàng CŨ từ DB
            string sqlGetChiTiet = "SELECT MaHang, SoLuong FROM ChiTietHoaDonNhap WHERE SoHDN = @soHDN";
            DataTable tblChiTietCu = dt.ExecuteQuery(sqlGetChiTiet, new SqlParameter[] { new SqlParameter("@soHDN", txtMa.Text) });

            // Hoàn trả tồn kho cũ
            foreach (DataRow row in tblChiTietCu.Rows)
            {
                string sqlHoanTra = @"UPDATE DanhMucHang 
                             SET SoLuong = SoLuong - @soLuongHoanTra 
                             WHERE MaHang = @maHang";
                SqlCommand cmdHoanTra = new SqlCommand(sqlHoanTra, conn, transaction);
                cmdHoanTra.Parameters.AddWithValue("@soLuongHoanTra", row["SoLuong"]);
                cmdHoanTra.Parameters.AddWithValue("@maHang", row["MaHang"]);
                cmdHoanTra.ExecuteNonQuery();
            }

            // Xóa chi tiết cũ khỏi bảng
            string sqlDeleteChiTiet = "DELETE FROM ChiTietHoaDonNhap WHERE SoHDN = @soHDN";
            SqlCommand cmdDeleteChiTiet = new SqlCommand(sqlDeleteChiTiet, conn, transaction);
            cmdDeleteChiTiet.Parameters.AddWithValue("@soHDN", txtMa.Text);
            cmdDeleteChiTiet.ExecuteNonQuery();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // 
            if (tblChiTietDonHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 1. Khởi tạo SaveFileDialog
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Xuất hóa đơn nhập hàng ra Excel",
                FileName = "HoaDonNhap_" + txtMa.Text + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".xlsx"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            // 2. Khởi tạo Excel ẩn
            Excel.Application exApp = null;
            Excel.Workbook exBook = null;
            Excel.Worksheet exSheet = null;

            try
            {
                exApp = new Excel.Application();
                exApp.Visible = false;
                exBook = exApp.Workbooks.Add(Missing.Value);
                exSheet = (Excel.Worksheet)exBook.Worksheets.get_Item(1);
                exSheet.Name = "Hóa Đơn Nhập Hàng";

                // Định dạng tiêu đề
                exSheet.Range["A1:F1"].Merge(true);
                exSheet.Range["A1"].Value = "HÓA ĐƠN NHẬP Ô TÔ";
                exSheet.Range["A1"].Font.Bold = true;
                exSheet.Range["A1"].Font.Size = 16;
                exSheet.Range["A1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Thông tin hóa đơn
                exSheet.Range["A3"].Value = "Mã HĐ:";
                exSheet.Range["B3"].Value = txtMa.Text;
                exSheet.Range["A4"].Value = "Ngày đặt:";
                exSheet.Range["B4"].Value = dtpNgayNhap.Value.ToString("dd/MM/yyyy");


                // Thông tin khách hàng
                exSheet.Range["A6"].Value = "Nhà cung cấp:";
                exSheet.Range["B6"].Value = txtTenNCC.Text;
                exSheet.Range["A7"].Value = "Địa chỉ:";
                exSheet.Range["B7"].Value = txtDiaChi.Text;
                exSheet.Range["A8"].Value = "Điện thoại:";
                exSheet.Range["B8"].Value = txtDienThoai.Text;

                // Thông tin Nhân viên bán
                exSheet.Range["D6"].Value = "Nhân viên:";
                exSheet.Range["E6"].Value = txtTenNV.Text;

                // 
                exSheet.Range["A10"].Value = "STT";
                exSheet.Range["B10"].Value = "Tên hàng";
                exSheet.Range["C10"].Value = "Số lượng";
                exSheet.Range["D10"].Value = "Đơn giá";
                exSheet.Range["E10"].Value = "Giảm giá";
                exSheet.Range["F10"].Value = "Thành tiền";

                // Định dạng tiêu đề cột
                exSheet.Range["A10:F10"].Font.Bold = true;
                exSheet.Range["A10:F10"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // 
                int startRow = 11;
                for (int i = 0; i < tblChiTietDonHang.Rows.Count; i++)
                {
                    DataRow row = tblChiTietDonHang.Rows[i];
                    exSheet.Range["A" + (startRow + i)].Value = i + 1;
                    exSheet.Range["B" + (startRow + i)].Value = row["TenHang"];
                    exSheet.Range["C" + (startRow + i)].Value = row["SoLuong"];
                    exSheet.Range["D" + (startRow + i)].Value = row["DonGia"];
                    exSheet.Range["E" + (startRow + i)].Value = row["GiamGia"];
                    exSheet.Range["F" + (startRow + i)].Value = row["ThanhTien"];
                }

                //
                int lastDataRow = startRow + tblChiTietDonHang.Rows.Count - 1;
                int totalRow = lastDataRow + 2;

                exSheet.Range["E" + totalRow].Value = "Tổng tiền hàng:";
                exSheet.Range["F" + totalRow].Formula = "=SUM(F" + startRow + ":F" + lastDataRow + ")";

                exSheet.Range["E" + (totalRow + 3)].Value = "TỔNG CỘNG:";
                exSheet.Range["F" + (totalRow + 3)].Value = txtTongTien.Text;
                exSheet.Range["E" + (totalRow + 3) + ":F" + (totalRow + 3)].Font.Bold = true;

                exSheet.Columns["A:F"].AutoFit();
                exSheet.Range["A10:F" + lastDataRow].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // Lưu và đóng ứng dụng
                exBook.SaveAs(sfd.FileName);
                exBook.Close(false);
                exApp.Quit();

                //cập nhật trạng thái sau khi xuất thành công
                string sqlUpdateStatus = "UPDATE HoaDonNhap SET TrangThai = 1 WHERE SoHDN = @ma";
                dt.ExecuteNonQuery(sqlUpdateStatus, new SqlParameter[] { new SqlParameter("@ma", txtMa.Text) });

                MessageBox.Show("Đã xuất hóa đơn ra Excel thành công!\n" + System.IO.Path.GetFileName(sfd.FileName), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset form sau khi in
                //reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (exSheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(exSheet);
                if (exBook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(exBook);
                if (exApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                exSheet = null;
                exBook = null;
                exApp = null;
                GC.Collect();
            }
        }

        private void cboTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTimKiem.SelectedIndex != -1)
            {
                btnTimKiem.Enabled = true;
                //btnHuy.Enabled = true;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //nếu có So HDN thì fill lên form
            string soHDNCanTim = cboTimKiem.SelectedValue.ToString();

            resetGrbTTC();
            resetGrbCTMH();
            resetDGV();

            try
            {
                //
                string sqlDonHang = @"
                                    SELECT dh.*, nv.TenNV, ncc.TenNCC, ncc.DiaChi, ncc.DienThoai
                                    FROM HoaDonNhap dh
                                    JOIN NhanVien nv ON dh.MaNV = nv.MaNV
                                    JOIN NhaCungCap ncc ON ncc.MaNCC = dh.MaNCC
                                    WHERE dh.SoHDN = @ma";

                DataTable tblDonHang = dt.ExecuteQuery(sqlDonHang, new SqlParameter[] { new SqlParameter("@ma", soHDNCanTim) });

                int trangThai = 0;

                if (tblDonHang.Rows.Count > 0)
                {
                    DataRow rowDH = tblDonHang.Rows[0];

                    txtMa.Text = rowDH["SoHDN"].ToString();
                    dtpNgayNhap.Value = (DateTime)rowDH["NgayNhap"];
                    cboMaNV.Text = rowDH["TenNV"].ToString();
                    txtTenNV.Text = rowDH["TenNV"].ToString();
                    cboMaNCC.SelectedValue = rowDH["MaNCC"];
                    txtTenNCC.Text = rowDH["TenNCC"].ToString();
                    txtDiaChi.Text = rowDH["DiaChi"].ToString();
                    txtDienThoai.Text = rowDH["DienThoai"].ToString();

                    txtTongTien.Text = string.Format("{0:N2} VNĐ", (decimal)rowDH["TongTien"]);
                    trangThai = (int)rowDH["TrangThai"];
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reset();
                    return;
                }

                //
                string sqlChiTiet = @"
                                    SELECT ct.MaHang, dm.TenHang, ct.SoLuong, ct.DonGia, ct.GiamGia, ct.ThanhTien
                                    FROM ChiTietHoaDonNhap AS ct
                                    JOIN DanhMucHang AS dm ON ct.MaHang = dm.MaHang
                                    WHERE ct.SoHDN = @ma";

                DataTable tblChiTiet = dt.ExecuteQuery(sqlChiTiet, new SqlParameter[] { new SqlParameter("@ma", soHDNCanTim) });

                // Đổ dữ liệu vào bảng tạm tblChiTietDonHang
                foreach (DataRow rowCT in tblChiTiet.Rows)
                {
                    tblChiTietDonHang.Rows.Add(
                        rowCT["MaHang"],
                        rowCT["TenHang"],
                        rowCT["SoLuong"],
                        rowCT["DonGia"],
                        rowCT["GiamGia"],
                        rowCT["ThanhTien"]
                    );
                }

                //kiemtra trang thai
                if (trangThai == 0)
                {
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnBoQua.Enabled = true;
                    btnIn.Enabled = true;
                    btnTimKiem.Enabled = true;
                    //btnHuy.Enabled = true;
                    cboTimKiem.Enabled = true;
                }
                else if (trangThai == 1)
                {
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnBoQua.Enabled = true;
                    btnIn.Enabled = true;
                    btnTimKiem.Enabled = true;
                    //btnHuy.Enabled = true;
                    cboTimKiem.Enabled = true;
                }

                //ẩn
                btnThem.Enabled = false;
                btnLuu.Enabled = false;
                btnThemSP.Enabled = false;
                btnXoaSP.Enabled = false;

                action = "search";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reset();
            }
        }

        //private void btnHuy_Click(object sender, EventArgs e)
        //{
        //    reset();
        //}

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

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            if (action == "edit" || action == "delete")
            {
                //bật
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
                btnIn.Enabled = true;
                btnTimKiem.Enabled = true;
                //btnHuy.Enabled = true;
                cboTimKiem.Enabled = true;

                //ẩn
                btnThem.Enabled = false;
                btnLuu.Enabled = false;
                btnThemSP.Enabled = false;
                btnXoaSP.Enabled = false;

                action = "search";
            }
            else if (action == "search" || action == "add")
            {
                reset();
            }
        }

        //truy vấn ncc băng sdt
        private void txtDienThoai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                KiemTraNCCTheoSDT();
            }
        }

        private void KiemTraNCCTheoSDT()
        {
            string sdt = txtDienThoai.Text.Trim();
            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "SELECT MaNCC, TenNCC, DiaChi, DienThoai FROM NhaCungCap WHERE DienThoai = @dt";
            var tbl = dt.ExecuteQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@dt", sdt)
            });
            if (tbl.Rows.Count > 0)
            {
                DataRow row = tbl.Rows[0];
                cboMaNCC.Text = row["MaNCC"].ToString();
                txtTenNCC.Text = row["TenNCC"].ToString();
                txtDiaChi.Text = row["DiaChi"].ToString();

                MessageBox.Show("Đã tìm thấy thông tin nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    "Số điện thoại này chưa có trong hệ thống. Bạn có muốn thêm nhà cung cấp mới không?",
                    "Khách hàng mới",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    frmNhaCungCap f = new frmNhaCungCap();
                    f.IsFromNCC = true;
                    f.DT_BanHang = sdt;
                    f.ShowDialog();
                }
            }
        }
    }
}
