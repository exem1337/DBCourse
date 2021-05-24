using System.Data.SqlClient;
using System.Data;
using System;

namespace DBrealkurs
{
    public class Works
    {

        string Credentials = string.Empty;
        SqlConnection connection;

        public Works(string Credentials)
        {
            this.Credentials = Credentials;
            connection = new SqlConnection(Credentials);
            connection.Open(); GC.SuppressFinalize(this);
        }

        ~Works()
        {
            connection.Close();
            
        }

        public DataSet dataSet(string Columns, string Tables, string Arguments)
        {
            DataSet Temp = new DataSet();
            SqlDataAdapter sqlData = new SqlDataAdapter($"SELECT {Columns} FROM {Tables} {Arguments}", connection);
            sqlData.Fill(Temp);
            return Temp;
        }

        // Запосы
        public DataSet ReturnTable(string Columns, string TablesName, string Arguments)
        {
            SqlDataAdapter sqlData = new SqlDataAdapter($"SELECT {Columns} FROM {TablesName} {Arguments}", connection);
            DataSet dataSet = new DataSet();
            sqlData.Fill(dataSet);
            return dataSet;
        }

        public DataSet ReturnQuery(string query)
        {
            SqlDataAdapter sqlData = new SqlDataAdapter(query, connection);
            DataSet dataSet = new DataSet();
            sqlData.Fill(dataSet);
            return dataSet;
        }

        public DataSet returnAuthorsCount(int courseID)
        {
            SqlDataAdapter sqlData = new SqlDataAdapter($"SELECT Автор.authorID, Фамилия FROM [Список_авторов_курса], Автор WHERE CourseID = {courseID} AND Автор.AuthorID = [Список_авторов_курса].AuthorID;", connection);
            DataSet dataSet = new DataSet();
            sqlData.Fill(dataSet);
            return dataSet;
        }

        public DataSet courseAndStudents()
        {
            SqlDataAdapter sqlData = new SqlDataAdapter($"SELECT Фамилия, Имя, Отчество, Название as [Название курса], [Требуемый уровень], Уровень_подготовки_пользователя FROM Пользователь, Курс, Список_Студентов_Курса WHERE CourseID = ID AND Список_Студентов_Курса.userID = Пользователь.userID; ", connection);
            DataSet dataSet = new DataSet();
            sqlData.Fill(dataSet);
            return dataSet;
        }

        #region добавление
        public string addUser(string lastName, string name, string surName)
        {
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO Пользователь (Фамилия, Имя, Отчество) VALUES ('{lastName}', '{name}', '{surName}')", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {
              
                return e.ToString();
            }
        }

        public string newCourseStudent(int userID, int CourseID, int skillLevel)
        {
     
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO Список_Студентов_Курса (courseID, userID, Уровень_подготовки_пользователя) VALUES ({CourseID}, {userID}, {skillLevel})", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {
           
                return e.ToString();
            }
        }

        public string newAuthor(string LastName, string Name, string Surname)
        {
       
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO Автор (Фамилия, Имя, Отчество) VALUES ('{LastName}', '{Name}', '{Surname}')", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {
            
                return e.ToString();
            }
        }

        public string newCourse(int hours, int requiredSkill, DateTime beginDate, DateTime endDate, string courseName)
        {
        
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO Курс (Продолжительность, [Требуемый уровень], [Дата начала], [Дата окончания], Название) VALUES ({hours}, {requiredSkill}, '{beginDate}', '{endDate}', '{courseName}')", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {
              
                return e.ToString();
            }
        }

        public string newAuthorAndCourseRecord(int AuthorID, int CourseID)
        {
        
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO Список_авторов_курса (AuthorID, CourseID) VALUES ({AuthorID}, {CourseID})", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {
         
                return e.ToString();
            }
        }

        #endregion

        public string sqlExecute(string query)
        {
          
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {
              
                return e.ToString();
            }
        } //любой запрос

        #region Удаление
        public string deleteCourse(int courseID)
        {
            try
            {
                SqlCommand command = new SqlCommand($"DELETE FROM Курс WHERE ID = {courseID}", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            { 
                return e.ToString();
            }
        }

        public string deleteUser(int ID)
        {
            try
            {
                SqlCommand command = new SqlCommand($"DELETE FROM Пользователь WHERE userID = {ID}", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string deleteAuthor(int ID)
        {
            try
            {
                SqlCommand command = new SqlCommand($"DELETE FROM Автор WHERE authorID = {ID}", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string deleteAuthorFromAuthorsAndCourse(int id)
        {
            try
            {
                SqlCommand command = new SqlCommand($"DELETE FROM [Список_авторов_курса] WHERE authorID = {id}", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string deleteStudentFromCourse(int courseID, int studentID) 
        {
            try
            {
                SqlCommand command = new SqlCommand($"DELETE FROM [Список_студентов_курса] WHERE courseID = {courseID} AND userID = {studentID}", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        #endregion

        #region update
        public string updateUser(string LastName, string Name, string Surname, int id)
        {
            try
            {
                SqlCommand command = new SqlCommand($"UPDATE Пользователь SET Фамилия = '{LastName}', Имя = '{Name}', Отчество = '{Surname}' WHERE userID = {id};", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }

        public string updateAuthor(string LastName, string Name, string Surname, int id)
        {
            try
            {
                SqlCommand command = new SqlCommand($"UPDATE Автор SET Фамилия = '{LastName}', Имя = '{Name}', Отчество = '{Surname}' WHERE authorID = {id};", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }

        public string updateCourse(int id, int hours, int requiredSkill, DateTime beginDate, DateTime endDate, string courseName)
        {
            try
            {
                SqlCommand command = new SqlCommand($"UPDATE Курс SET Продолжительность = {hours}, [Требуемый уровень] = {requiredSkill}, Название = '{courseName}', [Дата начала] = '{beginDate}', [Дата окончания] = '{endDate}' WHERE ID = {id};", connection);
                return $"Команда выполнена. Задействовано строк таблицы: {command.ExecuteNonQuery()}";
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }

        #endregion

    }
}
