namespace GoWorkFactoryViewAdministrator.Forms
{
    partial class ReportForm
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
            this.buttonInExcel = new System.Windows.Forms.Button();
            this.buttonGenerateData = new System.Windows.Forms.Button();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tourColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.componentColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Materials = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonInExcel
            // 
            this.buttonInExcel.Location = new System.Drawing.Point(696, 441);
            this.buttonInExcel.Name = "buttonInExcel";
            this.buttonInExcel.Size = new System.Drawing.Size(92, 31);
            this.buttonInExcel.TabIndex = 7;
            this.buttonInExcel.Text = "Отправить";
            this.buttonInExcel.UseVisualStyleBackColor = true;
            this.buttonInExcel.Click += new System.EventHandler(this.buttonInExcel_Click);
            // 
            // buttonGenerateData
            // 
            this.buttonGenerateData.Location = new System.Drawing.Point(673, 10);
            this.buttonGenerateData.Name = "buttonGenerateData";
            this.buttonGenerateData.Size = new System.Drawing.Size(115, 31);
            this.buttonGenerateData.TabIndex = 6;
            this.buttonGenerateData.Text = "Сформировать";
            this.buttonGenerateData.UseVisualStyleBackColor = true;
            this.buttonGenerateData.Click += new System.EventHandler(this.buttonGenerateData_Click);
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(406, 12);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerTo.TabIndex = 5;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(99, 12);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(200, 22);
            this.dateTimePickerFrom.TabIndex = 4;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(422, 448);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(53, 17);
            this.labelEmail.TabIndex = 8;
            this.labelEmail.Text = "Почта:";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(491, 445);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(189, 22);
            this.textBox.TabIndex = 9;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tourColumn,
            this.componentColumnType,
            this.Materials,
            this.Count,
            this.countColumn});
            this.dataGridView.Location = new System.Drawing.Point(12, 48);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(776, 389);
            this.dataGridView.TabIndex = 10;
            // 
            // tourColumn
            // 
            this.tourColumn.HeaderText = "Дата";
            this.tourColumn.MinimumWidth = 6;
            this.tourColumn.Name = "tourColumn";
            this.tourColumn.Width = 125;
            // 
            // componentColumnType
            // 
            this.componentColumnType.HeaderText = "Тип";
            this.componentColumnType.MinimumWidth = 6;
            this.componentColumnType.Name = "componentColumnType";
            this.componentColumnType.Width = 125;
            // 
            // Materials
            // 
            this.Materials.HeaderText = "Материалы";
            this.Materials.MinimumWidth = 6;
            this.Materials.Name = "Materials";
            this.Materials.Width = 125;
            // 
            // Count
            // 
            this.Count.HeaderText = "Количество";
            this.Count.MinimumWidth = 6;
            this.Count.Name = "Count";
            this.Count.Width = 125;
            // 
            // countColumn
            // 
            this.countColumn.HeaderText = "Сумма";
            this.countColumn.MinimumWidth = 6;
            this.countColumn.Name = "countColumn";
            this.countColumn.Width = 125;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 484);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.buttonInExcel);
            this.Controls.Add(this.buttonGenerateData);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Name = "ReportForm";
            this.Text = "Отчет по заявкам и заказам";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInExcel;
        private System.Windows.Forms.Button buttonGenerateData;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn tourColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn componentColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Materials;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn countColumn;
    }
}