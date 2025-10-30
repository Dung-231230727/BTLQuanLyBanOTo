using BTLQuanLyBanOTo.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.DanhMuc
{
    public partial class frmSanPham : Form
    {
        public frmSanPham()
        {
            InitializeComponent();
        }
        DataProcesser dt;
        private void frmSanPham_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();

            LoadDGV();
            LoadAllCBO();
            reset();
        }

        public void LoadDGV()
        {
            string sql = @"
                            SELECT MaHang, TenHang, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, Anh,
                                   h.TenHangSX, tl.TenLoai, ms.TenMau, dx.TenDoi, 
                                   scn.TenSoCho, ns.TenNuocSX, tt.TenTinhTrang,       
                                   sp.MaHangSX, sp.MaLoai, sp.MaMau, sp.MaDoi, 
                                   sp.MaSoCho, sp.MaNuocSX, sp.MaTinhTrang
                            FROM DanhMucHang AS sp
                            LEFT JOIN HangSX AS h ON sp.MaHangSX = h.MaHangSX
                            LEFT JOIN TheLoai AS tl ON sp.MaLoai = tl.MaLoai
                            LEFT JOIN MauSac AS ms ON sp.MaMau = ms.MaMau
                            LEFT JOIN DoiXe AS dx ON sp.MaDoi = dx.MaDoi
                            LEFT JOIN SoChoNgoi AS scn ON sp.MaSoCho = scn.MaSoCho
                            LEFT JOIN NuocSX AS ns ON sp.MaNuocSX = ns.MaNuocSX
                            LEFT JOIN TinhTrang AS tt ON sp.MaTinhTrang = tt.MaTinhTrang";
            dgvSanPham.DataSource = dt.ExecuteQuery(sql);
            // Ẩn các cột Mã không cần thiết
            dgvSanPham.Columns["MaHangSX"].Visible = false;
            dgvSanPham.Columns["MaLoai"].Visible = false;
            dgvSanPham.Columns["MaMau"].Visible = false;
            dgvSanPham.Columns["MaDoi"].Visible = false;
            dgvSanPham.Columns["MaSoCho"].Visible = false;
            dgvSanPham.Columns["MaNuocSX"].Visible = false;
            dgvSanPham.Columns["MaTinhTrang"].Visible = false;
        }

        public void LoadCBO(ComboBox cbo, string sql, string displayMember, string valueMember)
        {
            DataTable tbl = dt.ExecuteQuery(sql);
            cbo.DataSource = tbl;
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
            cbo.SelectedIndex = -1;
        }

        private void LoadAllCBO()
        {
            LoadCBO(cboHangSX, "SELECT MaHangSX, TenHangSX FROM HangSX", "TenHangSX", "MaHangSX");
            LoadCBO(cboLoaiXe, "SELECT MaLoai, TenLoai FROM TheLoai", "TenLoai", "MaLoai");
            LoadCBO(cboMauSac, "SELECT MaMau, TenMau FROM MauSac", "TenMau", "MaMau");
            LoadCBO(cboDoiXe, "SELECT MaDoi, TenDoi FROM DoiXe", "TenDoi", "MaDoi");
            LoadCBO(cboSoCho, "SELECT MaSoCho, TenSoCho FROM SoChoNgoi", "TenSoCho", "MaSoCho");
            LoadCBO(cboNuocSX, "SELECT MaNuocSX, TenNuocSX FROM NuocSX", "TenNuocSX", "MaNuocSX");
            LoadCBO(cboTinhTrang, "SELECT MaTinhTrang, TenTinhTrang FROM TinhTrang", "TenTinhTrang", "MaTinhTrang");
        }

        public void reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            numSoLuong.Value = 0;
            txtDGN.Text = "0";
            txtDGB.Text = "0";
            txtTGBH.Text = "";

            // Reset tất cả ComboBox
            cboHangSX.SelectedIndex = -1;
            cboLoaiXe.SelectedIndex = -1;
            cboMauSac.SelectedIndex = -1;
            cboDoiXe.SelectedIndex = -1;
            cboSoCho.SelectedIndex = -1;
            cboNuocSX.SelectedIndex = -1;
            cboTinhTrang.SelectedIndex = -1;

            picAnh.Image = null;
            picAnh.Tag = null;

            txtMa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;

            action = "";
            txtMa.Focus();
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            // 1. Mở cửa sổ chọn file
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            ofd.Title = "Chọn ảnh sản phẩm";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 2. Lấy đường dẫn file nguồn
                    string sourceFilePath = ofd.FileName;

                    // 3. Lấy tên file
                    string fileName = Path.GetFileName(sourceFilePath);

                    // 4. Tạo đường dẫn thư mục đích (trong thư mục chạy .exe)
                    // Application.StartupPath là thư mục .../bin/Debug
                    string destFolder = Path.Combine(Application.StartupPath, "Images", "Oto");

                    // 5. Tạo đường dẫn file đích
                    string destFilePath = Path.Combine(destFolder, fileName);

                    // 6. Sao chép file (với 'true' để ghi đè nếu file đã tồn tại)
                    // Kiểm tra để tránh lỗi tự copy chính nó
                    if (sourceFilePath != destFilePath)
                    {
                        File.Copy(sourceFilePath, destFilePath, true);
                    }

                    // 7. Hiển thị ảnh lên PictureBox
                    // Phải tải từ file đích để đảm bảo file đã được copy thành công
                    picAnh.Image = Image.FromFile(destFilePath);

                    // 8. Lưu TÊN FILE vào Tag để sau này dùng cho nút Lưu
                    picAnh.Tag = fileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sao chép hoặc tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string action = "";
        private void btnThem_Click(object sender, EventArgs e)
        {
            reset();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            action = "add";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnDong.Enabled = true;
            action = "edit";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnDong.Enabled = true;
            action = "delete";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //kiểm tra rỗng
            if (string.IsNullOrEmpty(txtMa.Text) ||
                string.IsNullOrEmpty(txtTen.Text) ||
                string.IsNullOrEmpty(txtDGN.Text) ||
                string.IsNullOrEmpty(txtDGB.Text) ||
                cboHangSX.SelectedIndex == -1 ||
                cboLoaiXe.SelectedIndex == -1 ||
                cboMauSac.SelectedIndex == -1 ||
                cboDoiXe.SelectedIndex == -1 ||
                cboSoCho.SelectedIndex == -1 ||
                cboNuocSX.SelectedIndex == -1 ||
                cboTinhTrang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //kiểm tra giá
            decimal dgn, dgb;
            if (!decimal.TryParse(txtDGN.Text, out dgn) || !decimal.TryParse(txtDGB.Text, out dgb))
            {
                MessageBox.Show("Đơn giá nhập và đơn giá bán phải là số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgb < dgn)
            {
                MessageBox.Show("Đơn giá bán phải lớn hơn hoặc bằng đơn giá nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy tên ảnh một cách AN TOÀN
            string tenFileAnh = "";
            if (picAnh.Tag != null)
            {
                tenFileAnh = picAnh.Tag.ToString();
            }

            SqlParameter[] prms = new SqlParameter[]
            {
                new SqlParameter("@ma", txtMa.Text),
                new SqlParameter("@ten", txtTen.Text),
                new SqlParameter("@sl", numSoLuong.Value),
                new SqlParameter("@dgn", dgn),
                new SqlParameter("@dgb", dgb),
                new SqlParameter("@tgbh", txtTGBH.Text),
                new SqlParameter("@anh", tenFileAnh), 
        
                // Lấy SelectedValue một cách AN TOÀN
                new SqlParameter("@ml", cboLoaiXe.SelectedValue ?? DBNull.Value),
                new SqlParameter("@mhsx", cboHangSX.SelectedValue ?? DBNull.Value),
                new SqlParameter("@mm", cboMauSac.SelectedValue ?? DBNull.Value),
                new SqlParameter("@md", cboDoiXe.SelectedValue ?? DBNull.Value),
                new SqlParameter("@msc", cboSoCho.SelectedValue ?? DBNull.Value),
                new SqlParameter("@mnsx", cboNuocSX.SelectedValue ?? DBNull.Value),
                new SqlParameter("@mtt", cboTinhTrang.SelectedValue ?? DBNull.Value)
            };


            if (action == "add")
            {
                //lọc trùng
                string sqlKT = "select count(*) from DanhMucHang where MaHang = @ma";
                int count = (int)dt.ExecuteScalar(sqlKT, new SqlParameter[] { new SqlParameter("@ma", txtMa.Text) });
                if (count > 0)
                {
                    MessageBox.Show("Đã có mã sản phẩm này. Vui lòng nhập mã khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql = "insert into DanhMucHang(MaHang,TenHang,SoLuong,DonGiaNhap,DonGiaBan,ThoiGianBaoHanh,Anh,MaLoai,MaHangSX,MaMau,MaDoi,MaSoCho,MaNuocSX,MaTinhTrang) " +
                             "values(@ma,@ten,@sl,@dgn,@dgb,@tgbh,@anh,@ml,@mhsx,@mm,@md,@msc,@mnsx,@mtt)";

                int r = dt.ExecuteNonQuery(sql, prms); // Truyền mảng tham số đã tạo
                if (r > 0)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (action == "edit")
            {
                string sql = "update DanhMucHang set TenHang=@ten,SoLuong=@sl,DonGiaNhap=@dgn,DonGiaBan=@dgb,ThoiGianBaoHanh=@tgbh,Anh=@anh,MaLoai=@ml,MaHangSX=@mhsx,MaMau=@mm,MaDoi=@md,MaSoCho=@msc,MaNuocSX=@mnsx,MaTinhTrang=@mtt where MaHang=@ma";

                int r = dt.ExecuteNonQuery(sql, prms);
                if (r > 0)
                {
                    MessageBox.Show("Sửa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sửa sản phẩm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (action == "delete")
            {
                string sql = "delete from DanhMucHang where MaHang=@ma";
                int r = dt.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@ma", txtMa.Text) });
                if (r > 0)
                {
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa sản phẩm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            LoadDGV();
            reset();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 2. Thiết lập trạng thái các nút
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = true;

            // Khóa TextBox Mã
            txtMa.Enabled = false;

            // 3. Lấy dòng được chọn
            var row = dgvSanPham.Rows[e.RowIndex];

            // 4. Hàm helper để gán giá trị an toàn (tránh lỗi DBNull)
            // Bạn có thể đặt hàm này bên trong hàm CellClick, hoặc bên ngoài
            Action<Control, object> safeSetValue = (ctrl, value) =>
            {
                if (value == null || value == DBNull.Value)
                {
                    if (ctrl is TextBox) ((TextBox)ctrl).Text = "";
                    if (ctrl is NumericUpDown) ((NumericUpDown)ctrl).Value = 0;
                    if (ctrl is ComboBox) ((ComboBox)ctrl).SelectedValue = -1;
                    // (Thêm cho DateTimePicker nếu cần)
                }
                else
                {
                    if (ctrl is TextBox) ((TextBox)ctrl).Text = value.ToString();
                    if (ctrl is NumericUpDown) ((NumericUpDown)ctrl).Value = Convert.ToDecimal(value);
                    if (ctrl is ComboBox) ((ComboBox)ctrl).SelectedValue = value;
                }
            };

            // 5. Gán giá trị cho các control
            safeSetValue(txtMa, row.Cells["MaHang"].Value);
            safeSetValue(txtTen, row.Cells["TenHang"].Value);
            safeSetValue(numSoLuong, row.Cells["SoLuong"].Value);
            safeSetValue(txtDGN, row.Cells["DonGiaNhap"].Value);
            safeSetValue(txtDGB, row.Cells["DonGiaBan"].Value);
            safeSetValue(txtTGBH, row.Cells["ThoiGianBaoHanh"].Value);

            // 6. Gán giá trị cho 7 ComboBox 
            safeSetValue(cboHangSX, row.Cells["MaHangSX"].Value);
            safeSetValue(cboLoaiXe, row.Cells["MaLoai"].Value);
            safeSetValue(cboMauSac, row.Cells["MaMau"].Value);
            safeSetValue(cboDoiXe, row.Cells["MaDoi"].Value);
            safeSetValue(cboSoCho, row.Cells["MaSoCho"].Value);
            safeSetValue(cboNuocSX, row.Cells["MaNuocSX"].Value);
            safeSetValue(cboTinhTrang, row.Cells["MaTinhTrang"].Value);

            // 7. Hiển thị ảnh
            object anhValue = row.Cells["Anh"].Value;
            if (anhValue != null && anhValue != DBNull.Value)
            {
                string fileName = anhValue.ToString();
                string fullPath = Path.Combine(Application.StartupPath, "Images", "Oto", fileName);

                if (File.Exists(fullPath))
                {
                    picAnh.Image = Image.FromFile(fullPath);
                    picAnh.Tag = fileName; // Lưu tên file vào Tag
                }
                else
                {
                    picAnh.Image = null; // Hoặc ảnh "Not Found"
                    picAnh.Tag = null;
                }
            }
            else
            {
                picAnh.Image = null;
                picAnh.Tag = null;
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
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
