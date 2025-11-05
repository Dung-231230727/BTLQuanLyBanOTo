using System;
using System.Text;
using System.Windows.Forms;

namespace BTLQuanLyBanOTo.TroGiup
{
    public partial class frmHuongDan : Form
    {
        public frmHuongDan()
        {
            InitializeComponent();
        }

        private void frmHuongDan_Load(object sender, EventArgs e)
        {
            LoadContent();
        }

        private void LoadContent()
        {
            // Sử dụng StringBuilder để xây dựng nội dung hướng dẫn không dùng ký tự đặc biệt
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("HƯỚNG DẪN SỬ DỤNG PHẦN MỀM QUẢN LÝ CỬA HÀNG BÁN Ô TÔ");
            sb.AppendLine();

            sb.AppendLine("I. HƯỚNG DẪN THAO TÁC TRÊN FORM CHÍNH (FRMMAIN)");
            sb.AppendLine();
            sb.AppendLine("Form Chính của hệ thống được chia thành hai khu vực chính:");
            sb.AppendLine("1. Khu vực ListView (Thanh điều hướng nhanh): Liệt kê các chức năng chính của hệ thống. Người dùng có thể KÍCH ĐÚP vào tên chức năng để mở form tương ứng.");
            sb.AppendLine("2. Khu vực Thanh Menu (MenuStrip): Bao gồm tất cả các chức năng của hệ thống được phân loại theo nhóm.");
            sb.AppendLine();

            sb.AppendLine("II. TỔNG HỢP CÁC CHỨC NĂNG CỦA HỆ THỐNG");
            sb.AppendLine();
            sb.AppendLine("1. HỆ THỐNG:");
            sb.AppendLine("   - Đăng nhập/Đăng ký");
            sb.AppendLine("   - Đăng xuất");
            sb.AppendLine("   - Thoát");
            sb.AppendLine("2. DANH MỤC:");
            sb.AppendLine("   - Quản lý sản phẩm");
            sb.AppendLine("   - Quản lý nhân viên");
            sb.AppendLine("   - Quản lý khách hàng");
            sb.AppendLine("   - Quản lý nhà cung cấp");
            sb.AppendLine("   - Danh mục chung:");
            sb.AppendLine("     + Công việc");
            sb.AppendLine("     + Đời xe");
            sb.AppendLine("     + Hãng sản xuất");
            sb.AppendLine("     + Loại xe");
            sb.AppendLine("     + Màu sắc");
            sb.AppendLine("     + Nước sản xuất");
            sb.AppendLine("     + Số chỗ ngồi");
            sb.AppendLine("     + Tình trạng");
            sb.AppendLine("3. NGHIỆP VỤ:");
            sb.AppendLine("   - Bán hàng");
            sb.AppendLine("   - Nhập hàng");
            sb.AppendLine("4. BÁO CÁO VÀ THỐNG KÊ:");
            sb.AppendLine("   - Báo cáo doanh thu");
            sb.AppendLine("   - Báo cáo tồn kho");
            sb.AppendLine("5. TRỢ GIÚP:");
            sb.AppendLine("   - Thông tin");
            sb.AppendLine("   - Hướng dẫn");
            sb.AppendLine();

            sb.AppendLine("III. QUY TRÌNH THAO TÁC CƠ BẢN");
            sb.AppendLine();
            sb.AppendLine("Các form quản lý danh mục (Thêm/Sửa/Xóa) đều tuân theo quy trình cơ bản:");
            sb.AppendLine("1. Thêm mới: Nhấn nút Thêm, điền đầy đủ thông tin, sau đó nhấn Lưu.");
            sb.AppendLine("2. Sửa/Xóa: Kích chuột vào một dòng trong lưới dữ liệu (DataGridView) để chọn bản ghi, sau đó nhấn nút Sửa hoặc Xóa.");
            sb.AppendLine("3. Thao tác Bán/Nhập hàng:");
            sb.AppendLine("   - Chọn Mã sản phẩm và nhập Số lượng.");
            sb.AppendLine("   - Nhấn Thêm sản phẩm để đưa vào giỏ hàng (lưới dữ liệu chi tiết).");
            sb.AppendLine("   - Nhấn Lưu để hoàn tất hóa đơn (hệ thống sẽ tự động cập nhật tồn kho).");
            sb.AppendLine("   - Có thể chọn Xuất (In) hóa đơn ra file Excel.");

            rtxHuongDan.Text = sb.ToString();
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