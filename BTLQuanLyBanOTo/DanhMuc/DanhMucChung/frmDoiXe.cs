using BTLQuanLyBanOTo.Classes;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.DanhMuc.DanhMucChung
{
    public partial class frmDoiXe : Form
    {
        public frmDoiXe()
        {
            InitializeComponent();
        }

        DataProcesser dt;
        private int TrangThai_HienTai = 1;
        private void frmDoiXe_Load(object sender, EventArgs e)
        {
            dt = new DataProcesser();

            reset();
            LoadDGV();
        }

        public void reset()
        {
            txtMa.Focus();
            txtMa.Text = "";
            txtTen.Text = "";

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;

            radHD.Checked = false;
            radKHD.Checked = false;
            TrangThai_HienTai = 1;

            action = "";
        }

        public void LoadDGV()
        {
            try
            {
                string sql = "select * from DoiXe";
                dgvDoiXe.DataSource = dt.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string TaoMaTuDong()
        {
            string prefix = "DX";

            string sql = "SELECT MAX(MaDoi) FROM DoiXe WHERE MaDoi LIKE @prefix";
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

        private string action = "";
        private void btnThem_Click(object sender, EventArgs e)
        {
            reset();
            txtMa.Text = TaoMaTuDong();
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            action = "add";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Vui lòng chọn bản ghi cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (TrangThai_HienTai == 1)
            {
                // Trạng thái = 1: Đang hoạt động, KHÔNG CHO PHÉP XÓA VĨNH VIỄN
                MessageBox.Show("Đời xe đang được sử dụng (Trạng thái = 1). Vui lòng chuyển sang trạng thái 0 (Sửa -> Ngừng hoạt động) trước khi xóa vĩnh viễn.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                // Kiểm tra dữ liệu trống
                if (string.IsNullOrEmpty(txtMa.Text) || string.IsNullOrEmpty(txtTen.Text) || (radHD.Checked == false && radKHD.Checked == false))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (action == "add")
                {
                    string sqlKT = "select count(*) from DoiXe where MaDoi = @ma";
                    int rKT = Convert.ToInt32(dt.ExecuteScalar(sqlKT, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim())
                    }));

                    if (rKT > 0)
                    {
                        MessageBox.Show("Đã có mã đời xe này. Vui lòng tạo mã khác!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sql = "insert into DoiXe(MaDoi,TenDoi,TrangThai) values(@ma,@ten,@tt)";
                    int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim()),
                        new SqlParameter("@ten", txtTen.Text.Trim()),
                        new SqlParameter("@tt", radHD.Checked==true? 1:0)
                    });

                    MessageBox.Show(r > 0 ? "Thêm thành công!" : "Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (action == "edit")
                {
                    string sql = "update DoiXe set TenDoi = @ten, TrangThai=@tt where MaDoi = @ma";
                    int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim()),
                        new SqlParameter("@ten", txtTen.Text.Trim()),
                        new SqlParameter("@tt", radHD.Checked==true? 1:0)
                    });

                    MessageBox.Show(r > 0 ? "Sửa thành công!" : "Sửa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (action == "delete")
                {
                    DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa bản ghi này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.No) return;

                    string sql = "delete from DoiXe where MaDoi = @ma";
                    int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim())
                    });

                    MessageBox.Show(r > 0 ? "Xóa thành công!" : "Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadDGV();
                reset();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show("Không thể xóa vì đời xe này đang được sử dụng ở bảng khác!", "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Lỗi SQL:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            if (action == "edit" || action == "delete")
            {
                //ẩn
                btnThem.Enabled = false;
                btnLuu.Enabled = false;
                //hiện
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
                action = "cell_click";
            }
            else if (action == "cell_click" || action == "add")
            {
                reset();
            }
        }


        private void dgvDoiXe_CellClick(object sender, DataGridViewCellEventArgs e)
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

                var row = dgvDoiXe.Rows[e.RowIndex];
                txtMa.Text = row.Cells["MaDoi"].Value?.ToString();
                txtTen.Text = row.Cells["TenDoi"].Value?.ToString();
                var trangThaiValue = row.Cells["TrangThai"].Value;
                TrangThai_HienTai = (trangThaiValue != null && trangThaiValue != DBNull.Value) ? Convert.ToInt32(trangThaiValue) : 1;
                radHD.Checked = (TrangThai_HienTai == 1);
                radKHD.Checked = (TrangThai_HienTai == 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị dữ liệu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
