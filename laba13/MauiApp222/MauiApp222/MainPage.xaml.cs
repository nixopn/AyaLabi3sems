using System.Data.SqlClient;

namespace MauiApp222
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        SqlConnection sqlConnection;

        public partial class Student
        {
            public int Id { get; set; }

            public string? Name { get; set; }

            public int GroupId { get; set; }
        }




        public partial class Group
        {
            public int GroupId { get; set; }

            public string GroupName { get; set; }

            public int? Course { get; set; }

            public string Speciality { get; set; }
        }







        public partial class Subject
        {
            public int SubjectId { get; set; }

            public string SubjectName { get; set; }

            public int TeacherId { get; set; }
        }

        public partial class Teacher
        {
            public int TeacherId { get; set; }

            public string Name { get; set; }

            public string Department { get; set; }
        }

        public MainPage()
        {
            InitializeComponent();
            string srvrdbname = "";
            string srvrname = "";
            string srvrusername = "";
            string srvrpassword = "";

            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);

            Console.WriteLine("Podkluchilsya");
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            string sql = $"";
            try
            {

                sqlConnection.Open();
                Console.WriteLine("Podkluchilsya");
                await App.Current.MainPage.DisplayAlert("Trevoga", "Connection sdelano", "Ok");
                sqlConnection.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        private async void get_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Student> students = new List<Student>();
                sqlConnection.Open();
                string queryStringsser = "Select * from dbo.Students";
                SqlCommand sqlCommand = new SqlCommand(queryStringsser, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student() { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString(), GroupId = Convert.ToInt32(reader["GroupId"]) });

                }

                PosmotrertSpisokeeer.ItemsSource = students;


                reader.Close();
                sqlConnection.Close();



            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga",ex.Message,"Ok");
                throw;
            }


        }

        private async void Post_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO dbo.Students VALUES(@Id, @Name, @GroupId)", sqlConnection))
                {
                    command.Parameters.Add(new SqlParameter("Id", Id.Text ));
                    command.Parameters.Add(new SqlParameter("Name", Name.Text));
                    command.Parameters.Add(new SqlParameter("GroupId", GroupId.Text));
                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Dobavil", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");
                
            }
        }

        private async void Put_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                
                int idvstavlerer = Convert.ToInt32(Id.Text);

                string NameUpdteder = Name.Text;

                int idgrvsatvler = Convert.ToInt32(GroupId.Text);

                string qcomandaerer = $"UPDATE dbo.Students SET Id = '{idvstavlerer}', Name='{NameUpdteder}', GroupId='{idgrvsatvler}' WHERE Id= '{idvstavlerer}'";
                using (SqlCommand command = new SqlCommand(qcomandaerer, sqlConnection))
                {

                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Obnovil", "Ok");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");
                throw;
            }
        }

        private async void del_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();

                int idudndvsiudter = Convert.ToInt32(Id.Text);

                using (SqlCommand command = new SqlCommand($"Delete FROM dbo.Students WHERE Id = {idudndvsiudter}", sqlConnection))
                {

                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Vse she vzsayal i udalil, oh", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");
                throw;
            }
        }

        private async void get_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                List<Group> students22 = new List<Group>();
                sqlConnection.Open();
                string queryStringsser = "Select * from dbo.Groups";
                SqlCommand sqlCommand = new SqlCommand(queryStringsser, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    students22.Add(new Group() { GroupId = Convert.ToInt32(reader["GroupId"]), GroupName = reader["GroupName"].ToString(), Course = Convert.ToInt32(reader["Course"]), Speciality = reader["Speciality"].ToString() });

                }

                PosmotrertSpisokeeer22.ItemsSource = students22;


                reader.Close();
                sqlConnection.Close();



            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");
                throw;
            }
        }

        private async void Post_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO dbo.Groups VALUES(@GroupId, @GroupName, @Course, @Speciality)", sqlConnection))
                {
                    command.Parameters.Add(new SqlParameter("GroupId", GroupId2.Text));
                    command.Parameters.Add(new SqlParameter("GroupName", GroupName.Text));
                    command.Parameters.Add(new SqlParameter("Course", Course.Text));
                    command.Parameters.Add(new SqlParameter("Speciality", Speciality.Text));
                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Dobavil", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");

            }
        }

        private async void Put_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();

                int idvstavlerer = Convert.ToInt32(GroupId2.Text);

                string NameUpdteder = GroupName.Text;

                int idgrvsatvler = Convert.ToInt32(Course.Text);

                string NameUpdteder22 = Speciality.Text;

                string qcomandaerer = $"UPDATE dbo.Groups SET GroupId = '{idvstavlerer}', GroupName='{NameUpdteder}', Course='{idgrvsatvler}', Speciality='{NameUpdteder22}' WHERE GroupId= '{idvstavlerer}'";
                using (SqlCommand command = new SqlCommand(qcomandaerer, sqlConnection))
                {

                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Obnovil", "Ok");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");
                throw;
            }
        }

        private async void del_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();

                int idudndvsiudter = Convert.ToInt32(GroupId2.Text);

                using (SqlCommand command = new SqlCommand($"Delete FROM dbo.Groups WHERE GroupId = {idudndvsiudter}", sqlConnection))
                {

                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Vse she vzsayal i udalil, oh", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");
                throw;
            }
        }

        private async void get8_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Subject> students22 = new List<Subject>();
                sqlConnection.Open();
                string queryStringsser = "Select * from dbo.Subjects";
                SqlCommand sqlCommand = new SqlCommand(queryStringsser, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    students22.Add(new Subject() { SubjectId = Convert.ToInt32(reader["SubjectId"]), SubjectName = reader["SubjectName"].ToString(), TeacherId = Convert.ToInt32(reader["TeacherId"])});

                }

                PosmotrertSpisokeeer2222.ItemsSource = students22;


                reader.Close();
                sqlConnection.Close();



            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");
                throw;
            }
        }

        private async void Post8_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO dbo.Subjects VALUES(@SubjectId, @SubjectName, @TeacherId)", sqlConnection))
                {
                    command.Parameters.Add(new SqlParameter("SubjectId", SubjectId.Text));
                    command.Parameters.Add(new SqlParameter("SubjectName", SubjectName.Text));
                    command.Parameters.Add(new SqlParameter("TeacherId", TeacherId.Text));
                    
                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Dobavil", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");

            }
        }

        private async void Put8_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();

                int idvstavlerer = Convert.ToInt32(SubjectId.Text);

                string NameUpdteder = SubjectName.Text;

                int idgrvsatvler = Convert.ToInt32(TeacherId.Text);



                string qcomandaerer = $"UPDATE dbo.Subjects SET SubjectId = '{idvstavlerer}', SubjectName='{NameUpdteder}', TeacherId='{idgrvsatvler}' WHERE SubjectId= '{idvstavlerer}'";
                using (SqlCommand command = new SqlCommand(qcomandaerer, sqlConnection))
                {

                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Obnovil", "Ok");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");


                sqlConnection.Close();




            }
        }

        private async void del8_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();

                int idudndvsiudter = Convert.ToInt32(SubjectId.Text);

                using (SqlCommand command = new SqlCommand($"Delete FROM dbo.Subjects WHERE SubjectId = {idudndvsiudter}", sqlConnection))
                {

                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Vse she vzsayal i udalil, oh", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");
                throw;
            }
        }

        private async void get12_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Teacher> students22 = new List<Teacher>();
                sqlConnection.Open();
                string queryStringsser = "Select * from dbo.Teachers";
                SqlCommand sqlCommand = new SqlCommand(queryStringsser, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    students22.Add(new Teacher() { TeacherId = Convert.ToInt32(reader["TeacherId"]), Name = reader["Name"].ToString(), Department = reader["Department"].ToString() });

                }

                PosmotrertSpisokeeer22222222.ItemsSource = students22;


                reader.Close();
                sqlConnection.Close();



            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");
                throw;
            }
        }

        private async void Post12_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO dbo.Teachers VALUES(@TeacherId, @Name, @Department)", sqlConnection))
                {
                    command.Parameters.Add(new SqlParameter("TeacherId", TeacherId2.Text));
                    command.Parameters.Add(new SqlParameter("Name", Name2.Text));
                    command.Parameters.Add(new SqlParameter("Department", Department.Text));

                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Dobavil", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");

            }
        }



        private async void Put12_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();

                int idvstavlerer = Convert.ToInt32(TeacherId2.Text);

                string NameUpdteder = Name2.Text;

                string idgrvsatvler = Department.Text;



                string qcomandaerer = $"UPDATE dbo.Teachers SET TeacherId = '{idvstavlerer}', Name='{NameUpdteder}', Department='{idgrvsatvler}' WHERE TeacherId= '{idvstavlerer}'";
                using (SqlCommand command = new SqlCommand(qcomandaerer, sqlConnection))
                {

                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Obnovil", "Ok");

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");


                sqlConnection.Close();




            }
        }

        private async void del12_Clicked(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();

                int idudndvsiudter = Convert.ToInt32(TeacherId2.Text);

                using (SqlCommand command = new SqlCommand($"Delete FROM dbo.Teachers WHERE TeacherId = {idudndvsiudter}", sqlConnection))
                {

                    command.ExecuteNonQuery();
                }

                sqlConnection.Close();


                await App.Current.MainPage.DisplayAlert("Trevoga", "Vse she vzsayal i udalil, oh", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Trevoga", ex.Message, "Ok");
                throw;
            }
        }


    }
}


