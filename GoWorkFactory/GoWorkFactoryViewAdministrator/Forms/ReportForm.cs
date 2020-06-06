using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.BusinessLogics;
using GoWorkFactoryBusinessLogic.Interfaces;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace GoWorkFactoryViewAdministrator.Forms
{
    public partial class ReportForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ReportLogic reportLogic;
        public ReportForm(ReportLogic reportLogic)
        {
            InitializeComponent();
            this.reportLogic = reportLogic;
        }

        private void buttonGenerateData_Click(object sender, EventArgs e)
        {
            try
            {

                var reportRequestOrders = reportLogic.GetRequestsOrders(dateTimePickerTo.Value, dateTimePickerFrom.Value); 
                if (reportRequestOrders != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in reportRequestOrders)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Date, pc.Type, pc.NameMaterial[0], pc.Count.ElementAtOrDefault(0), pc.Price.ElementAtOrDefault(0) });
                        for (int i = 1; i < pc.NameMaterial.Count; i++)
                        {
                            dataGridView.Rows.Add(new object[] { "", "", pc.NameMaterial[i], pc.Count.ElementAtOrDefault(i), pc.Type == "Заказ" ? pc.Price.ElementAtOrDefault(0) : pc.Price.ElementAtOrDefault(i) });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonInExcel_Click(object sender, EventArgs e)
        {

        }
    }
}
