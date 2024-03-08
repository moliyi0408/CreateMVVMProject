using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateMVVMProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLoadPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };
            dialog.ShowDialog();

            txtLoadPath.Text = dialog.SelectedPath;
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLoadPath.Text) && !string.IsNullOrEmpty(txtProjectName.Text))
            {
                string ProjectPath = string.Format(@"{0}\{1}", txtLoadPath.Text, txtProjectName.Text);
                if (Directory.Exists(ProjectPath))
                {
                    MessageBox.Show("專案已存在");
                }
                else
                {
                    Directory.CreateDirectory(ProjectPath);
                    string ExamplePath = ConfigurationManager.AppSettings["ExampleProjectPath"];
                    CreateLevel1(ExamplePath, ProjectPath); //改專案名
                    // CreateLevel2(string.Format(@"{0}\ReportExample", ExamplePath), string.Format(@"{0}\{1}", ProjectPath,txtProjectName.Text));
                    CreateLevel2(string.Format(@"{0}\WpfMVVMExample", ExamplePath), string.Format(@"{0}\{1}", ProjectPath,txtProjectName.Text));
                    // CreateLevel3Properties(string.Format(@"{0}\ReportExample\Properties", ExamplePath), string.Format(@"{0}\{1}\Properties", ProjectPath, txtProjectName.Text));
                    CreateLevel3Properties(string.Format(@"{0}\WpfMVVMExample\Properties", ExamplePath), string.Format(@"{0}\{1}\Properties", ProjectPath, txtProjectName.Text));
                    // CreateLevel3ReportInfo(string.Format(@"{0}\ReportExample\ReportInfo", ExamplePath), string.Format(@"{0}\{1}\ReportInfo", ProjectPath, txtProjectName.Text));

                    MessageBox.Show("新增完畢");
                }

            }
            else
            {
                MessageBox.Show("未填寫完成");
            }

        }


        private void CreateLevel1(string ReadPath, string CreatePath)
        {
            Directory.CreateDirectory(string.Format(@"{0}\{1}", CreatePath, txtProjectName.Text));

            //   CreateReplceFile(ReadPath, CreatePath, "ReportExample.sln", txtProjectName.Text + ".sln");
            CreateReplceFile(ReadPath, CreatePath, "WpfMVVMExample.sln", txtProjectName.Text + ".sln");
        }

        private void CreateLevel2(string ReadPath, string CreatePath)
        {

            CreateReplceFile(ReadPath, CreatePath, "App.config");
            Directory.CreateDirectory(string.Format(@"{0}\Properties", CreatePath));
            //Directory.CreateDirectory(string.Format(@"{0}\ReportInfo", CreatePath));
            //
            // CreateReplceFile(ReadPath, CreatePath, "Program.cs");
            // CreateReplceFile(ReadPath, CreatePath, "REPORT.cs")
            
            // CreateReplceFile(ReadPath, CreatePath, "ReportExample.csproj", txtProjectName.Text + ".csproj");
            CreateReplceFile(ReadPath, CreatePath, "WpfMVVMExample.csproj", txtProjectName.Text + ".csproj");


            CreateReplceFile(ReadPath, CreatePath, "MainWindow.xaml.cs");
            CreateReplceFile(ReadPath, CreatePath, "MainWindow.xaml");
            CreateReplceFile(ReadPath, CreatePath, "App.xaml.cs");
            CreateReplceFile(ReadPath, CreatePath, "App.xaml");
            
            
            CreateReplceFile(ReadPath, CreatePath, "MainViewModel.cs");
            CreateReplceFile(ReadPath, CreatePath, "ViewModelBase.cs");
            CreateReplceFile(ReadPath, CreatePath, "RelayCommand.cs");

        }

        private void CreateLevel3Properties(string ReadPath, string CreatePath)
        {
            CreateReplceFile(ReadPath, CreatePath, "AssemblyInfo.cs");

            CreateReplceFile(ReadPath, CreatePath, "Resources.Designer.cs");
            CreateReplceFile(ReadPath, CreatePath, "Resources.resx");
            CreateReplceFile(ReadPath, CreatePath, "Settings.Designer.cs");
            CreateReplceFile(ReadPath, CreatePath, "Settings.settings");
        }

        //private void CreateLevel3ReportInfo(string ReadPath, string CreatePath)
        //{
        //    CreateReplceFile(ReadPath, CreatePath, "BlockInfo.cs");
        //    CreateReplceFile(ReadPath, CreatePath, "ConditionInfo.cs");
        //    CreateReplceFile(ReadPath, CreatePath, "LayoutInfo.cs");
        //    CreateReplceFile(ReadPath, CreatePath, "UrlLinkInfo.cs");
        //}


        private void CreateReplceFile(string ReadPath, string CreatePath, string FileName)
        {
            //寫到檔案裡面更改
            foreach (var str in File.ReadAllLines(string.Format(@"{0}\{1}", ReadPath, FileName)))
            {
                File.AppendAllText(string.Format(@"{0}\{1}", CreatePath, FileName), str.Replace("WpfMVVMExample", txtProjectName.Text) + "\r\n");
            }
        }
        private void CreateReplceFile(string ReadPath, string CreatePath, string FileName, string NewName)
        {
            foreach (var str in File.ReadAllLines(string.Format(@"{0}\{1}", ReadPath, FileName)))
            {
                File.AppendAllText(string.Format(@"{0}\{1}", CreatePath, NewName), str.Replace("WpfMVVMExample", txtProjectName.Text) + "\r\n");
                //自己範例的專案名
            }
        }

    }
}
