namespace GoWorkFactoryViewAdministrator.Forms
{
    partial class ProductMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonCreateProduct = new System.Windows.Forms.Button();
            this.buttonSettingProduct = new System.Windows.Forms.Button();
            this.buttonRemoveProduct = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.материалыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оформитьЗаявкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетПоЗаявкамИЗаказамToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.бэкапToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 48);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(566, 390);
            this.dataGridView.TabIndex = 0;
            // 
            // buttonCreateProduct
            // 
            this.buttonCreateProduct.Location = new System.Drawing.Point(588, 48);
            this.buttonCreateProduct.Name = "buttonCreateProduct";
            this.buttonCreateProduct.Size = new System.Drawing.Size(200, 38);
            this.buttonCreateProduct.TabIndex = 1;
            this.buttonCreateProduct.Text = "Создать товар";
            this.buttonCreateProduct.UseVisualStyleBackColor = true;
            this.buttonCreateProduct.Click += new System.EventHandler(this.buttonCreateProduct_Click);
            // 
            // buttonSettingProduct
            // 
            this.buttonSettingProduct.Location = new System.Drawing.Point(588, 92);
            this.buttonSettingProduct.Name = "buttonSettingProduct";
            this.buttonSettingProduct.Size = new System.Drawing.Size(200, 38);
            this.buttonSettingProduct.TabIndex = 2;
            this.buttonSettingProduct.Text = "Редактировать товар";
            this.buttonSettingProduct.UseVisualStyleBackColor = true;
            this.buttonSettingProduct.Click += new System.EventHandler(this.buttonSettingProduct_Click);
            // 
            // buttonRemoveProduct
            // 
            this.buttonRemoveProduct.Location = new System.Drawing.Point(588, 136);
            this.buttonRemoveProduct.Name = "buttonRemoveProduct";
            this.buttonRemoveProduct.Size = new System.Drawing.Size(200, 38);
            this.buttonRemoveProduct.TabIndex = 3;
            this.buttonRemoveProduct.Text = "Удалить товар";
            this.buttonRemoveProduct.UseVisualStyleBackColor = true;
            this.buttonRemoveProduct.Click += new System.EventHandler(this.buttonRemoveProduct_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.материалыToolStripMenuItem,
            this.оформитьЗаявкуToolStripMenuItem,
            this.отчетПоЗаявкамИЗаказамToolStripMenuItem,
            this.бэкапToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // материалыToolStripMenuItem
            // 
            this.материалыToolStripMenuItem.Name = "материалыToolStripMenuItem";
            this.материалыToolStripMenuItem.Size = new System.Drawing.Size(103, 24);
            this.материалыToolStripMenuItem.Text = "Материалы";
            this.материалыToolStripMenuItem.Click += new System.EventHandler(this.материалыToolStripMenuItem_Click);
            // 
            // оформитьЗаявкуToolStripMenuItem
            // 
            this.оформитьЗаявкуToolStripMenuItem.Name = "оформитьЗаявкуToolStripMenuItem";
            this.оформитьЗаявкуToolStripMenuItem.Size = new System.Drawing.Size(145, 24);
            this.оформитьЗаявкуToolStripMenuItem.Text = "Оформить заявку";
            this.оформитьЗаявкуToolStripMenuItem.Click += new System.EventHandler(this.оформитьЗаявкуToolStripMenuItem_Click);
            // 
            // отчетПоЗаявкамИЗаказамToolStripMenuItem
            // 
            this.отчетПоЗаявкамИЗаказамToolStripMenuItem.Name = "отчетПоЗаявкамИЗаказамToolStripMenuItem";
            this.отчетПоЗаявкамИЗаказамToolStripMenuItem.Size = new System.Drawing.Size(218, 24);
            this.отчетПоЗаявкамИЗаказамToolStripMenuItem.Text = "Отчет по заявкам и заказам";
            this.отчетПоЗаявкамИЗаказамToolStripMenuItem.Click += new System.EventHandler(this.отчетПоЗаявкамИЗаказамToolStripMenuItem_Click);
            // 
            // бэкапToolStripMenuItem
            // 
            this.бэкапToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jsonToolStripMenuItem,
            this.xmlToolStripMenuItem});
            this.бэкапToolStripMenuItem.Name = "бэкапToolStripMenuItem";
            this.бэкапToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.бэкапToolStripMenuItem.Text = "Бэкап";
            // 
            // jsonToolStripMenuItem
            // 
            this.jsonToolStripMenuItem.Name = "jsonToolStripMenuItem";
            this.jsonToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.jsonToolStripMenuItem.Text = "Json";
            this.jsonToolStripMenuItem.Click += new System.EventHandler(this.jsonToolStripMenuItem_Click);
            // 
            // xmlToolStripMenuItem
            // 
            this.xmlToolStripMenuItem.Name = "xmlToolStripMenuItem";
            this.xmlToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.xmlToolStripMenuItem.Text = "Xml";
            this.xmlToolStripMenuItem.Click += new System.EventHandler(this.xmlToolStripMenuItem_Click);
            // 
            // ProductMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonRemoveProduct);
            this.Controls.Add(this.buttonSettingProduct);
            this.Controls.Add(this.buttonCreateProduct);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ProductMainForm";
            this.Text = "Товары";
            this.Load += new System.EventHandler(this.ProductMainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonCreateProduct;
        private System.Windows.Forms.Button buttonSettingProduct;
        private System.Windows.Forms.Button buttonRemoveProduct;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem материалыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оформитьЗаявкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетПоЗаявкамИЗаказамToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem бэкапToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xmlToolStripMenuItem;
    }
}