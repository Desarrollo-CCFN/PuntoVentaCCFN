using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Capa_Entidad;
using System.Diagnostics;
using System.Text.RegularExpressions;

 
 



namespace Capa_Datos
{
    public class CD_Denominacion
    {

         
        private CE_Denominacion? ce = new CE_Denominacion();
       
        private readonly CD_Conexion conn = new CD_Conexion();

        #region Consultar
        public List<CE_Denominacion> consulta(string _PayForm)
        {
            List<CE_Denominacion> denominaciones = new List<CE_Denominacion>();

            MySqlDataAdapter da = new MySqlDataAdapter("SP_TC_CashDenom", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("_PayForm", MySqlDbType.String).Value = _PayForm;
            DataSet ds = new DataSet();

            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                CE_Denominacion ce = new CE_Denominacion
                {
                    IdCDenom = Convert.ToInt32(row["Id"]),
                    PayForm = Convert.ToString(row["PayForm"]),
                    Descrip = Convert.ToString(row["Descrip"]),
                    AmountValue = Convert.ToDecimal(row["AmountValue"])
                };
                denominaciones.Add(ce);
            }

            return denominaciones;
        }
        #endregion


        #region Consultar
        public List<CE_Denominacion> Cajeras(string DfltsGroup)
        {
            List<CE_Denominacion> cajeras = new List<CE_Denominacion>();


            MySqlDataAdapter da = new MySqlDataAdapter("SP_V_Cajeras", conn.AbrirConexion());
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("_DfltsGroup", MySqlDbType.String).Value = DfltsGroup;
            DataSet ds = new DataSet();

            ds.Clear();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                CE_Denominacion ce = new CE_Denominacion
                {
                    Name = Convert.ToString(row["U_NAME"]) ,
                    INTERNAL_K = Convert.ToInt32(row["INTERNAL_K"])
                  //  AmountValue = Convert.ToDecimal(row["AmountValue"])
                };
                cajeras.Add(ce);
            }


           
            return cajeras;
        }
        #endregion
         

        #region InsertarRetiro

        public string InsertarRetiro(CE_Denominacion retiro)
        {
               

            try
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn.AbrirConexion();
                        cmd.CommandText = "SP_V_VentaCajaRetiro";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("_PayForm", retiro.PayForm);
                        cmd.Parameters.AddWithValue("_IdCDenom", retiro.IdCDenom);
                        cmd.Parameters.AddWithValue("_Quantity", retiro.Quantity);
                        cmd.Parameters.AddWithValue("_TotAmount", retiro.TotAmount);
                        cmd.Parameters.AddWithValue("_User", retiro.Usuario);
                        cmd.Parameters.AddWithValue("_Super", retiro.Super);
                        cmd.Parameters.AddWithValue("_Sucursal", retiro.Sucursal);
                        cmd.Parameters.AddWithValue("_StationId", retiro.StationId);
                        cmd.Parameters.AddWithValue("_StatusCaja", retiro.StatusCaja);



                    // IN _Serie Varchar(3),

                    // Ejecutar el procedimiento almacenado y capturar el IdCash devuelto
                     var idCash = Convert.ToInt32(cmd.ExecuteScalar());
                  
                     

                    // Verificar si se obtuvo un IdCash válido
                    if (idCash > 10)
                        {
                            try
                            {
                            

                                // Crear una instancia de ProcessStartInfo
                                ProcessStartInfo startInfo = new ProcessStartInfo();
                                startInfo.FileName = @"C:\PuntoVenta\impresora\WindowsTesoreria.exe"; // Ruta completa al ejecutable de RetirosCaja
                            //startInfo.Arguments = idCash.ToString(); // Pasar el IdCash como argumento
                                 startInfo.Arguments = $"{idCash}";
                          //  startInfo.Arguments = $"{idCash} {Param}";

                            // Ejecutar el programa externo
                            Process.Start(startInfo);

                            return "El retiro se ha registrado correctamente.";
                        }
                            catch (Exception ex)
                            {
                                // Manejo de errores al intentar ejecutar el programa externo
                               // Console.WriteLine($"Error al ejecutar el programa externo: {ex.Message}", "Error");
                                return $"Error al ejecutar el programa externo: {ex.Message}";
                        }
                        }
                        else
                        {
                        if (idCash == 1) {
                            return  "EXISTE UNA APERTURA EN PESOS EN ESTA CAJA SE DEBE CERRAR ANTES";



                        } 
                        else if (idCash == 2)
                        {

                            return "EXISTE UNA APERTURA EN DOLARES EN ESTA CAJA SE DEBE CERRAR ANTES";

                        }


                        else if (idCash == 3)
                        {

                            return "NO EXISTE REGISTRO DE UNA APERTURA EN PESOS EN ESTA CAJA SE DEBE REALIZAR ANTES UNA APERTURA";

                        }

                        else if (idCash == 4)
                        {

                            return "NO EXISTE REGISTRO DE UNA APERTURA EN DOLARES EN ESTA CAJA SE DEBE REALIZAR ANTES UNA APERTURA";

                        } 
                        else
                        {

                            // Console.WriteLine("No se pudo obtener un IdCash válido.", "Error");
                            return "No se pudo obtener un IdCash válido.";

                        } 

                        }
                    }
                }
                catch (MySqlException ex)
                {
                // Errores específicos de MySQL
                // Console.WriteLine($"Error en la base de datos: {ex.Message}", "Error");
                return $"Error en la base de datos: {ex.Message}";
            }
                catch (Exception ex)
                {
                // otro tipo de error
                //Console.WriteLine($"Ocurrió un error: {ex.Message}", "Error");
                return $"Ocurrió un error: {ex.Message}";
            }
                finally
                {
                    conn.CerrarConexion();
                }
            }
        #endregion



        #region Movimiento Caja
        public bool VerificarCaja(int stationId, string Sucursal, ref string sMensaje)
        {
            string Cadena = "";
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SP_V_VerificarCaja", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_stationId", stationId);
                da.SelectCommand.Parameters.AddWithValue("_sucursal", Sucursal);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        ce.status = Convert.ToString(row[0]);
                        ce.INTERNAL_K = Convert.ToInt32(row[2]);

                        if (ce.status.Equals("O"))
                        {
                            sMensaje = ce.INTERNAL_K.ToString();
                            return true;
                        }
                    }
                }

                conn.CerrarConexion();
                sMensaje = "Caja Cerrada";
                return false;
            }
            catch (Exception ex)
            {
                conn.CerrarConexion();
                sMensaje = "Ocurrio un error al verificar caja";
                return false;
            }
        }




        #endregion

        #region verificar id cash
        public CE_Denominacion GetIdCash(int stationId, string Sucursal, string Cashier, ref string sMensaje)
        {
            
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SP_V_GETIDCASH", conn.AbrirConexion());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_StationId", stationId);
                da.SelectCommand.Parameters.AddWithValue("_WhsCode", Sucursal);
                da.SelectCommand.Parameters.AddWithValue("_Cashier", Cashier);

                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        ce.IdCash = Convert.ToInt32(row[0]);

                    }
                }

                conn.CerrarConexion();
                return ce;
            }
            catch (Exception ex)
            {
                conn.CerrarConexion();
                sMensaje = "Ocurrio un error al verificar caja";
                return ce;
            }
        }
        #endregion

    }
}
