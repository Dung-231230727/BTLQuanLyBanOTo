using BTLQuanLyBanOTo.Classes;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.DanhMuc
{
    public partial class frmNhaCungCap : Form
    {
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        DataProcesser dt;
        private string action = "";
        public bool IsFromNCC { get; set; } = false;
        public string DT_BanHang = "";
        private void frmNhaCungCap_Load(object sender, EventArgs e)
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
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";

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
                string sql = "select * from NhaCungCap";
                dgvNhaCungCap.DataSource = dt.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string TaoMaTuDong()
        {
            string prefix = "NCC";

            string sql = "SELECT MAX(MaNCC) FROM NhaCungCap WHERE MaNCC LIKE @prefix";
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
            if (IsFromNCC)
            {
                txtDienThoai.Text = DT_BanHang;
            }
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
                if (string.IsNullOrEmpty(txtMa.Text) ||
                    string.IsNullOrEmpty(txtTen.Text) ||
                    string.IsNullOrEmpty(txtDienThoai.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra SDT hợp lệ (9–11 chữ số)
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtDienThoai.Text.Trim(), @"^[0-9]{9,11}$"))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ! (chỉ gồm 9–11 chữ số)",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDienThoai.Focus();
                    return;
                }

                if (action == "add")
                {
                    string sqlKT = "select count(*) from NhaCungCap where MaNCC = @ma";
                    int rKT = Convert.ToInt32(dt.ExecuteScalar(sqlKT, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim())
                    }));

                    if (rKT > 0)
                    {
                        MessageBox.Show("Đã có mã nhà cung cấp này. Vui lòng nhập mã khác!",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sql = "insert into NhaCungCap(MaNCC, TenNCC, DiaChi, DienThoai) " +
                                 "values(@ma, @ten, @dc, @dt)";
                    int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim()),
                        new SqlParameter("@ten", txtTen.Text.Trim()),
                        new SqlParameter("@dc", txtDiaChi.Text.Trim()),
                        new SqlParameter("@dt", txtDienThoai.Text.Trim())
                    });

                    MessageBox.Show(r > 0 ? "Thêm nhà cung cấp thành công!"
                                          : "Thêm thất bại!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (IsFromNCC)
                    {
                        this.Close();
                    }
                }

                if (action == "edit")
                {
                    string sql = "update NhaCungCap set TenNCC = @ten, DiaChi = @dc, DienThoai = @dt where MaNCC = @ma";
                    int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim()),
                        new SqlParameter("@ten", txtTen.Text.Trim()),
                        new SqlParameter("@dc", txtDiaChi.Text.Trim()),
                        new SqlParameter("@dt", txtDienThoai.Text.Trim())
                    });

                    MessageBox.Show(r > 0 ? "Sửa thành công!"
                                          : "Sửa thất bại!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (action == "delete")
                {
                    DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa nhà cung cấp này?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.No) return;

                    string sql = "delete from NhaCungCap where MaNCC = @ma";
                    int r = dt.ExecuteNonQuery(sql, new SqlParameter[]
                    {
                        new SqlParameter("@ma", txtMa.Text.Trim())
                    });

                    MessageBox.Show(r > 0 ? "Xóa thành công!"
                                          : "Xóa thất bại!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadDGV();
                reset();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show("Không thể xóa vì nhà cung cấp này đang được sử dụng ở bảng khác!",
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
            else if (action == "cell_click" || action == "add")
            {
                reset();
            }
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
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

                var row = dgvNhaCungCap.Rows[e.RowIndex];
                txtMa.Text = row.Cells["MaNCC"].Value?.ToString();
                txtTen.Text = row.Cells["TenNCC"].Value?.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
                txtDienThoai.Text = row.Cells["DienThoai"].Value?.ToString();
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
