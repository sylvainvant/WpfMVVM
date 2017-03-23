using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Windows;

namespace WpfMvvmTest
{
    public class CreateEmployeeViewModel : Observable
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        public CreateEmployeeViewModel()
        {
            SaveCommand = new DelegateCommand(Save, () => CanSave);

        }

        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                //NotifyOfPropertyChange("ID");
                DoNotifyChanged("ID");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                //NotifyOfPropertyChange("FirstName");
                DoNotifyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                //NotifyOfPropertyChange("Address");
                DoNotifyChanged("LastName");
            }
        }

        public ICommand SaveCommand { get; private set; }

        public bool CanSave
        {
             get { return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName); }
                
            //get { return !string.IsNullOrEmpty(FirstName); }
    
        }
        
        public void Save()
        {

            SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog=EmployeeDB;Trusted_Connection=Yes;");
            //SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO Employee(EMP_First_Name,EMP_LAST_NAME)VALUES(@FirstName,@LastName)";
            //cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            MessageBox.Show("Data Saved Successfully.");
        }

        /*private void doNotify(string propertyName)
        {
            var p = PropertyChanged;
            if (p == null) return;
            p(this, new PropertyChangedEventArgs(propertyName));
        }*/

        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
