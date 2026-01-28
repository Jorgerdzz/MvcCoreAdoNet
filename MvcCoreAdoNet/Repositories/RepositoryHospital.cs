using Microsoft.Data.SqlClient;
using MvcCoreAdoNet.Models;

namespace MvcCoreAdoNet.Repositories
{
    public class RepositoryHospital
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        public RepositoryHospital()
        {
            string connectionString = @"Data Source=LOCALHOST\DEVELOPER;Initial Catalog=HOSPITAL;User ID=SA;Trust Server Certificate=True";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            string sql = "select * from HOSPITAL";
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<Hospital> hospitales = new List<Hospital>();
            while(await this.reader.ReadAsync())
            {
                Hospital h = new Hospital();
                h.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
                h.Nombre = this.reader["NOMBRE"].ToString();
                h.Direccion = this.reader["DIRECCION"].ToString();
                h.Telefono = this.reader["TELEFONO"].ToString();
                h.Camas = int.Parse(this.reader["NUM_CAMA"].ToString());
                hospitales.Add(h);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return hospitales;
        }

        public async Task<Hospital> FindHospitalAsync (int idHospital)
        {
            string sql = "select * from HOSPITAL where HOSPITAL_COD=@hospitalcod";
            this.com.Parameters.AddWithValue("@hospitalcod", idHospital);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            Hospital hospital = new Hospital();
            await this.reader.ReadAsync();
            hospital.IdHospital = int.Parse(this.reader["HOSPITAL_COD"].ToString());
            hospital.Nombre = this.reader["NOMBRE"].ToString();
            hospital.Direccion = this.reader["DIRECCION"].ToString();
            hospital.Telefono = this.reader["TELEFONO"].ToString();
            hospital.Camas = int.Parse(this.reader["NUM_CAMA"].ToString());
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return hospital;

        }

        public async Task InsertHospitalesAsync(int id, string nombre, string direccion, string telefono, int camas)
        {
            string sql = "insert into HOSPITAL values(@id, @nombre, @direccion, @telefono, @camas)";
            this.com.Parameters.AddWithValue("@id", id);
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@direccion", direccion);
            this.com.Parameters.AddWithValue("@telefono", telefono);
            this.com.Parameters.AddWithValue("@camas", camas);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();

        }

        public async Task UpdateHospitalAsync(int id, string nombre, string direccion, string telefono, int camas)
        {
            string sql = "update HOSPITAL set NOMBRE=@nombre, DIRECCION=@direccion, TELEFONO=@telefono, NUM_CAMA=@camas" +
                " where HOSPITAL_COD=@id";
            this.com.Parameters.AddWithValue("@nombre", nombre);
            this.com.Parameters.AddWithValue("@direccion", direccion);
            this.com.Parameters.AddWithValue("@telefono", telefono);
            this.com.Parameters.AddWithValue("@camas", camas);
            this.com.Parameters.AddWithValue("@id", id);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task DeleteHospitalAsync(int id)
        {
            string sql = "delete from HOSPITAL where HOSPITAL_COD = @id";
            this.com.Parameters.AddWithValue("@id", id);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

    }
}
