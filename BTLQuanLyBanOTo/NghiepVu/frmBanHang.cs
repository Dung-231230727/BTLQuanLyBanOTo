﻿using BTLQuanLyBanOTo.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
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

            // 4. (Tùy chọn) Định dạng cột
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
            // 1. Nạp ComboBox Nhân viên
            //LoadCBO(cboMaNV, "SELECT MaNV, TenNV FROM NhanVien", "MaNV", "MaNV");

            // 2. Nạp ComboBox Khách hàng
            LoadCBO(cboMaKH, "SELECT MaKhach, TenKhach FROM KhachHang", "MaKhach", "MaKhach");

            // 3. Nạp ComboBox Sản phẩm (Xe)
            LoadCBO(cboMaSP, "SELECT MaHang, TenHang FROM DanhMucHang WHERE SoLuong > 0", "MaHang", "MaHang");

            LoadCBOTimKiem();
        }

        public void LoadCBOTimKiem()
        {
            // 4. Nạp ComboBox Tìm kiếm Đơn hàng
            string sqlTim = "SELECT SoDDH FROM DonDatHang";
            DataTable tblTim = dt.ExecuteQuery(sqlTim);
            cboTimKiem.DataSource = tblTim;
            cboTimKiem.DisplayMember = "SoDDH";
            cboTimKiem.ValueMember = "SoDDH";
            cboTimKiem.SelectedIndex = -1;
        }

        public void resetGrbTTC()
        {
            txtMa.Text = "";
            dtpNgayDat.Value = DateTime.Now;
            dtpNgayGiao.Value = DateTime.Now;
            txtThue.Text = "0";

            // Lấy thông tin từ frmMain và gán thẳng
            cboMaNV.DataSource = null;
            cboMaNV.Items.Clear();
            cboMaNV.Items.Add(frmMain.TenNV_DangNhap);
            cboMaNV.SelectedIndex = 0;
            cboMaNV.Enabled = false;

            txtTenNV.Text = frmMain.TenNV_DangNhap;
            txtTenNV.Enabled = false;

            cboMaKH.Text = "";
            cboMaKH.SelectedIndex = -1;
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
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
            btnTimKiem.Enabled = false;
            btnHuy.Enabled = false;
            btnThemSP.Enabled = false;
            btnXoaSP.Enabled = false;
            btnLuu.Enabled = false;
            btnIn.Enabled = false;

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
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            reset();
            txtMa.Text = TaoMaDonHang();
            btnThemSP.Enabled = true;
            btnLuu.Enabled = true;
            btnIn.Enabled = true;
        }

        /// <summary>
        /// Sinh mã Đơn đặt hàng tự động theo quy tắc HĐB_ddMMyyyyXXXX
        /// Ví dụ: HĐB_261020250001
        /// </summary>
        /// <returns>Mã đơn hàng mới</returns>
        private string TaoMaDonHang()
        {
            // 1. Lấy ngày hiện tại và tạo prefix
            string ngayHienTai = DateTime.Now.ToString("ddMMyyyy");
            string prefix = "HĐB_" + ngayHienTai; // Ví dụ: HĐB_26102025

            // 2. Tìm mã lớn nhất TRONG NGÀY HÔM NAY
            // Câu lệnh này tìm tất cả các mã bắt đầu bằng "HĐB_26102025" và lấy cái lớn nhất
            string sql = "SELECT MAX(SoDDH) FROM DonDatHang WHERE SoDDH LIKE @prefix";
            SqlParameter[] prms = new SqlParameter[] {
                new SqlParameter("@prefix", prefix + "%")
            };

            object result = dt.ExecuteScalar(sql, prms);

            int soThuTuMoi = 1; // Mặc định là hóa đơn đầu tiên trong ngày (0001)

            if (result != null && result != DBNull.Value)
            {
                // Nếu đã có hóa đơn, lấy 4 số cuối (ví dụ: "0001")
                string maLonNhat = result.ToString();
                string soCuoi = maLonNhat.Substring(prefix.Length); // Lấy phần "0001"

                // Chuyển "0001" thành số (1), rồi cộng 1
                soThuTuMoi = int.Parse(soCuoi) + 1;
            }

            // 3. Định dạng mã mới
            // Nối "HĐB_26102025" với "0002" (định dạng 4 chữ số "D4")
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
                txtDG.Text = tblSP.Rows[0]["DonGiaBan"].ToString();
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
            txtTT.Text = thanhTien.ToString("N0");
        }

        public void TinhTongTien()
        {
            decimal tongTienHang = 0;

            foreach (DataRow row in tblChiTietDonHang.Rows)
            {
                tongTienHang += (decimal)row["ThanhTien"];
            }

            // 2. Lấy Thuế và Đặt cọc
            decimal thue = 0;
            decimal datCoc = 0;
            decimal.TryParse(txtThue.Text, out thue);
            decimal.TryParse(txtDatCoc.Text, out datCoc);

            // 3. Tính toán tổng cuối cùng
            decimal tongThue = (tongTienHang * thue) / 100;
            decimal tongCong = (tongTienHang + tongThue) - datCoc;

            txtTongTien.Text = tongCong.ToString("N0");
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
            // Kiểm tra xem đã chọn sản phẩm chưa
            if (cboMaSP.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số lượng bán phải lớn hơn 0
            if (numSLB.Value <= 0)
            {
                MessageBox.Show("Số lượng bán phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSLB.Focus();
                return;
            }

            // === 2. Yêu cầu: Kiểm tra tồn kho ===
            int soLuongTon = 0;
            int.TryParse(txtSLT.Text, out soLuongTon);

            if (numSLB.Value > soLuongTon)
            {
                MessageBox.Show("Số lượng bán vượt quá số lượng tồn kho! (Tồn: " + soLuongTon + ")", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSLB.Focus();
                return;
            }

            // === 3. Kiểm tra xem sản phẩm đã có trong giỏ chưa ===
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
            if (e.RowIndex >= 0)
            {
                btnXoaSP.Enabled = true;

                var rows = dgvGioHang.Rows[e.RowIndex];
                cboMaSP.Text = rows.Cells["MaHang"].Value.ToString();
                numSLB.Value = (int)rows.Cells["SoLuong"].Value;
                txtGG.Text = rows.Cells["GiamGia"].Value.ToString();
            }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            string tenSP_dangChon = dgvGioHang.CurrentRow.Cells["TenHang"].Value.ToString();

            // 3. Hiển thị thông báo xác nhận
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
                    // Lấy index của dòng đang chọn
                    int rowIndex = dgvGioHang.CurrentRow.Index;

                    // Xóa dòng đó khỏi DataTable tạm (giỏ hàng)
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
            //ktra trống
            if (cboMaKH.SelectedIndex == -1 || cboMaNV.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Nhân viên và Khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //lọc trùng

            //ktra du lieu trong dgv
            if (tblChiTietDonHang.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn phải có ít nhất một sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //lưu vào db (dùng transaction)
            using (SqlConnection conn = new SqlConnection("Data Source=DUNG;Initial Catalog=QuanLyCuaHangOto;Integrated Security=True"))
            {
                conn.Open();
                // Bắt đầu giao dịch
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // --- BƯỚC 3A: LƯU VÀO BẢNG `DonDatHang` (Hóa đơn chính) ---

                    // Tính toán tổng tiền cuối cùng (từ Label)
                    decimal tongTien = 0;
                    decimal.TryParse(lblTongTien.Text.Replace(",", ""), out tongTien); // Xóa dấu phẩy

                    string sqlDonHang = @"INSERT INTO DonDatHang(SoDDH, MaNV, MaKhach, NgayDat, NgayGiao, DatCoc, Thue, TongTien)
                                          VALUES(@soDDH, @maNV, @maKhach, @ngayDat, @ngayGiao, @datCoc, @thue, @tongTien)";

                    SqlCommand cmdDonHang = new SqlCommand(sqlDonHang, conn, transaction);
                    cmdDonHang.Parameters.AddWithValue("@soDDH", txtMa.Text);
                    cmdDonHang.Parameters.AddWithValue("@maNV", frmMain.MaNV_DangNhap);
                    cmdDonHang.Parameters.AddWithValue("@maKhach", cboMaKH.SelectedValue);
                    cmdDonHang.Parameters.AddWithValue("@ngayDat", dtpNgayDat.Value);
                    cmdDonHang.Parameters.AddWithValue("@ngayGiao", dtpNgayGiao.Value);
                    cmdDonHang.Parameters.AddWithValue("@datCoc", decimal.Parse(txtDatCoc.Text));
                    cmdDonHang.Parameters.AddWithValue("@thue", float.Parse(txtThue.Text));
                    cmdDonHang.Parameters.AddWithValue("@tongTien", tongTien);

                    cmdDonHang.ExecuteNonQuery();

                    // --- BƯỚC 3B & 3C: LẶP QUA GIỎ HÀNG (DataGridView) ---
                    foreach (DataRow row in tblChiTietDonHang.Rows)
                    {
                        // --- 3B: LƯU VÀO BẢNG `ChiTietDonDatHang` ---
                        string sqlChiTiet = @"INSERT INTO ChiTietDonDatHang(SoDDH, MaHang, SoLuong, GiamGia, ThanhTien)
                                              VALUES(@soDDH, @maHang, @soLuong, @giamGia, @thanhTien)";

                        SqlCommand cmdChiTiet = new SqlCommand(sqlChiTiet, conn, transaction);
                        cmdChiTiet.Parameters.AddWithValue("@soDDH", txtMa.Text);
                        cmdChiTiet.Parameters.AddWithValue("@maHang", row["MaHang"]);
                        cmdChiTiet.Parameters.AddWithValue("@soLuong", row["SoLuong"]);
                        cmdChiTiet.Parameters.AddWithValue("@giamGia", row["GiamGia"]);
                        cmdChiTiet.Parameters.AddWithValue("@thanhTien", row["ThanhTien"]);

                        cmdChiTiet.ExecuteNonQuery();

                        // --- 3C: CẬP NHẬT TỒN KHO (Bảng `DanhMucHang`) ---
                        string sqlCapNhatKho = @"UPDATE DanhMucHang 
                                                SET SoLuong = SoLuong - @soLuongBan 
                                                WHERE MaHang = @maHang";

                        SqlCommand cmdCapNhatKho = new SqlCommand(sqlCapNhatKho, conn, transaction);
                        cmdCapNhatKho.Parameters.AddWithValue("@soLuongBan", row["SoLuong"]);
                        cmdCapNhatKho.Parameters.AddWithValue("@maHang", row["MaHang"]);

                        cmdCapNhatKho.ExecuteNonQuery();
                    }

                    // --- 4. KẾT THÚC GIAO DỊCH (THÀNH CÔNG) ---
                    transaction.Commit();
                    MessageBox.Show("Lưu hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // --- 5. HỦY GIAO DỊCH (THẤT BẠI) ---
                    transaction.Rollback();
                    MessageBox.Show("Lưu hóa đơn thất bại! Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Tải lại CBO Sản phẩm và tìm kiếm
            LoadCBO(cboMaSP, "SELECT MaHang, TenHang FROM DanhMucHang WHERE SoLuong > 0", "TenHang", "MaHang");
            LoadCBOTimKiem();
            resetGrbCTMH();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // === 1. Yêu cầu: Kiểm tra dữ liệu trong DGV ===
            if (tblChiTietDonHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // === 2. Khởi tạo Excel ===
            Excel.Application exApp = new Excel.Application();
            Excel.Workbook exBook = null;
            Excel.Worksheet exSheet = null;

            try
            {
                exBook = exApp.Workbooks.Add(Missing.Value);
                exSheet = (Excel.Worksheet)exBook.Worksheets.get_Item(1);
                exSheet.Name = "Hóa Đơn Bán Hàng";

                // === 3. Thêm Tiêu đề và Thông tin chung ===
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

                // === 4. Thêm Tiêu đề cột cho Chi tiết sản phẩm ===
                exSheet.Range["A10"].Value = "STT";
                exSheet.Range["B10"].Value = "Tên hàng";
                exSheet.Range["C10"].Value = "Số lượng";
                exSheet.Range["D10"].Value = "Đơn giá";
                exSheet.Range["E10"].Value = "Giảm giá";
                exSheet.Range["F10"].Value = "Thành tiền";

                // Định dạng tiêu đề cột
                exSheet.Range["A10:F10"].Font.Bold = true;
                exSheet.Range["A10:F10"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // === 5. Thêm Dữ liệu chi tiết từ Giỏ hàng (tblChiTietDonHang) ===
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

                // === 6. Thêm Tổng tiền ===
                int lastDataRow = startRow + tblChiTietDonHang.Rows.Count - 1;
                int totalRow = lastDataRow + 2; // Cách ra 1 dòng

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
                // Giải phóng tài nguyên COM (rất quan trọng)
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
                btnHuy.Enabled = true;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //nếu có So DDH thì fill lên form
            string soDDHCanTim = cboTimKiem.SelectedValue.ToString();

            // === 2. Xóa thông tin cũ trên form ===
            // Reset các khu vực nhập liệu và giỏ hàng
            resetGrbTTC();
            resetGrbCTMH();
            resetDGV();

            try
            {
                // === 3. Nạp thông tin chung (DonDatHang) ===
                string sqlDonHang = @"
                                    SELECT dh.*, nv.TenNV, kh.TenKhach, kh.DiaChi, kh.DienThoai
                                    FROM DonDatHang dh
                                    JOIN NhanVien nv ON dh.MaNV = nv.MaNV
                                    JOIN KhachHang kh ON dh.MaKhach = kh.MaKhach
                                    WHERE dh.SoDDH = @ma";

                DataTable tblDonHang = dt.ExecuteQuery(sqlDonHang, new SqlParameter[] { new SqlParameter("@ma", soDDHCanTim) });

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
                    txtTongTien.Text = string.Format("{0:N0}", (decimal)rowDH["TongTien"]);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reset();
                    return;
                }

                // === 4. Nạp chi tiết đơn hàng (ChiTietDonDatHang) vào giỏ hàng ===
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

                // === 5. Cập nhật trạng thái nút bấm ===
                btnThem.Enabled = true;     // Cho phép tạo hóa đơn mới
                btnTimKiem.Enabled = true;  // Vẫn cho tìm hóa đơn khác
                cboTimKiem.Enabled = true;

                btnLuu.Enabled = false;     // Không cho Lưu lại hóa đơn cũ
                btnThemSP.Enabled = false;  // Không cho Thêm SP vào HĐ cũ
                btnXoaSP.Enabled = false;   // Không cho Xóa SP khỏi HĐ cũ
                btnIn.Enabled = true;       // Cho phép In hóa đơn cũ
                btnHuy.Enabled = true;      // Cho phép Hủy (Reset form)

                // Khóa các control nhập liệu chính
                cboMaNV.Enabled = false;
                cboMaKH.Enabled = false;
                txtDatCoc.Enabled = false;
                txtThue.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reset(); // Reset về trạng thái ban đầu nếu có lỗi
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            reset();
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
    }
}
