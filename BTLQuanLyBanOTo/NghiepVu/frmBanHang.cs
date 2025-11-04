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
    public partial class frmBanHang : Form
    {
        public frmBanHang()
        {
            InitializeComponent();
        }

        DataProcesser dt;
        DataTable tblChiTietDonHang;
        private void frmBanHang_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();

            LoadAllCBO();
            LoadDGV();
            reset();
        }

        public void LoadDGV()
        {
            // 1. Khởi tạo bảng tạm (giỏ hàng)
            tblChiTietDonHang = new DataTable();

            // 2. Định nghĩa các cột cho giỏ hàng
            tblChiTietDonHang.Columns.Add("MaHang", typeof(string));
            tblChiTietDonHang.Columns.Add("TenHang", typeof(string));
            tblChiTietDonHang.Columns.Add("SoLuong", typeof(int));
            tblChiTietDonHang.Columns.Add("DonGia", typeof(decimal));
            tblChiTietDonHang.Columns.Add("GiamGia", typeof(decimal));
            tblChiTietDonHang.Columns.Add("ThanhTien", typeof(decimal));

            // 3. Gán bảng tạm (đang trống) này làm Nguồn dữ liệu
            dgvGioHang.DataSource = tblChiTietDonHang;

            // 4.Định dạng cột
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

        public void LoadAllCBO()
        {
            //nv
            //LoadCBO(cboMaNV, "SELECT MaNV, TenNV FROM NhanVien", "MaNV", "MaNV");
            //kh
            LoadCBO(cboMaKH, "SELECT MaKhach, TenKhach FROM KhachHang", "MaKhach", "MaKhach");
            //sp
            LoadCBO(cboMaSP, "SELECT MaHang, TenHang FROM DanhMucHang WHERE SoLuong > 0", "MaHang", "MaHang");
            //tk
            LoadCBOTimKiem();
        }

        public void LoadCBOTimKiem()
        {
            string sqlTim = "SELECT SoDDH FROM DonDatHang order by SoDDH desc";
            DataTable tblTim = dt.ExecuteQuery(sqlTim);
            cboTimKiem.DataSource = tblTim;
            cboTimKiem.DisplayMember = "SoDDH";
            cboTimKiem.ValueMember = "SoDDH";
            cboTimKiem.SelectedIndex = -1;
        }

        public void resetGrbTTC()
        {
            txtMa.Enabled = true;
            txtMa.Text = "";
            dtpNgayDat.Enabled = true;
            dtpNgayDat.Value = DateTime.Now;
            dtpNgayGiao.Enabled = true;
            dtpNgayGiao.Value = DateTime.Now;
            txtThue.Enabled = true;
            txtThue.Text = "0";

            cboMaNV.DataSource = null;
            cboMaNV.Items.Clear();
            cboMaNV.Items.Add(frmMain.MaNV_DangNhap);
            cboMaNV.SelectedIndex = 0;
            cboMaNV.Enabled = false;

            txtTenNV.Text = frmMain.TenNV_DangNhap;
            txtTenNV.Enabled = false;

            cboMaKH.Enabled = true;
            cboMaKH.Text = "";
            cboMaKH.SelectedIndex = -1;
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtDatCoc.Enabled = true;
            txtDatCoc.Text = "0";

            cboTimKiem.Text = "";
            cboTimKiem.SelectedIndex = -1;
        }

        public void resetGrbCTMH()
        {
            cboMaSP.Text = "";
            cboMaSP.SelectedIndex = -1;
            txtTenSP.Text = "";
            txtDG.Text = "0";
            txtSLT.Text = "0";
            numSLB.Value = 0;
            txtGG.Text = "0";
            txtTT.Text = "0";
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

        public void resetDGV()
        {
            if (tblChiTietDonHang != null)
            {
                tblChiTietDonHang.Clear();
            }
            txtTongTien.Text = "0";
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
            // lấy ngày hiện tại
            string ngayHienTai = DateTime.Now.ToString("ddMMyyyy");
            string prefix = "HĐB_" + ngayHienTai; // Ví dụ: HĐB_26102025

            // lấy mã lớn nhất
            string sql = "SELECT MAX(SoDDH) FROM DonDatHang WHERE SoDDH LIKE @prefix";
            SqlParameter[] prms = new SqlParameter[] {
                new SqlParameter("@prefix", prefix + "%")
            };

            object result = dt.ExecuteScalar(sql, prms);
            int soThuTuMoi = 1;
            if (result != null && result != DBNull.Value)
            {
                string maLonNhat = result.ToString();
                string soCuoi = maLonNhat.Substring(prefix.Length);

                soThuTuMoi = int.Parse(soCuoi) + 1; //cộng 1 vào số cũ
            }

            // nối lại mã
            string maMoi = prefix + soThuTuMoi.ToString("D4");

            return maMoi;
        }

        private void cboMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaKH.SelectedIndex == -1)
            {
                return;
            }

            string sql = "SELECT * FROM KhachHang WHERE MaKhach = @ma";
            SqlParameter[] prms = new SqlParameter[] {
                new SqlParameter("@ma", cboMaKH.SelectedValue.ToString())
            };
            DataTable tblKH = dt.ExecuteQuery(sql, prms);

            if (tblKH.Rows.Count > 0)
            {
                txtTenKH.Text = tblKH.Rows[0]["TenKhach"].ToString();
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
                object donGiaBanValue = tblSP.Rows[0]["DonGiaBan"];
                if (donGiaBanValue != DBNull.Value)
                {
                    decimal donGiaBan = Convert.ToDecimal(donGiaBanValue);
                    txtDG.Text = donGiaBan.ToString("N2");
                }
                txtSLT.Text = tblSP.Rows[0]["SoLuong"].ToString();
            }
        }

        public void TinhThanhTien()
        {
            decimal donGia = 0;
            decimal giamGia = 0;
            int soLuongBan = (int)numSLB.Value;

            decimal.TryParse(txtDG.Text, out donGia);
            decimal.TryParse(txtGG.Text, out giamGia);

            decimal thanhTien = (soLuongBan * donGia) - giamGia;
            txtTT.Text = string.Format("{0:N2} VNĐ", thanhTien);
        }

        public void TinhTongTien()
        {
            decimal tongTienHang = 0;

            foreach (DataRow row in tblChiTietDonHang.Rows)
            {
                tongTienHang += (decimal)row["ThanhTien"];
            }

            decimal thue = 0;
            decimal datCoc = 0;
            decimal.TryParse(txtThue.Text, out thue);
            decimal.TryParse(txtDatCoc.Text, out datCoc);

            decimal tongThue = (tongTienHang * thue) / 100;
            decimal tongCong = (tongTienHang + tongThue) - datCoc;

            txtTongTien.Text = string.Format("{0:N2} VNĐ", tongCong);
        }

        private void txtDG_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void numSLB_ValueChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void txtGG_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            // ktra chọn
            if (cboMaSP.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ktra slb
            if (numSLB.Value <= 0)
            {
                MessageBox.Show("Số lượng bán phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSLB.Focus();
                return;
            }

            // ktra tồn
            int soLuongTon = 0;
            int.TryParse(txtSLT.Text, out soLuongTon);

            if (numSLB.Value > soLuongTon)
            {
                MessageBox.Show("Số lượng bán vượt quá số lượng tồn kho! (Tồn: " + soLuongTon + ")", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSLB.Focus();
                return;
            }

            // ktra giỏ
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
            int soLuongBan = (int)numSLB.Value;

            decimal donGia, giamGia, thanhTien;
            decimal.TryParse(txtDG.Text, out donGia);
            decimal.TryParse(txtGG.Text, out giamGia);
            decimal.TryParse(txtTT.Text, out thanhTien);

            tblChiTietDonHang.Rows.Add(maHang, tenHang, soLuongBan, donGia, giamGia, thanhTien);
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

            decimal tongTien = 0, datCoc = 0;
            float thue = 0;

            string tongTienStr = txtTongTien.Text.Trim();
            decimal.TryParse(tongTienStr, NumberStyles.Currency, CultureInfo.CurrentCulture, out tongTien);
            decimal.TryParse(txtDatCoc.Text, out datCoc);
            float.TryParse(txtThue.Text, out thue);
            DateTime ngayDat = dtpNgayDat.Value;
            DateTime ngayGiao = dtpNgayGiao.Value;

            // ktra
            if (cboMaKH.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //
            if (ngayDat.Date > ngayGiao.Date)
            {
                MessageBox.Show("Thời gian giao phải bằng hoặc ở sau thời gian đặt!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ngayDat.Date < DateTime.Now.Date || ngayGiao.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Thời gian đặt và thời gian giao phải ở sau thời gian hiện tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // chỉ ktra khi kp là xóa
            if (tblChiTietDonHang.Rows.Count == 0 && action != "delete")
            {
                MessageBox.Show("Hóa đơn phải có ít nhất một sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // transaction
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    if (action == "add" || action == "edit")
                    {
                        // edit
                        if (action == "edit")
                        {
                            HoanTraKhoVaXoaChiTietCu(conn, transaction);
                        }

                        // toán tử 3 ngôi thêm nếu action == add và sửa nếu ko phải
                        string sqlDonHang = (action == "add") ?
                            @"INSERT INTO DonDatHang(SoDDH, MaNV, MaKhach, NgayDat, NgayGiao, DatCoc, Thue, TongTien, TrangThai)
                            VALUES(@soDDH, @maNV, @maKhach, @ngayDat, @ngayGiao, @datCoc, @thue, @tongTien, @trangThai)"
                            :
                            @"UPDATE DonDatHang SET MaKhach = @maKhach, NgayDat = @ngayDat, NgayGiao = @ngayGiao, DatCoc = @datCoc, 
                            Thue = @thue, TongTien = @tongTien, TrangThai = @trangThai WHERE SoDDH = @soDDH";

                        SqlCommand cmdDonHang = new SqlCommand(sqlDonHang, conn, transaction);
                        cmdDonHang.Parameters.AddWithValue("@soDDH", txtMa.Text);
                        cmdDonHang.Parameters.AddWithValue("@maNV", frmMain.MaNV_DangNhap);
                        cmdDonHang.Parameters.AddWithValue("@maKhach", cboMaKH.SelectedValue);
                        cmdDonHang.Parameters.AddWithValue("@ngayDat", ngayDat);
                        cmdDonHang.Parameters.AddWithValue("@ngayGiao", ngayGiao);
                        cmdDonHang.Parameters.AddWithValue("@datCoc", datCoc);
                        cmdDonHang.Parameters.AddWithValue("@thue", thue);
                        cmdDonHang.Parameters.AddWithValue("@tongTien", tongTien);
                        cmdDonHang.Parameters.AddWithValue("@trangThai", 0);
                        cmdDonHang.ExecuteNonQuery();

                        //lưu chi tiết
                        LuuChiTietVaCapNhatKho(conn, transaction);

                        MessageBox.Show((action == "add" ? "Thêm" : "Sửa") + " hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (action == "delete")
                    {
                        // delete
                        HoanTraKhoVaXoaChiTietCu(conn, transaction);

                        // xóa bảng ĐonatHang
                        string sqlDeleteDonHang = "DELETE FROM DonDatHang WHERE SoDDH = @soDDH";
                        SqlCommand cmdDeleteDonHang = new SqlCommand(sqlDeleteDonHang, conn, transaction);
                        cmdDeleteDonHang.Parameters.AddWithValue("@soDDH", txtMa.Text);
                        cmdDeleteDonHang.ExecuteNonQuery();

                        MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset();
                    }
                    transaction.Commit();

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

        // lưu chi tiết và cập nhật kho
        private void LuuChiTietVaCapNhatKho(SqlConnection conn, SqlTransaction transaction)
        {
            foreach (DataRow row in tblChiTietDonHang.Rows)
            {
                string sqlChiTiet = @"INSERT INTO ChiTietDonDatHang(SoDDH, MaHang, SoLuong, GiamGia, ThanhTien)
                             VALUES(@soDDH, @maHang, @soLuong, @giamGia, @thanhTien)";

                SqlCommand cmdChiTiet = new SqlCommand(sqlChiTiet, conn, transaction);
                cmdChiTiet.Parameters.AddWithValue("@soDDH", txtMa.Text);
                cmdChiTiet.Parameters.AddWithValue("@maHang", row["MaHang"]);
                cmdChiTiet.Parameters.AddWithValue("@soLuong", row["SoLuong"]);
                cmdChiTiet.Parameters.AddWithValue("@giamGia", row["GiamGia"]);
                cmdChiTiet.Parameters.AddWithValue("@thanhTien", row["ThanhTien"]);
                cmdChiTiet.ExecuteNonQuery();

                string sqlCapNhatKho = @"UPDATE DanhMucHang SET SoLuong = SoLuong - @soLuongBan WHERE MaHang = @maHang";

                SqlCommand cmdCapNhatKho = new SqlCommand(sqlCapNhatKho, conn, transaction);
                cmdCapNhatKho.Parameters.AddWithValue("@soLuongBan", row["SoLuong"]);
                cmdCapNhatKho.Parameters.AddWithValue("@maHang", row["MaHang"]);
                cmdCapNhatKho.ExecuteNonQuery();
            }
        }

        // hoàn trả và xóa chi tiết cũ
        private void HoanTraKhoVaXoaChiTietCu(SqlConnection conn, SqlTransaction transaction)
        {
            string sqlGetChiTiet = "SELECT MaHang, SoLuong FROM ChiTietDonDatHang WHERE SoDDH = @soDDH";
            DataTable tblChiTietCu = dt.ExecuteQuery(sqlGetChiTiet, new SqlParameter[] { new SqlParameter("@soDDH", txtMa.Text) });

            // Hoàn trả 
            foreach (DataRow row in tblChiTietCu.Rows)
            {
                string sqlHoanTra = @"UPDATE DanhMucHang 
                             SET SoLuong = SoLuong + @soLuongHoanTra 
                             WHERE MaHang = @maHang";
                SqlCommand cmdHoanTra = new SqlCommand(sqlHoanTra, conn, transaction);
                cmdHoanTra.Parameters.AddWithValue("@soLuongHoanTra", row["SoLuong"]);
                cmdHoanTra.Parameters.AddWithValue("@maHang", row["MaHang"]);
                cmdHoanTra.ExecuteNonQuery();
            }

            // Xóa 
            string sqlDeleteChiTiet = "DELETE FROM ChiTietDonDatHang WHERE SoDDH = @soDDH";
            SqlCommand cmdDeleteChiTiet = new SqlCommand(sqlDeleteChiTiet, conn, transaction);
            cmdDeleteChiTiet.Parameters.AddWithValue("@soDDH", txtMa.Text);
            cmdDeleteChiTiet.ExecuteNonQuery();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // ktra dgv
            if (tblChiTietDonHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // khởi tạo excel
            Excel.Application exApp = new Excel.Application();
            Excel.Workbook exBook = null;
            Excel.Worksheet exSheet = null;

            try
            {
                exBook = exApp.Workbooks.Add(Missing.Value);
                exSheet = (Excel.Worksheet)exBook.Worksheets.get_Item(1);
                exSheet.Name = "Hóa Đơn Bán Hàng";

                // Định dạng tiêu đề
                exSheet.Range["A1:F1"].Merge(true); // Gộp ô
                exSheet.Range["A1"].Value = "HÓA ĐƠN BÁN Ô TÔ";
                exSheet.Range["A1"].Font.Bold = true;
                exSheet.Range["A1"].Font.Size = 16;
                exSheet.Range["A1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Thông tin hóa đơn
                exSheet.Range["A3"].Value = "Mã HĐ:";
                exSheet.Range["B3"].Value = txtMa.Text; // Mã HĐ
                exSheet.Range["A4"].Value = "Ngày đặt:";
                exSheet.Range["B4"].Value = dtpNgayDat.Value.ToString("dd/MM/yyyy");
                exSheet.Range["D4"].Value = "Ngày giao:";
                exSheet.Range["E4"].Value = dtpNgayGiao.Value.ToString("dd/MM/yyyy");

                // Thông tin khách hàng
                exSheet.Range["A6"].Value = "Khách hàng:";
                exSheet.Range["B6"].Value = txtTenKH.Text;
                exSheet.Range["A7"].Value = "Địa chỉ:";
                exSheet.Range["B7"].Value = txtDiaChi.Text;
                exSheet.Range["A8"].Value = "Điện thoại:";
                exSheet.Range["B8"].Value = txtDienThoai.Text;

                // Thông tin Nhân viên bán
                exSheet.Range["D6"].Value = "Nhân viên:";
                exSheet.Range["E6"].Value = txtTenNV.Text;

                // Phần chi tiết
                exSheet.Range["A10"].Value = "STT";
                exSheet.Range["B10"].Value = "Tên hàng";
                exSheet.Range["C10"].Value = "Số lượng";
                exSheet.Range["D10"].Value = "Đơn giá";
                exSheet.Range["E10"].Value = "Giảm giá";
                exSheet.Range["F10"].Value = "Thành tiền";

                // Định dạng tiêu đề cột
                exSheet.Range["A10:F10"].Font.Bold = true;
                exSheet.Range["A10:F10"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Ghi dữ liệu
                int startRow = 11;
                for (int i = 0; i < tblChiTietDonHang.Rows.Count; i++)
                {
                    DataRow row = tblChiTietDonHang.Rows[i];
                    exSheet.Range["A" + (startRow + i)].Value = i + 1; // Số thứ tự
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

                //thêm Thuế, Đặt cọc 
                exSheet.Range["E" + (totalRow + 1)].Value = "Thuế (%):";
                exSheet.Range["F" + (totalRow + 1)].Value = txtThue.Text;
                exSheet.Range["E" + (totalRow + 2)].Value = "Đặt cọc:";
                exSheet.Range["F" + (totalRow + 2)].Value = txtDatCoc.Text;

                exSheet.Range["E" + (totalRow + 3)].Value = "TỔNG CỘNG:";
                exSheet.Range["F" + (totalRow + 3)].Value = txtTongTien.Text;
                exSheet.Range["E" + (totalRow + 3) + ":F" + (totalRow + 3)].Font.Bold = true;


                // === 7. Định dạng chung ===
                exSheet.Columns["A:F"].AutoFit(); // Tự động căn chỉnh độ rộng cột
                                                  // Kẻ viền (optional)
                exSheet.Range["A10:F" + lastDataRow].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // === 8. Hiển thị Excel ===
                exApp.Visible = true; // Mở file Excel lên cho người dùng xem
                                      // (Hoặc bạn có thể lưu file tự động: exBook.SaveAs("D:\\HoaDon_" + txtMa.Text + ".xlsx");)

                string sqlUpdateStatus = "UPDATE DonDatHang SET TrangThai = 1 WHERE SoDDH = @ma";
                dt.ExecuteNonQuery(sqlUpdateStatus, new SqlParameter[] { new SqlParameter("@ma", txtMa.Text) });

                MessageBox.Show("Đã xuất hóa đơn ra Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset form sau khi in
                //reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //
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
                // btnHuy.Enabled = true;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //nếu có So DDH thì fill lên form
            string soDDHCanTim = cboTimKiem.SelectedValue.ToString();

            resetGrbTTC();
            resetGrbCTMH();
            resetDGV();

            try
            {
                // nạp tt lên grb
                string sqlDonHang = @"
                                    SELECT dh.*, nv.TenNV, kh.TenKhach, kh.DiaChi, kh.DienThoai
                                    FROM DonDatHang dh
                                    JOIN NhanVien nv ON dh.MaNV = nv.MaNV
                                    JOIN KhachHang kh ON dh.MaKhach = kh.MaKhach
                                    WHERE dh.SoDDH = @ma";

                DataTable tblDonHang = dt.ExecuteQuery(sqlDonHang, new SqlParameter[] { new SqlParameter("@ma", soDDHCanTim) });

                int trangThai = 0;

                if (tblDonHang.Rows.Count > 0)
                {
                    DataRow rowDH = tblDonHang.Rows[0];

                    txtMa.Text = rowDH["SoDDH"].ToString();
                    dtpNgayDat.Value = (DateTime)rowDH["NgayDat"];
                    dtpNgayGiao.Value = (DateTime)rowDH["NgayGiao"];
                    cboMaNV.Text = rowDH["TenNV"].ToString();
                    txtTenNV.Text = rowDH["TenNV"].ToString();
                    txtDatCoc.Text = rowDH["DatCoc"].ToString();
                    txtThue.Text = rowDH["Thue"].ToString();
                    cboMaKH.SelectedValue = rowDH["MaKhach"];
                    txtTenKH.Text = rowDH["TenKhach"].ToString();
                    txtDiaChi.Text = rowDH["DiaChi"].ToString();
                    txtDienThoai.Text = rowDH["DienThoai"].ToString();

                    // Hiển thị tổng tiền từ CSDL
                    txtTongTien.Text = string.Format("{0:N2} VNĐ", (decimal)rowDH["TongTien"]);
                    trangThai = (int)rowDH["TrangThai"];
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reset();
                    return;
                }

                // ghi chi tiết lên giỏ hàng
                string sqlChiTiet = @"
                                    SELECT ct.MaHang, dm.TenHang, ct.SoLuong, dm.DonGiaBan AS DonGia, ct.GiamGia, ct.ThanhTien
                                    FROM ChiTietDonDatHang AS ct
                                    JOIN DanhMucHang AS dm ON ct.MaHang = dm.MaHang
                                    WHERE ct.SoDDH = @ma";

                DataTable tblChiTiet = dt.ExecuteQuery(sqlChiTiet, new SqlParameter[] { new SqlParameter("@ma", soDDHCanTim) });

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
                    cboTimKiem.Enabled = true;
                    //btnHuy.Enabled = true;
                    cboTimKiem.Enabled = true;
                }
                else if (trangThai == 1)
                {
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnBoQua.Enabled = true;
                    btnIn.Enabled = true;
                    cboTimKiem.Enabled = true;
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
                cboTimKiem.Enabled = true;
                //btnHuy.Enabled = true;

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

        //truy vấn kh bằng sdt
        private void txtDienThoai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                KiemTraKhachHangTheoSDT();
            }
        }

        private void KiemTraKhachHangTheoSDT()
        {
            string sdt = txtDienThoai.Text.Trim();
            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "SELECT MaKhach, TenKhach, DiaChi, DienThoai FROM KhachHang WHERE DienThoai = @dt";
            var tbl = dt.ExecuteQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@dt", sdt)
            });
            if (tbl.Rows.Count > 0)
            {
                DataRow row = tbl.Rows[0];
                cboMaKH.Text = row["MaKhach"].ToString();
                txtTenKH.Text = row["TenKhach"].ToString();
                txtDiaChi.Text = row["DiaChi"].ToString();

                MessageBox.Show("Đã tìm thấy thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    "Số điện thoại này chưa có trong hệ thống. Bạn có muốn thêm khách hàng mới không?",
                    "Khách hàng mới",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    frmKhachHang f = new frmKhachHang();
                    f.IsFromBanHang = true;
                    f.DT_BanHang = sdt;
                    f.ShowDialog();
                }
            }
        }
    }
}
