namespace SPGen2008.Components.Selector
{
    partial class FSelector_TableColumns
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
            this._DataGridView = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._Cancel_button = new System.Windows.Forms.Button();
            this._Submit_button = new System.Windows.Forms.Button();
            this._IsPrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this._Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Caption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._Length = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._AllowNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._DataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _DataGridView
            // 
            this._DataGridView.AllowUserToAddRows = false;
            this._DataGridView.AllowUserToDeleteRows = false;
            this._DataGridView.AllowUserToOrderColumns = true;
            this._DataGridView.AllowUserToResizeRows = false;
            this._DataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._DataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._IsPrimaryKey,
            this._Name,
            this._Caption,
            this._Memo,
            this._DataType,
            this._Length,
            this._AllowNull});
            this._DataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this._DataGridView.Location = new System.Drawing.Point(12, 12);
            this._DataGridView.MultiSelect = false;
            this._DataGridView.Name = "_DataGridView";
            this._DataGridView.RowHeadersWidth = 26;
            this._DataGridView.RowTemplate.Height = 23;
            this._DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._DataGridView.Size = new System.Drawing.Size(819, 382);
            this._DataGridView.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.DimGray;
            this.pictureBox1.Location = new System.Drawing.Point(13, 407);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(818, 1);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // _Cancel_button
            // 
            this._Cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._Cancel_button.Location = new System.Drawing.Point(756, 421);
            this._Cancel_button.Name = "_Cancel_button";
            this._Cancel_button.Size = new System.Drawing.Size(75, 23);
            this._Cancel_button.TabIndex = 5;
            this._Cancel_button.Text = "&Cancel";
            this._Cancel_button.UseVisualStyleBackColor = true;
            // 
            // _Submit_button
            // 
            this._Submit_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._Submit_button.Location = new System.Drawing.Point(661, 421);
            this._Submit_button.Name = "_Submit_button";
            this._Submit_button.Size = new System.Drawing.Size(75, 23);
            this._Submit_button.TabIndex = 5;
            this._Submit_button.Text = "&Submit";
            this._Submit_button.UseVisualStyleBackColor = true;
            // 
            // _IsPrimaryKey
            // 
            this._IsPrimaryKey.HeaderText = "Select";
            this._IsPrimaryKey.MinimumWidth = 100;
            this._IsPrimaryKey.Name = "_IsPrimaryKey";
            this._IsPrimaryKey.ToolTipText = "Multi Choose";
            // 
            // _Name
            // 
            this._Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._Name.HeaderText = "FieldName";
            this._Name.MinimumWidth = 155;
            this._Name.Name = "_Name";
            this._Name.ReadOnly = true;
            this._Name.Width = 155;
            // 
            // _Caption
            // 
            this._Caption.HeaderText = "DisplayName";
            this._Caption.MinimumWidth = 100;
            this._Caption.Name = "_Caption";
            // 
            // _Memo
            // 
            this._Memo.HeaderText = "Discription";
            this._Memo.MinimumWidth = 255;
            this._Memo.Name = "_Memo";
            // 
            // _DataType
            // 
            this._DataType.HeaderText = "DataType";
            this._DataType.MinimumWidth = 100;
            this._DataType.Name = "_DataType";
            this._DataType.ReadOnly = true;
            // 
            // _Length
            // 
            this._Length.HeaderText = "L";
            this._Length.MinimumWidth = 30;
            this._Length.Name = "_Length";
            this._Length.ReadOnly = true;
            this._Length.ToolTipText = "数据最大长度";
            // 
            // _AllowNull
            // 
            this._AllowNull.HeaderText = "E";
            this._AllowNull.MinimumWidth = 20;
            this._AllowNull.Name = "_AllowNull";
            this._AllowNull.ReadOnly = true;
            this._AllowNull.ToolTipText = "是否可空";
            // 
            // FSelector_TableColumns
            // 
            this.AcceptButton = this._Submit_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._Cancel_button;
            this.ClientSize = new System.Drawing.Size(843, 456);
            this.Controls.Add(this._Submit_button);
            this.Controls.Add(this._Cancel_button);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._DataGridView);
            this.Name = "FSelector_TableColumns";
            this.Text = "表--字段选择器";
            ((System.ComponentModel.ISupportInitialize)(this._DataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _DataGridView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button _Cancel_button;
        private System.Windows.Forms.Button _Submit_button;
        private System.Windows.Forms.DataGridViewCheckBoxColumn _IsPrimaryKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Caption;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Memo;
        private System.Windows.Forms.DataGridViewTextBoxColumn _DataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn _Length;
        private System.Windows.Forms.DataGridViewCheckBoxColumn _AllowNull;

    }
}