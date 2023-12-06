using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;
using ENTIDADES;

namespace DAL
{
    //clase que contiene todos los metodos para administrar las obras, clientes,
    //categoria de obras
    //ademas del balance de obra
    public class DALObra
    {
        DALConexion conn = new DALConexion();


        public DALObra()
        {

        }
        

        #region ABM

        //dar de alta una nueva obra
        public void GrabarObra(Obra nuevaObra)
        {   
            //creamos el comando y seteamos sus propiedades
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Obra_Alta";
            //seteamos los parametros
            cmd.Parameters.AddWithValue("@imputacion", nuevaObra.Imputacion);
            cmd.Parameters.AddWithValue("@nombre", nuevaObra.NombreObra);
            cmd.Parameters.AddWithValue("@direccion", nuevaObra.DireccionObra);
            cmd.Parameters.AddWithValue("@categoria", nuevaObra.IdCateObra);          
            cmd.Parameters.AddWithValue("@cliente", nuevaObra.Cliente);
            cmd.Parameters.AddWithValue("@alta", nuevaObra.AltaF);
            cmd.Parameters.AddWithValue("@cuit", nuevaObra.Cuit);
            cmd.Parameters.AddWithValue("@idlocalidad", nuevaObra.IdLocalidad);
            cmd.Parameters.AddWithValue("@idprovincia", nuevaObra.IdProvincia);
            try
            {
                conn.AbriConexion();
                cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        // actualizar los datos de una obra
        public void ActualizarObra(Obra obra_actualizar)
        {
           
            //creamos el comando y seteamos sus propiedades
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Obra_Modifica";
            //seteamos los parametros
            cmd.Parameters.AddWithValue("@idobra", obra_actualizar.IdObra);
            cmd.Parameters.AddWithValue("@imputacion", obra_actualizar.Imputacion);
            cmd.Parameters.AddWithValue("@nombre", obra_actualizar.NombreObra);
            cmd.Parameters.AddWithValue("@direccion", obra_actualizar.DireccionObra);
            cmd.Parameters.AddWithValue("@localidad", obra_actualizar.IdLocalidad);
            cmd.Parameters.AddWithValue("@provincia", obra_actualizar.IdProvincia);
            cmd.Parameters.AddWithValue("@categoria", obra_actualizar.IdCateObra);           
            cmd.Parameters.AddWithValue("@cliente", obra_actualizar.Cliente);
            cmd.Parameters.AddWithValue("@cuit", obra_actualizar.Cuit);
           
            

            try
            {
                conn.AbriConexion();
                 cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
           

        }
        #endregion
        
        //listar todas las obras activas
        public List<Obra>ObrasActivas()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Obras_Activas";
            List<Obra> lista_obras = new List<Obra>();
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                lista_obras = ArmarLista(lectura_reg);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_obras;

        }

        //listar todas las obras inactivas 
        public List<Obra>ObrasInactivas()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Obras_Inactivas";
            List<Obra> lista_obras = new List<Obra>();
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                lista_obras = ArmarLista(lectura_reg);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista_obras;
        }

        //buscar datos de una obra por imputacion
        public Obra BuscarImputacion(int imputacion)
        {
            Obra obra = new Obra();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Obra_Buscar_Imputacion";
            cmd.Parameters.AddWithValue("@imputacion", imputacion);

            try
            {
                conn.AbriConexion();
                SqlDataReader lectura_reg = cmd.ExecuteReader();
                
                obra = ArmarObra(lectura_reg);
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return obra;
        }

        //validar imputacion 
        public bool ValidarNumeroImputacion(int numero_imputacion)
        {
            bool existe_imputacion = false;
          
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Obra_Validar_Imputacion";
            cmd.Parameters.AddWithValue("@imputacion", numero_imputacion);

            try
            {
                conn.AbriConexion();
               SqlDataReader resultadoprocedure = cmd.ExecuteReader();
               
                if (resultadoprocedure.Read())
                {
                    existe_imputacion =true;
                }
                else
                {
                    existe_imputacion = false;
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return existe_imputacion;
        }

        //listar todas las maquinas asignadas a una obra en particular
        public void ListarMaquinasObra(int imputacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa";

        }

        //listar los trabajadores asignados a una obra en particular
        public List<Empleado> ListarEmpleadosObra(int imputacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Obras_Empleados";
            cmd.Parameters.AddWithValue("@imputacion", imputacion);

            List<Empleado> lista = new List<Empleado>();
            try
            {
                conn.AbriConexion();
                SqlDataReader lectura = cmd.ExecuteReader();
                while (lectura.Read())
                {
                    Empleado empleado = new Empleado();
                    empleado.IdEmpleado = (int)lectura["idempleado"];
                    empleado.Nombre = (string)lectura["nombre"];
                    lista.Add(empleado);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }


        //finalizar una obra Una vez finalizada la obra no se puede imputar mas nada
        public void BajaDeUnaObra(int imputacion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Obra_Baja_Una";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@imputacion", imputacion);
            try
            {
                conn.AbriConexion();
                cmd.ExecuteNonQuery();
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
        }
        

        


        public List<CategoriaObra> ObrasCategoria()
        {
            List<CategoriaObra> cateobras = new List<CategoriaObra>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pa_Obra_Listar_Categorias";
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CategoriaObra c = new CategoriaObra();
                    c.IdCateObra = (int)reader["idcateobra"];
                    c.NombreCateO = (string)reader["nombre_categoria"];
                    cateobras.Add(c);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return cateobras;
        }
        
        public List<Casa>ListarCasaUnaObra(int _imputacion)
        {
            List<Casa> casas = new List<Casa>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Casas_De_Una_Obra";
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);

            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Casa c = new Casa();
                    c.IdCasa = (int)reader["idcasa"];
                    c.AltaF = (DateTime)reader["altaf"];
                    if (reader["bajaf"] == DBNull.Value)
                    {
                        c.BajaF = null;
                    }
                    else
                    {

                        c.BajaF = (DateTime)reader["bajaf"];
                    }
                    c.CostoAcumulado = (decimal)reader["costo_acumulado"];
                    c.CostoAlquiler = (decimal)reader["costo_alquiler"];
                    c.Descripcion = (string)reader["descripcion"];
                    c.Direccion = (string)reader["direccion"];
                    c.Estado = (int)reader["estado"];
                    if (reader["fin_alquiler"] == DBNull.Value)
                    {
                        c.FinAlquiler = null;
                    }
                    else
                    {
                        c.FinAlquiler = (DateTime)reader["fin_alquiler"];
                    }
                    c.IdLocalidad = (int)reader["idlocalidad"];
                    c.Imputacion = (int)reader["imputacion"];
                    if (reader["inicio_alquiler"] == DBNull.Value)
                    {
                        c.InicioAlquiler = null;
                    }
                    else
                    {
                        c.InicioAlquiler = (DateTime)reader["inicio_alquiler"];
                    }
                    c.Nota = (string)reader["nota"];
                    c.IdProvincia = (int)reader["idprovincia"];
                    c.Localidad = (string)reader["nomlocalidad"];
                    c.CP = (string)reader["cp"];
                    c.Provincia = (string)reader["nomprovincia"];

                    casas.Add(c);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }

            return casas;
        }


        #region [metodos privados]

        private List<Obra>ArmarLista(SqlDataReader lectura)
        {
            List<Obra> lista = new List<Obra>();
            while (lectura.Read())
            {
                Obra obra = new Obra();
                obra.IdObra = (int)lectura["idobra"];
                obra.IdCateObra = (int)lectura["idcateobra"];
                obra.Imputacion = (int)lectura["imputacion"];
                obra.IdLocalidad = (int)lectura["idlocalidad"];
                obra.IdProvincia = (int)lectura["idprovincia"];
               
                obra.NombreObra = (string)lectura["nomobra"];
                obra.DireccionObra = (string)lectura["direccion"];
                if (lectura["fechainicio"] != DBNull.Value)
                {
                    obra.FechaInicio = (DateTime)lectura["fechainicio"];
                }
                else { obra.FechaInicio = null; }

                if (lectura["fechafin"]!= DBNull.Value)
                {
                    obra.FechaFin = (DateTime) lectura["fechafin"];
                }
                else
                {
                    obra.FechaFin = null;
                }
                
                obra.ValorObra = (decimal)lectura["valorobra"];
                obra.Cuit = (string)lectura["cuit_obra"];
                obra.Cliente = (string)lectura["cliente"];
                obra.Localidad = (string)lectura["nomlocalidad"];
                obra.Provincia = (string)lectura["nomprovincia"];
                obra.CateObra = (string)lectura["nombre_categoria"];
               
                lista.Add(obra);

            }
            return lista;
        }

        private Obra ArmarObra(SqlDataReader lectura)
        {
            Obra obra = new Obra();
            while (lectura.Read())
            {
                obra.IdObra = (int)lectura["idobra"];
                obra.IdCateObra = (int)lectura["idcateobra"];
                obra.Imputacion = (int)lectura["imputacion"];
                obra.IdLocalidad = (int)lectura["idlocalidad"];
                obra.IdProvincia = (int)lectura["idprovincia"];
                obra.Estado = (string)lectura["estado"];
                obra.NombreObra = (string)lectura["nomobra"];
                obra.DireccionObra = (string)lectura["direccion"];
                if (lectura["fechainicio"] != DBNull.Value)
                {
                    obra.FechaInicio = (DateTime)lectura["fechainicio"];
                }
                else
                {
                    obra.FechaInicio = obra.FechaInicio;
                }
                if (lectura["fechafin"] != DBNull.Value)
                {
                    obra.FechaFin = (DateTime)lectura["fechafin"];
                }
                else
                {
                    obra.FechaFin = obra.FechaFin;
                }
               
                obra.ValorObra = (decimal)lectura["valorobra"];
                obra.Cuit = (string)lectura["cuit_obra"];
                obra.Cliente = (string)lectura["cliente"];
                obra.Localidad = (string)lectura["nomlocalidad"];
                obra.Provincia = (string)lectura["nomprovincia"];
                obra.CateObra = (string)lectura["nombre_categoria"];
            }
            return obra;
        }

        #endregion

        #region Vehiculos
         public List<BalanceObraTiposVehiculo>BalanceTiposVehiculo(int _imputacion)
        {
            List<BalanceObraTiposVehiculo> lista = new List<BalanceObraTiposVehiculo>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Obra_Tipos_Vehiculos_Asignados";
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BalanceObraTiposVehiculo bl = new BalanceObraTiposVehiculo();
                    bl.IdCateVh = (int)reader["idcatevh"];
                    bl.NombreCateVh = (string)reader["nomcate"];
                    bl.CantidadAsignada = (int)reader["cantidad"];
                  
                    lista.Add(bl);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public List<Mante_vh> ListarGastosMantenimientoVehiculos(int _imputacion)
        {
            List<Mante_vh> lista = new List<Mante_vh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Obra_Gastos_ManteVH_Una_Imputacion";
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Mante_vh mante_Vh = new Mante_vh();
                    mante_Vh.IdManteVh = (int)reader["idmantevh"];
                    mante_Vh.Importe = (decimal)reader["importe"];

                    lista.Add(mante_Vh);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public List<Asignacion_vh>ListarAsignacionesUnaObra(int _imputacion) 
        {
            List<Asignacion_vh> lista = new List<Asignacion_vh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Obra_Balance_Asignaciones_Una";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Asignacion_vh asig = new Asignacion_vh();
                    asig.IdVh = (int)reader["idvh"];
                    asig.Dominio = (string)reader["dominio"];
                    asig.Modelo = (string)reader["modelo"];
                    asig.IdEmpleado = (int)reader["idempleado"];
                    asig.Categoria = (string)reader["nomcate"];
                    asig.NombreEmpleado = (string)reader["nombre"];
                    asig.DiasAcumulados = (int)reader["diasacu"];
                    asig.HsAcuObra = (decimal)reader["hsacu"];
                    asig.KmAcuObra = (decimal)reader["kmacu"];
                    asig.CostoAsignacion = (decimal)reader["costoasig"];

                    lista.Add(asig);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

       public List<Asignacion_vh>ListaVehiculosUnaObra(int _imputacion)
        {
            List<Asignacion_vh> lista = new List<Asignacion_vh>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Obra_Lista_Vehiculos_Asignados_Una_Imputacion";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Asignacion_vh asig = new Asignacion_vh();
                    asig.IdVh = (int)reader["idvh"];
                    asig.Dominio = (string)reader["dominio"];
                    asig.Modelo = (string)reader["modelo"];
                    asig.Marca = (string)reader["nombre_marca"];
                    asig.Categoria = (string)reader["nomcate"];
                   

                    lista.Add(asig);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }
        #endregion

        #region Productos
        public List<BalanceObraProductos> BalanceTipoProductos(int _imputacion)
        {
            List<BalanceObraProductos> lista = new List<BalanceObraProductos>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Obra_Balance_Tipos_Productos";
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BalanceObraProductos bl = new BalanceObraProductos();
                    bl.IdTipoPro = (int)reader["idtipo_p"];
                    bl.NombreTipo = (string)reader["nomtipo"];
                    bl.CantEntregada = (int)reader["cantenviada"];
                    bl.CostoEntregas = (decimal)reader["costostk"];
                    bl.CantProDistintos = (int)reader["cantProductos"];
                    lista.Add(bl);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public List<BalanceObraCatPro>BalanceCatProducto(int _imputacion, int _idtipoproducto)
        {
            List<BalanceObraCatPro> lista = new List<BalanceObraCatPro>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Obra_Balance_Tipos_Productos";
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            cmd.Parameters.AddWithValue("@idtipo", _idtipoproducto);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BalanceObraCatPro bl = new BalanceObraCatPro();
                    bl.IdCateP = (int)reader["idcate_p"];
                    bl.NombreCateP = (string)reader["nomcate_p"];
                    bl.CantEnviada = (int)reader["cantenviada"];
                    bl.CantDevolucion = (int)reader["cantdevo"];
                    bl.CostoItem = (decimal)reader["costostk"];
                    lista.Add(bl);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        public List<Mante_P> ListarGastosMantenimientoProductos(int _imputacion)
        {
            List<Mante_P> lista = new List<Mante_P>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Obra_Gastos_ManteP_Una_Imputacion";
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Mante_P mante_Vh = new Mante_P();
                    mante_Vh.IdManteP = (int)reader["idmantep"];
                    mante_Vh.ImporteFactura = (decimal)reader["importe_factura"];

                    lista.Add(mante_Vh);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }


        public List<BalanceObraProductosDetalle>BalanceProductosUnaObra(int imputacion)
        {

            List<BalanceObraProductosDetalle> _balance = new List<BalanceObraProductosDetalle>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Obra_Balance_Productos_Una_Imputacion";
            
            cmd.Parameters.AddWithValue("@imputacion", imputacion);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BalanceObraProductosDetalle bl = new BalanceObraProductosDetalle();
                    bl.IdProducto = (int)reader["idproducto"];
                    bl.PrecioEnRemitoProducto = (decimal)reader["preciounitario"];
                    bl.CantEntregada = (int)reader["cantientregada"];
                    bl.CantDevolucion = (int)reader["cantidevolucion"];
                    bl.DifCantidad = (int)reader["difcantidad"];
                    bl.Imputacion = (int)reader["imputacion"];
                    bl.Nombre = (string)reader["nombre"];
                    bl.Modelo = (string)reader["modelo"];
                    bl.Marca = (string)reader["nombre_marca"];
                    bl.CodInventario = (string)reader["cod_inventario"];
                    bl.NumSerie = (string)reader["numserie"];
                    bl.IdTipoP = (int)reader["idtipo_p"];
                    bl.IdCateP = (int)reader["idcate_p"];
                    bl.IdSegP = (int)reader["idseg_p"];
                    bl.Tipo = (string)reader["nomtipo"];
                    bl.Categoria = (string)reader["nomcate_p"];
                    bl.IdEmpleado = (int)reader["idempleado"];
                    bl.NombreEmpleado = (string)reader["nomempleado"];
                    _balance.Add(bl);
                    
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return _balance;
        }

       public List<CategoriaP>BalanceCatePUnaObra(int imputacion)
        {
            List<CategoriaP> _balance = new List<CategoriaP>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandText = "Obra_Balance_CategoriaP";
            cmd.Parameters.AddWithValue("@imputacion", imputacion);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CategoriaP categoriaP = new CategoriaP();
                    categoriaP.IdCateP = (int)reader["idcate_p"];
                    categoriaP.NomCateP = (string)reader["nomcate_p"];
                    categoriaP.IdTipoP = (int)reader["idtipo_P"];
                    _balance.Add(categoriaP);

                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return _balance;
        }
        #endregion

        #region Empleados
        public List<BalanceObraEmpleados>ListarEmpleadosCostoUnaObra(int imputacion)
        {
            List<BalanceObraEmpleados> lista = new List<BalanceObraEmpleados>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Obra_Balance_Empleados_Costo_Productos";
            cmd.Parameters.AddWithValue("@imputacion", imputacion);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BalanceObraEmpleados balance = new BalanceObraEmpleados();
                    balance.IdEmpleado = (int)reader["idempleado"];
                    balance.Dni = (int)reader["dni"];
                    balance.NombreEmpleado = (string)reader["nombre"];
                    balance.CostoHerramientas = (decimal)reader["costoherramientas"];
                      

                    lista.Add(balance);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;

        }

        public List<Empleado>ListarEmpleadosUnaObra(int _imputacion)
        {
            List<Empleado> lista = new List<Empleado>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn.AbriConexion();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Obras_Balance_Empleados_Una";
            cmd.Parameters.AddWithValue("@imputacion", _imputacion);
            try
            {
                conn.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Empleado empleado = new Empleado();
                    empleado.IdEmpleado = (int)reader["idempleado"];
                    empleado.Nombre = (string)reader["nombre"];
                    empleado.Dni = (int)reader["dni"];
                    empleado.IdSector = (int)reader["idsector"];
                    empleado.NomSector = (string)reader["nomsector"];


                    lista.Add(empleado);
                }
                conn.CerrarConexion();
            }
            catch (Exception)
            {

                throw;
            }
            return lista;

        }
        #endregion

    }

}
