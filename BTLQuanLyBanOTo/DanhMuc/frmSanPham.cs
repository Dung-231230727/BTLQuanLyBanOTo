using BTLQuanLyBanOTo.Classes;
using System;
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

        private DataProcesser dt;
        private string action = "";
        private int TrangThai_HienTai = 1;
        private void frmSanPham_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();
            reset();
            LoadDGV();
            LoadAllCBO();
        }

        private void reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            numSoLuong.ReadOnly = false;
            numSoLuong.Value = 0;
            txtDGN.Text = "0";
            txtDGB.Text = "0";
            txtTGBH.Text = "";

            cboHangSX.SelectedIndex = -1;
            cboLoaiXe.SelectedIndex = -1;
            cboMauSac.SelectedIndex = -1;
            cboDoiXe.SelectedIndex = -1;
            cboSoCho.SelectedIndex = -1;
            cboNuocSX.SelectedIndex = -1;
            cboTinhTrang.SelectedIndex = -1;

            picAnh.Image = null;
            picAnh.Tag = null;

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;

            radHD.Checked = false;
            radKHD.Checked = false;
            TrangThai_HienTai = 1;

            action = "";
            txtMa.Focus();
        }

        private void LoadDGV()
        {
            try
            {
                string sql = @"
                    SELECT MaHang, TenHang, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, Anh,
                           h.TenHangSX, tl.TenLoai, ms.TenMau, dx.TenDoi,
                           scn.TenSoCho, ns.TenNuocSX, tt.TenTinhTrang,
                           sp.MaHangSX, sp.MaLoai, sp.MaMau, sp.MaDoi,
                           sp.MaSoCho, sp.MaNuocSX, sp.MaTinhTrang, sp.TrangThai
                    FROM DanhMucHang AS sp
                    LEFT JOIN HangSX AS h ON sp.MaHangSX = h.MaHangSX
                    LEFT JOIN TheLoai AS tl ON sp.MaLoai = tl.MaLoai
                    LEFT JOIN MauSac AS ms ON sp.MaMau = ms.MaMau
                    LEFT JOIN DoiXe AS dx ON sp.MaDoi = dx.MaDoi
                    LEFT JOIN SoChoNgoi AS scn ON sp.MaSoCho = scn.MaSoCho
                    LEFT JOIN NuocSX AS ns ON sp.MaNuocSX = ns.MaNuocSX
                    LEFT JOIN TinhTrang AS tt ON sp.MaTinhTrang = tt.MaTinhTrang";

                dgvSanPham.DataSource = dt.ExecuteQuery(sql);

                // Ẩn các mã liên kết
                dgvSanPham.Columns["MaHangSX"].Visible = false;
                dgvSanPham.Columns["MaLoai"].Visible = false;
                dgvSanPham.Columns["MaMau"].Visible = false;
                dgvSanPham.Columns["MaDoi"].Visible = false;
                dgvSanPham.Columns["MaSoCho"].Visible = false;
                dgvSanPham.Columns["MaNuocSX"].Visible = false;
                dgvSanPham.Columns["MaTinhTrang"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCBO(ComboBox cbo, string sql, string display, string value)
        {
            try
            {
                cbo.DataSource = dt.ExecuteQuery(sql);
                cbo.DisplayMember = display;
                cbo.ValueMember = value;
                cbo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllCBO()
        {
            LoadCBO(cboHangSX, "SELECT MaHangSX, TenHangSX FROM HangSX where TrangThai = 1", "TenHangSX", "MaHangSX");
            LoadCBO(cboLoaiXe, "SELECT MaLoai, TenLoai FROM TheLoai where TrangThai = 1", "TenLoai", "MaLoai");
            LoadCBO(cboMauSac, "SELECT MaMau, TenMau FROM MauSac where TrangThai = 1", "TenMau", "MaMau");
            LoadCBO(cboDoiXe, "SELECT MaDoi, TenDoi FROM DoiXe where TrangThai = 1", "TenDoi", "MaDoi");
            LoadCBO(cboSoCho, "SELECT MaSoCho, TenSoCho FROM SoChoNgoi where TrangThai = 1", "TenSoCho", "MaSoCho");
            LoadCBO(cboNuocSX, "SELECT MaNuocSX, TenNuocSX FROM NuocSX where TrangThai = 1", "TenNuocSX", "MaNuocSX");
            LoadCBO(cboTinhTrang, "SELECT MaTinhTrang, TenTinhTrang FROM TinhTrang where TrangThai = 1", "TenTinhTrang", "MaTinhTrang");
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Ảnh (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                Title = "Chọn ảnh sản phẩm"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string src = ofd.FileName;
                    string fileName = Path.GetFileName(src);
                    string folder = Path.Combine(Application.StartupPath, "Images", "Oto");

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string dest = Path.Combine(folder, fileName);
                    if (src != dest) File.Copy(src, dest, true);

                    using (var temp = Image.FromFile(dest))
                    {
                        picAnh.Image = new Bitmap(temp);
                    }

                    picAnh.Tag = fileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xử lý ảnh:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string TaoMaTuDong()
        {
            string prefix = "SP";

            string sql = "SELECT MAX(MaHang) FROM DanhMucHang WHERE MaHang LIKE @prefix";
            SqlParameter[] prms = new SqlParameter[] {
                new SqlParameter("@prefix", prefix + "%")
            };

            object result = dt.ExecuteScalar(sql, prms);
            int soThuTuMoi = 1;
            int soChuSo = 4;

            if (result != null && result != DBNull.Value)
            {
                string maLonNhat = result.ToString();

                if (maLonNhat.StartsWith(prefix))
                {
                    string soCuoi = maLonNhat.Substring(prefix.Length);

                    if (int.TryParse(soCuoi, out int soHienTai))
                    {
                        soThuTuMoi = soHienTai + 1;
                    }
                }
            }

            string maMoi = prefix + soThuTuMoi.ToString("D" + soChuSo);

            return maMoi;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            reset();
            txtMa.Text = TaoMaTuDong();
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            numSoLuong.ReadOnly = true;
            action = "add";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần sửa!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            action = "edit";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần xóa!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (TrangThai_HienTai == 1)
            {
                // Trạng thái = 1: Đang hoạt động, KHÔNG CHO PHÉP XÓA VĨNH VIỄN
                MessageBox.Show("Sản phẩm đang được sử dụng (Trạng thái = 1). Vui lòng chuyển sang trạng thái 0 (Sửa -> Ngừng hoạt động) trước khi xóa vĩnh viễn.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            action = "delete";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu
                if (string.IsNullOrEmpty(txtMa.Text) || string.IsNullOrEmpty(txtTen.Text) ||
                    cboHangSX.SelectedIndex == -1 || cboLoaiXe.SelectedIndex == -1 ||
                    (radHD.Checked == false && radKHD.Checked == false))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtDGN.Text, out decimal dgn) || !decimal.TryParse(txtDGB.Text, out decimal dgb))
                {
                    MessageBox.Show("Đơn giá nhập và bán phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dgb < dgn)
                {
                    MessageBox.Show("Đơn giá bán phải lớn hơn hoặc bằng giá nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string anh = picAnh.Tag?.ToString() ?? "";

                SqlParameter[] prms =
                {
                    new SqlParameter("@ma", txtMa.Text.Trim()),
                    new SqlParameter("@ten", txtTen.Text.Trim()),
                    new SqlParameter("@sl", numSoLuong.Value),
                    new SqlParameter("@dgn", dgn),
                    new SqlParameter("@dgb", dgb),
                    new SqlParameter("@tgbh", txtTGBH.Text.Trim()),
                    new SqlParameter("@anh", anh),
                    new SqlParameter("@ml", cboLoaiXe.SelectedValue),
                    new SqlParameter("@mhsx", cboHangSX.SelectedValue),
                    new SqlParameter("@mm", cboMauSac.SelectedValue),
                    new SqlParameter("@md", cboDoiXe.SelectedValue),
                    new SqlParameter("@msc", cboSoCho.SelectedValue),
                    new SqlParameter("@mnsx", cboNuocSX.SelectedValue),
                    new SqlParameter("@mtt", cboTinhTrang.SelectedValue),
                    new SqlParameter("@tt", radHD.Checked==true? 1:0)
                };

                if (action == "add")
                {
                    int kt = Convert.ToInt32(dt.ExecuteScalar("SELECT COUNT(*) FROM DanhMucHang WHERE MaHang=@ma", new SqlParameter[] {
                        new SqlParameter("@ma", txtMa.Text.Trim())
                    }));
                    if (kt > 0)
                    {
                        MessageBox.Show("Đã tồn tại mã này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sql = "INSERT INTO DanhMucHang(MaHang,TenHang,SoLuong,DonGiaNhap,DonGiaBan,ThoiGianBaoHanh,Anh,MaLoai,MaHangSX,MaMau,MaDoi,MaSoCho,MaNuocSX,MaTinhTrang,TrangThai) VALUES(@ma,@ten,@sl,@dgn,@dgb,@tgbh,@anh,@ml,@mhsx,@mm,@md,@msc,@mnsx,@mtt,@tt)";
                    int r = dt.ExecuteNonQuery(sql, prms);
                    MessageBox.Show(r > 0 ? "Thêm thành công!" : "Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (action == "edit")
                {
                    string sql = "UPDATE DanhMucHang SET TenHang=@ten,SoLuong=@sl,DonGiaNhap=@dgn,DonGiaBan=@dgb,ThoiGianBaoHanh=@tgbh,Anh=@anh,MaLoai=@ml,MaHangSX=@mhsx,MaMau=@mm,MaDoi=@md,MaSoCho=@msc,MaNuocSX=@mnsx,MaTinhTrang=@mtt, TrangThai=@tt WHERE MaHang=@ma";
                    int r = dt.ExecuteNonQuery(sql, prms);
                    MessageBox.Show(r > 0 ? "Sửa thành công!" : "Sửa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (action == "delete")
                {
                    DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.No) return;

                    string sql = "DELETE FROM DanhMucHang WHERE MaHang=@ma";
                    int r = dt.ExecuteNonQuery(sql, new SqlParameter[]{
                        new SqlParameter("@ma", txtMa.Text)
                    });
                    MessageBox.Show(r > 0 ? "Xóa thành công!" : "Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadDGV();
                reset();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show("Không thể xóa vì dữ liệu đang được sử dụng!", "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Lỗi SQL:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                txtMa.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
                btnThem.Enabled = false;
                btnLuu.Enabled = false;
                action = "cell_click";

                var row = dgvSanPham.Rows[e.RowIndex];
                txtMa.Text = row.Cells["MaHang"].Value?.ToString();
                txtTen.Text = row.Cells["TenHang"].Value?.ToString();
                numSoLuong.Value = Convert.ToDecimal(row.Cells["SoLuong"].Value ?? 0);
                txtDGN.Text = row.Cells["DonGiaNhap"].Value?.ToString();
                txtDGB.Text = row.Cells["DonGiaBan"].Value?.ToString();
                txtTGBH.Text = row.Cells["ThoiGianBaoHanh"].Value?.ToString();

                cboHangSX.SelectedValue = row.Cells["MaHangSX"].Value;
                cboLoaiXe.SelectedValue = row.Cells["MaLoai"].Value;
                cboMauSac.SelectedValue = row.Cells["MaMau"].Value;
                cboDoiXe.SelectedValue = row.Cells["MaDoi"].Value;
                cboSoCho.SelectedValue = row.Cells["MaSoCho"].Value;
                cboNuocSX.SelectedValue = row.Cells["MaNuocSX"].Value;
                cboTinhTrang.SelectedValue = row.Cells["MaTinhTrang"].Value;

                string fileName = row.Cells["Anh"].Value?.ToString();
                if (!string.IsNullOrEmpty(fileName))
                {
                    string path = Path.Combine(Application.StartupPath, "Images", "Oto", fileName);
                    if (File.Exists(path))
                    {
                        using (var imgTemp = Image.FromFile(path))
                        {
                            picAnh.Image = new Bitmap(imgTemp);
                        }
                        picAnh.Tag = fileName;
                    }
                    else
                    {
                        picAnh.Image = null;
                        picAnh.Tag = null;
                    }
                }

                var trangThaiValue = row.Cells["TrangThai"].Value;
                TrangThai_HienTai = (trangThaiValue != null && trangThaiValue != DBNull.Value) ? Convert.ToInt32(trangThaiValue) : 1;
                radHD.Checked = (TrangThai_HienTai == 1);
                radKHD.Checked = (TrangThai_HienTai == 0);
            }
            catch { }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            if (action == "edit" || action == "delete")
            {
                btnThem.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
                action = "cell_click";
            }
            else
            {
                reset();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        private void numSoLuong_Click(object sender, EventArgs e)
        {
            if (action == "add")
            {
                MessageBox.Show("Không thể thay đổi số lượng khi thêm mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numSoLuong.Value = 0;
            }
        }
    }
}
