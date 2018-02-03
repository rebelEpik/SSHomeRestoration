﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace SSHomeRestoration
{
    public partial class AddEmployee : MaterialForm
    {
        private EmployeeAdded _employeeAddedCallback;

        public AddEmployee(EmployeeAdded employeeAddedCallBack)
        {
            InitializeComponent();
            _employeeAddedCallback = employeeAddedCallBack;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);


        }
    }
}
