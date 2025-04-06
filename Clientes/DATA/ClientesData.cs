using Clientes.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
namespace Clientes.DATA
{
    public class ClientesData
    {
        private readonly string conexion;

        public ClientesData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSql")!;
        }

        //Metodo llamar a lista
        public async Task<List<Cliente>> Lista()
        {
            List<Cliente> Lista = new List<Cliente>();
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_GetClientes", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Lista.Add(new Cliente
                        {
                            cliente_id = Convert.ToInt32(reader["cliente_id"]),
                            nit = Convert.ToInt32(reader["nit"]),
                            nombre = reader["nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            email = reader["email"].ToString(),
                            telefono = Convert.ToInt32(reader["telefono"]),
                            direccion = reader["direccion"].ToString()
                        });
                    }
                }
            }
            return Lista;
        }
        //Metodo Obtener
        public async Task<Cliente> Obtener(int cliente_id)
        {
            Cliente objeto = new Cliente();
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_cliente", con);
                cmd.Parameters.AddWithValue("@cliente_id", cliente_id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Cliente
                        {
                            cliente_id = Convert.ToInt32(reader["cliente_id"]),
                            nit = Convert.ToInt32(reader["nit"]),
                            nombre = reader["nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            email = reader["email"].ToString(),
                            telefono = Convert.ToInt32(reader["telefono"]),
                            direccion = reader["direccion"].ToString()
                        };
                    }
                }
            }
            return objeto;
        }
        //Metodo Agregar
        public async Task<bool> Agregar(Cliente objeto)

        {
            bool respuesta = true;
            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_CrearClientes", con);
                cmd.Parameters.AddWithValue("@nit", objeto.nit);
                cmd.Parameters.AddWithValue("@nombre", objeto.nombre);
                cmd.Parameters.AddWithValue("@Apellido", objeto.Apellido);
                cmd.Parameters.AddWithValue("@email", objeto.email);
                cmd.Parameters.AddWithValue("@telefono", objeto.telefono);
                cmd.Parameters.AddWithValue("@direccion", objeto.direccion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
        //Metodo Editar
        public async Task<bool> Editar(Cliente objeto)
        {
            bool respuesta = true;
            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_EditarClientes", con);
                cmd.Parameters.AddWithValue("@nit", objeto.nit);
                cmd.Parameters.AddWithValue("@nombre", objeto.nombre);
                cmd.Parameters.AddWithValue("@Apellido", objeto.Apellido);
                cmd.Parameters.AddWithValue("@email", objeto.email);
                cmd.Parameters.AddWithValue("@telefono", objeto.telefono);
                cmd.Parameters.AddWithValue("@direccion", objeto.direccion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = true;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
        //Metodo Eliminar
        public async Task<bool> Eliminar(int nit)
        {
            bool respuesta = true;
            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_EliminarClientes", con);
                cmd.Parameters.AddWithValue("@nit", nit);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}