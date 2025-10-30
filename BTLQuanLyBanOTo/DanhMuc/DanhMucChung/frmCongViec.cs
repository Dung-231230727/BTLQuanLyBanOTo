using BTLQuanLyBanOTo.Classes;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.DanhMuc.DanhMucChung
{
    public partial class frmCongViec : Form
    {
        public frmCongViec()
        {
            InitializeComponent();
        }
        DataProcesser dt;

        private void frmCongViec_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();

            reset();
            LoadDGV();
        }

        public void reset()
        {
            txtMa.Enabled = true;
            txtMa.Text = "";
            txtTen.Text = "";
            txtMa.Focus();

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;

            action = "";
        }

        public void LoadDGV()
        {
            string sql = "select * from CongViec";
            dgvCongViec.DataSource = dt.ExecuteQuery(sql);
        }

        private string action = "";
        private void btnThem_Click(object sender, EventArgs e)
        {
            reset();
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
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
            if (string.IsNullOrEmpty(txtMa.Text) || string.IsNullOrEmpty(txtTen.Text))
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
                string sqlKT = "select count(*) from CongViec where MaCV = @ma";
                int rKT = (int)dt.ExecuteScalar(sqlKT, new SqlParameter[]
                {
                    new SqlParameter("@ma", txtMa.Text.ToString())
                });
                if (rKT > 0)
                {
                    MessageBox.Show(
                        "Đã có mã công việc này. Vui lòng tạo mã khác!",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                        );
                    return;
                }

                string sql = "insert into CongViec(MaCV,TenCV,LuongThang) values(@ma,@ten,@luong)";
                int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                {
                    new SqlParameter("@ma", txtMa.Text.ToString()),
                    new SqlParameter("@ten",txtTen.Text.ToString()),
                    new SqlParameter("@luong",txtLuong.Text.ToString())
                });
                if (r > 0)
                {
                    MessageBox.Show(
                        "Thêm thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                }
                else
                {
                    MessageBox.Show(
                        "Thêm thất bại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    return;
                }
            }

            if (action == "edit")
            {
                string sql = "update CongViec set TenCV = @ten, LuongThang = @luong where MaCV = @ma";
                int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                {
                    new SqlParameter("@ma", txtMa.Text.ToString()),
                    new SqlParameter("@ten",txtTen.Text.ToString()),
                    new SqlParameter("@luong",txtLuong.Text.ToString())
                });
                if (r > 0)
                {
                    MessageBox.Show(
                        "Sửa thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                }
                else
                {
                    MessageBox.Show(
                        "Sửa thất bại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    return;
                }
            }

            if (action == "delete")
            {
                string sql = "delete from CongViec where MaCV = @ma";
                int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                {
                    new SqlParameter("@ma", txtMa.Text.ToString())
                });
                if (r > 0)
                {
                    MessageBox.Show(
                        "Xóa thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                }
                else
                {
                    MessageBox.Show(
                        "Xóa thất bại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                    return;
                }
            }

            LoadDGV();
            reset();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void dgvCongViec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMa.Enabled = false;

                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
                btnThem.Enabled = false;
                btnLuu.Enabled = false;

                var row = dgvCongViec.Rows[e.RowIndex];
                txtMa.Text = row.Cells["MaCV"].Value.ToString();
                txtTen.Text = row.Cells["TenCV"].Value.ToString();
                txtLuong.Text = row.Cells["LuongThang"].Value.ToString();
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
