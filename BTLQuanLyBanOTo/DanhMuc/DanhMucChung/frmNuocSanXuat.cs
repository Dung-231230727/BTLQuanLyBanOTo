using BTLQuanLyBanOTo.Classes;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.DanhMuc.DanhMucChung
{
    public partial class frmNuocSanXuat : Form
    {
        public frmNuocSanXuat()
        {
            InitializeComponent();
        }

        DataProcesser dt;

        private void frmNuocSanXuat_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();

            reset();
            LoadDGV();
        }

        public void reset()
        {
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
            string sql = "select * from NuocSX";
            dgvNuocSX.DataSource = dt.ExecuteQuery(sql);
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
                string sqlKT = "select count(*) from NuocSX where MaNuocSX = @ma";
                int rKT = (int)dt.ExecuteScalar(sqlKT, new SqlParameter[]
                {
                    new SqlParameter("@ma", txtMa.Text.ToString())
                });
                if (rKT > 0)
                {
                    MessageBox.Show(
                        "Đã có mã nước sản xuất này. Vui lòng tạo mã khác!",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                        );
                    return;
                }

                string sql = "insert into NuocSX(MaNuocSX,TenNuocSX) values(@ma,@ten)";
                int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                {
                    new SqlParameter("@ma", txtMa.Text.ToString()),
                    new SqlParameter("@ten",txtTen.Text.ToString())
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
                string sql = "update NuocSX set TenNuocSX = @ten where MaNuocSX = @ma";
                int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                {
                    new SqlParameter("@ma", txtMa.Text.ToString()),
                    new SqlParameter("@ten",txtTen.Text.ToString())
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
                string sql = "delete from NuocSX where MaNuocSX = @ma";
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

        private void dgvNuocSX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
                btnThem.Enabled = false;
                btnLuu.Enabled = false;

                var row = dgvNuocSX.Rows[e.RowIndex];
                txtMa.Text = row.Cells["MaNuocSX"].Value.ToString();
                txtTen.Text = row.Cells["TenNuocSX"].Value.ToString();
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
