using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.Interfaces;
using System;
using System.Windows.Forms;
using Unity;

namespace GoWorkFactoryViewAdministrator.Forms
{
    public partial class ProductMainForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IProductLogic productLogic;
        public ProductMainForm(IProductLogic productLogic)
        {
            InitializeComponent();
            this.productLogic = productLogic;
        }

        private void LoadData()
        {
            try
            {
                var products = productLogic.Read(null);
                if (products != null)
                {
                    dataGridView.DataSource = products;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[4].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void материалыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<MaterialsForm>();
            form.ShowDialog();
        }

        private void buttonCreateProduct_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<SettingProductForm>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void ProductMainForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonRemoveProduct_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        productLogic.Remove(new ProductBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonSettingProduct_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<SettingProductForm>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void оформитьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<CreateRequestForm>();
            form.ShowDialog();
        }

        private void отчетПоЗаявкамИЗаказамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<ReportForm>();
            form.ShowDialog();
        }
    }
}
