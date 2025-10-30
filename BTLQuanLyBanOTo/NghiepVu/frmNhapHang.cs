using BTLQuanLyBanOTo.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
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
            // 4. Nạp ComboBox Tìm kiếm Đơn hàng
            string sqlTim = "SELECT SoHDN FROM HoaDonNhap order by SoHDN desc";
            DataTable tblTim = dt.ExecuteQuery(sqlTim);
            cboTimKiem.DataSource = tblTim;
            cboTimKiem.DisplayMember = "SoHDN";
            cboTimKiem.ValueMember = "SoHDN";
            cboTimKiem.SelectedIndex = -1;
        }

        public void LoadAllCBO()
        {
            // 1. Nạp ComboBox Nhân viên
            //LoadCBO(cboMaNV, "SELECT MaNV, TenNV FROM NhanVien", "MaNV", "MaNV");

            // 2. Nạp ComboBox NCC
            LoadCBO(cboMaNCC, "SELECT MaNCC, TenNCC FROM NhaCungCap", "MaNCC", "MaNCC");

            // 3. Nạp ComboBox Sản phẩm (Xe)
            LoadCBO(cboMaSP, "SELECT MaHang, TenHang FROM DanhMucHang WHERE SoLuong > 0", "MaHang", "MaHang");

            LoadCBOTimKiem();
        }

        public void resetGrbTTC()
        {
            txtMa.Text = "";
            dtpNgayNhap.Value = DateTime.Now;

            // Lấy thông tin từ frmMain và gán thẳng
            cboMaNV.DataSource = null;
            cboMaNV.Items.Clear();
            cboMaNV.Items.Add(frmMain.TenNV_DangNhap);
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
            btnTimKiem.Enabled = false;
            btnHuy.Enabled = false;
            btnThemSP.Enabled = false;
            btnXoaSP.Enabled = false;
            btnLuu.Enabled = false;
            btnIn.Enabled = false;

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

        /// <summary>
        /// Sinh mã Đơn đặt hàng tự động theo quy tắc HĐB_ddMMyyyyXXXX
        /// Ví dụ: HĐB_261020250001
        /// </summary>
        /// <returns>Mã đơn hàng mới</returns>
        private string TaoMaDonHang()
        {
            // 1. Lấy ngày hiện tại và tạo prefix
            string ngayHienTai = DateTime.Now.ToString("ddMMyyyy");
            string prefix = "HĐN_" + ngayHienTai; // Ví dụ: HĐB_26102025

            // 2. Tìm mã lớn nhất TRONG NGÀY HÔM NAY
            // Câu lệnh này tìm tất cả các mã bắt đầu bằng "HĐB_26102025" và lấy cái lớn nhất
            string sql = "SELECT MAX(SoHDN) FROM HoaDonNhap WHERE SoHDN LIKE @prefix";
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            reset();
            txtMa.Text = TaoMaDonHang();
            btnThemSP.Enabled = true;
            btnLuu.Enabled = true;
            btnIn.Enabled = true;
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
            txtTT.Text = thanhTien.ToString("N2");
        }

        public void TinhTongTien()
        {
            decimal tongTienHang = 0;

            foreach (DataRow row in tblChiTietDonHang.Rows)
            {
                tongTienHang += (decimal)row["ThanhTien"];
            }

            // 3. Tính toán tổng cuối cùng
            decimal tongCong = tongTienHang;

            txtTongTien.Text = tongCong.ToString("N2");
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
            // Kiểm tra xem đã chọn sản phẩm chưa
            if (cboMaSP.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số lượng nhập phải lớn hơn 0
            if (numSLN.Value <= 0)
            {
                MessageBox.Show("Số lượng bán phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSLN.Focus();
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
            if (e.RowIndex >= 0)
            {
                btnXoaSP.Enabled = true;
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
            //kta trống
            if (cboMaNCC.SelectedIndex == -1 || cboMaNV.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

                    // Tính toán tổng tiền cuối cùng
                    decimal tongTien = 0;
                    decimal.TryParse(txtTongTien.Text.Replace(",", ""), out tongTien);

                    string sqlDonHang = @"INSERT INTO HoaDonNhap(SoHDN, MaNV, MaNCC, NgayNhap,TongTien)
                                          VALUES(@soHDN, @maNV, @maNCC, @ngayNhap,@tongTien)";

                    SqlCommand cmdDonHang = new SqlCommand(sqlDonHang, conn, transaction);
                    cmdDonHang.Parameters.AddWithValue("@soHDN", txtMa.Text);
                    cmdDonHang.Parameters.AddWithValue("@maNV", frmMain.MaNV_DangNhap);
                    cmdDonHang.Parameters.AddWithValue("@maNCC", cboMaNCC.SelectedValue);
                    cmdDonHang.Parameters.AddWithValue("@ngayNhap", dtpNgayNhap.Value);
                    cmdDonHang.Parameters.AddWithValue("@tongTien", tongTien);

                    cmdDonHang.ExecuteNonQuery();

                    // --- BƯỚC 3B & 3C: LẶP QUA GIỎ HÀNG (DataGridView) ---
                    foreach (DataRow row in tblChiTietDonHang.Rows)
                    {
                        // --- 3B: LƯU VÀO BẢNG `ChiTietHoaDonNhap` ---
                        string sqlChiTiet = @"INSERT INTO ChiTietHoaDonNhap(SoHDN, MaHang, SoLuong, DonGia, GiamGia, ThanhTien)
                                              VALUES(@soDDH, @maHang, @soLuong,@donGia, @giamGia, @thanhTien)";

                        SqlCommand cmdChiTiet = new SqlCommand(sqlChiTiet, conn, transaction);
                        cmdChiTiet.Parameters.AddWithValue("@soDDH", txtMa.Text);
                        cmdChiTiet.Parameters.AddWithValue("@maHang", row["MaHang"]);
                        cmdChiTiet.Parameters.AddWithValue("@soLuong", row["SoLuong"]);
                        cmdChiTiet.Parameters.AddWithValue("@donGia", row["DonGia"]);
                        cmdChiTiet.Parameters.AddWithValue("@giamGia", row["GiamGia"]);
                        cmdChiTiet.Parameters.AddWithValue("@thanhTien", row["ThanhTien"]);

                        cmdChiTiet.ExecuteNonQuery();

                        // --- 3C: CẬP NHẬT TỒN KHO (Bảng `DanhMucHang`) ---
                        string sqlCapNhatKho = @"UPDATE DanhMucHang 
                                                SET SoLuong = SoLuong + @soLuongNhap 
                                                WHERE MaHang = @maHang";

                        SqlCommand cmdCapNhatKho = new SqlCommand(sqlCapNhatKho, conn, transaction);
                        cmdCapNhatKho.Parameters.AddWithValue("@soLuongNhap", row["SoLuong"]);
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
                exSheet.Name = "Hóa Đơn Nhập Hàng";

                // === 3. Thêm Tiêu đề và Thông tin chung ===
                // Định dạng tiêu đề
                exSheet.Range["A1:F1"].Merge(true); // Gộp ô
                exSheet.Range["A1"].Value = "HÓA ĐƠN NHẬP Ô TÔ";
                exSheet.Range["A1"].Font.Bold = true;
                exSheet.Range["A1"].Font.Size = 16;
                exSheet.Range["A1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Thông tin hóa đơn
                exSheet.Range["A3"].Value = "Mã HĐ:";
                exSheet.Range["B3"].Value = txtMa.Text; // Mã HĐ
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


        private string action = "";
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //nếu có So HDN thì fill lên form
            string soHDNCanTim = cboTimKiem.SelectedValue.ToString();

            // === 2. Xóa thông tin cũ trên form ===
            // Reset các khu vực nhập liệu và giỏ hàng
            resetGrbTTC();
            resetGrbCTMH();
            resetDGV();

            try
            {
                // === 3. Nạp thông tin chung (HoaDonNhap) ===
                string sqlDonHang = @"
                                    SELECT dh.*, nv.TenNV, ncc.TenNCC, ncc.DiaChi, ncc.DienThoai
                                    FROM HoaDonNhap dh
                                    JOIN NhanVien nv ON dh.MaNV = nv.MaNV
                                    JOIN NhaCungCap ncc ON ncc.MaNCC = dh.MaNCC
                                    WHERE dh.SoHDN = @ma";

                DataTable tblDonHang = dt.ExecuteQuery(sqlDonHang, new SqlParameter[] { new SqlParameter("@ma", soHDNCanTim) });

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
                cboMaNCC.Enabled = false;

                action = "search";
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
