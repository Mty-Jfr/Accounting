using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.DataLayer.Context;

namespace Accounting.App
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCutomers.AutoGenerateColumns = false;
                dgvCutomers.DataSource = db.CustomerRepository.GetAllCustomers();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            BindGrid();
            txtFilter.Text = "";
        }

        private void txtFilter_Click(object sender, EventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCutomers.DataSource = db.CustomerRepository.GetCustomersByFilter(txtFilter.Text);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dgvCutomers.CurrentRow != null)
            {
                using (UnitOfWork db = new UnitOfWork())
                {
                    string name = dgvCutomers.CurrentRow.Cells[1].Value.ToString();
                    if (MessageBox.Show($"آیا از حذف {name} مطمئن هستید ؟", "توجه", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        int customerId = int.Parse(dgvCutomers.CurrentRow.Cells[0].Value.ToString());
                        db.CustomerRepository.DeleteCustomer(customerId);
                        db.save();
                        BindGrid();
                    }
                }
            }
            else
            {
                RtlMessageBox.Show("لطفا شخص را انتخاب کنید");
            }
        }

        private void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            frmAddOrEditCustomer frmAdd = new frmAddOrEditCustomer();
            frmAdd.ShowDialog();
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {

            if (dgvCutomers.CurrentRow != null)
            {
                int customerId = int.Parse(dgvCutomers.CurrentRow.Cells[0].Value.ToString());
                frmAddOrEditCustomer frmAddOrEdit = new frmAddOrEditCustomer();
                frmAddOrEdit.customerId = customerId;
                if (frmAddOrEdit.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }

            }
        }

        private void dgvCutomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

