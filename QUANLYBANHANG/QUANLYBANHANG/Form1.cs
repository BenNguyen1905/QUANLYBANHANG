using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYBANHANG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.btnAdd.Click += btnAdd_Click;
            this.btnDelete.Click += btnDelete_Click;
            this.btnEdit.Click += btnEdit_Click;
            this.btnClose.Click += btnClose_Click;
            this.lstNhanVien.Click += lstNhanVien_Click;
        }

        void lstNhanVien_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.txtMaNV.Enabled = false;
            var row = lstNhanVien.SelectedRows[0];
            var cell = row.Cells["Mã_nhân_viên"];
            string manvtmp = (string)cell.Value;
            this.txtMaNV.Text = manvtmp;
            var cell1 = row.Cells["Tên_nhân_viên"];
            string tennvtmp = (string)cell1.Value;
            this.txtTenNV.Text = tennvtmp;

            var cell2 = row.Cells["Giới_tính"];
            string gioitinhtmp = (string)cell2.Value;
            if (gioitinhtmp == "Nam")
            {
                this.chkGioiTinh.Checked = true;
            }
            else
            {
                this.chkGioiTinh.Checked = false;
            }

            var cell3 = row.Cells["Địa_chỉ"];
            string diachitmp = (string)cell3.Value;
            this.txtDiaChi.Text = diachitmp;

            var cell4 = row.Cells["Điện_thoại"];
            string dienthoaitmp = (string)cell4.Value;
            this.txtSDT.Text = dienthoaitmp;

            var cell5 = row.Cells["Ngày_sinh"];
            DateTime ngaysinhtmp = (DateTime)cell5.Value;
            this.txtNgaySinh.Text = ngaysinhtmp + "";

        }
    

        void btnClose_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.Close();
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            //var row = lstNhanVien.SelectedRows[0];
            //var cell = row.Cells["Mã_nhân_viên"];
            //string manvtmp = (string)cell.Value;
            QUANLYBANHANGEntities1 ql = new QUANLYBANHANGEntities1();
            NHANVIEN nv = new NHANVIEN();
            string manv = this.txtMaNV.Text;
            string tennv = this.txtTenNV.Text;
            string gioitinh = "";
            if (chkGioiTinh.Checked == true)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
            }
            string diachi = this.txtDiaChi.Text;
            string sdt = this.txtSDT.Text;
            string ngaysinh = this.txtNgaySinh.Text;
            nv.MANV = manv;
            nv.TENNV = tennv;
            nv.DIENTHOAI = sdt;
            nv.GIOITINH = gioitinh;
            nv.DIACHI = diachi;
            nv.NGAYSINH = DateTime.Parse(ngaysinh);
            ql.Entry(nv).State = System.Data.Entity.EntityState.Modified;
            ql.SaveChanges();
            MessageBox.Show("Sửa nhân viên thành công", "Thông báo");
            this.loadNhanVienList();


        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstNhanVien.SelectedRows.Count == 1)
            {
                var row = lstNhanVien.SelectedRows[0];
                var cell = row.Cells["Mã_nhân_viên"];
                string manvtmp = (string)cell.Value;
                QUANLYBANHANGEntities1 ql = new QUANLYBANHANGEntities1();
                NHANVIEN nv = new NHANVIEN();
                nv = ql.NHANVIENs.Single(x => x.MANV == manvtmp);
                ql.NHANVIENs.Remove(nv);
                ql.SaveChanges();
                MessageBox.Show("Xóa nhân viên thành công", "Thông báo");
                this.loadNhanVienList();
            }
            else
            {
                MessageBox.Show("Bạn phải chọn nhân viên cần xóa", "Lỗi");
            }


        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            QUANLYBANHANGEntities1 ql = new QUANLYBANHANGEntities1();
            NHANVIEN nv = new NHANVIEN();
            int id = 1;
            string ma = "NV0" + id;
            while (ql.NHANVIENs.Where(x=>x.MANV.Equals(ma)).ToList().Count() != 0)
            {
                id += 1;
                ma = "NV0" + id;
            }
            //string manv = this.txtMaNV.Text;
            string tennv = this.txtTenNV.Text;
            string gioitinh = "";
            if (chkGioiTinh.Checked == true)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nữ";
            }
            string diachi = this.txtDiaChi.Text;
            string sdt = this.txtSDT.Text;
            string ngaysinh = this.txtNgaySinh.Text;
            nv.MANV = ma;
            nv.TENNV = tennv;
            nv.DIENTHOAI = sdt;
            nv.GIOITINH = gioitinh;
            nv.DIACHI = diachi;
            nv.NGAYSINH = DateTime.Parse(ngaysinh);
            ql.NHANVIENs.Add(nv);
            ql.SaveChanges();
            MessageBox.Show("Thêm nhân viên thành công", "Thông báo");
            this.loadNhanVienList();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            this.loadNhanVienList();
            this.txtMaNV.Enabled = false;
        }
        void loadNhanVienList()
        {
            QUANLYBANHANGEntities1 ql = new QUANLYBANHANGEntities1();
            this.lstNhanVien.DataSource = ql.NHANVIENs.Select(x => new { Mã_nhân_viên = x.MANV, Tên_nhân_viên = x.TENNV, Giới_tính = x.GIOITINH, Địa_chỉ = x.DIACHI, Điện_thoại = x.DIENTHOAI, Ngày_sinh = x.NGAYSINH }).ToList();

        }


    }
}
