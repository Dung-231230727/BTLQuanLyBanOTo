using BTLQuanLyBanOTo.Classes;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.DanhMuc
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        DataProcesser dt;
        private string action = "";

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();
            reset();
            LoadDGV();
            LoadCBO();
        }

        public void reset()
        {
            txtMa.Text = "";
            txtMa.Focus();
            txtTen.Text = "";
            cboChucVu.SelectedIndex = -1;
            radNam.Checked = false;
            radNu.Checked = false;
            dtpNgaySinh.Value = DateTime.Now;
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            txtMK.Text = "";

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;

            action = "";
        }

        public void LoadDGV()
        {
            try
            {
                string sql = "SELECT nv.MaNV, nv.TenNV, nv.GioiTinh, nv.NgaySinh, nv.DienThoai, nv.DiaChi, nv.MatKhau, nv.MaCV, cv.TenCV " +
                             "FROM NhanVien nv JOIN CongViec cv ON cv.MaCV = nv.MaCV";
                dgvNhanVien.DataSource = dt.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadCBO()
        {
            try
            {
                string sql = "SELECT * FROM CongViec";
                var tbl = dt.ExecuteQuery(sql);
                cboChucVu.DataSource = tbl;
                cboChucVu.DisplayMember = "TenCV";
                cboChucVu.ValueMember = "MaCV";
                cboChucVu.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách chức vụ:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private string TaoMaTuDong()
        //{

        //}

        private void btnThem_Click(object sender, EventArgs e)
        {
            reset();
            txtMa.Text = TaoMaTuDong();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            action = "add";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!",
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
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            action = "delete";
        }

        private string TaoMaTuDong()
        {
            string prefix = "NV";

            string sql = "SELECT MAX(MaNV) FROM NhanVien WHERE MaNV LIKE @prefix";
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra trống
                if (string.IsNullOrEmpty(txtMa.Text) ||
                    string.IsNullOrEmpty(txtTen.Text) ||
                    cboChucVu.SelectedIndex == -1 ||
                    (!radNam.Checked && !radNu.Checked) ||
                    string.IsNullOrEmpty(txtDienThoai.Text) ||
                    string.IsNullOrEmpty(txtDiaChi.Text) ||
                    string.IsNullOrEmpty(txtMK.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra tuổi
                if (dtpNgaySinh.Value.Date > DateTime.Now.AddYears(-18))
                {
                    MessageBox.Show("Nhân viên phải đủ 18 tuổi!",
                        "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra SDT hợp lệ
                string sdt = txtDienThoai.Text.Trim();
                if (sdt.Length < 9 || sdt.Length > 11 || !sdt.All(char.IsDigit))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ (9–11 chữ số)!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDienThoai.Focus();
                    return;
                }

                if (action == "add")
                {
                    // Kiểm tra trùng mã
                    string sqlKT = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @ma";
                    int rKT = Convert.ToInt32(dt.ExecuteScalar(sqlKT, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim())
                    }));

                    if (rKT > 0)
                    {
                        MessageBox.Show("Đã có mã nhân viên này, vui lòng nhập mã khác!",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sql = "INSERT INTO NhanVien(MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCV, MatKhau) " +
                                 "VALUES(@ma, @ten, @gt, @ns, @dt, @dc, @macv, @mk)";
                    int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim()),
                        new SqlParameter("@ten", txtTen.Text.Trim()),
                        new SqlParameter("@gt", radNam.Checked ? "Nam" : "Nữ"),
                        new SqlParameter("@ns", dtpNgaySinh.Value),
                        new SqlParameter("@dt", txtDienThoai.Text.Trim()),
                        new SqlParameter("@dc", txtDiaChi.Text.Trim()),
                        new SqlParameter("@macv", cboChucVu.SelectedValue),
                        new SqlParameter("@mk", txtMK.Text.Trim())
                    });

                    MessageBox.Show(r > 0 ? "Thêm nhân viên thành công!" : "Thêm thất bại!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (action == "edit")
                {
                    string sql = "UPDATE NhanVien SET TenNV=@ten, GioiTinh=@gt, NgaySinh=@ns, DienThoai=@dt, DiaChi=@dc, MaCV=@macv, MatKhau=@mk WHERE MaNV=@ma";
                    int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim()),
                        new SqlParameter("@ten", txtTen.Text.Trim()),
                        new SqlParameter("@gt", radNam.Checked ? "Nam" : "Nữ"),
                        new SqlParameter("@ns", dtpNgaySinh.Value),
                        new SqlParameter("@dt", txtDienThoai.Text.Trim()),
                        new SqlParameter("@dc", txtDiaChi.Text.Trim()),
                        new SqlParameter("@macv", cboChucVu.SelectedValue),
                        new SqlParameter("@mk", txtMK.Text.Trim())
                    });

                    MessageBox.Show(r > 0 ? "Sửa nhân viên thành công!" : "Sửa thất bại!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (action == "delete")
                {
                    DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.No) return;

                    string sql = "DELETE FROM NhanVien WHERE MaNV=@ma";
                    int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim())
                    });
                    MessageBox.Show(r > 0 ? "Xóa thành công!" : "Xóa thất bại!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadDGV();
                reset();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show("Không thể xóa vì nhân viên này đang được sử dụng ở bảng khác!",
                        "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Lỗi SQL:\n" + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
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

                var row = dgvNhanVien.Rows[e.RowIndex];
                txtMa.Text = row.Cells["MaNV"].Value?.ToString();
                txtTen.Text = row.Cells["TenNV"].Value?.ToString();
                cboChucVu.SelectedValue = row.Cells["MaCV"].Value?.ToString();
                string gt = row.Cells["GioiTinh"].Value?.ToString();
                radNam.Checked = gt == "Nam";
                radNu.Checked = gt == "Nữ";
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtDienThoai.Text = row.Cells["DienThoai"].Value?.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
                txtMK.Text = row.Cells["MatKhau"].Value?.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có chắc muốn thoát không?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
