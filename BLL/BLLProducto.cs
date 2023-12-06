using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections.ObjectModel;

using ENTIDADES;
using DAL;

namespace BLL
{
    public class BLLProducto
    {

        DALProducto dAL = new DALProducto();
        DALConexion conexion = new DALConexion();
        public BLLProducto()
        { }


        #region ABMProducto

        
        public void ProductoAtlta(Producto producto)
        {
            dAL.ProductoAtlta(producto);
        }
        public void ProductoModificar(Producto producto)
        {
            dAL.ProductoModificar(producto);

        }
        public ObservableCollection<Producto> ListarTodos()
        {
            ObservableCollection<Producto> productos = dAL.ListarTodos();

            return productos;
        }
        public Producto BuscarDatosUno(int _idproducto)
        {
            Producto producto = dAL.BuscarDatosUno(_idproducto);
            return producto;
        }
        public void ProductoCambiarEstado(int producto, int nuevo_estado)
        {
            dAL.ProductoCambiarEstado(producto, nuevo_estado);
        }
        public Producto ObtenerUltimoId()
        {

            Producto ultimo = dAL.ObtenerUltimoId();

            return ultimo;
        }

        public void LiquidarElStockPorBaja(int _idproducto, int iddepo)
        {
            dAL.LiquidarElStockPorBaja(_idproducto, iddepo);
        }
        public void BajaUnProducto(int idproducto, int idcausabaja, int idusuario, int imputacion, string descripcion, DateTime fecha)
        {
            dAL.BajaUnProducto(idproducto, idcausabaja, idusuario, imputacion, descripcion, fecha);
        }

        public ObservableCollection<Producto> ListarTodosBajas()
        {
            ObservableCollection<Producto> productos = dAL.ListarTodosBajas();
          
            return productos;
        }

        #endregion

        #region Tipos,Categorias,Segmentos,Marcas



        public bool MarcaProductoAlta(string _nombreMarca)
        {
            int _rows = dAL.MarcaProductoAlta(_nombreMarca);

            if (_rows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool MarcaProductoModifica(int _idmarca, string _nombreMarca)
        {
            int _rows = dAL.MarcaProductoModifica(_idmarca, _nombreMarca);

            if (_rows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool MarcaProductoBaja(int _idmarca)
        {
            int _rows = dAL.MarcaProductoBaja(_idmarca);

            if (_rows == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TipoProductoAlta(string _nombre)
        {
            dAL.TipoProductoAlta(_nombre);
        }

        public void TipoProductoModi(int _idtipo, string _nom)
        {
            dAL.TipoProductoModi(_idtipo, _nom);
        }

        public VarPrecioTipoP BuscarUltimaVariacionPrecioUnTipoProducto(int idtipo_p)
        {
            VarPrecioTipoP varPrecio = dAL.BuscarUltimaVariacionPrecioUnTipoProducto(idtipo_p);
            
            return varPrecio;
        }

        public void CateProductoAlta(int _idtipoP, string _nombre, decimal _costo)
        {
            dAL.CateProductoAlta(_idtipoP, _nombre, _costo);
        }

        public void CateProductoModi(int _id, string _nom, decimal _costo)
        {
            dAL.CateProductoModi(_id, _nom, _costo);
        }

        public void CateProductoBaja(int _id)
        {
            dAL.CateProductoBaja(_id);
        }

        public void SegProductoAlta(int _idcate, string _nombre, decimal precio)
        {
            dAL.SegProductoAlta(_idcate, _nombre, precio);
        }

        public void SegProductoModi(SegmentoP segmento)
        {
            dAL.SegProductoModi(segmento);
        }

        public void SegProductoBaja(int _id)
        {
            dAL.SegProductoBaja(_id);
        }

        public SegmentoP BuscarUnSegmento(int idsegmento)
        {
            SegmentoP segmento = dAL.BuscarUnSegmento(idsegmento);
           
            return segmento;
        }
        public ObservableCollection<TipoProducto> ListarTipos()
        {
            ObservableCollection<TipoProducto> tipoProductos = dAL.ListarTipos();

            return tipoProductos;
        }

        public ObservableCollection<CategoriaP> ListarCategoriasTipo(int idtipo)
        {
            ObservableCollection<CategoriaP> categoriaPs = dAL.ListarCategoriasTipo(idtipo);

            return categoriaPs;
        }

        public ObservableCollection<CategoriaP> ListarCategorias()
        {
            ObservableCollection<CategoriaP> categoriaPs = dAL.ListarCategorias();

            return categoriaPs;
        }
        public ObservableCollection<SegmentoP> ListarSegmentoCategoria(int idcategoria)
        {
            ObservableCollection<SegmentoP> segmentoPs = dAL.ListarSegmentoCategoria(idcategoria);

            return segmentoPs;
        }

        public ObservableCollection<SegmentoP> ListarSegmentos()
        {
            ObservableCollection<SegmentoP> segmentoPs = dAL.ListarSegmentos();

            return segmentoPs;
        }

        public ObservableCollection<MarcaProductos> ListarMarcas()
        {
            ObservableCollection<MarcaProductos> marcaProductos = dAL.ListarMarcas();

            return marcaProductos;
        }


        public List<SegmentoP> RepuestosVhListarCategorias()
        {

            List<SegmentoP> cate_repuestos = new List<SegmentoP>();
            cate_repuestos = dAL.RepuestosVhListarCategorias();

            return cate_repuestos;
        }

        public ObservableCollection<Producto> ListarTodosTipoCategoriaSegmento(int idtipo, int idcate, int idseg)
        {
            ObservableCollection<Producto> productos = dAL.ListarTodosTipoCategoriaSegmento(idtipo, idcate, idseg);

            return productos;
        }

        public ObservableCollection<ConsultaExistencias> ConsultaExistenciaEnObra(int idcategoria)
        {
            ObservableCollection<ConsultaExistencias> categoriaPs = dAL.ConsultaExistenciaEnObra(idcategoria);
           
            return categoriaPs;
        }
        public ObservableCollection<ConsultaExistencias> ConsultaExistenciaEnStock(int idcategoria)
        {
            ObservableCollection<ConsultaExistencias> categoriaPs = dAL.ConsultaExistenciaEnStock(idcategoria);
           
            return categoriaPs;
        }

        public List<CausaAjusteStock> ListaCausaAjusteStock()
        {
            List<CausaAjusteStock> lista_causa = dAL.ListaCausaAjusteStock();
           

            return lista_causa;
        }

        public ObservableCollection<Producto> ListarProductosDeUnTipo(int idtipo)
        {
            ObservableCollection<Producto> lista = dAL.ListarProductosDeUnTipo(idtipo);
            
            return lista;
        }
        #endregion




        #region Stock


        public ObservableCollection<StockProducto> SelectorProducto()
        {

            ObservableCollection<StockProducto> stockProductos = dAL.SelectorProducto();
            return stockProductos;
        }

        public ObservableCollection<StockProducto> SelectorProductoPorDeposito(int _iddeposito)
        {

            ObservableCollection<StockProducto> stockProductos = dAL.SelectorProductoPorDeposito(_iddeposito);
            return stockProductos;
        }

      
        public void ActualizarStock(int producto, int cantidad, int deposito, string operacion, decimal _preciop)
        {
            dAL.ActualizarStock(producto, cantidad, deposito, operacion,_preciop);
        }

        public void AltaEnStock(int producto, int cantidad, int deposito)
        {
            dAL.AltaEnStock(producto, cantidad, deposito);
        }

        public void ActualizarSituacionFisica(int producto, int sf)
        {
            dAL.ActualizarSituacionFisica(producto, sf);

        }
        

        public ObservableCollection<StockProducto> StockDeUnProducto(int _idproducto)
        {

            ObservableCollection<StockProducto> stockProductos = dAL.StockDeUnProducto(_idproducto);
           
            return stockProductos;
        }
        public void ActualizarCostoStockUno(int _idproducto, decimal _precio)
        {
            dAL.ActualizarCostoStockUno(_idproducto, _precio);
        }
        


        public void ActualizarPuntoPedido(int idstk, int iddepo, decimal nuevopp)
        {
            dAL.ActualizarPuntoPedido(idstk, iddepo, nuevopp);
        }

        public void ActualizarPosicionProducto(int idstk, int iddepo, string frente, string col, string estante, string fila)
        {
            dAL.ActualizarPosicionProducto(idstk, iddepo, frente, col, estante, fila);
        }

        public void AjustarStockUnProducto(int idusuario, int idproducto, int cantidad, int iddeposito, int idcausa)
        {
            dAL.AjustarStockUnProducto(idusuario, idproducto, cantidad, iddeposito, idcausa);
        }

        public bool ValidarExistenciaDeStockUnProducto(int _idproducto)
        {
            bool existeStock;
           
            decimal stock_actual = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "Producto_Validar_Existencia_Stock";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idproducto", _idproducto);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   
                    stock_actual = (decimal)reader["stkactual"];
                }
                if (stock_actual>0)
                {
                    existeStock = true;
                }
                else
                {
                    existeStock = false;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return existeStock;
        }
        public List<SituacionStk> ListarSituacionStk()
        {
           
            List<SituacionStk> lista = dAL.ListarSituacionStk();
            
            return lista;
        }

        public void CambiarSituacionStockcUnProducto(int _idpro, int _idsituacion)
        {
            dAL.CambiarSituacionStockUnProducto(_idpro, _idsituacion);

        }

        public void RecalcularCostoStock(int idproducto, int iddeposito, decimal precio_producto)
        {
            dAL.RecalcularCostoStock(idproducto, iddeposito, precio_producto);

        }

        public void ActualizarHistorialPrecios(int idproducto, decimal precio)
        {
            dAL.ActualizarHistorialPrecios(idproducto, precio);
        }

        public decimal StockUnProductoUnDeposito(int _idproducto, int iddeposito)
        {
            decimal stockProducto = dAL.StockUnProductoUnDeposito(_idproducto,iddeposito);
           
            return stockProducto;
        }

        public List<AjusteStockProducto> ListarAjustesUnProducto(int _idproducto)
        {
            List<AjusteStockProducto> list_ajustes = dAL.ListarAjustesUnProducto(_idproducto);
           
            return list_ajustes;
        }


        public ObservableCollection<CategoriaP> ListarStockActualCategoriasIndumentaria()
        {
            ObservableCollection<CategoriaP> lista_stock = dAL.ListarStockActualCategoriasIndumentaria();

            return lista_stock;
        }

        public ObservableCollection<StockProducto> ListarStockActualIndumentariaUnaCategoria(int idcategoria, int iddeposito)
        {
            ObservableCollection<StockProducto> lista_stock = dAL.ListarStockActualIndumentariaUnaCategoria(idcategoria,iddeposito);
          
                 
            return lista_stock;
        }


        public ObservableCollection<CategoriaP> ListarEntregasIndumentariaAnio(int _anio, int _iddepo)
        {
            ObservableCollection<CategoriaP> lista_stock = dAL.ListarEntregasIndumentariaAnio(_anio,_iddepo);
           
            return lista_stock;
        }

        public ObservableCollection<StockProducto> ListarDetalleUnaCategoriaAnio(int _idcate, int _iddepo, int _anio)
        {
            ObservableCollection<StockProducto> lista_stock = dAL.ListarDetalleUnaCategoriaAnio(_idcate,_iddepo,_anio);
          
            return lista_stock;

        }

        public ObservableCollection<CategoriaP> ListarEntregasIndumentarioF1F2(int _iddepo, DateTime _desde, DateTime _hasta)
        {
            ObservableCollection<CategoriaP> lista_stock = dAL.ListarEntregasIndumentarioF1F2(_iddepo,_desde,_hasta);
           
            return lista_stock;
        }

        public ObservableCollection<StockProducto> ListarDetalleUnaCategoriaF1F2(int _idcate, int _iddepo, DateTime _desde, DateTime _hasta)
        {
            ObservableCollection<StockProducto> lista_stock = dAL.ListarDetalleUnaCategoriaF1F2(_idcate, _iddepo, _desde, _hasta);
           
            return lista_stock;
        }

        public ObservableCollection<StockTipoProducto> ListarStockActualTipoProductos(int _iddeposito, int _idsf)
        {
            ObservableCollection<StockTipoProducto> lista_stock = dAL.ListarStockActualTipoProductos(_iddeposito, _idsf);
           
            return lista_stock;
        }
        public ObservableCollection<StockCategoriaProducto> ListarStockActualCategoriasTipoProducto(int idtipo_p, int _iddeposito, int _idsf)
        {
            ObservableCollection<StockCategoriaProducto> lista_stock = dAL.ListarStockActualCategoriasTipoProducto(idtipo_p, _iddeposito,_idsf);
           
            return lista_stock;
        }

        public ObservableCollection<StockTipoProducto> ListarStockPPTipoProducto(int _iddeposito, int _idsf)
        {
            ObservableCollection<StockTipoProducto> lista_stock = dAL.ListarStockPPTipoProducto(_iddeposito,_idsf);
           
            return lista_stock;
        }
        public ObservableCollection<StockCategoriaProducto> ListarStockPPCategoriasTipoProducto(int idtipo_p, int _iddeposito, int _idsf)
        {
            ObservableCollection<StockCategoriaProducto> lista_stock = dAL.ListarStockPPCategoriasTipoProducto(idtipo_p,_iddeposito,_idsf);
           
            return lista_stock;
        }

        public ObservableCollection<StockProducto> ListarCantProXCateMante(int _idcate)
        {
            ObservableCollection<StockProducto> lista_stock = dAL.ListarCantProXCateMante(_idcate);
           
            return lista_stock;

        }

        public ObservableCollection<StockProducto> ListarCantProXCateActivo(int _idcate, int _iddeposito, int _idsf)
        {
            ObservableCollection<StockProducto> lista_stock = dAL.ListarCantProXCateActivo(_idcate,_iddeposito,_idsf);
            
            return lista_stock;
        }
        public ObservableCollection<StockProducto> ListarCantProXCatePP(int _idcate, int _iddeposito, int _idsf)
        {
            ObservableCollection<StockProducto> lista_stock = dAL.ListarCantProXCatePP(_idcate,_iddeposito,_idsf);
           
            return lista_stock;
        }


        public List<StockProducto> ListarProductosEnPP(int iddeposito)
        {
            List<StockProducto> lista_stock = dAL.ListarProductosEnPP(iddeposito);
            return lista_stock;

           
        }

        #endregion

        #region Compras

        public void CompraEncabezadoAlta(CompraP nueva_compraP)
        {
            dAL.CompraEncabezadoAlta(nueva_compraP);
        }
        public void CompraDetalleAlta(CompraPDetalle nueva_compraPDetalle)
        {
            dAL.CompraDetalleAlta(nueva_compraPDetalle);
        }
        public int ObtenerUltimoIdCompra()
        {
            int _ultimoIdDoc = dAL.ObtenerUltimoIdCompra();
            return _ultimoIdDoc;
        }
        public void ActualizarRegistradosDIP(int _idremito)
        {
            dAL.ActualizarRegistradosDIP(_idremito);

        }
        public void ActualizarPreciosProductos(int idpro, decimal precio)
        {
            dAL.ActualizarPreciosProductos(idpro, precio);
        }
        public ObservableCollection<CompraP> ComprasListarTodos()
        {
            ObservableCollection<CompraP> lista_compras = dAL.ComprasListarTodos();

            return lista_compras;
        }

        public ObservableCollection<CompraPDetalle> CompraListarDetalleUna(int _idcompra)
        {
            ObservableCollection<CompraPDetalle> lista_det_compras = dAL.CompraListarDetalleUna(_idcompra);

            return lista_det_compras;
        }
        public ObservableCollection<CompraP> CompraListarUnProducto(int idproducto)
        {
            ObservableCollection<CompraP> lista_compras = dAL.CompraListarUnProducto(idproducto);

            return lista_compras;

        }
        public void CompraBorrarUna(int idcompra)
        {
            dAL.CompraBorrarUna(idcompra);
        }

        public void AnularRegistradosDIP(int _idremito)
        {
            dAL.AnularRegistradosDIP(_idremito);
        }

        public void ActualizarDetDocuARegistrado(int _idremito, int _idproducto)
        {
            dAL.ActualizarDetDocuARegistrado(_idremito, _idproducto);
        }
        #endregion


        #region Fotos
        public void GuardarFoto(FotoProducto foto)
        {
            dAL.GuardarFoto(foto);
        }

        public void BorrarFoto(int _idfoto)
        {
            dAL.BorrarFoto(_idfoto);

        }

        public ObservableCollection<FotoProducto> ProductoListaFotos(int _idproducto)
        {
            ObservableCollection<FotoProducto> fotos = new ObservableCollection<FotoProducto>();
            fotos = dAL.ProductoListaFotos(_idproducto);
            return fotos;
        }



        #endregion

        #region Mantenimientos
        public void AltaMantenimiento(Mante_P mante_P)
        {
            dAL.AltaMantenimiento(mante_P);

        }

        public void AnularMantenimiento(int _idmantep)
        {
            dAL.AnularMantenimiento(_idmantep);
        }

        public ObservableCollection<Mante_P> ListarManteUnProducto(int _idproducto)
        {
            //listar todos los mantenimientos de un producto
            ObservableCollection<Mante_P> mante_Ps = dAL.ListarManteUnProducto(_idproducto);
            
            return mante_Ps;
        }

        public ObservableCollection<Mante_P> ListarTodosLosMantenimientos(DateTime desde, DateTime hasta)
        {
            //listar todos los mantenimientos de un producto
            ObservableCollection<Mante_P> mante_Ps = dAL.ListarTodosLosMantenimientos(desde, hasta);
          
            return mante_Ps;
        }

        public void NuevoRMAProducto(RMAProducto rMA)
        {
            dAL.NuevoRMAProducto(rMA);
        }

        public void BajaRMAProducto(int idrma)
        {
            dAL.BajaRMAProducto(idrma);
        }

        public void CambiarEstadoRMAProducto(int idrma, int idestado,DateTime _fecha, int _iduser)
        {
            dAL.CambiarEstadoRMAProducto(idrma, idestado, _fecha, _iduser);

        }

        public ObservableCollection<RMAProducto> ListarRMAUnProducto(int idproducto)
        {
            ObservableCollection<RMAProducto> lista_rma = dAL.ListarRMAUnProducto(idproducto);
           
            return lista_rma;
        }

        public int ObtenerUltimoIdRMA()
        {
            int _ultimoIdDoc = dAL.ObtenerUltimoIdRMA();

          
            return _ultimoIdDoc;
        }

        public ObservableCollection<RMAProducto> RMAListarTodos()
        {
            ObservableCollection<RMAProducto> lista_rma = dAL.RMAListarTodos();
            
            return lista_rma;
        }

        public bool ValidarRMA( int _idrma)
        {
            int _idrma2=0;
            bool valido;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion.AbriConexion();
            cmd.CommandText = "RMA_Buscar_Una";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idrma", _idrma);
            try
            {
                conexion.AbriConexion();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["idrma"] == null)
                    {
                        _idrma2 = 0;
                    }
                    else
                    {
                        _idrma2 = (int)reader["idrma"];
                    }
                }
                conexion.CerrarConexion();
                if (_idrma == _idrma2)
                {
                    valido = true;
                }
                else
                {
                    valido = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return valido;
        }

        public RMAProducto BuscarUnRMAPorId(int idrma)
        {
            RMAProducto rMA = dAL.BuscarUnRMAPorId(idrma);
            return rMA;
        }
            #endregion

            #region Movimientos
            public ObservableCollection<Documento> ListarMovObraUnProducto(int _idproducto)
        {
            ObservableCollection<Documento> lista_mov = dAL.ListarMovObraUnProducto(_idproducto);
           
            return lista_mov;
        }

        #endregion

        #region Instrumentos

      

        public void InstrumentoModificar(string escala, string rango, string exactitud, int patron, int contraste, int idproducto)
        {
            dAL.InstrumentoModificar(escala, rango, exactitud, patron, contraste, idproducto);
        }


        #endregion

        #region Costos
        public decimal CostoTotalComprasUnAnio(int _anio)
        {
            decimal _costo = dAL.CostoTotalComprasUnAnio(_anio);
           

            return _costo;
        }

        public decimal CostoTotalMantenimientoUnAnio(int _anio)
        {
            decimal _costo = dAL.CostoTotalMantenimientoUnAnio(_anio);
            

            return _costo;

        }

        //costo mensuales 
        public List<CostosGenericos> CostoMantenimientoAnioMes(int _anio)
        {
            List<CostosGenericos> lista = dAL.CostoMantenimientoAnioMes(_anio);
           
            return lista;
        }

        // costo de las compras mes a mes anual
        public List<CostosGenericos> CostoComprasAnioMes(int _anio)
        {
            List<CostosGenericos> lista = dAL.CostoComprasAnioMes(_anio);
           
            return lista;
        }


        public List<CostosGenericos> CostosComprasTipoProductoAnio(int anio)
        {
            List<CostosGenericos> lista = dAL.CostosComprasTipoProductoAnio(anio);
           
            return lista;
        }

        #endregion
    }
}
