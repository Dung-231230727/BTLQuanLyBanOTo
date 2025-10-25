using BTLQuanLyBanOTo.Classes;
using System;
using System.Data.SqlClient;
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
            cboChucVu.Text = "";
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
        }

        public void LoadDGV()
        {
            string sql = "select nv.MaNV, nv.TenNV, nv.GioiTinh, nv.NgaySinh, nv.DienThoai, nv.DiaChi, nv.MatKhau, nv.MaCV, cv.TenCV from NhanVien nv join CongViec cv on cv.MaCV = nv.MaCV";
            dgvNhanVien.DataSource = dt.ExecuteQuery(sql);
        }

        public void LoadCBO()
        {
            string sql = "select * from CongViec";
            var tbl = dt.ExecuteQuery(sql);
            cboChucVu.DataSource = tbl;
            cboChucVu.DisplayMember = "TenCV";
            cboChucVu.ValueMember = "MaCV";
            cboChucVu.SelectedIndex = -1;
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
            btnBoQua.Enabled = true;
            action = "edit";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            action = "delete";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //kiem tra trong
            if (string.IsNullOrEmpty(txtMa.Text) ||
                string.IsNullOrEmpty(txtTen.Text) ||
                cboChucVu.SelectedIndex == -1 ||
                (!radNam.Checked && !radNu.Checked) ||
                string.IsNullOrEmpty(txtDienThoai.Text) ||
                string.IsNullOrEmpty(txtDiaChi.Text) ||
                string.IsNullOrEmpty(txtMK.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ thông tin!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (action == "add")
            {
                //lọc trùng
                string sqlKT = "select count(*) from NhanVien where MaNV = @ma";
                int count = (int)dt.ExecuteScalar(sqlKT, new SqlParameter[]
                {
                    new SqlParameter("@ma", txtMa.Text.ToString())
                });
                if (count > 0)
                {
                    MessageBox.Show(
                        "Đã có mã nhân viên này. Vui lòng tạo mã khác",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                        );
                    return;
                }

                string sql = "insert into NhanVien(MaNV,TenNV,GioiTinh,NgaySinh,DienThoai,DiaChi,MaCV,MatKhau) values(@ma,@ten,@gt,@ns,@dt,@dc,@macv,@mk)";
                int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                {
                    new SqlParameter("@ma",txtMa.Text.ToString()),
                    new SqlParameter("@ten",txtTen.Text.ToString()),
                    new SqlParameter("@gt",radNam.Checked == true ? "Nam":"Nữ"),
                    new SqlParameter("@ns",dtpNgaySinh.Value),
                    new SqlParameter("dt", txtDienThoai.Text.ToString()),
                    new SqlParameter("@dc",txtDiaChi.Text.ToString()),
                    new SqlParameter("@macv",cboChucVu.SelectedValue),
                    new SqlParameter("@mk",txtMK.Text.ToString())
                });
                if (r > 0)
                {
                    MessageBox.Show(
                        "Thêm nhân viên thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                }
                else
                {
                    MessageBox.Show(
                        "Thêm nhân viên thất bại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    return;
                }
            }

            if (action == "edit")
            {
                string sql = "update NhanVien set TenNV=@ten,GioiTinh=@gt,NgaySinh=@ns,DienThoai=@dt,DiaChi=@dc,MaCV=@macv,MatKhau=@mk where MaNV=@ma";
                int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                {
                    new SqlParameter("@ma",txtMa.Text.ToString()),
                    new SqlParameter("@ten",txtTen.Text.ToString()),
                    new SqlParameter("@gt",radNam.Checked == true ? "Nam":"Nữ"),
                    new SqlParameter("@ns",dtpNgaySinh.Value),
                    new SqlParameter("dt", txtDienThoai.Text.ToString()),
                    new SqlParameter("@dc",txtDiaChi.Text.ToString()),
                    new SqlParameter("@macv",cboChucVu.SelectedValue),
                    new SqlParameter("@mk",txtMK.Text.ToString())
                });
                if (r > 0)
                {
                    MessageBox.Show(
                        "Sửa viên thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                }
                else
                {
                    MessageBox.Show(
                        "Sửa nhân viên thất bại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    return;
                }
            }

            if (action == "delete")
            {
                string sql = "delete from NhanVien where MaNV=@ma";
                int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                {
                    new SqlParameter("@ma",txtMa.Text.ToString())
                });
                if (r > 0)
                {
                    MessageBox.Show(
                        "Xóa viên thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                }
                else
                {
                    MessageBox.Show(
                        "Xóa nhân viên thất bại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    return;
                }
            }

            reset();
            LoadDGV();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnThem.Enabled = false;
                btnLuu.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;

                var rows = dgvNhanVien.Rows[e.RowIndex];
                txtMa.Text = rows.Cells["MaNV"].Value.ToString();
                txtTen.Text = rows.Cells["TenNV"].Value.ToString();
                cboChucVu.SelectedValue = rows.Cells["MaCV"].Value.ToString();
                string gt = rows.Cells["GioiTinh"].Value.ToString();
                if (gt == "Nam")
                {
                    radNam.Checked = true;
                }
                else
                {
                    radNu.Checked = true;
                }
                dtpNgaySinh.Value = (DateTime)rows.Cells["NgaySinh"].Value;
                txtDienThoai.Text = rows.Cells["DienThoai"].Value.ToString();
                txtDiaChi.Text = rows.Cells["DiaChi"].Value.ToString();
                txtMK.Text = rows.Cells["MatKhau"].Value.ToString();
            }
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
